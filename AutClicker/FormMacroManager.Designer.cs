namespace AutClicker
{
    partial class FormMacroManager
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
            this.MacroListBox = new System.Windows.Forms.ListBox();
            this.LoadBTN = new System.Windows.Forms.Button();
            this.EditBTN = new System.Windows.Forms.Button();
            this.DeleteBTN = new System.Windows.Forms.Button();
            this.RefreshBTN = new System.Windows.Forms.Button();
            this.CloseBTN = new System.Windows.Forms.Button();
            this.MacroDetailsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MacroListBox
            // 
            this.MacroListBox.FormattingEnabled = true;
            this.MacroListBox.Location = new System.Drawing.Point(12, 25);
            this.MacroListBox.Name = "MacroListBox";
            this.MacroListBox.Size = new System.Drawing.Size(300, 200);
            this.MacroListBox.TabIndex = 0;
            this.MacroListBox.SelectedIndexChanged += new System.EventHandler(this.MacroListBox_SelectedIndexChanged);
            // 
            // LoadBTN
            // 
            this.LoadBTN.Enabled = false;
            this.LoadBTN.Location = new System.Drawing.Point(12, 240);
            this.LoadBTN.Name = "LoadBTN";
            this.LoadBTN.Size = new System.Drawing.Size(75, 23);
            this.LoadBTN.TabIndex = 1;
            this.LoadBTN.Text = "Cargar";
            this.LoadBTN.UseVisualStyleBackColor = true;
            this.LoadBTN.Click += new System.EventHandler(this.LoadBTN_Click);
            // 
            // EditBTN
            // 
            this.EditBTN.Enabled = false;
            this.EditBTN.Location = new System.Drawing.Point(93, 240);
            this.EditBTN.Name = "EditBTN";
            this.EditBTN.Size = new System.Drawing.Size(75, 23);
            this.EditBTN.TabIndex = 2;
            this.EditBTN.Text = "Editar";
            this.EditBTN.UseVisualStyleBackColor = true;
            this.EditBTN.Click += new System.EventHandler(this.EditBTN_Click);
            // 
            // DeleteBTN
            // 
            this.DeleteBTN.Enabled = false;
            this.DeleteBTN.Location = new System.Drawing.Point(174, 240);
            this.DeleteBTN.Name = "DeleteBTN";
            this.DeleteBTN.Size = new System.Drawing.Size(75, 23);
            this.DeleteBTN.TabIndex = 3;
            this.DeleteBTN.Text = "Eliminar";
            this.DeleteBTN.UseVisualStyleBackColor = true;
            this.DeleteBTN.Click += new System.EventHandler(this.DeleteBTN_Click);
            // 
            // RefreshBTN
            // 
            this.RefreshBTN.Location = new System.Drawing.Point(255, 240);
            this.RefreshBTN.Name = "RefreshBTN";
            this.RefreshBTN.Size = new System.Drawing.Size(75, 23);
            this.RefreshBTN.TabIndex = 4;
            this.RefreshBTN.Text = "Actualizar";
            this.RefreshBTN.UseVisualStyleBackColor = true;
            this.RefreshBTN.Click += new System.EventHandler(this.RefreshBTN_Click);
            // 
            // CloseBTN
            // 
            this.CloseBTN.Location = new System.Drawing.Point(255, 290);
            this.CloseBTN.Name = "CloseBTN";
            this.CloseBTN.Size = new System.Drawing.Size(75, 23);
            this.CloseBTN.TabIndex = 5;
            this.CloseBTN.Text = "Cerrar";
            this.CloseBTN.UseVisualStyleBackColor = true;
            this.CloseBTN.Click += new System.EventHandler(this.CloseBTN_Click);
            // 
            // MacroDetailsLabel
            // 
            this.MacroDetailsLabel.BackColor = System.Drawing.SystemColors.Info;
            this.MacroDetailsLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MacroDetailsLabel.Location = new System.Drawing.Point(330, 25);
            this.MacroDetailsLabel.Name = "MacroDetailsLabel";
            this.MacroDetailsLabel.Size = new System.Drawing.Size(200, 100);
            this.MacroDetailsLabel.TabIndex = 6;
            this.MacroDetailsLabel.Text = "Selecciona una macro para ver detalles";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Macros guardadas:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(330, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Detalles:";
            // 
            // FormMacroManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 325);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MacroDetailsLabel);
            this.Controls.Add(this.CloseBTN);
            this.Controls.Add(this.RefreshBTN);
            this.Controls.Add(this.DeleteBTN);
            this.Controls.Add(this.EditBTN);
            this.Controls.Add(this.LoadBTN);
            this.Controls.Add(this.MacroListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMacroManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestor de Macros";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ListBox MacroListBox;
        private System.Windows.Forms.Button LoadBTN;
        private System.Windows.Forms.Button EditBTN;
        private System.Windows.Forms.Button DeleteBTN;
        private System.Windows.Forms.Button RefreshBTN;
        private System.Windows.Forms.Button CloseBTN;
        private System.Windows.Forms.Label MacroDetailsLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}