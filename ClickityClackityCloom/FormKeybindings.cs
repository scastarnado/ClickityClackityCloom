using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ClickityClacityCloom
{
    public partial class FormKeybindings : Form
    {
        private KeyBindingConfiguration configuration;
        private string configFilePath;
        private bool capturingKey = false;
        private string capturingActionName = null;

        public FormKeybindings()
        {
            InitializeComponent();
            this.KeyPreview = true;
            LoadConfiguration();
            PopulateListView();
        }

        private void LoadConfiguration()
        {
            try
            {
                configFilePath = System.IO.Path.Combine(Application.StartupPath, "Config", "keybindings.json");
                configuration = KeyBindingConfiguration.Load(configFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading keybindings: {ex.Message}\n\nUsing default keybindings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                configuration = KeyBindingConfiguration.CreateDefault();
            }
        }

        private void PopulateListView()
        {
            KeybindingsListView.Items.Clear();
            
            foreach (var binding in configuration.Bindings)
            {
                var item = new ListViewItem(binding.ActionDescription);
                item.SubItems.Add(binding.KeyDisplayName);
                item.Tag = binding;
                KeybindingsListView.Items.Add(item);
            }

            if (KeybindingsListView.Items.Count > 0)
            {
                KeybindingsListView.Items[0].Selected = true;
            }
        }

        private void ChangeKeyButton_Click(object sender, EventArgs e)
        {
            if (KeybindingsListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an action to change its key.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedItem = KeybindingsListView.SelectedItems[0];
            var binding = selectedItem.Tag as KeyBinding;

            capturingKey = true;
            capturingActionName = binding.ActionName;
            StatusLabel.Text = $"Press a new key for '{binding.ActionDescription}' (ESC to cancel)...";
            StatusLabel.ForeColor = Color.Blue;
            ChangeKeyButton.Enabled = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (capturingKey)
            {
                if (keyData == Keys.Escape)
                {
                    capturingKey = false;
                    capturingActionName = null;
                    StatusLabel.Text = "Key capture cancelled.";
                    StatusLabel.ForeColor = Color.Black;
                    ChangeKeyButton.Enabled = true;
                    return true;
                }

                var binding = configuration.Bindings.FirstOrDefault(b => b.ActionName == capturingActionName);
                if (binding != null)
                {
                    if (configuration.HasConflict(capturingActionName, keyData))
                    {
                        string conflictingAction = configuration.GetConflictingAction(capturingActionName, keyData);
                        DialogResult result = MessageBox.Show(
                            $"The key '{keyData}' is already assigned to '{conflictingAction}'.\n\nDo you want to reassign it?",
                            "Key Conflict",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (result == DialogResult.No)
                        {
                            capturingKey = false;
                            capturingActionName = null;
                            StatusLabel.Text = "Key change cancelled.";
                            StatusLabel.ForeColor = Color.Black;
                            ChangeKeyButton.Enabled = true;
                            return true;
                        }
                    }

                    binding.Key = keyData;
                    PopulateListView();
                    StatusLabel.Text = $"Key for '{binding.ActionDescription}' changed to '{keyData}'.";
                    StatusLabel.ForeColor = Color.Green;
                }

                capturingKey = false;
                capturingActionName = null;
                ChangeKeyButton.Enabled = true;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ResetDefaultsButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to reset all keybindings to default values?",
                "Reset Keybindings",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                configuration = KeyBindingConfiguration.CreateDefault();
                PopulateListView();
                StatusLabel.Text = "Keybindings reset to defaults.";
                StatusLabel.ForeColor = Color.Green;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                configuration.Save(configFilePath);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving keybindings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
