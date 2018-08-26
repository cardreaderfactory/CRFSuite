/********************************************************************************
 * \copyright
 * Copyright 2009-2017, Card Reader Factory.  All rights were reserved.
 * From 2018 this code has been made PUBLIC DOMAIN.
 * This means that there are no longer any ownership rights such as copyright, trademark, or patent over this code.
 * This code can be modified, distributed, or sold even without any attribution by anyone.
 *
 * We would however be very grateful to anyone using this code in their product if you could add the line below into your product's documentation:
 * Special thanks to Nicholas Alexander Michael Webber, Terry Botten & all the staff working for Operation (Police) Academy. Without these people this code would not have been made public and the existance of this very product would be very much in doubt.
 *
 *******************************************************************************/

namespace crf.Forms
{
    partial class DecodeSettings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.timeFormat = new System.Windows.Forms.ComboBox();
            this.time = new System.Windows.Forms.CheckBox();
            this.direction = new System.Windows.Forms.CheckBox();
            this.groupSwipesUnits = new System.Windows.Forms.ComboBox();
            this.alignChars = new System.Windows.Forms.TextBox();
            this.groupSwipes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.decodeFormat = new System.Windows.Forms.ComboBox();
            this.track3Stats = new System.Windows.Forms.Label();
            this.track2Stats = new System.Windows.Forms.Label();
            this.track1Stats = new System.Windows.Forms.Label();
            this.track3Start = new System.Windows.Forms.TextBox();
            this.track2Start = new System.Windows.Forms.TextBox();
            this.track1Start = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.track3BPC = new System.Windows.Forms.ComboBox();
            this.track2BPC = new System.Windows.Forms.ComboBox();
            this.track1BPC = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.showTrack3 = new System.Windows.Forms.CheckBox();
            this.showTrack2 = new System.Windows.Forms.CheckBox();
            this.showTrack1 = new System.Windows.Forms.CheckBox();
            this.decodeMethod = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.showDecodeSettings = new System.Windows.Forms.CheckBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.radioAdvancedMode = new System.Windows.Forms.RadioButton();
            this.radioSimpleMode = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.timeFormat);
            this.groupBox1.Controls.Add(this.time);
            this.groupBox1.Controls.Add(this.direction);
            this.groupBox1.Controls.Add(this.groupSwipesUnits);
            this.groupBox1.Controls.Add(this.alignChars);
            this.groupBox1.Controls.Add(this.groupSwipes);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 85);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(346, 115);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Display settings";
            // 
            // timeFormat
            // 
            this.timeFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeFormat.FormattingEnabled = true;
            this.timeFormat.Items.AddRange(new object[] {
            "dd/mm/yyyy hh:mm:ss",
            "dd/mm/yyyy hh:mm:ss.fff",
            "hhhhhh:mm:ss",
            "hhhhhh:mm:ss.fff",
            "hh:mm:ss",
            "hh:mm:ss.fff",
            "ssssssssssssssss",
            "ssssssssssssssss.fff",
            "ssssss",
            "ssssss.fff",
            "diff",
            "diff.fff"});
            this.timeFormat.Location = new System.Drawing.Point(98, 86);
            this.timeFormat.Margin = new System.Windows.Forms.Padding(2);
            this.timeFormat.Name = "timeFormat";
            this.timeFormat.Size = new System.Drawing.Size(157, 21);
            this.timeFormat.TabIndex = 9;
            this.timeFormat.SelectedIndexChanged += new System.EventHandler(this.timeFormatChanged);
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.Location = new System.Drawing.Point(8, 88);
            this.time.Margin = new System.Windows.Forms.Padding(2);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(75, 17);
            this.time.TabIndex = 8;
            this.time.Text = "Show time";
            this.time.UseVisualStyleBackColor = true;
            this.time.CheckedChanged += new System.EventHandler(this.time_CheckedChanged);
            // 
            // direction
            // 
            this.direction.AutoSize = true;
            this.direction.Location = new System.Drawing.Point(8, 65);
            this.direction.Margin = new System.Windows.Forms.Padding(2);
            this.direction.Name = "direction";
            this.direction.Size = new System.Drawing.Size(96, 17);
            this.direction.TabIndex = 7;
            this.direction.Text = "Show direction";
            this.direction.UseVisualStyleBackColor = true;
            this.direction.CheckedChanged += new System.EventHandler(this.direction_CheckedChanged);
            // 
            // groupSwipesUnits
            // 
            this.groupSwipesUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.groupSwipesUnits.FormattingEnabled = true;
            this.groupSwipesUnits.Items.AddRange(new object[] {
            "minutes",
            "seconds",
            "miliseconds"});
            this.groupSwipesUnits.Location = new System.Drawing.Point(151, 17);
            this.groupSwipesUnits.Margin = new System.Windows.Forms.Padding(2);
            this.groupSwipesUnits.Name = "groupSwipesUnits";
            this.groupSwipesUnits.Size = new System.Drawing.Size(104, 21);
            this.groupSwipesUnits.TabIndex = 5;
            this.groupSwipesUnits.SelectedIndexChanged += new System.EventHandler(this.groupSwipesUnitsChanged);
            // 
            // alignChars
            // 
            this.alignChars.Location = new System.Drawing.Point(98, 41);
            this.alignChars.Margin = new System.Windows.Forms.Padding(2);
            this.alignChars.MaxLength = 2;
            this.alignChars.Name = "alignChars";
            this.alignChars.Size = new System.Drawing.Size(46, 20);
            this.alignChars.TabIndex = 4;
            this.alignChars.Validating += new System.ComponentModel.CancelEventHandler(this.alignChars_Validating);
            // 
            // groupSwipes
            // 
            this.groupSwipes.Location = new System.Drawing.Point(98, 17);
            this.groupSwipes.Margin = new System.Windows.Forms.Padding(2);
            this.groupSwipes.MaxLength = 5;
            this.groupSwipes.Name = "groupSwipes";
            this.groupSwipes.Size = new System.Drawing.Size(46, 20);
            this.groupSwipes.TabIndex = 3;
            this.groupSwipes.Validating += new System.ComponentModel.CancelEventHandler(this.groupSwipes_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(148, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "common characters";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Align by";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Grouping interval";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.decodeFormat);
            this.groupBox2.Controls.Add(this.track3Stats);
            this.groupBox2.Controls.Add(this.track2Stats);
            this.groupBox2.Controls.Add(this.track1Stats);
            this.groupBox2.Controls.Add(this.track3Start);
            this.groupBox2.Controls.Add(this.track2Start);
            this.groupBox2.Controls.Add(this.track1Start);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.track3BPC);
            this.groupBox2.Controls.Add(this.track2BPC);
            this.groupBox2.Controls.Add(this.track1BPC);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.showTrack3);
            this.groupBox2.Controls.Add(this.showTrack2);
            this.groupBox2.Controls.Add(this.showTrack1);
            this.groupBox2.Controls.Add(this.decodeMethod);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(7, 207);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(346, 149);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Decoding parameters";
            // 
            // decodeFormat
            // 
            this.decodeFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.decodeFormat.FormattingEnabled = true;
            this.decodeFormat.Items.AddRange(new object[] {
            "ISO 7811 (IATA + ABA + THRIFT)",
            "AAMVA (IATA + ABA + IATA)",
            "California Driving License (MINTS + ABA + MINTS)",
            "Reverse ISO 7811 (THRIFT+ ABA + IATA)",
            "All ABA (ABA + ABA + ABA)",
            "Custom format"});
            this.decodeFormat.Location = new System.Drawing.Point(7, 120);
            this.decodeFormat.Margin = new System.Windows.Forms.Padding(2);
            this.decodeFormat.Name = "decodeFormat";
            this.decodeFormat.Size = new System.Drawing.Size(332, 21);
            this.decodeFormat.TabIndex = 29;
            this.decodeFormat.SelectedIndexChanged += new System.EventHandler(this.standardChanged);
            // 
            // track3Stats
            // 
            this.track3Stats.AutoSize = true;
            this.track3Stats.Location = new System.Drawing.Point(219, 99);
            this.track3Stats.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.track3Stats.Name = "track3Stats";
            this.track3Stats.Size = new System.Drawing.Size(65, 13);
            this.track3Stats.TabIndex = 28;
            this.track3Stats.Text = "0 cards (0%)";
            // 
            // track2Stats
            // 
            this.track2Stats.AutoSize = true;
            this.track2Stats.Location = new System.Drawing.Point(219, 74);
            this.track2Stats.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.track2Stats.Name = "track2Stats";
            this.track2Stats.Size = new System.Drawing.Size(65, 13);
            this.track2Stats.TabIndex = 27;
            this.track2Stats.Text = "0 cards (0%)";
            // 
            // track1Stats
            // 
            this.track1Stats.AutoSize = true;
            this.track1Stats.Location = new System.Drawing.Point(219, 49);
            this.track1Stats.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.track1Stats.Name = "track1Stats";
            this.track1Stats.Size = new System.Drawing.Size(65, 13);
            this.track1Stats.TabIndex = 26;
            this.track1Stats.Text = "0 cards (0%)";
            // 
            // track3Start
            // 
            this.track3Start.Location = new System.Drawing.Point(171, 95);
            this.track3Start.Margin = new System.Windows.Forms.Padding(2);
            this.track3Start.Name = "track3Start";
            this.track3Start.Size = new System.Drawing.Size(44, 20);
            this.track3Start.TabIndex = 25;
            // 
            // track2Start
            // 
            this.track2Start.Location = new System.Drawing.Point(171, 70);
            this.track2Start.Margin = new System.Windows.Forms.Padding(2);
            this.track2Start.Name = "track2Start";
            this.track2Start.Size = new System.Drawing.Size(44, 20);
            this.track2Start.TabIndex = 24;
            // 
            // track1Start
            // 
            this.track1Start.Location = new System.Drawing.Point(171, 45);
            this.track1Start.Margin = new System.Windows.Forms.Padding(2);
            this.track1Start.Name = "track1Start";
            this.track1Start.Size = new System.Drawing.Size(44, 20);
            this.track1Start.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(138, 99);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Start";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(138, 74);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Start";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(138, 49);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Start";
            // 
            // track3BPC
            // 
            this.track3BPC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.track3BPC.FormattingEnabled = true;
            this.track3BPC.Items.AddRange(new object[] {
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.track3BPC.Location = new System.Drawing.Point(98, 95);
            this.track3BPC.Margin = new System.Windows.Forms.Padding(2);
            this.track3BPC.Name = "track3BPC";
            this.track3BPC.Size = new System.Drawing.Size(36, 21);
            this.track3BPC.TabIndex = 19;
            // 
            // track2BPC
            // 
            this.track2BPC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.track2BPC.FormattingEnabled = true;
            this.track2BPC.Items.AddRange(new object[] {
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.track2BPC.Location = new System.Drawing.Point(98, 70);
            this.track2BPC.Margin = new System.Windows.Forms.Padding(2);
            this.track2BPC.Name = "track2BPC";
            this.track2BPC.Size = new System.Drawing.Size(36, 21);
            this.track2BPC.TabIndex = 18;
            // 
            // track1BPC
            // 
            this.track1BPC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.track1BPC.FormattingEnabled = true;
            this.track1BPC.Items.AddRange(new object[] {
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.track1BPC.Location = new System.Drawing.Point(98, 45);
            this.track1BPC.Margin = new System.Windows.Forms.Padding(2);
            this.track1BPC.Name = "track1BPC";
            this.track1BPC.Size = new System.Drawing.Size(36, 21);
            this.track1BPC.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(71, 99);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "BPC";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(71, 74);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "BPC";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(71, 49);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "BPC";
            // 
            // showTrack3
            // 
            this.showTrack3.AutoSize = true;
            this.showTrack3.Location = new System.Drawing.Point(8, 97);
            this.showTrack3.Margin = new System.Windows.Forms.Padding(2);
            this.showTrack3.Name = "showTrack3";
            this.showTrack3.Size = new System.Drawing.Size(63, 17);
            this.showTrack3.TabIndex = 13;
            this.showTrack3.Text = "Track 3";
            this.showTrack3.UseVisualStyleBackColor = true;
            // 
            // showTrack2
            // 
            this.showTrack2.AutoSize = true;
            this.showTrack2.Location = new System.Drawing.Point(8, 72);
            this.showTrack2.Margin = new System.Windows.Forms.Padding(2);
            this.showTrack2.Name = "showTrack2";
            this.showTrack2.Size = new System.Drawing.Size(63, 17);
            this.showTrack2.TabIndex = 12;
            this.showTrack2.Text = "Track 2";
            this.showTrack2.UseVisualStyleBackColor = true;
            // 
            // showTrack1
            // 
            this.showTrack1.AutoSize = true;
            this.showTrack1.Location = new System.Drawing.Point(8, 47);
            this.showTrack1.Margin = new System.Windows.Forms.Padding(2);
            this.showTrack1.Name = "showTrack1";
            this.showTrack1.Size = new System.Drawing.Size(63, 17);
            this.showTrack1.TabIndex = 11;
            this.showTrack1.Text = "Track 1";
            this.showTrack1.UseVisualStyleBackColor = true;
            // 
            // decodeMethod
            // 
            this.decodeMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.decodeMethod.FormattingEnabled = true;
            this.decodeMethod.Items.AddRange(new object[] {
            "Show everything (including the errors)",
            "Show only the valid data, hide the errors",
            "Show the best guess"});
            this.decodeMethod.Location = new System.Drawing.Point(98, 17);
            this.decodeMethod.Margin = new System.Windows.Forms.Padding(2);
            this.decodeMethod.Name = "decodeMethod";
            this.decodeMethod.Size = new System.Drawing.Size(241, 21);
            this.decodeMethod.TabIndex = 10;
            this.decodeMethod.SelectedIndexChanged += new System.EventHandler(this.decodeMethodChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Decode method";
            // 
            // showDecodeSettings
            // 
            this.showDecodeSettings.AutoSize = true;
            this.showDecodeSettings.Location = new System.Drawing.Point(16, 360);
            this.showDecodeSettings.Margin = new System.Windows.Forms.Padding(2);
            this.showDecodeSettings.Name = "showDecodeSettings";
            this.showDecodeSettings.Size = new System.Drawing.Size(206, 17);
            this.showDecodeSettings.TabIndex = 5;
            this.showDecodeSettings.Text = "&Show this window when opening a file";
            this.showDecodeSettings.UseVisualStyleBackColor = true;
            this.showDecodeSettings.CheckedChanged += new System.EventHandler(this.showDecodeSettings_CheckedChanged);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(218, 383);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(2);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(90, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdOk.Location = new System.Drawing.Point(56, 383);
            this.cmdOk.Margin = new System.Windows.Forms.Padding(2);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(90, 23);
            this.cmdOk.TabIndex = 0;
            this.cmdOk.Text = "&Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // radioAdvancedMode
            // 
            this.radioAdvancedMode.AutoSize = true;
            this.radioAdvancedMode.Location = new System.Drawing.Point(8, 20);
            this.radioAdvancedMode.Name = "radioAdvancedMode";
            this.radioAdvancedMode.Size = new System.Drawing.Size(202, 17);
            this.radioAdvancedMode.TabIndex = 1;
            this.radioAdvancedMode.TabStop = true;
            this.radioAdvancedMode.Text = "Advanced (Grid Mode, more features)";
            this.radioAdvancedMode.UseVisualStyleBackColor = true;
            this.radioAdvancedMode.CheckedChanged += new System.EventHandler(this.showAdvanceDecode_Changed);
            // 
            // radioSimpleMode
            // 
            this.radioSimpleMode.AutoSize = true;
            this.radioSimpleMode.Location = new System.Drawing.Point(8, 42);
            this.radioSimpleMode.Name = "radioSimpleMode";
            this.radioSimpleMode.Size = new System.Drawing.Size(116, 17);
            this.radioSimpleMode.TabIndex = 0;
            this.radioSimpleMode.TabStop = true;
            this.radioSimpleMode.Text = "Simple (Text Mode)";
            this.radioSimpleMode.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioSimpleMode);
            this.groupBox3.Controls.Add(this.radioAdvancedMode);
            this.groupBox3.Location = new System.Drawing.Point(7, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(346, 67);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Decoder";
            // 
            // DecodeSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(364, 415);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.showDecodeSettings);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::crf.Properties.Resources.CRFIcon;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "DecodeSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Decode Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fClosing);
            this.Shown += new System.EventHandler(this.DecodeSettings_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox timeFormat;
        private System.Windows.Forms.CheckBox time;
        private System.Windows.Forms.CheckBox direction;
        private System.Windows.Forms.ComboBox groupSwipesUnits;
        private System.Windows.Forms.TextBox alignChars;
        private System.Windows.Forms.TextBox groupSwipes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox track3BPC;
        private System.Windows.Forms.ComboBox track2BPC;
        private System.Windows.Forms.ComboBox track1BPC;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox showTrack3;
        private System.Windows.Forms.CheckBox showTrack2;
        private System.Windows.Forms.CheckBox showTrack1;
        private System.Windows.Forms.ComboBox decodeMethod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox decodeFormat;
        private System.Windows.Forms.Label track3Stats;
        private System.Windows.Forms.Label track2Stats;
        private System.Windows.Forms.Label track1Stats;
        private System.Windows.Forms.TextBox track3Start;
        private System.Windows.Forms.TextBox track2Start;
        private System.Windows.Forms.TextBox track1Start;
        private System.Windows.Forms.CheckBox showDecodeSettings;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.RadioButton radioAdvancedMode;
        private System.Windows.Forms.RadioButton radioSimpleMode;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}