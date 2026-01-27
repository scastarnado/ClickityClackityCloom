namespace ClickityClacityCloom
{
    partial class FormSaveRecord
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MacroNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SaveRecordBTN = new System.Windows.Forms.Button();
            this.CancelRecordBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MacroNameTextBox
            // 
            this.MacroNameTextBox.Location = new System.Drawing.Point(12, 25);
            this.MacroNameTextBox.Name = "MacroNameTextBox";
            this.MacroNameTextBox.Size = new System.Drawing.Size(336, 20);
            this.MacroNameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Record Save Name";
            // 
            // SaveRecordBTN
            // 
            this.SaveRecordBTN.Location = new System.Drawing.Point(12, 111);
            this.SaveRecordBTN.Name = "SaveRecordBTN";
            this.SaveRecordBTN.Size = new System.Drawing.Size(75, 23);
            this.SaveRecordBTN.TabIndex = 2;
            this.SaveRecordBTN.Text = "SAVE";
            this.SaveRecordBTN.UseVisualStyleBackColor = true;
            this.SaveRecordBTN.Click += new System.EventHandler(this.SaveRecordBTN_Click);
            // 
            // CancelRecordBTN
            // 
            this.CancelRecordBTN.Location = new System.Drawing.Point(93, 111);
            this.CancelRecordBTN.Name = "CancelRecordBTN";
            this.CancelRecordBTN.Size = new System.Drawing.Size(75, 23);
            this.CancelRecordBTN.TabIndex = 3;
            this.CancelRecordBTN.Text = "CANCEL";
            this.CancelRecordBTN.UseVisualStyleBackColor = true;
            this.CancelRecordBTN.Click += new System.EventHandler(this.CancelRecordBTN_Click);
            // 
            // FormSaveRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 146);
            this.Controls.Add(this.CancelRecordBTN);
            this.Controls.Add(this.SaveRecordBTN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MacroNameTextBox);
            this.Name = "FormSaveRecord";
            this.Text = "FormSaveRecord";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MacroNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SaveRecordBTN;
        private System.Windows.Forms.Button CancelRecordBTN;
    }
}