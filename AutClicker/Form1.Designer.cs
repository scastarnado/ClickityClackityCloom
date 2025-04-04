namespace AutClicker
{
    partial class AppWindow
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppWindow));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.MilisecondsValue = new System.Windows.Forms.NumericUpDown();
            this.SecondsValue = new System.Windows.Forms.NumericUpDown();
            this.MinutesValue = new System.Windows.Forms.NumericUpDown();
            this.HoursValue = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ClickTypeDropBox = new System.Windows.Forms.ComboBox();
            this.MouseButtonDropBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.RepeatTilStoppedRadioBTN = new System.Windows.Forms.RadioButton();
            this.RepeatTimesRadioBTN = new System.Windows.Forms.RadioButton();
            this.RepeatTimesValue = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.VariableClickSpeedUnit = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.VariableClickSpeedValue = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.VariableClickPositionValue = new System.Windows.Forms.NumericUpDown();
            this.VariableClickPositionCheck = new System.Windows.Forms.CheckBox();
            this.VariableClickSpeedCheck = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ListeningLabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.YMouseLocation = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.PickLocationBTN = new System.Windows.Forms.Button();
            this.XMouseLocation = new System.Windows.Forms.NumericUpDown();
            this.FixedLocationRadioBTN = new System.Windows.Forms.RadioButton();
            this.CurrentLocationRadioBTN = new System.Windows.Forms.RadioButton();
            this.PlayStopLabel = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.StartStopWorker = new System.ComponentModel.BackgroundWorker();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MilisecondsValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondsValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinutesValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HoursValue)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RepeatTimesValue)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VariableClickSpeedValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VariableClickPositionValue)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YMouseLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XMouseLocation)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.MilisecondsValue);
            this.groupBox1.Controls.Add(this.SecondsValue);
            this.groupBox1.Controls.Add(this.MinutesValue);
            this.groupBox1.Controls.Add(this.HoursValue);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(538, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Click Interval";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Maroon;
            this.label14.Location = new System.Drawing.Point(12, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(364, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "DISCLAIMER: If you leave 0 on each field, the minimum will be 1 Milisecond";
            // 
            // MilisecondsValue
            // 
            this.MilisecondsValue.Location = new System.Drawing.Point(385, 60);
            this.MilisecondsValue.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.MilisecondsValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MilisecondsValue.Name = "MilisecondsValue";
            this.MilisecondsValue.Size = new System.Drawing.Size(120, 20);
            this.MilisecondsValue.TabIndex = 10;
            this.MilisecondsValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // SecondsValue
            // 
            this.SecondsValue.Location = new System.Drawing.Point(259, 60);
            this.SecondsValue.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.SecondsValue.Name = "SecondsValue";
            this.SecondsValue.Size = new System.Drawing.Size(120, 20);
            this.SecondsValue.TabIndex = 9;
            // 
            // MinutesValue
            // 
            this.MinutesValue.Location = new System.Drawing.Point(132, 60);
            this.MinutesValue.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.MinutesValue.Name = "MinutesValue";
            this.MinutesValue.Size = new System.Drawing.Size(120, 20);
            this.MinutesValue.TabIndex = 8;
            // 
            // HoursValue
            // 
            this.HoursValue.Location = new System.Drawing.Point(6, 60);
            this.HoursValue.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.HoursValue.Name = "HoursValue";
            this.HoursValue.Size = new System.Drawing.Size(120, 20);
            this.HoursValue.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(382, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Miliseconds";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(256, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Seconds";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Hours";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Minutes";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ClickTypeDropBox);
            this.groupBox2.Controls.Add(this.MouseButtonDropBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Click Options";
            // 
            // ClickTypeDropBox
            // 
            this.ClickTypeDropBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ClickTypeDropBox.FormattingEnabled = true;
            this.ClickTypeDropBox.Items.AddRange(new object[] {
            "Single Click",
            "Double Click"});
            this.ClickTypeDropBox.Location = new System.Drawing.Point(95, 63);
            this.ClickTypeDropBox.Name = "ClickTypeDropBox";
            this.ClickTypeDropBox.Size = new System.Drawing.Size(134, 21);
            this.ClickTypeDropBox.TabIndex = 14;
            // 
            // MouseButtonDropBox
            // 
            this.MouseButtonDropBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MouseButtonDropBox.FormattingEnabled = true;
            this.MouseButtonDropBox.Items.AddRange(new object[] {
            "Left",
            "Middle",
            "Right"});
            this.MouseButtonDropBox.Location = new System.Drawing.Point(95, 22);
            this.MouseButtonDropBox.Name = "MouseButtonDropBox";
            this.MouseButtonDropBox.Size = new System.Drawing.Size(134, 21);
            this.MouseButtonDropBox.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Click Type:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Mouse Button:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.RepeatTilStoppedRadioBTN);
            this.groupBox3.Controls.Add(this.RepeatTimesRadioBTN);
            this.groupBox3.Controls.Add(this.RepeatTimesValue);
            this.groupBox3.Location = new System.Drawing.Point(253, 104);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(297, 100);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Click Repetition";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(140, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Clicks";
            // 
            // RepeatTilStoppedRadioBTN
            // 
            this.RepeatTilStoppedRadioBTN.AutoSize = true;
            this.RepeatTilStoppedRadioBTN.Checked = true;
            this.RepeatTilStoppedRadioBTN.Location = new System.Drawing.Point(7, 66);
            this.RepeatTilStoppedRadioBTN.Name = "RepeatTilStoppedRadioBTN";
            this.RepeatTilStoppedRadioBTN.Size = new System.Drawing.Size(127, 17);
            this.RepeatTilStoppedRadioBTN.TabIndex = 13;
            this.RepeatTilStoppedRadioBTN.TabStop = true;
            this.RepeatTilStoppedRadioBTN.Text = "Repeat Until Stopped";
            this.RepeatTilStoppedRadioBTN.UseVisualStyleBackColor = true;
            // 
            // RepeatTimesRadioBTN
            // 
            this.RepeatTimesRadioBTN.AutoSize = true;
            this.RepeatTimesRadioBTN.Location = new System.Drawing.Point(7, 27);
            this.RepeatTimesRadioBTN.Name = "RepeatTimesRadioBTN";
            this.RepeatTimesRadioBTN.Size = new System.Drawing.Size(60, 17);
            this.RepeatTimesRadioBTN.TabIndex = 12;
            this.RepeatTimesRadioBTN.TabStop = true;
            this.RepeatTimesRadioBTN.Text = "Repeat";
            this.RepeatTimesRadioBTN.UseVisualStyleBackColor = true;
            // 
            // RepeatTimesValue
            // 
            this.RepeatTimesValue.Location = new System.Drawing.Point(73, 27);
            this.RepeatTimesValue.Name = "RepeatTimesValue";
            this.RepeatTimesValue.Size = new System.Drawing.Size(61, 20);
            this.RepeatTimesValue.TabIndex = 11;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.VariableClickSpeedUnit);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.VariableClickSpeedValue);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.VariableClickPositionValue);
            this.groupBox4.Controls.Add(this.VariableClickPositionCheck);
            this.groupBox4.Controls.Add(this.VariableClickSpeedCheck);
            this.groupBox4.Location = new System.Drawing.Point(12, 210);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(538, 100);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Game Section";
            // 
            // VariableClickSpeedUnit
            // 
            this.VariableClickSpeedUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VariableClickSpeedUnit.FormattingEnabled = true;
            this.VariableClickSpeedUnit.Items.AddRange(new object[] {
            "Miliseconds",
            "Seconds",
            "Minutes",
            "Hours"});
            this.VariableClickSpeedUnit.Location = new System.Drawing.Point(241, 18);
            this.VariableClickSpeedUnit.Name = "VariableClickSpeedUnit";
            this.VariableClickSpeedUnit.Size = new System.Drawing.Size(95, 21);
            this.VariableClickSpeedUnit.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(222, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(13, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "±";
            // 
            // VariableClickSpeedValue
            // 
            this.VariableClickSpeedValue.Location = new System.Drawing.Point(142, 19);
            this.VariableClickSpeedValue.Name = "VariableClickSpeedValue";
            this.VariableClickSpeedValue.Size = new System.Drawing.Size(74, 20);
            this.VariableClickSpeedValue.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(12, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(426, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "THIS WILL NOT GUARANTEE THAT YOU DO NOT GET BANNED! USE CAREFULLY";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(222, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(240, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "± Pixels (Only works if Fixed Location is selected!)";
            // 
            // VariableClickPositionValue
            // 
            this.VariableClickPositionValue.Location = new System.Drawing.Point(142, 52);
            this.VariableClickPositionValue.Name = "VariableClickPositionValue";
            this.VariableClickPositionValue.Size = new System.Drawing.Size(74, 20);
            this.VariableClickPositionValue.TabIndex = 11;
            // 
            // VariableClickPositionCheck
            // 
            this.VariableClickPositionCheck.AutoSize = true;
            this.VariableClickPositionCheck.Location = new System.Drawing.Point(7, 53);
            this.VariableClickPositionCheck.Name = "VariableClickPositionCheck";
            this.VariableClickPositionCheck.Size = new System.Drawing.Size(130, 17);
            this.VariableClickPositionCheck.TabIndex = 1;
            this.VariableClickPositionCheck.Text = "Variable Click Position";
            this.VariableClickPositionCheck.UseVisualStyleBackColor = true;
            // 
            // VariableClickSpeedCheck
            // 
            this.VariableClickSpeedCheck.AutoSize = true;
            this.VariableClickSpeedCheck.Location = new System.Drawing.Point(7, 20);
            this.VariableClickSpeedCheck.Name = "VariableClickSpeedCheck";
            this.VariableClickSpeedCheck.Size = new System.Drawing.Size(124, 17);
            this.VariableClickSpeedCheck.TabIndex = 0;
            this.VariableClickSpeedCheck.Text = "Variable Click Speed";
            this.VariableClickSpeedCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ListeningLabel);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.YMouseLocation);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.PickLocationBTN);
            this.groupBox5.Controls.Add(this.XMouseLocation);
            this.groupBox5.Controls.Add(this.FixedLocationRadioBTN);
            this.groupBox5.Controls.Add(this.CurrentLocationRadioBTN);
            this.groupBox5.Location = new System.Drawing.Point(12, 316);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(295, 100);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Cursor Position";
            // 
            // ListeningLabel
            // 
            this.ListeningLabel.AutoSize = true;
            this.ListeningLabel.Location = new System.Drawing.Point(109, 41);
            this.ListeningLabel.Name = "ListeningLabel";
            this.ListeningLabel.Size = new System.Drawing.Size(74, 13);
            this.ListeningLabel.TabIndex = 23;
            this.ListeningLabel.Text = "Start Listening";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(256, 78);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "Y";
            // 
            // YMouseLocation
            // 
            this.YMouseLocation.Location = new System.Drawing.Point(189, 74);
            this.YMouseLocation.Maximum = new decimal(new int[] {
            3840,
            0,
            0,
            0});
            this.YMouseLocation.Minimum = new decimal(new int[] {
            3840,
            0,
            0,
            -2147483648});
            this.YMouseLocation.Name = "YMouseLocation";
            this.YMouseLocation.Size = new System.Drawing.Size(61, 20);
            this.YMouseLocation.TabIndex = 21;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(256, 57);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "X";
            // 
            // PickLocationBTN
            // 
            this.PickLocationBTN.Location = new System.Drawing.Point(107, 57);
            this.PickLocationBTN.Name = "PickLocationBTN";
            this.PickLocationBTN.Size = new System.Drawing.Size(76, 35);
            this.PickLocationBTN.TabIndex = 17;
            this.PickLocationBTN.Text = "Pick Location";
            this.PickLocationBTN.UseVisualStyleBackColor = true;
            this.PickLocationBTN.Click += new System.EventHandler(this.PickLocationBTN_Click);
            // 
            // XMouseLocation
            // 
            this.XMouseLocation.Location = new System.Drawing.Point(189, 53);
            this.XMouseLocation.Maximum = new decimal(new int[] {
            3840,
            0,
            0,
            0});
            this.XMouseLocation.Minimum = new decimal(new int[] {
            3840,
            0,
            0,
            -2147483648});
            this.XMouseLocation.Name = "XMouseLocation";
            this.XMouseLocation.Size = new System.Drawing.Size(61, 20);
            this.XMouseLocation.TabIndex = 19;
            // 
            // FixedLocationRadioBTN
            // 
            this.FixedLocationRadioBTN.AutoSize = true;
            this.FixedLocationRadioBTN.Location = new System.Drawing.Point(7, 66);
            this.FixedLocationRadioBTN.Name = "FixedLocationRadioBTN";
            this.FixedLocationRadioBTN.Size = new System.Drawing.Size(94, 17);
            this.FixedLocationRadioBTN.TabIndex = 20;
            this.FixedLocationRadioBTN.TabStop = true;
            this.FixedLocationRadioBTN.Text = "Fixed Location";
            this.FixedLocationRadioBTN.UseVisualStyleBackColor = true;
            // 
            // CurrentLocationRadioBTN
            // 
            this.CurrentLocationRadioBTN.AutoSize = true;
            this.CurrentLocationRadioBTN.Checked = true;
            this.CurrentLocationRadioBTN.Location = new System.Drawing.Point(7, 19);
            this.CurrentLocationRadioBTN.Name = "CurrentLocationRadioBTN";
            this.CurrentLocationRadioBTN.Size = new System.Drawing.Size(103, 17);
            this.CurrentLocationRadioBTN.TabIndex = 19;
            this.CurrentLocationRadioBTN.TabStop = true;
            this.CurrentLocationRadioBTN.Text = "Current Location";
            this.CurrentLocationRadioBTN.UseVisualStyleBackColor = true;
            // 
            // PlayStopLabel
            // 
            this.PlayStopLabel.AutoSize = true;
            this.PlayStopLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayStopLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.PlayStopLabel.Location = new System.Drawing.Point(363, 336);
            this.PlayStopLabel.Name = "PlayStopLabel";
            this.PlayStopLabel.Size = new System.Drawing.Size(143, 25);
            this.PlayStopLabel.TabIndex = 17;
            this.PlayStopLabel.Text = " IS STOPPED";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(393, 369);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(92, 13);
            this.label17.TabIndex = 18;
            this.label17.Text = "Use F6 to START";
            // 
            // StartStopWorker
            // 
            this.StartStopWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.StartStopWorker_DoWork);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(393, 386);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(85, 13);
            this.label18.TabIndex = 19;
            this.label18.Text = "Use F7 to STOP";
            // 
            // AppWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(559, 430);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.PlayStopLabel);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AppWindow";
            this.Text = "Clickity Clacity Cloom";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AppClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MilisecondsValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondsValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinutesValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HoursValue)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RepeatTimesValue)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VariableClickSpeedValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VariableClickPositionValue)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YMouseLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XMouseLocation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown MilisecondsValue;
        private System.Windows.Forms.NumericUpDown SecondsValue;
        private System.Windows.Forms.NumericUpDown MinutesValue;
        private System.Windows.Forms.NumericUpDown HoursValue;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ClickTypeDropBox;
        private System.Windows.Forms.ComboBox MouseButtonDropBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton RepeatTilStoppedRadioBTN;
        private System.Windows.Forms.RadioButton RepeatTimesRadioBTN;
        private System.Windows.Forms.NumericUpDown RepeatTimesValue;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown VariableClickPositionValue;
        private System.Windows.Forms.CheckBox VariableClickPositionCheck;
        private System.Windows.Forms.CheckBox VariableClickSpeedCheck;
        private System.Windows.Forms.ComboBox VariableClickSpeedUnit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown VariableClickSpeedValue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown YMouseLocation;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button PickLocationBTN;
        private System.Windows.Forms.NumericUpDown XMouseLocation;
        private System.Windows.Forms.RadioButton FixedLocationRadioBTN;
        private System.Windows.Forms.RadioButton CurrentLocationRadioBTN;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label PlayStopLabel;
        private System.Windows.Forms.Label label17;
        private System.ComponentModel.BackgroundWorker StartStopWorker;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label ListeningLabel;
    }
}

