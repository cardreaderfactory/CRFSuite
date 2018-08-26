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
    partial class Preferences
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.askToChangeDefaultPassCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.associationStatusLabel = new System.Windows.Forms.Label();
            this.keepFileAssociationsCheckBox = new System.Windows.Forms.CheckBox();
            this.cmdAssociateCRF = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.editKeysCheckBox = new System.Windows.Forms.CheckBox();
            this.keysTextBox = new System.Windows.Forms.TextBox();
            this.keyCountLabel = new System.Windows.Forms.Label();
            this.cmdEraseKeys = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.programNameTextBox = new System.Windows.Forms.TextBox();
            this.removeOEMcheckBox = new System.Windows.Forms.CheckBox();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdApply = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.radioDownloadYes = new System.Windows.Forms.RadioButton();
            this.radioDownloadNo = new System.Windows.Forms.RadioButton();
            this.radioDownloadAsk = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(9, 10);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(377, 183);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(369, 157);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.radioDownloadAsk);
            this.groupBox2.Controls.Add(this.radioDownloadNo);
            this.groupBox2.Controls.Add(this.radioDownloadYes);
            this.groupBox2.Controls.Add(this.askToChangeDefaultPassCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(4, 76);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(361, 74);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "User Interface";
            // 
            // askToChangeDefaultPassCheckBox
            // 
            this.askToChangeDefaultPassCheckBox.AutoSize = true;
            this.askToChangeDefaultPassCheckBox.Location = new System.Drawing.Point(7, 22);
            this.askToChangeDefaultPassCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.askToChangeDefaultPassCheckBox.Name = "askToChangeDefaultPassCheckBox";
            this.askToChangeDefaultPassCheckBox.Size = new System.Drawing.Size(191, 17);
            this.askToChangeDefaultPassCheckBox.TabIndex = 0;
            this.askToChangeDefaultPassCheckBox.Text = "Ask to change insecure passwords";
            this.askToChangeDefaultPassCheckBox.UseVisualStyleBackColor = true;
            this.askToChangeDefaultPassCheckBox.CheckedChanged += new System.EventHandler(this.askToChangeDefaultPassCheckBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.associationStatusLabel);
            this.groupBox1.Controls.Add(this.keepFileAssociationsCheckBox);
            this.groupBox1.Controls.Add(this.cmdAssociateCRF);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(361, 67);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Windows Integration";
            // 
            // associationStatusLabel
            // 
            this.associationStatusLabel.AutoSize = true;
            this.associationStatusLabel.Location = new System.Drawing.Point(4, 17);
            this.associationStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.associationStatusLabel.Name = "associationStatusLabel";
            this.associationStatusLabel.Size = new System.Drawing.Size(92, 13);
            this.associationStatusLabel.TabIndex = 2;
            this.associationStatusLabel.Text = "Association status";
            // 
            // keepFileAssociationsCheckBox
            // 
            this.keepFileAssociationsCheckBox.AutoSize = true;
            this.keepFileAssociationsCheckBox.Location = new System.Drawing.Point(179, 38);
            this.keepFileAssociationsCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.keepFileAssociationsCheckBox.Name = "keepFileAssociationsCheckBox";
            this.keepFileAssociationsCheckBox.Size = new System.Drawing.Size(163, 17);
            this.keepFileAssociationsCheckBox.TabIndex = 1;
            this.keepFileAssociationsCheckBox.Text = "Check association on startup";
            this.keepFileAssociationsCheckBox.UseVisualStyleBackColor = true;
            this.keepFileAssociationsCheckBox.CheckedChanged += new System.EventHandler(this.keepFileAssociationsCheckBox_CheckedChanged);
            // 
            // cmdAssociateCRF
            // 
            this.cmdAssociateCRF.Location = new System.Drawing.Point(7, 35);
            this.cmdAssociateCRF.Margin = new System.Windows.Forms.Padding(2);
            this.cmdAssociateCRF.Name = "cmdAssociateCRF";
            this.cmdAssociateCRF.Size = new System.Drawing.Size(157, 23);
            this.cmdAssociateCRF.TabIndex = 0;
            this.cmdAssociateCRF.Text = "Associate with .crf files";
            this.cmdAssociateCRF.UseVisualStyleBackColor = true;
            this.cmdAssociateCRF.Click += new System.EventHandler(this.cmdAssociateCRF_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.editKeysCheckBox);
            this.tabPage2.Controls.Add(this.keysTextBox);
            this.tabPage2.Controls.Add(this.keyCountLabel);
            this.tabPage2.Controls.Add(this.cmdEraseKeys);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(369, 173);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Keys";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // editKeysCheckBox
            // 
            this.editKeysCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editKeysCheckBox.AutoSize = true;
            this.editKeysCheckBox.Location = new System.Drawing.Point(275, 6);
            this.editKeysCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.editKeysCheckBox.Name = "editKeysCheckBox";
            this.editKeysCheckBox.Size = new System.Drawing.Size(69, 17);
            this.editKeysCheckBox.TabIndex = 0;
            this.editKeysCheckBox.Text = "Edit keys";
            this.editKeysCheckBox.UseVisualStyleBackColor = true;
            this.editKeysCheckBox.CheckedChanged += new System.EventHandler(this.editKeysCheckBox_CheckedChanged);
            // 
            // keysTextBox
            // 
            this.keysTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keysTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keysTextBox.Location = new System.Drawing.Point(4, 4);
            this.keysTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.keysTextBox.MaxLength = 1048576;
            this.keysTextBox.Multiline = true;
            this.keysTextBox.Name = "keysTextBox";
            this.keysTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.keysTextBox.Size = new System.Drawing.Size(264, 148);
            this.keysTextBox.TabIndex = 1;
            this.keysTextBox.TextChanged += new System.EventHandler(this.keysChanged);
            // 
            // keyCountLabel
            // 
            this.keyCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.keyCountLabel.AutoSize = true;
            this.keyCountLabel.Location = new System.Drawing.Point(272, 114);
            this.keyCountLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.keyCountLabel.Name = "keyCountLabel";
            this.keyCountLabel.Size = new System.Drawing.Size(70, 13);
            this.keyCountLabel.TabIndex = 1;
            this.keyCountLabel.Text = "0 keys stored";
            // 
            // cmdEraseKeys
            // 
            this.cmdEraseKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEraseKeys.Location = new System.Drawing.Point(275, 129);
            this.cmdEraseKeys.Margin = new System.Windows.Forms.Padding(2);
            this.cmdEraseKeys.Name = "cmdEraseKeys";
            this.cmdEraseKeys.Size = new System.Drawing.Size(90, 23);
            this.cmdEraseKeys.TabIndex = 2;
            this.cmdEraseKeys.Text = "Erase all";
            this.cmdEraseKeys.UseVisualStyleBackColor = true;
            this.cmdEraseKeys.Click += new System.EventHandler(this.cmdEraseKeys_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.programNameTextBox);
            this.tabPage3.Controls.Add(this.removeOEMcheckBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(369, 173);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "OEM";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "CRFSuite new name:";
            // 
            // programNameTextBox
            // 
            this.programNameTextBox.Location = new System.Drawing.Point(130, 49);
            this.programNameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.programNameTextBox.Name = "programNameTextBox";
            this.programNameTextBox.Size = new System.Drawing.Size(227, 20);
            this.programNameTextBox.TabIndex = 1;
            this.programNameTextBox.TextChanged += new System.EventHandler(this.nameChanged);
            // 
            // removeOEMcheckBox
            // 
            this.removeOEMcheckBox.AutoSize = true;
            this.removeOEMcheckBox.Location = new System.Drawing.Point(12, 15);
            this.removeOEMcheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.removeOEMcheckBox.Name = "removeOEMcheckBox";
            this.removeOEMcheckBox.Size = new System.Drawing.Size(134, 17);
            this.removeOEMcheckBox.TabIndex = 0;
            this.removeOEMcheckBox.Text = "Remove OEM features";
            this.removeOEMcheckBox.UseVisualStyleBackColor = true;
            this.removeOEMcheckBox.CheckedChanged += new System.EventHandler(this.removeOEMCheckBox_CheckedChanged);
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOk.Location = new System.Drawing.Point(170, 197);
            this.cmdOk.Margin = new System.Windows.Forms.Padding(2);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(68, 23);
            this.cmdOk.TabIndex = 1;
            this.cmdOk.Text = "&Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(242, 197);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(2);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(68, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdApply
            // 
            this.cmdApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdApply.Location = new System.Drawing.Point(314, 197);
            this.cmdApply.Margin = new System.Windows.Forms.Padding(2);
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Size = new System.Drawing.Size(68, 23);
            this.cmdApply.TabIndex = 3;
            this.cmdApply.Text = "&Apply";
            this.cmdApply.UseVisualStyleBackColor = true;
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 30000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.IsBalloon = true;
            this.toolTip.ReshowDelay = 100;
            // 
            // radioDownloadYes
            // 
            this.radioDownloadYes.AutoSize = true;
            this.radioDownloadYes.Location = new System.Drawing.Point(136, 44);
            this.radioDownloadYes.Name = "radioDownloadYes";
            this.radioDownloadYes.Size = new System.Drawing.Size(43, 17);
            this.radioDownloadYes.TabIndex = 1;
            this.radioDownloadYes.TabStop = true;
            this.radioDownloadYes.Text = "Yes";
            this.radioDownloadYes.UseVisualStyleBackColor = true;
            this.radioDownloadYes.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioDownloadNo
            // 
            this.radioDownloadNo.AutoSize = true;
            this.radioDownloadNo.Location = new System.Drawing.Point(182, 44);
            this.radioDownloadNo.Name = "radioDownloadNo";
            this.radioDownloadNo.Size = new System.Drawing.Size(39, 17);
            this.radioDownloadNo.TabIndex = 2;
            this.radioDownloadNo.TabStop = true;
            this.radioDownloadNo.Text = "No";
            this.radioDownloadNo.UseVisualStyleBackColor = true;
            this.radioDownloadNo.CheckedChanged += new System.EventHandler(this.radioDownloadNo_CheckedChanged);
            // 
            // radioDownloadAsk
            // 
            this.radioDownloadAsk.AutoSize = true;
            this.radioDownloadAsk.Location = new System.Drawing.Point(224, 44);
            this.radioDownloadAsk.Name = "radioDownloadAsk";
            this.radioDownloadAsk.Size = new System.Drawing.Size(43, 17);
            this.radioDownloadAsk.TabIndex = 3;
            this.radioDownloadAsk.TabStop = true;
            this.radioDownloadAsk.Text = "Ask";
            this.radioDownloadAsk.UseVisualStyleBackColor = true;
            this.radioDownloadAsk.CheckedChanged += new System.EventHandler(this.radioDownloadAsk_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Open file after download:";
            // 
            // Preferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(397, 228);
            this.Controls.Add(this.cmdApply);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.tabControl1);
            this.Icon = global::crf.Properties.Resources.CRFIcon;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Preferences";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox askToChangeDefaultPassCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox keepFileAssociationsCheckBox;
        private System.Windows.Forms.Button cmdAssociateCRF;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdApply;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label associationStatusLabel;
        private System.Windows.Forms.Label keyCountLabel;
        private System.Windows.Forms.Button cmdEraseKeys;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox programNameTextBox;
        private System.Windows.Forms.CheckBox removeOEMcheckBox;
        private System.Windows.Forms.TextBox keysTextBox;
        private System.Windows.Forms.CheckBox editKeysCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioDownloadAsk;
        private System.Windows.Forms.RadioButton radioDownloadNo;
        private System.Windows.Forms.RadioButton radioDownloadYes;

    }
}