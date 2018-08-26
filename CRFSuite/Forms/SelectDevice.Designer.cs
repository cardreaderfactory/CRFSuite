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
    partial class SelectDevice
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
            this.radioButton168v = new System.Windows.Forms.RadioButton();
            this.radioButton328p = new System.Windows.Forms.RadioButton();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.MSRv007 = new System.Windows.Forms.RadioButton();
            this.MSRv008 = new System.Windows.Forms.RadioButton();
            this.MSRv009 = new System.Windows.Forms.RadioButton();
            this.MSRv010 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton3284 = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.recoveryCodeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButton168v
            // 
            this.radioButton168v.AutoSize = true;
            this.radioButton168v.Location = new System.Drawing.Point(18, 6);
            this.radioButton168v.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton168v.Name = "radioButton168v";
            this.radioButton168v.Size = new System.Drawing.Size(49, 17);
            this.radioButton168v.TabIndex = 0;
            this.radioButton168v.TabStop = true;
            this.radioButton168v.Text = "168v";
            this.radioButton168v.UseVisualStyleBackColor = true;
            this.radioButton168v.CheckedChanged += new System.EventHandler(this.cpuSelected);
            // 
            // radioButton328p
            // 
            this.radioButton328p.AutoSize = true;
            this.radioButton328p.Location = new System.Drawing.Point(97, 6);
            this.radioButton328p.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton328p.Name = "radioButton328p";
            this.radioButton328p.Size = new System.Drawing.Size(49, 17);
            this.radioButton328p.TabIndex = 1;
            this.radioButton328p.TabStop = true;
            this.radioButton328p.Text = "328p";
            this.radioButton328p.UseVisualStyleBackColor = true;
            this.radioButton328p.CheckedChanged += new System.EventHandler(this.cpuSelected);
            // 
            // cmdOk
            // 
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOk.Enabled = false;
            this.cmdOk.Location = new System.Drawing.Point(304, 344);
            this.cmdOk.Margin = new System.Windows.Forms.Padding(2);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 2;
            this.cmdOk.Text = "&Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(384, 344);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(2);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // MSRv007
            // 
            this.MSRv007.Appearance = System.Windows.Forms.Appearance.Button;
            this.MSRv007.AutoSize = true;
            this.MSRv007.Image = global::crf.Properties.Resources.v007;
            this.MSRv007.Location = new System.Drawing.Point(8, 30);
            this.MSRv007.Margin = new System.Windows.Forms.Padding(2);
            this.MSRv007.Name = "MSRv007";
            this.MSRv007.Size = new System.Drawing.Size(106, 106);
            this.MSRv007.TabIndex = 1;
            this.MSRv007.TabStop = true;
            this.MSRv007.UseVisualStyleBackColor = true;
            this.MSRv007.CheckedChanged += new System.EventHandler(this.deviceSelected);
            // 
            // MSRv008
            // 
            this.MSRv008.Appearance = System.Windows.Forms.Appearance.Button;
            this.MSRv008.AutoSize = true;
            this.MSRv008.Image = global::crf.Properties.Resources.v008;
            this.MSRv008.Location = new System.Drawing.Point(118, 30);
            this.MSRv008.Margin = new System.Windows.Forms.Padding(2);
            this.MSRv008.Name = "MSRv008";
            this.MSRv008.Size = new System.Drawing.Size(106, 106);
            this.MSRv008.TabIndex = 2;
            this.MSRv008.TabStop = true;
            this.MSRv008.UseVisualStyleBackColor = true;
            this.MSRv008.CheckedChanged += new System.EventHandler(this.deviceSelected);
            // 
            // MSRv009
            // 
            this.MSRv009.Appearance = System.Windows.Forms.Appearance.Button;
            this.MSRv009.AutoSize = true;
            this.MSRv009.Image = global::crf.Properties.Resources.v009;
            this.MSRv009.Location = new System.Drawing.Point(228, 30);
            this.MSRv009.Margin = new System.Windows.Forms.Padding(2);
            this.MSRv009.Name = "MSRv009";
            this.MSRv009.Size = new System.Drawing.Size(106, 106);
            this.MSRv009.TabIndex = 3;
            this.MSRv009.TabStop = true;
            this.MSRv009.UseVisualStyleBackColor = true;
            this.MSRv009.CheckedChanged += new System.EventHandler(this.deviceSelected);
            // 
            // MSRv010
            // 
            this.MSRv010.Appearance = System.Windows.Forms.Appearance.Button;
            this.MSRv010.AutoSize = true;
            this.MSRv010.Image = global::crf.Properties.Resources.v010;
            this.MSRv010.Location = new System.Drawing.Point(338, 30);
            this.MSRv010.Margin = new System.Windows.Forms.Padding(2);
            this.MSRv010.Name = "MSRv010";
            this.MSRv010.Size = new System.Drawing.Size(106, 106);
            this.MSRv010.TabIndex = 4;
            this.MSRv010.TabStop = true;
            this.MSRv010.UseVisualStyleBackColor = true;
            this.MSRv010.CheckedChanged += new System.EventHandler(this.deviceSelected);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.MSRv010);
            this.groupBox1.Controls.Add(this.MSRv007);
            this.groupBox1.Controls.Add(this.MSRv009);
            this.groupBox1.Controls.Add(this.MSRv008);
            this.groupBox1.Location = new System.Drawing.Point(9, 152);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(452, 188);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Option 2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton3284);
            this.panel1.Controls.Add(this.radioButton328p);
            this.panel1.Controls.Add(this.radioButton168v);
            this.panel1.Location = new System.Drawing.Point(97, 154);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(246, 29);
            this.panel1.TabIndex = 1;
            // 
            // radioButton3284
            // 
            this.radioButton3284.AutoSize = true;
            this.radioButton3284.Location = new System.Drawing.Point(176, 6);
            this.radioButton3284.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton3284.Name = "radioButton3284";
            this.radioButton3284.Size = new System.Drawing.Size(49, 17);
            this.radioButton3284.TabIndex = 2;
            this.radioButton3284.TabStop = true;
            this.radioButton3284.Text = "3284";
            this.radioButton3284.UseVisualStyleBackColor = true;
            this.radioButton3284.CheckedChanged += new System.EventHandler(this.cpuSelected);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 139);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(167, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "2. What is your device CPU type?";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 15);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(173, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "1. How does your device look like?";
            // 
            // recoveryCodeTextBox
            // 
            this.recoveryCodeTextBox.Location = new System.Drawing.Point(172, 14);
            this.recoveryCodeTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.recoveryCodeTextBox.MaxLength = 32;
            this.recoveryCodeTextBox.Name = "recoveryCodeTextBox";
            this.recoveryCodeTextBox.Size = new System.Drawing.Size(272, 20);
            this.recoveryCodeTextBox.TabIndex = 1;
            this.recoveryCodeTextBox.TextChanged += new System.EventHandler(this.recoveryCodeChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Build number or Recovery code:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(11, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "Warning:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(432, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Please select the correct parameters only! Using the wrong settings can get your " +
    "device to";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 83);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(444, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "stop working. If you are not sure, please do not proceed and contact your distrib" +
    "uitor for help.";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.recoveryCodeTextBox);
            this.groupBox3.Location = new System.Drawing.Point(9, 107);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(452, 41);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Option 1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(401, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Your device appears to be in firmware recovery mode.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 26);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(435, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "In order to get it working again, we need some details about it. Please complete " +
    "only one of";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 39);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "the options groups below.";
            // 
            // SelectDevice
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(470, 376);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.Icon = global::crf.Properties.Resources.CRFIcon;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectDevice";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Recovery mode";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton168v;
        private System.Windows.Forms.RadioButton radioButton328p;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.RadioButton MSRv007;
        private System.Windows.Forms.RadioButton MSRv008;
        private System.Windows.Forms.RadioButton MSRv009;
        private System.Windows.Forms.RadioButton MSRv010;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox recoveryCodeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton3284;
    }
}