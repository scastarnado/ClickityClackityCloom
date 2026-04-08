namespace ClickityClacityCloom
{
    partial class FormKeybindings
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.KeybindingsListView = new System.Windows.Forms.ListView();
            this.ActionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.KeyColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChangeKeyButton = new System.Windows.Forms.Button();
            this.ResetDefaultsButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // KeybindingsListView
            // 
            this.KeybindingsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ActionColumn,
            this.KeyColumn});
            this.KeybindingsListView.FullRowSelect = true;
            this.KeybindingsListView.HideSelection = false;
            this.KeybindingsListView.Location = new System.Drawing.Point(12, 12);
            this.KeybindingsListView.MultiSelect = false;
            this.KeybindingsListView.Name = "KeybindingsListView";
            this.KeybindingsListView.Size = new System.Drawing.Size(460, 250);
            this.KeybindingsListView.TabIndex = 0;
            this.KeybindingsListView.UseCompatibleStateImageBehavior = false;
            this.KeybindingsListView.View = System.Windows.Forms.View.Details;
            // 
            // ActionColumn
            // 
            this.ActionColumn.Text = "Action";
            this.ActionColumn.Width = 300;
            // 
            // KeyColumn
            // 
            this.KeyColumn.Text = "Key";
            this.KeyColumn.Width = 150;
            // 
            // ChangeKeyButton
            // 
            this.ChangeKeyButton.Location = new System.Drawing.Point(12, 268);
            this.ChangeKeyButton.Name = "ChangeKeyButton";
            this.ChangeKeyButton.Size = new System.Drawing.Size(110, 30);
            this.ChangeKeyButton.TabIndex = 1;
            this.ChangeKeyButton.Text = "Change Key";
            this.ChangeKeyButton.UseVisualStyleBackColor = true;
            this.ChangeKeyButton.Click += new System.EventHandler(this.ChangeKeyButton_Click);
            // 
            // ResetDefaultsButton
            // 
            this.ResetDefaultsButton.Location = new System.Drawing.Point(128, 268);
            this.ResetDefaultsButton.Name = "ResetDefaultsButton";
            this.ResetDefaultsButton.Size = new System.Drawing.Size(120, 30);
            this.ResetDefaultsButton.TabIndex = 2;
            this.ResetDefaultsButton.Text = "Reset Defaults";
            this.ResetDefaultsButton.UseVisualStyleBackColor = true;
            this.ResetDefaultsButton.Click += new System.EventHandler(this.ResetDefaultsButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(316, 304);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 30);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(397, 304);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 30);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(12, 313);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(165, 13);
            this.StatusLabel.TabIndex = 5;
            this.StatusLabel.Text = "Select an action and change key.";
            // 
            // FormKeybindings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 346);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ResetDefaultsButton);
            this.Controls.Add(this.ChangeKeyButton);
            this.Controls.Add(this.KeybindingsListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormKeybindings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure Keyboard Shortcuts";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ListView KeybindingsListView;
        private System.Windows.Forms.ColumnHeader ActionColumn;
        private System.Windows.Forms.ColumnHeader KeyColumn;
        private System.Windows.Forms.Button ChangeKeyButton;
        private System.Windows.Forms.Button ResetDefaultsButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label StatusLabel;
    }
}
