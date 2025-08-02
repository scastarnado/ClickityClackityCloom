using Newtonsoft.Json;
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
            MessageBox.Show("Cargando macros desde el directorio de macros...\n" +
                Application.StartupPath);
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
                        Console.WriteLine($"Error cargando macro {file}: {ex.Message}");
                    }
                }

                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando macros: {ex.Message}");
            }
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = MacroListBox.SelectedIndex >= 0;
            LoadBTN.Enabled = hasSelection;
            EditBTN.Enabled = hasSelection;
            DeleteBTN.Enabled = hasSelection;

            if (hasSelection)
            {
                var selectedMacro = macros[MacroListBox.SelectedIndex];
                MacroDetailsLabel.Text = $"Creada: {selectedMacro.CreatedDate:dd/MM/yyyy HH:mm}\n" +
                                       $"Acciones: {selectedMacro.ActionCount}\n" +
                                       $"Archivo: {Path.GetFileName(selectedMacro.FilePath)}";
            }
            else
            {
                MacroDetailsLabel.Text = "Selecciona una macro para ver detalles";
            }
        }

        private void MacroListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void LoadBTN_Click(object sender, EventArgs e)
        {
            if (MacroListBox.SelectedIndex >= 0)
            {
                SelectedMacro = macros[MacroListBox.SelectedIndex];
                Action = "Load";
                DialogResult = DialogResult.OK;
                Close();
            }
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
                    $"¿Estás seguro de que quieres eliminar la macro '{selectedMacro.Name}'?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(selectedMacro.FilePath);
                        MessageBox.Show($"Macro '{selectedMacro.Name}' eliminada exitosamente.");
                        LoadMacrosList(); // Recargar la lista
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error eliminando macro: {ex.Message}");
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
    }

    public class MacroInfo
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ActionCount { get; set; }
    }
}