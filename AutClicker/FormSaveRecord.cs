using System;
using System.Windows.Forms;

namespace ClickityClacityCloom
{
    public partial class FormSaveRecord : Form
    {
        public string MacroName { get; private set; }
        public bool SaveConfirmed { get; private set; }

        public FormSaveRecord()
        {
            InitializeComponent();
            SaveConfirmed = false;

        }

        private void CancelRecordBTN_Click(object sender, System.EventArgs e)
        {
            SaveConfirmed = false;
            this.Close();
        }

        private void SaveRecordBTN_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MacroNameTextBox.Text))
            {
                MessageBox.Show("Please, set a name to the macro.", "Name required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MacroName = MacroNameTextBox.Text.Trim();
            SaveConfirmed = true;
            this.Close();
        }

        // Método para precargar un nombre (útil para edición)
        public void SetMacroName(string name)
        {
            MacroNameTextBox.Text = name;
        }
    }
}
