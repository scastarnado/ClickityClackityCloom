using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickityClacityCloom;

namespace AutClicker
{
    public partial class AppWindow : Form
    {
        #region DLL imports

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        #endregion

        #region Constants

        // These are the codes for each Mouse Button Action
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x05;
        private const int MOUSEEVENTF_MIDDLEUP = 0x07;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;

        #endregion

        #region Fields

        static public bool hasToStop = false;
        Random rand = new Random();
        private bool isPlaying;
        private bool isListening = false;
        private IntPtr hookID = IntPtr.Zero;
        private List<ClickAction> recordedActions = new List<ClickAction>();
        private bool isRecording = false;
        private bool isMacroPlaying = false;
        private CancellationTokenSource macroPlaybackCancellation;
        private KeyBindingConfiguration keyBindings;
        private string keyBindingsFilePath;

        #endregion

        #region Properties

        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                if (isPlaying != value)
                {
                    isPlaying = value;
                    OnPropertyChanged("IsPlaying");
                }
            }
        }

        #endregion

        #region Events and Delegates

        public static event PropertyChangedEventHandler PropertyChanged;

        private delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        private HookProc mouseHookProcedure;
        private IntPtr mouseHookID = IntPtr.Zero;

        #endregion

        #region Constructor

        public AppWindow()
        {
            InitializeComponent();
            IsPlaying = false;

            // Load keybindings
            keyBindingsFilePath = Path.Combine(Application.StartupPath, "Config", "keybindings.json");
            try
            {
                keyBindings = KeyBindingConfiguration.Load(keyBindingsFilePath);
                UpdateKeybindingLabels();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading keybindings: {ex.Message}\n\nUsing default keybindings.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                keyBindings = KeyBindingConfiguration.CreateDefault();
            }

            StartStopWorker.RunWorkerAsync();

            // Set the combobox default text
            MouseButtonDropBox.Text = "Left";
            MouseButtonDropBox.SelectedText = "Left";

            ClickTypeDropBox.Text = "Single Click";
            ClickTypeDropBox.SelectedText = "Single Click";

            PopulateMacrosListSelector();
        }

        private void PopulateMacrosListSelector()
        {
            MacrosListSelector.Items.Clear();
            string macrosDirectory = Path.Combine(Application.StartupPath, "Macros");
            if (Directory.Exists(macrosDirectory))
            {
                string[] macroFiles = Directory.GetFiles(macrosDirectory, "*.json");
                foreach (string file in macroFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    MacrosListSelector.Items.Add(fileName);
                }
            }
        }

        // Set up the mouse hook when the form loads
        private void MainForm_Load(object sender, EventArgs e)
        {
            mouseHookID = SetHook(mouseHookProcedure);
        }

        // Unhook the mouse hook when the form closes
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnhookWindowsHookEx(mouseHookID);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == "IsPlaying")
            {
                IsPlayingChanged(); // Call IsPlayingChanged when IsPlaying changes
            }
        }

        #endregion

        void IsPlayingChanged()
        {
            ChangeTextStartStop(IsPlaying);
            if (IsPlaying) DoClicks();
            else return;
        }

        private async void StartStopWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    if (keyBindings != null)
                    {
                        // Emergency stop
                        var emergencyKey = keyBindings.GetKeyForAction("EmergencyStop");
                        if (emergencyKey != Keys.None && GetAsyncKeyState(emergencyKey) < 0)
                        {
                            await Task.Delay(200); // Debounce
                            IsPlaying = false;
                            if (isMacroPlaying)
                            {
                                StopMacroPlayback();
                            }
                            await Task.Delay(200);
                        }

                        // Start/Stop clicking
                        var startKey = keyBindings.GetKeyForAction("StartClicking");
                        var stopKey = keyBindings.GetKeyForAction("StopClicking");

                        if (startKey != Keys.None && GetAsyncKeyState(startKey) < 0 && !isPlaying)
                        {
                            await Task.Delay(200);
                            IsPlaying = true;
                            await Task.Delay(200);
                        }
                        else if (stopKey != Keys.None && GetAsyncKeyState(stopKey) < 0 && isPlaying)
                        {
                            await Task.Delay(200);
                            IsPlaying = false;
                            await Task.Delay(200);
                        }

                        // Start macro playback
                        var startMacroKey = keyBindings.GetKeyForAction("StartMacro");
                        if (startMacroKey != Keys.None && GetAsyncKeyState(startMacroKey) < 0 && !isMacroPlaying)
                        {
                            await Task.Delay(200);
                            if (InvokeRequired)
                            {
                                Invoke(new Action(async () => await StartMacroPlayback()));
                            }
                            else
                            {
                                await StartMacroPlayback();
                            }
                            await Task.Delay(200);
                        }

                        // Stop macro playback
                        var stopMacroKey = keyBindings.GetKeyForAction("StopMacro");
                        if (stopMacroKey != Keys.None && GetAsyncKeyState(stopMacroKey) < 0 && isMacroPlaying)
                        {
                            await Task.Delay(200);
                            StopMacroPlayback();
                            await Task.Delay(200);
                        }
                    }

                    await Task.Delay(5);
                }
                catch (Exception ex)
                {
                    LogError("Error in StartStopWorker", ex);
                    await Task.Delay(100);
                }
            }
        }

        public void ChangeTextStartStop(bool isPlaying)
        {
            // Use Invoke to update the UI on the main thread
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdateKeybindingLabels();
                }));
            }
            else
            {
                UpdateKeybindingLabels();
            }
        }

        public async Task DoClicks()
        {
            int TimeIntervalMilliseconds = 1;
            int iterations = 0;

            // Get the interval milliseconds based on the selected radio button
            if (RepeatTilStoppedRadioBTN.Checked)
            {
                TimeIntervalMilliseconds = (int)(MilisecondsValue.Value + (SecondsValue.Value * 1000) + (MinutesValue.Value * 60000) + (HoursValue.Value * 3600000));
            }
            else if (RepeatTimesRadioBTN.Checked)
            {
                iterations = (int)RepeatTimesValue.Value;
                TimeIntervalMilliseconds = (int)((MilisecondsValue.Value + (SecondsValue.Value * 1000) + (MinutesValue.Value * 60000) + (HoursValue.Value * 3600000)) / iterations);
            }

            // Handling 0 ms interval
            if (TimeIntervalMilliseconds == 0) TimeIntervalMilliseconds = 1;

            // Loop while the button is pressed
            while (IsPlaying)
            {
                // Get the mouse button click values
                uint[] MouseButtonToClickValue = MouseButtonToClick();

                // Get the current mouse position
                int[] MousePosition = GetMousePosition();

                if (FixedLocationRadioBTN.Checked)
                {
                    // Set the cursor position and clear any cursor clipping
                    Cursor.Position = new Point(MousePosition[0], MousePosition[1]);
                    Cursor.Clip = new Rectangle();
                }

                // Simulate mouse click
                mouse_event(dwFlags: MouseButtonToClickValue[0], dx: (uint)MousePosition[0], dy: (uint)MousePosition[1], cButtons: 0, dwExtraInfo: 0);

                // Display the clicked position

                // Use Invoke to call ChangeTextStartStop on the main thread
                if (InvokeRequired)
                {
                    Invoke(new Action(() => ChangeTextStartStop(IsPlaying)));
                }
                else
                {
                    ChangeTextStartStop(IsPlaying);
                }

                // Delay for a random time (up to 2 milliseconds)
                await Task.Delay(rand.Next(2));

                // Simulate releasing the mouse button
                mouse_event(dwFlags: MouseButtonToClickValue[1], dx: (uint)MousePosition[0], dy: (uint)MousePosition[1], cButtons: 0, dwExtraInfo: 0);

                // Check if a specific number of iterations are requested
                if (iterations > 0)
                {
                    iterations--;
                    if (iterations == 0)
                        IsPlaying = false;
                }

                // Store the base interval for later use
                int baseInterval = TimeIntervalMilliseconds;

                // Handle 'Variable Click Speed' Checkbox
                if (VariableClickSpeedCheck.Checked)
                {
                    // Get the variable milliseconds to add to the click interval
                    int msVariable = (int)VariableClickSpeedValue.Value;

                    // Convert the value if the unit is other than milliseconds
                    switch (VariableClickSpeedUnit.Text)
                    {
                        case "Seconds":
                            msVariable *= 1000;
                            break;
                        case "Minutes":
                            msVariable *= 60000;
                            break;
                        case "Hours":
                            msVariable *= 3600000;
                            break;
                    }

                    // Generate a random value between 0 and msVariable
                    int RandomValueToAdd = rand.Next(msVariable);
                    // Update the time interval with the base interval plus the random value
                    TimeIntervalMilliseconds = (baseInterval + RandomValueToAdd);
                }

                // Delay for the calculated time interval
                await Task.Delay(TimeIntervalMilliseconds);

                // Restore the base interval
                TimeIntervalMilliseconds = baseInterval;

                if (hasToStop) IsPlaying = false;
            }
        }

        uint[] MouseButtonToClick()
        {
            // I created an array of 2 values so I can store both actions that will be played
            uint[] value = new uint[2];
            switch (MouseButtonDropBox.Text)
            {
                case "Left":
                    value[0] = MOUSEEVENTF_LEFTDOWN;
                    value[1] = MOUSEEVENTF_LEFTUP;
                    break;
                case "Middle":
                    value[0] = MOUSEEVENTF_MIDDLEDOWN;
                    value[1] = MOUSEEVENTF_MIDDLEUP;
                    break;
                case "Right":
                    value[0] = MOUSEEVENTF_RIGHTDOWN;
                    value[1] = MOUSEEVENTF_RIGHTUP;
                    break;
                default:
                    value[0] = MOUSEEVENTF_LEFTDOWN;
                    value[1] = MOUSEEVENTF_LEFTUP;
                    break;
            }

            return value;
        }

        int[] GetMousePosition()
        {
            int[] MousePosition = new int[2];
            if (CurrentLocationRadioBTN.Checked)
            {
                MousePosition[0] = Control.MousePosition.X;
                MousePosition[1] = Control.MousePosition.Y;
            }
            else if (FixedLocationRadioBTN.Checked)
            {
                MousePosition[0] = (int)XMouseLocation.Value;
                MousePosition[1] = (int)YMouseLocation.Value;
            }

            // Handle Variable Click Position Checkbox
            if (VariableClickPositionCheck.Checked)
            {
                // MessageBox.Show("Do this work?"); // it does
                int VariablePixels = (int)VariableClickPositionValue.Value;
                int randomX = rand.Next(-VariablePixels, VariablePixels);
                int randomY = rand.Next(-VariablePixels, VariablePixels);

                // Add the random values to the MousePosition Array
                MousePosition[0] += randomX;
                MousePosition[1] += randomY;
            }

            return MousePosition;
        }

        private void PickLocationBTN_Click(object sender, EventArgs e)
        {

            if (!isListening)
            {
                isListening = true;
                PickLocationBTN.Text = "Listening...";
                hookID = SetHook(MouseHookCallback);
            }
            else
            {
                isListening = false;
                PickLocationBTN.Text = "Pick Location";
                UnhookWindowsHookEx(hookID);
            }

        }

        private IntPtr SetHook(HookProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private int MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_LBUTTONDOWN)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                int x = hookStruct.pt.x;
                int y = hookStruct.pt.y;

                XMouseLocation.Value = x;
                YMouseLocation.Value = y;

                // Unhook the mouse hook after one click
                UnhookWindowsHookEx(hookID);
                isListening = false;
                // Optionally, you can change the button text here
                PickLocationBTN.Invoke((MethodInvoker)(() => PickLocationBTN.Text = "Pick Location"));
            }
            return (int)CallNextHookEx(hookID, nCode, wParam, lParam);
        }


        #region P/Invoke Declarations for mouse hook

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        #endregion

        #region Recording macros

        private DateTime lastActionTime;
        private IntPtr recordingHookID = IntPtr.Zero;

        private void RecordBTN_Click(object sender, EventArgs e)
        {
            if (!isRecording)
            {
                // Iniciar grabación
                recordedActions.Clear();
                isRecording = true;
                lastActionTime = DateTime.Now;
                RecordBTN.Text = "Stop Recording";

                // Instalar hook para capturar clics
                recordingHookID = SetHook(RecordingMouseHookCallback);
            }
            else
            {
                // Detener grabación
                isRecording = false;
                RecordBTN.Text = "Record Macro";
                RecordBTN.BackColor = SystemColors.Control;

                // Remover hook
                if (recordingHookID != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(recordingHookID);
                    recordingHookID = IntPtr.Zero;
                }

                if (recordedActions.Count > 0)
                {
                    // Mostrar formulario para guardar
                    using (var saveForm = new ClickityClacityCloom.FormSaveRecord())
                    {
                        // Open the dialog itself
                        saveForm.ShowDialog();

                        // If the variable SaveConfirmed is true
                        if (saveForm.SaveConfirmed)
                        {
                            SaveMacro(saveForm.MacroName);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No actions were saved.");
                }
            }
        }

        private int RecordingMouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && isRecording)
            {
                if (wParam == (IntPtr)WM_LBUTTONDOWN)
                {
                    MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                    DateTime currentTime = DateTime.Now;
                    long interval = (long)(currentTime - lastActionTime).TotalMilliseconds;

                    var action = new ClickAction
                    {
                        X = hookStruct.pt.x,
                        Y = hookStruct.pt.y,
                        Interval = recordedActions.Count == 0 ? 0 : interval, // Primera acción sin delay
                        MouseButton = "Left",
                        ClickType = "Single Click",
                        Timestamp = currentTime
                    };

                    recordedActions.Add(action);
                    lastActionTime = currentTime;
                }
            }

            return (int)CallNextHookEx(recordingHookID, nCode, wParam, lParam);
        }

        private void SaveMacro(string macroName)
        {
            try
            {
                // Crear directorio de macros si no existe
                string macrosDirectory = Path.Combine(Application.StartupPath, "Macros");
                if (!Directory.Exists(macrosDirectory))
                {
                    Directory.CreateDirectory(macrosDirectory);
                }

                string fileName = Path.Combine(macrosDirectory, $"{macroName}.json");

                MessageBox.Show("It reaches with filename: " + fileName);
                // Verificar si ya existe
                if (File.Exists(fileName))
                {
                    DialogResult result = MessageBox.Show(
                        $"Already exists a macro with the name '{macroName}'. Do you want to overwrite it?",
                        "Existing macro",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.No) return;
                }

                // This method record the last click, when the user wants to stop recording
                // make sure the last action is deleted
                recordedActions.RemoveAt(recordedActions.Count - 1);

                var macroData = new
                {
                    Name = macroName,
                    CreatedDate = DateTime.Now,
                    Actions = recordedActions
                };


                string json = JsonConvert.SerializeObject(macroData, Formatting.Indented);
                File.WriteAllText(fileName, json);

                MessageBox.Show($"Macro '{macroName}' saved successfully {recordedActions.Count} clicks.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving macro: " + ex.Message);
            }
        }

        #endregion

        #region Load and Play Macros

        private void LoadMacro(string fileName)
        {
            try
            {
                string pathToFile = Path.Combine(Application.StartupPath, "Macros", fileName);
                string json = File.ReadAllText(pathToFile);
                dynamic macroData = JsonConvert.DeserializeObject(json);
                recordedActions.Clear();
                foreach (var action in macroData.Actions)
                {
                    recordedActions.Add(new ClickAction
                    {
                        X = action.X,
                        Y = action.Y,
                        Interval = action.Interval,
                        MouseButton = action.MouseButton ?? "Left",
                        ClickType = action.ClickType ?? "Single Click",
                        Timestamp = action.Timestamp
                    });
                }

                MessageBox.Show($"Macro '{macroData.Name}' imported");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading macro: " + ex.Message);
            }
        }

        private async void PlayMacroBTN_Click(object sender, EventArgs e)
        {
            if (isMacroPlaying)
            {
                StopMacroPlayback();
            }
            else
            {
                await StartMacroPlayback();
            }
        }

        private async Task PlayRecordedMacro()
        {
            foreach (var action in recordedActions)
            {
                // Esperar el intervalo antes de la acción
                if (action.Interval > 0)
                {
                    await Task.Delay((int)action.Interval);
                }

                // Establecer posición del cursor
                Cursor.Position = new Point(action.X, action.Y);

                // Obtener códigos del botón del mouse
                uint[] mouseButtonCodes = GetMouseButtonCodes(action.MouseButton);

                // Simular clic
                mouse_event(mouseButtonCodes[0], (uint)action.X, (uint)action.Y, 0, 0);
                await Task.Delay(50); // Pequeño delay entre down/up
                mouse_event(mouseButtonCodes[1], (uint)action.X, (uint)action.Y, 0, 0);

                // Si hay que parar, salir del bucle
                if (hasToStop)
                {
                    break;
                }
            }
        }

        private async Task StartMacroPlayback()
        {
            if (MacrosListSelector.SelectedItem == null)
            {
                MessageBox.Show("Please select a macro to play.");
                return;
            }

            LoadMacro(MacrosListSelector.SelectedItem.ToString() + ".json");

            if (recordedActions.Count == 0)
            {
                MessageBox.Show("No macro to play");
                return;
            }

            isMacroPlaying = true;
            macroPlaybackCancellation = new CancellationTokenSource();
            UpdateMacroPlaybackStatus(true);

            try
            {
                await PlayRecordedMacroInfinite(macroPlaybackCancellation.Token);
            }
            catch (OperationCanceledException)
            {
                // Normal cancellation
            }
            finally
            {
                isMacroPlaying = false;
                UpdateMacroPlaybackStatus(false);
            }
        }

        private void StopMacroPlayback()
        {
            macroPlaybackCancellation?.Cancel();
            isMacroPlaying = false;
            UpdateMacroPlaybackStatus(false);
        }

        private void UpdateMacroPlaybackStatus(bool isPlaying)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdateKeybindingLabels();
                    MacroStatusLabel.ForeColor = isPlaying ? Color.Green : Color.Red;
                }));
            }
            else
            {
                UpdateKeybindingLabels();
                MacroStatusLabel.ForeColor = isPlaying ? Color.Green : Color.Red;
            }
        }

        private async Task PlayRecordedMacroInfinite(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var action in recordedActions)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    // Wait the interval before the action
                    if (action.Interval > 0)
                    {
                        await Task.Delay((int)action.Interval, cancellationToken);
                    }

                    // Set cursor position
                    Cursor.Position = new Point(action.X, action.Y);

                    // Get mouse button codes
                    uint[] mouseButtonCodes = GetMouseButtonCodes(action.MouseButton);

                    // Simulate click
                    mouse_event(mouseButtonCodes[0], (uint)action.X, (uint)action.Y, 0, 0);
                    await Task.Delay(50, cancellationToken);
                    mouse_event(mouseButtonCodes[1], (uint)action.X, (uint)action.Y, 0, 0);
                }

                // Small delay between loops
                await Task.Delay(100, cancellationToken);
            }
        }

        private uint[] GetMouseButtonCodes(string mouseButton)
        {
            uint[] codes = new uint[2];

            switch (mouseButton)
            {
                case "Left":
                    codes[0] = MOUSEEVENTF_LEFTDOWN;
                    codes[1] = MOUSEEVENTF_LEFTUP;
                    break;
                case "Right":
                    codes[0] = MOUSEEVENTF_RIGHTDOWN;
                    codes[1] = MOUSEEVENTF_RIGHTUP;
                    break;
                case "Middle":
                    codes[0] = MOUSEEVENTF_MIDDLEDOWN;
                    codes[1] = MOUSEEVENTF_MIDDLEUP;
                    break;
                default:
                    codes[0] = MOUSEEVENTF_LEFTDOWN;
                    codes[1] = MOUSEEVENTF_LEFTUP;
                    break;
            }

            return codes;
        }

        #endregion

        #region Macro Management

        private void ManageMacrosBTN_Click(object sender, EventArgs e)
        {
            using (var macroManager = new FormMacroManager())
            {
                if (macroManager.ShowDialog() == DialogResult.OK)
                {
                    switch (macroManager.Action)
                    {
                        case "Load":
                            LoadMacro(macroManager.SelectedMacro.FilePath);
                            break;
                        case "Edit":
                            EditMacro(macroManager.SelectedMacro);
                            break;
                    }
                }
            }
        }

        private void EditMacro(MacroInfo macroInfo)
        {
            try
            {
                // Cargar la macro actual
                string json = File.ReadAllText(macroInfo.FilePath);
                dynamic macroData = JsonConvert.DeserializeObject(json);

                // Mostrar formulario para editar el nombre
                using (var editForm = new ClickityClacityCloom.FormSaveRecord())
                {
                    editForm.Text = "Edit Macro";
                    editForm.SetMacroName(macroInfo.Name);

                    if (editForm.ShowDialog() == DialogResult.OK && editForm.SaveConfirmed)
                    {
                        // Actualizar el nombre en los datos
                        var updatedMacroData = new
                        {
                            Name = editForm.MacroName,
                            CreatedDate = macroData.CreatedDate,
                            ModifiedDate = DateTime.Now,
                            Actions = macroData.Actions
                        };

                        // Guardar los cambios
                        string updatedJson = JsonConvert.SerializeObject(updatedMacroData, Formatting.Indented);
                        File.WriteAllText(macroInfo.FilePath, updatedJson);

                        MessageBox.Show($"Macro updated successfully: '{editForm.MacroName}'");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing macro: {ex.Message}");
            }
        }

        #endregion

        private void UpdateKeybindingLabels()
        {
            if (keyBindings == null) return;

            try
            {
                var emergencyKey = keyBindings.GetKeyForAction("EmergencyStop");
                var startKey = keyBindings.GetKeyForAction("StartClicking");
                var stopKey = keyBindings.GetKeyForAction("StopClicking");
                var startMacroKey = keyBindings.GetKeyForAction("StartMacro");
                var stopMacroKey = keyBindings.GetKeyForAction("StopMacro");

                PlayStopLabel.Text = isPlaying 
                    ? $"Playing ({stopKey} to stop)" 
                    : $"Stopped ({startKey} to start)";

                MacroStatusLabel.Text = isMacroPlaying
                    ? $"Macro: Playing ({stopMacroKey} to stop)"
                    : $"Macro: Stopped ({startMacroKey} to play)";

                MacroKeybindingsLabel.Text = $"Keybindings: {startMacroKey} = Play Macro | {stopMacroKey} = Stop Macro | " +
                                            $"{startKey} = Start Clicks | {stopKey} = Stop Clicks | {emergencyKey} = Emergency Stop";
            }
            catch (Exception ex)
            {
                LogError("Error updating keybinding labels", ex);
            }
        }

        private void LogError(string message, Exception ex = null)
        {
            try
            {
                string logDirectory = Path.Combine(Application.StartupPath, "Logs");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                string logFile = Path.Combine(logDirectory, $"error_log_{DateTime.Now:yyyyMMdd}.txt");
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";

                if (ex != null)
                {
                    logEntry += $"\nException: {ex.GetType().Name}\nMessage: {ex.Message}\nStackTrace: {ex.StackTrace}";
                }

                logEntry += "\n" + new string('-', 80) + "\n";

                File.AppendAllText(logFile, logEntry);
            }
            catch
            {
                // Logging failed, nothing we can do
            }
        }

        private void ConfigureKeybindingsBTN_Click(object sender, EventArgs e)
        {
            try
            {
                using (var keybindingsForm = new FormKeybindings())
                {
                    if (keybindingsForm.ShowDialog() == DialogResult.OK)
                    {
                        // Reload keybindings configuration
                        keyBindings = KeyBindingConfiguration.Load(keyBindingsFilePath);
                        UpdateKeybindingLabels();
                        MessageBox.Show("Keybindings updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("Error opening keybindings configuration", ex);
                MessageBox.Show($"Error opening keybindings configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReloadMacroSelectorBTN_Click(object sender, EventArgs e)
        {
            PopulateMacrosListSelector();
        }
    }

    [Serializable]
    public class ClickAction
    {
        public int X { get; set; }
        public int Y { get; set; }
        public long Interval { get; set; } // Intervalo desde la acción anterior en milisegundos
        public string MouseButton { get; set; } // "Left", "Right", "Middle"
        public string ClickType { get; set; } // "Single Click", "Double Click"
        public DateTime Timestamp { get; set; } // Para calcular intervalos
    }
}
