using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AutClicker
{
    public partial class FormMacroManager : Form
    {
        private List<MacroInfo> macros = new List<MacroInfo>();
        private string macrosDirectory;

        public MacroInfo SelectedMacro { get; private set; }
        public string Action { get; private set; } // "Load", "Edit", "Delete"

        public FormMacroManager()
        {
            InitializeComponent();

            // Crear directorio de macros si no existe
            macrosDirectory = Path.Combine(Application.StartupPath, "Macros");
            if (!Directory.Exists(macrosDirectory))
            {
                Directory.CreateDirectory(macrosDirectory);
            }

            LoadMacrosList();
        }

        private void LoadMacrosList()
        {
            macros.Clear();
            MacroListBox.Items.Clear();

            try
            {
                string[] macroFiles = Directory.GetFiles(macrosDirectory, "*.json");

                foreach (string file in macroFiles)
                {
                    try
                    {
                        string json = File.ReadAllText(file);
                        dynamic macroData = JsonConvert.DeserializeObject(json);

                        var macroInfo = new MacroInfo
                        {
                            Name = macroData.Name ?? Path.GetFileNameWithoutExtension(file),
                            FilePath = file,
                            CreatedDate = macroData.CreatedDate ?? DateTime.MinValue,
                            ActionCount = macroData.Actions?.Count ?? 0
                        };

                        macros.Add(macroInfo);
                        MacroListBox.Items.Add($"{macroInfo.Name} ({macroInfo.ActionCount} acciones)");
                    }
                    catch (Exception ex)
                    {
                        // Archivo corrupto, ignorar
                        Console.WriteLine($"Error loading macro {file}: {ex.Message}");
                    }
                }

                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading macros: {ex.Message}");
            }
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = MacroListBox.SelectedIndex >= 0;
            EditBTN.Enabled = hasSelection;
            DeleteBTN.Enabled = hasSelection;

            if (hasSelection)
            {
                var selectedMacro = macros[MacroListBox.SelectedIndex];
                MacroDetailsLabel.Text = $"Created: {selectedMacro.CreatedDate:dd/MM/yyyy HH:mm}\n" +
                                       $"Actions: {selectedMacro.ActionCount}\n" +
                                       $"File: {Path.GetFileName(selectedMacro.FilePath)}";
            }
            else
            {
                MacroDetailsLabel.Text = "Select a macro to see details";
            }
        }

        private void MacroListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void EditBTN_Click(object sender, EventArgs e)
        {
            if (MacroListBox.SelectedIndex >= 0)
            {
                SelectedMacro = macros[MacroListBox.SelectedIndex];
                Action = "Edit";
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void DeleteBTN_Click(object sender, EventArgs e)
        {
            if (MacroListBox.SelectedIndex >= 0)
            {
                var selectedMacro = macros[MacroListBox.SelectedIndex];

                DialogResult result = MessageBox.Show(
                    $"Are you sure want to delete '{selectedMacro.Name}'?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(selectedMacro.FilePath);
                        MessageBox.Show($"Macro '{selectedMacro.Name}' deleted successfully.");
                        LoadMacrosList(); // Recargar la lista
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting macro: {ex.Message}");
                    }
                }
            }
        }

        private void RefreshBTN_Click(object sender, EventArgs e)
        {
            LoadMacrosList();
        }

        private void CloseBTN_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ImportBTN_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\";
            ofd.DefaultExt = "json";
            ofd.Filter = "JSON files (*.json)|*.json";
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Multiselect = true;

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            if (ofd.FileNames.Length > 25)
            {
                MessageBox.Show("You can only select up to 25 files at once.", "Too many files", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> invalidFiles = new List<string>();
            List<string> importedFiles = new List<string>();

            foreach (var file in ofd.FileNames)
            {
                try
                {
                    string fileContent = File.ReadAllText(file);
                    if (string.IsNullOrWhiteSpace(fileContent))
                    {
                        invalidFiles.Add($"{Path.GetFileName(file)} - Empty file");
                        continue;
                    }

                    JObject obj = JObject.Parse(fileContent);

                    // Validate required structure
                    if (!ValidateMacroStructure(obj))
                    {
                        invalidFiles.Add($"{Path.GetFileName(file)} - Invalid structure");
                        continue;
                    }

                    // Generate unique filename in macros directory
                    string originalName = obj["Name"]?.ToString() ?? Path.GetFileNameWithoutExtension(file);
                    string targetFileName = GetUniqueFileName(originalName);
                    string targetPath = Path.Combine(macrosDirectory, targetFileName);

                    // Copy file to macros directory
                    File.WriteAllText(targetPath, fileContent);
                    importedFiles.Add(originalName);
                }
                catch (Exception ex)
                {
                    invalidFiles.Add($"{Path.GetFileName(file)} - {ex.Message}");
                }
            }

            // Show import results
            string message = $"Import completed:\n";
            message += $"Successfully imported: {importedFiles.Count} files\n";
            if (invalidFiles.Count > 0)
            {
                message += $"Failed to import: {invalidFiles.Count} files\n\nInvalid files:\n";
                message += string.Join("\n", invalidFiles.Take(10));
                if (invalidFiles.Count > 10)
                    message += $"\n... and {invalidFiles.Count - 10} more";
            }

            MessageBox.Show(message, "Import Results", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (importedFiles.Count > 0)
                LoadMacrosList();
        }

        private bool ValidateMacroStructure(JObject obj)
        {
            try
            {
                // Check required properties
                if (obj["Name"] == null || obj["CreatedDate"] == null || obj["Actions"] == null)
                    return false;

                JArray actions = obj["Actions"] as JArray;
                if (actions == null)
                    return false;

                // Validate each action has required properties
                foreach (JObject action in actions)
                {
                    if (action["X"] == null || action["Y"] == null || action["Interval"] == null ||
                        action["MouseButton"] == null || action["ClickType"] == null || action["TimeStamp"] == null)
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private string GetUniqueFileName(string baseName)
        {
            string fileName = $"{baseName}.json";
            string fullPath = Path.Combine(macrosDirectory, fileName);
            
            int counter = 1;
            while (File.Exists(fullPath))
            {
                fileName = $"{baseName}_{counter}.json";
                fullPath = Path.Combine(macrosDirectory, fileName);
                counter++;
            }
            
            return fileName;
        }
    }

    public class MacroInfo
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ActionCount { get; set; }
    }
}