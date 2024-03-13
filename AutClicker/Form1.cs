using Clickity_Clackity_Chucklefinger_3000;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO
// - Repeat X minutes -> ME DA MUCHA PEREZA ESTA COSA
// - Double click and Left Click
// - New Ideas??
// - Key bindings
// - Grabación de clicks / macros -> NO TENGO NI IDEA DE COMO EMPEZAR LMAO

namespace AutClicker
{
    public partial class AppWindow : Form
    {
        #region P/Invoke Declarations

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

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion

        #region Constants

        // These are the codes for each Mouse Button Action
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x05;
        private const int MOUSEEVENTF_MIDDLEUP = 0x07;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        #endregion

        #region Fields

        static public bool hasToStop = false;
        Random rand = new Random();
        private bool isPlaying;
        private bool isListening = false;
        private IntPtr hookID = IntPtr.Zero;

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

        #endregion

        #region Constructor

        public AppWindow()
        {
            InitializeComponent();
            IsPlaying = false;
            StartStopWorker.RunWorkerAsync();

            // Set the combobox default text
            MouseButtonDropBox.Text = "Left";
            MouseButtonDropBox.SelectedText = "Left";

            ClickTypeDropBox.Text = "Single Click";
            ClickTypeDropBox.SelectedText = "Single Click";
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
                if (GetAsyncKeyState(Keys.F6) < 0)
                {
                    IsPlaying = true;
                }

                else if (GetAsyncKeyState(Keys.F7) < 0)
                {
                    IsPlaying = false;
                }
                await Task.Delay(5); // Esperar un corto período antes de verificar nuevamente
            }
        }

        public void ChangeTextStartStop(bool isPlaying)
        {

            // Use Invoke to update the UI on the main thread
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    if (isPlaying) PlayStopLabel.Text = "IS PLAYING";
                    else PlayStopLabel.Text = "IS STOPPED";
                }));
            }
            else
            {
                if (isPlaying) PlayStopLabel.Text = "IS PLAYING";
                else PlayStopLabel.Text = "IS STOPPED";
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
                TimeIntervalMilliseconds = (int)(MilisecondsValue.Value + (SecondsValue.Value * 1000) + (MinutesValue.Value * 60000) + (HoursValue.Value * 3600000) / iterations);
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
        public static void ThreadTilMinutes(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                Thread.Sleep(60000);
            }
            hasToStop = true;
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
                int VariablePixels = (int)VariableClickPositionValue.Value;
                int randomX = rand.Next(-VariablePixels, VariablePixels);
                int randomY = rand.Next(-VariablePixels, VariablePixels);

                // Add the random values to the MousePosition Array
                MousePosition[0] += randomX;
                MousePosition[1] += randomY;
            }

            return MousePosition;
        }

        private async void PlayStopLabel_TextChanged(object sender, EventArgs e)
        {
            if (IsPlaying)
            {
                await DoClicks(); // Ejecutar DoClicks() en un subproceso separado
            }
        }

        private void PickLocationBTN_Click(object sender, EventArgs e)
        {
            if (!isListening)
            {
                isListening = true;
                PickLocationBTN.Text = "Listening...";

                hookID = SetHook(MouseHookCallback);
            }
        }

        private IntPtr SetHook(HookProc proc)
        {
            using (ProcessModule currentModule = Process.GetCurrentProcess().MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                    GetModuleHandle(currentModule.ModuleName), 0);
            }
        }

        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_LBUTTONDOWN)
            {
                MSLLHOOKSTRUCT hookStruct = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
                int x = hookStruct.pt.x;
                int y = hookStruct.pt.y;


                // Set the values on the corresponding fields
                XMouseLocation.Value = x; YMouseLocation.Value = y;

                isListening = false;
                PickLocationBTN.Text = "Start Listening";

                UnhookWindowsHookEx(hookID); // Unhook the mouse hook
            }
            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }

        #region P/Invoke Declarations for mouse hook

        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;

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

        // Define a class to store click information
        private class ClickInfo
        {
            public int X { get; set; } // This is used to know where to click
            public int Y { get; set; } // This is used to know where to click
            public long Interval { get; set; } // This is used to know when to click
        }

        private class RecordingInfo
        {
            public string RecordName { get; set; }
            public ClickInfo ClickInfo { get; set; }
        }

        private List<ClickInfo> clickInfoList = new List<ClickInfo>();
        private Stopwatch stopwatch = new Stopwatch();

        private void RecordBTN_Click(object sender, EventArgs e)
        {
            Console.WriteLine("RECORD");
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
                RecordBTN.Text = "Record";
                Console.WriteLine("Saving?");
                // Save the recorded info
                SaveClickInfo();

                // Unhook the mouse hook when recording stops
                UnhookWindowsHookEx(hookID);
            }
            else
            {
                clickInfoList.Clear();
                stopwatch.Reset();
                stopwatch.Start();
                RecordBTN.Text = "Stop Recording";
                Console.WriteLine("Started to record");
                // Set up the mouse hook when recording starts
                hookID = SetHook(RecordMouseHookCallback);
            }
        }

        private void SaveClickInfo()
        {
            // Prompt user for a name
            Console.Write("Enter a name for the record: ");
            FormSaveRecord formSaveRecord = new FormSaveRecord();
            formSaveRecord.ShowDialog();

            // Serialize the list of ClickInfo to JSON
            string json = JsonConvert.SerializeObject(clickInfoList, Newtonsoft.Json.Formatting.Indented);

            // Save the JSON to a file
            File.WriteAllText("C:\\Users\\Sergi Castarnado\\Desktop\\Records.json", json);
        }

        private IntPtr RecordMouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            const int WM_LBUTTONDOWN = 0x0201;
            const int WM_RBUTTONDOWN = 0x0204;

            if (nCode >= 0 && (wParam.ToInt32() == WM_LBUTTONDOWN || wParam.ToInt32() == WM_RBUTTONDOWN))
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                // Calculate the interval since the last click
                long interval = stopwatch.ElapsedMilliseconds;

                // Create a new ClickInfo object
                ClickInfo clickInfo = new ClickInfo
                {
                    X = hookStruct.pt.x,
                    Y = hookStruct.pt.y,
                    Interval = interval
                };

                // Add the ClickInfo to the list
                clickInfoList.Add(clickInfo);

                // Restart the stopwatch for the next click
                stopwatch.Restart();
            }

            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }




        #endregion
    }
}