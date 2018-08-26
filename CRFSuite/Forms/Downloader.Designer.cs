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

namespace crf
{
    partial class Downloader
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
            if (comm != null)
                comm.ClosePort();

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmdDownload = new System.Windows.Forms.Button();
            this.cmdErase = new System.Windows.Forms.Button();
            this.cmdPassword = new System.Windows.Forms.Button();
            this.cmdClock = new System.Windows.Forms.Button();
            this.cmdLogOut = new System.Windows.Forms.Button();
            this.cmdReboot = new System.Windows.Forms.Button();
            this.cmdDecode = new System.Windows.Forms.Button();
            this.cmdBluetooth = new System.Windows.Forms.Button();
            this.groupBoxStep2 = new System.Windows.Forms.GroupBox();
            this.cmdUpgrade = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBoxStatistics = new System.Windows.Forms.GroupBox();
            this.errorPictureBox = new System.Windows.Forms.PictureBox();
            this.warningPictureBox = new System.Windows.Forms.PictureBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.dataName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdOkPass = new System.Windows.Forms.Button();
            this.cmdCancelPass = new System.Windows.Forms.Button();
            this.checkBoxShowPass = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxPassword = new System.Windows.Forms.GroupBox();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxLedStartup = new System.Windows.Forms.CheckBox();
            this.checkBoxLedSwipe = new System.Windows.Forms.CheckBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.cmdSyncClock = new System.Windows.Forms.Button();
            this.cmdOkClock = new System.Windows.Forms.Button();
            this.cmdCancelClock = new System.Windows.Forms.Button();
            this.groupBoxStep1 = new System.Windows.Forms.GroupBox();
            this.cmdLogIn = new System.Windows.Forms.Button();
            this.radioScanUsb = new System.Windows.Forms.RadioButton();
            this.cboPort = new System.Windows.Forms.ComboBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.radioScanBluetooth = new System.Windows.Forms.RadioButton();
            this.cmdRescan = new System.Windows.Forms.Button();
            this.radioScanHID = new System.Windows.Forms.RadioButton();
            this.labelPassword = new System.Windows.Forms.Label();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.radioScanAll = new System.Windows.Forms.RadioButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recoveryOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.deviceManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.installToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.newModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oldModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.firmwareUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.firmwareUpdatefromInternetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportABugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.messagesLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxStep2.SuspendLayout();
            this.groupBoxStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.warningPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBoxPassword.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.groupBoxStep1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdDownload
            // 
            this.cmdDownload.Location = new System.Drawing.Point(5, 16);
            this.cmdDownload.Name = "cmdDownload";
            this.cmdDownload.Size = new System.Drawing.Size(67, 23);
            this.cmdDownload.TabIndex = 0;
            this.cmdDownload.Text = "&Download";
            this.cmdDownload.UseVisualStyleBackColor = true;
            this.cmdDownload.Click += new System.EventHandler(this.cmdDownload_Click);
            // 
            // cmdErase
            // 
            this.cmdErase.Location = new System.Drawing.Point(75, 16);
            this.cmdErase.Name = "cmdErase";
            this.cmdErase.Size = new System.Drawing.Size(67, 23);
            this.cmdErase.TabIndex = 1;
            this.cmdErase.Text = "&Erase";
            this.cmdErase.UseVisualStyleBackColor = true;
            this.cmdErase.Click += new System.EventHandler(this.cmdErase_Click);
            // 
            // cmdPassword
            // 
            this.cmdPassword.Location = new System.Drawing.Point(75, 42);
            this.cmdPassword.Name = "cmdPassword";
            this.cmdPassword.Size = new System.Drawing.Size(67, 23);
            this.cmdPassword.TabIndex = 4;
            this.cmdPassword.Text = "&Password";
            this.cmdPassword.UseVisualStyleBackColor = true;
            this.cmdPassword.Click += new System.EventHandler(this.cmdPassword_Click);
            // 
            // cmdClock
            // 
            this.cmdClock.Location = new System.Drawing.Point(5, 42);
            this.cmdClock.Name = "cmdClock";
            this.cmdClock.Size = new System.Drawing.Size(67, 23);
            this.cmdClock.TabIndex = 3;
            this.cmdClock.Text = "&Settings";
            this.cmdClock.UseVisualStyleBackColor = true;
            this.cmdClock.Click += new System.EventHandler(this.cmdSettings_Click);
            // 
            // cmdLogOut
            // 
            this.cmdLogOut.Location = new System.Drawing.Point(145, 68);
            this.cmdLogOut.Name = "cmdLogOut";
            this.cmdLogOut.Size = new System.Drawing.Size(67, 23);
            this.cmdLogOut.TabIndex = 8;
            this.cmdLogOut.Text = "&Log Out";
            this.cmdLogOut.UseVisualStyleBackColor = true;
            this.cmdLogOut.Click += new System.EventHandler(this.cmdLogOut_Click);
            // 
            // cmdReboot
            // 
            this.cmdReboot.Location = new System.Drawing.Point(5, 68);
            this.cmdReboot.Name = "cmdReboot";
            this.cmdReboot.Size = new System.Drawing.Size(67, 23);
            this.cmdReboot.TabIndex = 6;
            this.cmdReboot.Text = "Re&boot";
            this.cmdReboot.UseVisualStyleBackColor = true;
            this.cmdReboot.Click += new System.EventHandler(this.cmdReboot_Click);
            // 
            // cmdDecode
            // 
            this.cmdDecode.Location = new System.Drawing.Point(145, 16);
            this.cmdDecode.Name = "cmdDecode";
            this.cmdDecode.Size = new System.Drawing.Size(67, 23);
            this.cmdDecode.TabIndex = 2;
            this.cmdDecode.Text = "Dec&ode";
            this.cmdDecode.UseVisualStyleBackColor = true;
            this.cmdDecode.Click += new System.EventHandler(this.cmdDecode_Click);
            // 
            // cmdBluetooth
            // 
            this.cmdBluetooth.Location = new System.Drawing.Point(145, 42);
            this.cmdBluetooth.Name = "cmdBluetooth";
            this.cmdBluetooth.Size = new System.Drawing.Size(67, 23);
            this.cmdBluetooth.TabIndex = 5;
            this.cmdBluetooth.Text = "&Bluetooth";
            this.cmdBluetooth.UseVisualStyleBackColor = true;
            this.cmdBluetooth.Click += new System.EventHandler(this.cmdBluetooth_Click);
            // 
            // groupBoxStep2
            // 
            this.groupBoxStep2.Controls.Add(this.cmdUpgrade);
            this.groupBoxStep2.Controls.Add(this.progressBar);
            this.groupBoxStep2.Controls.Add(this.cmdBluetooth);
            this.groupBoxStep2.Controls.Add(this.cmdDecode);
            this.groupBoxStep2.Controls.Add(this.cmdReboot);
            this.groupBoxStep2.Controls.Add(this.cmdLogOut);
            this.groupBoxStep2.Controls.Add(this.cmdClock);
            this.groupBoxStep2.Controls.Add(this.cmdPassword);
            this.groupBoxStep2.Controls.Add(this.cmdErase);
            this.groupBoxStep2.Controls.Add(this.cmdDownload);
            this.groupBoxStep2.Enabled = false;
            this.groupBoxStep2.Location = new System.Drawing.Point(227, 146);
            this.groupBoxStep2.Name = "groupBoxStep2";
            this.groupBoxStep2.Size = new System.Drawing.Size(218, 96);
            this.groupBoxStep2.TabIndex = 2;
            this.groupBoxStep2.TabStop = false;
            this.groupBoxStep2.Text = "Step2: Command the device";
            this.groupBoxStep2.Visible = false;
            // 
            // cmdUpgrade
            // 
            this.cmdUpgrade.Location = new System.Drawing.Point(75, 68);
            this.cmdUpgrade.Margin = new System.Windows.Forms.Padding(2);
            this.cmdUpgrade.Name = "cmdUpgrade";
            this.cmdUpgrade.Size = new System.Drawing.Size(67, 23);
            this.cmdUpgrade.TabIndex = 7;
            this.cmdUpgrade.Text = "&Upgrade";
            this.cmdUpgrade.UseVisualStyleBackColor = true;
            this.cmdUpgrade.Click += new System.EventHandler(this.cmdUpgrade_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(204, 56);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(8, 23);
            this.progressBar.TabIndex = 9;
            this.progressBar.Visible = false;
            // 
            // groupBoxStatistics
            // 
            this.groupBoxStatistics.Controls.Add(this.errorPictureBox);
            this.groupBoxStatistics.Controls.Add(this.warningPictureBox);
            this.groupBoxStatistics.Controls.Add(this.dataGridView);
            this.groupBoxStatistics.Location = new System.Drawing.Point(227, 27);
            this.groupBoxStatistics.Name = "groupBoxStatistics";
            this.groupBoxStatistics.Size = new System.Drawing.Size(218, 115);
            this.groupBoxStatistics.TabIndex = 5;
            this.groupBoxStatistics.TabStop = false;
            this.groupBoxStatistics.Text = "Device Info";
            this.groupBoxStatistics.Visible = false;
            // 
            // errorPictureBox
            // 
            this.errorPictureBox.Location = new System.Drawing.Point(194, 36);
            this.errorPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.errorPictureBox.Name = "errorPictureBox";
            this.errorPictureBox.Size = new System.Drawing.Size(19, 20);
            this.errorPictureBox.TabIndex = 2;
            this.errorPictureBox.TabStop = false;
            this.errorPictureBox.Visible = false;
            // 
            // warningPictureBox
            // 
            this.warningPictureBox.Location = new System.Drawing.Point(194, 11);
            this.warningPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.warningPictureBox.Name = "warningPictureBox";
            this.warningPictureBox.Size = new System.Drawing.Size(19, 20);
            this.warningPictureBox.TabIndex = 1;
            this.warningPictureBox.TabStop = false;
            this.warningPictureBox.Visible = false;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.CausesValidation = false;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.ColumnHeadersVisible = false;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataName,
            this.dataValue});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.Location = new System.Drawing.Point(6, 11);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 15;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView.ShowCellErrors = false;
            this.dataGridView.ShowEditingIcon = false;
            this.dataGridView.ShowRowErrors = false;
            this.dataGridView.Size = new System.Drawing.Size(206, 99);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.TabStop = false;
            this.dataGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_click);
            // 
            // dataName
            // 
            this.dataName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataName.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataName.FillWeight = 30F;
            this.dataName.HeaderText = "Name";
            this.dataName.MaxInputLength = 31;
            this.dataName.MinimumWidth = 11;
            this.dataName.Name = "dataName";
            this.dataName.ReadOnly = true;
            this.dataName.Width = 11;
            // 
            // dataValue
            // 
            this.dataValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            this.dataValue.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataValue.HeaderText = "Value";
            this.dataValue.MaxInputLength = 254;
            this.dataValue.Name = "dataValue";
            this.dataValue.ReadOnly = true;
            // 
            // cmdOkPass
            // 
            this.cmdOkPass.Location = new System.Drawing.Point(94, 65);
            this.cmdOkPass.Name = "cmdOkPass";
            this.cmdOkPass.Size = new System.Drawing.Size(55, 23);
            this.cmdOkPass.TabIndex = 2;
            this.cmdOkPass.Text = "&Ok";
            this.cmdOkPass.UseVisualStyleBackColor = true;
            this.cmdOkPass.Click += new System.EventHandler(this.cmdOkPass_Click);
            // 
            // cmdCancelPass
            // 
            this.cmdCancelPass.Location = new System.Drawing.Point(157, 65);
            this.cmdCancelPass.Name = "cmdCancelPass";
            this.cmdCancelPass.Size = new System.Drawing.Size(55, 23);
            this.cmdCancelPass.TabIndex = 3;
            this.cmdCancelPass.Text = "&Cancel";
            this.cmdCancelPass.UseVisualStyleBackColor = true;
            this.cmdCancelPass.Click += new System.EventHandler(this.cmdCancelPass_Click);
            // 
            // checkBoxShowPass
            // 
            this.checkBoxShowPass.AutoSize = true;
            this.checkBoxShowPass.Location = new System.Drawing.Point(8, 71);
            this.checkBoxShowPass.Name = "checkBoxShowPass";
            this.checkBoxShowPass.Size = new System.Drawing.Size(78, 17);
            this.checkBoxShowPass.TabIndex = 6;
            this.checkBoxShowPass.Text = "&Show pass";
            this.checkBoxShowPass.UseVisualStyleBackColor = true;
            this.checkBoxShowPass.CheckedChanged += new System.EventHandler(this.checkBoxShowPass_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(94, 14);
            this.textBox1.MaxLength = 32;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(118, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.UseSystemPasswordChar = true;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.textBox1.Validating += new System.ComponentModel.CancelEventHandler(this.validatingTextBox);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(94, 41);
            this.textBox2.MaxLength = 32;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(118, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.UseSystemPasswordChar = true;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.textBox2.Validating += new System.ComponentModel.CancelEventHandler(this.validatingTextBox);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "&Enter password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "&Verify password:";
            // 
            // groupBoxPassword
            // 
            this.groupBoxPassword.Controls.Add(this.label2);
            this.groupBoxPassword.Controls.Add(this.label1);
            this.groupBoxPassword.Controls.Add(this.textBox2);
            this.groupBoxPassword.Controls.Add(this.textBox1);
            this.groupBoxPassword.Controls.Add(this.checkBoxShowPass);
            this.groupBoxPassword.Controls.Add(this.cmdCancelPass);
            this.groupBoxPassword.Controls.Add(this.cmdOkPass);
            this.groupBoxPassword.Location = new System.Drawing.Point(675, 148);
            this.groupBoxPassword.Name = "groupBoxPassword";
            this.groupBoxPassword.Size = new System.Drawing.Size(218, 96);
            this.groupBoxPassword.TabIndex = 4;
            this.groupBoxPassword.TabStop = false;
            this.groupBoxPassword.Text = "Change password";
            this.groupBoxPassword.Visible = false;
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.label4);
            this.groupBoxSettings.Controls.Add(this.label3);
            this.groupBoxSettings.Controls.Add(this.checkBoxLedStartup);
            this.groupBoxSettings.Controls.Add(this.checkBoxLedSwipe);
            this.groupBoxSettings.Controls.Add(this.dateTimePicker);
            this.groupBoxSettings.Controls.Add(this.cmdSyncClock);
            this.groupBoxSettings.Controls.Add(this.cmdOkClock);
            this.groupBoxSettings.Controls.Add(this.cmdCancelClock);
            this.groupBoxSettings.Location = new System.Drawing.Point(451, 148);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(218, 96);
            this.groupBoxSettings.TabIndex = 3;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            this.groupBoxSettings.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Enable Leds:";
            // 
            // checkBoxLedStartup
            // 
            this.checkBoxLedStartup.AutoSize = true;
            this.checkBoxLedStartup.Location = new System.Drawing.Point(82, 18);
            this.checkBoxLedStartup.Name = "checkBoxLedStartup";
            this.checkBoxLedStartup.Size = new System.Drawing.Size(60, 17);
            this.checkBoxLedStartup.TabIndex = 12;
            this.checkBoxLedStartup.Text = "Startup";
            this.checkBoxLedStartup.UseVisualStyleBackColor = true;
            // 
            // checkBoxLedSwipe
            // 
            this.checkBoxLedSwipe.AutoSize = true;
            this.checkBoxLedSwipe.Location = new System.Drawing.Point(157, 18);
            this.checkBoxLedSwipe.Name = "checkBoxLedSwipe";
            this.checkBoxLedSwipe.Size = new System.Drawing.Size(55, 17);
            this.checkBoxLedSwipe.TabIndex = 11;
            this.checkBoxLedSwipe.Text = "Swipe";
            this.checkBoxLedSwipe.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker.Location = new System.Drawing.Point(45, 41);
            this.dateTimePicker.MaxDate = new System.DateTime(2038, 1, 18, 0, 0, 0, 0);
            this.dateTimePicker.MinDate = new System.DateTime(1969, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(167, 20);
            this.dateTimePicker.TabIndex = 10;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.event_dateTimeChanged);
            // 
            // cmdSyncClock
            // 
            this.cmdSyncClock.Location = new System.Drawing.Point(9, 67);
            this.cmdSyncClock.Name = "cmdSyncClock";
            this.cmdSyncClock.Size = new System.Drawing.Size(55, 23);
            this.cmdSyncClock.TabIndex = 0;
            this.cmdSyncClock.Text = "&Sync";
            this.cmdSyncClock.UseVisualStyleBackColor = true;
            this.cmdSyncClock.Click += new System.EventHandler(this.cmdSyncClock_Click);
            // 
            // cmdOkClock
            // 
            this.cmdOkClock.Location = new System.Drawing.Point(82, 67);
            this.cmdOkClock.Name = "cmdOkClock";
            this.cmdOkClock.Size = new System.Drawing.Size(55, 23);
            this.cmdOkClock.TabIndex = 7;
            this.cmdOkClock.Text = "&Ok";
            this.cmdOkClock.UseVisualStyleBackColor = true;
            this.cmdOkClock.Click += new System.EventHandler(this.cmdOkClock_Click);
            // 
            // cmdCancelClock
            // 
            this.cmdCancelClock.Location = new System.Drawing.Point(157, 66);
            this.cmdCancelClock.Name = "cmdCancelClock";
            this.cmdCancelClock.Size = new System.Drawing.Size(55, 23);
            this.cmdCancelClock.TabIndex = 8;
            this.cmdCancelClock.Text = "&Cancel";
            this.cmdCancelClock.UseVisualStyleBackColor = true;
            this.cmdCancelClock.Click += new System.EventHandler(this.cmdCancelClock_Click);
            // 
            // groupBoxStep1
            // 
            this.groupBoxStep1.Controls.Add(this.cmdLogIn);
            this.groupBoxStep1.Controls.Add(this.radioScanUsb);
            this.groupBoxStep1.Controls.Add(this.cboPort);
            this.groupBoxStep1.Controls.Add(this.txtPassword);
            this.groupBoxStep1.Controls.Add(this.radioScanBluetooth);
            this.groupBoxStep1.Controls.Add(this.cmdRescan);
            this.groupBoxStep1.Controls.Add(this.radioScanHID);
            this.groupBoxStep1.Controls.Add(this.labelPassword);
            this.groupBoxStep1.Controls.Add(this.cmdConnect);
            this.groupBoxStep1.Controls.Add(this.radioScanAll);
            this.groupBoxStep1.Location = new System.Drawing.Point(3, 26);
            this.groupBoxStep1.Name = "groupBoxStep1";
            this.groupBoxStep1.Size = new System.Drawing.Size(218, 216);
            this.groupBoxStep1.TabIndex = 0;
            this.groupBoxStep1.TabStop = false;
            this.groupBoxStep1.Text = "Step1: Connect";
            // 
            // cmdLogIn
            // 
            this.cmdLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLogIn.Location = new System.Drawing.Point(145, 187);
            this.cmdLogIn.Name = "cmdLogIn";
            this.cmdLogIn.Size = new System.Drawing.Size(67, 23);
            this.cmdLogIn.TabIndex = 8;
            this.cmdLogIn.Text = "&Log In";
            this.cmdLogIn.UseVisualStyleBackColor = true;
            this.cmdLogIn.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // radioScanUsb
            // 
            this.radioScanUsb.AutoSize = true;
            this.radioScanUsb.Location = new System.Drawing.Point(6, 19);
            this.radioScanUsb.Name = "radioScanUsb";
            this.radioScanUsb.Size = new System.Drawing.Size(47, 17);
            this.radioScanUsb.TabIndex = 0;
            this.radioScanUsb.Text = "USB";
            this.radioScanUsb.UseVisualStyleBackColor = true;
            this.radioScanUsb.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // cboPort
            // 
            this.cboPort.DropDownWidth = 200;
            this.cboPort.FormattingEnabled = true;
            this.cboPort.Location = new System.Drawing.Point(6, 107);
            this.cboPort.Name = "cboPort";
            this.cboPort.Size = new System.Drawing.Size(206, 21);
            this.cboPort.TabIndex = 4;
            this.cboPort.SelectedIndexChanged += new System.EventHandler(this.portChanged);
            this.cboPort.SelectedValueChanged += new System.EventHandler(this.portChanged);
            this.cboPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboPort_keyPress);
            // 
            // txtPassword
            // 
            this.txtPassword.AcceptsReturn = true;
            this.txtPassword.Location = new System.Drawing.Point(73, 162);
            this.txtPassword.MaxLength = 32;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(140, 20);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_keyPress);
            // 
            // radioScanBluetooth
            // 
            this.radioScanBluetooth.AutoSize = true;
            this.radioScanBluetooth.Location = new System.Drawing.Point(6, 40);
            this.radioScanBluetooth.Name = "radioScanBluetooth";
            this.radioScanBluetooth.Size = new System.Drawing.Size(70, 17);
            this.radioScanBluetooth.TabIndex = 1;
            this.radioScanBluetooth.Text = "Bluetooth";
            this.radioScanBluetooth.UseVisualStyleBackColor = true;
            this.radioScanBluetooth.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // cmdRescan
            // 
            this.cmdRescan.Location = new System.Drawing.Point(145, 133);
            this.cmdRescan.Name = "cmdRescan";
            this.cmdRescan.Size = new System.Drawing.Size(67, 23);
            this.cmdRescan.TabIndex = 6;
            this.cmdRescan.Text = "&Rescan";
            this.cmdRescan.UseVisualStyleBackColor = true;
            this.cmdRescan.Click += new System.EventHandler(this.cmdRescan_Click);
            // 
            // radioScanHID
            // 
            this.radioScanHID.AutoSize = true;
            this.radioScanHID.Location = new System.Drawing.Point(6, 61);
            this.radioScanHID.Name = "radioScanHID";
            this.radioScanHID.Size = new System.Drawing.Size(62, 17);
            this.radioScanHID.TabIndex = 2;
            this.radioScanHID.Text = "All ports";
            this.radioScanHID.UseVisualStyleBackColor = true;
            this.radioScanHID.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(7, 163);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(56, 13);
            this.labelPassword.TabIndex = 9;
            this.labelPassword.Text = "Password:";
            // 
            // cmdConnect
            // 
            this.cmdConnect.Enabled = false;
            this.cmdConnect.Location = new System.Drawing.Point(72, 133);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(67, 23);
            this.cmdConnect.TabIndex = 5;
            this.cmdConnect.Text = "&Connect";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_click);
            // 
            // radioScanAll
            // 
            this.radioScanAll.AutoSize = true;
            this.radioScanAll.Location = new System.Drawing.Point(6, 82);
            this.radioScanAll.Name = "radioScanAll";
            this.radioScanAll.Size = new System.Drawing.Size(171, 17);
            this.radioScanAll.TabIndex = 3;
            this.radioScanAll.Text = "All ports (if everything else fails)";
            this.radioScanAll.UseVisualStyleBackColor = true;
            this.radioScanAll.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.OverwritePrompt = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.deviceToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(224, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.recoveryOpenToolStripMenuItem,
            this.toolStripSeparator5,
            this.deviceManagerToolStripMenuItem,
            this.installToToolStripMenuItem,
            this.toolStripSeparator3,
            this.propertiesToolStripMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.openToolStripMenuItem.Text = "&Open ...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.cmdDecode_Click);
            // 
            // recoveryOpenToolStripMenuItem
            // 
            this.recoveryOpenToolStripMenuItem.Name = "recoveryOpenToolStripMenuItem";
            this.recoveryOpenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.recoveryOpenToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.recoveryOpenToolStripMenuItem.Text = "Open (recovery mode)...";
            this.recoveryOpenToolStripMenuItem.Click += new System.EventHandler(this.cmdDecode_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(274, 6);
            // 
            // deviceManagerToolStripMenuItem
            // 
            this.deviceManagerToolStripMenuItem.Name = "deviceManagerToolStripMenuItem";
            this.deviceManagerToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.deviceManagerToolStripMenuItem.Text = "&Device Manager";
            this.deviceManagerToolStripMenuItem.Click += new System.EventHandler(this.deviceManagerToolStripMenuItem_Click);
            // 
            // installToToolStripMenuItem
            // 
            this.installToToolStripMenuItem.Name = "installToToolStripMenuItem";
            this.installToToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.installToToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.installToToolStripMenuItem.Text = "&Install ... to ...";
            this.installToToolStripMenuItem.Click += new System.EventHandler(this.installToToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(274, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.propertiesToolStripMenuItem.Text = "&Preferences ...";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(274, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // deviceToolStripMenuItem
            // 
            this.deviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshInfoToolStripMenuItem,
            this.toolStripSeparator6,
            this.newModeToolStripMenuItem,
            this.oldModeToolStripMenuItem,
            this.toolStripSeparator7,
            this.firmwareUpdateToolStripMenuItem,
            this.firmwareUpdatefromInternetToolStripMenuItem});
            this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
            this.deviceToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.deviceToolStripMenuItem.Text = "De&vice";
            // 
            // refreshInfoToolStripMenuItem
            // 
            this.refreshInfoToolStripMenuItem.Name = "refreshInfoToolStripMenuItem";
            this.refreshInfoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.refreshInfoToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.refreshInfoToolStripMenuItem.Text = "&Refresh info";
            this.refreshInfoToolStripMenuItem.Click += new System.EventHandler(this.refreshStats_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(268, 6);
            // 
            // newModeToolStripMenuItem
            // 
            this.newModeToolStripMenuItem.Name = "newModeToolStripMenuItem";
            this.newModeToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.newModeToolStripMenuItem.Text = "&New mode";
            this.newModeToolStripMenuItem.Click += new System.EventHandler(this.cmdReadMode_Click);
            // 
            // oldModeToolStripMenuItem
            // 
            this.oldModeToolStripMenuItem.Name = "oldModeToolStripMenuItem";
            this.oldModeToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.oldModeToolStripMenuItem.Text = "&Old mode";
            this.oldModeToolStripMenuItem.Click += new System.EventHandler(this.cmdReadMode_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(268, 6);
            // 
            // firmwareUpdateToolStripMenuItem
            // 
            this.firmwareUpdateToolStripMenuItem.Name = "firmwareUpdateToolStripMenuItem";
            this.firmwareUpdateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.firmwareUpdateToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.firmwareUpdateToolStripMenuItem.Text = "Firmware update (from &file) ...";
            this.firmwareUpdateToolStripMenuItem.Click += new System.EventHandler(this.firmwareUpdateToolStripMenuItem_Click);
            // 
            // firmwareUpdatefromInternetToolStripMenuItem
            // 
            this.firmwareUpdatefromInternetToolStripMenuItem.Name = "firmwareUpdatefromInternetToolStripMenuItem";
            this.firmwareUpdatefromInternetToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.firmwareUpdatefromInternetToolStripMenuItem.Text = "Firmware update (from &internet)";
            this.firmwareUpdatefromInternetToolStripMenuItem.Click += new System.EventHandler(this.firmwareUpdatefromInternetToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem,
            this.reportABugToolStripMenuItem,
            this.toolStripSeparator1,
            this.checkForUpdatesToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.viewHelpToolStripMenuItem.Text = "&View Help";
            this.viewHelpToolStripMenuItem.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
            // 
            // reportABugToolStripMenuItem
            // 
            this.reportABugToolStripMenuItem.Name = "reportABugToolStripMenuItem";
            this.reportABugToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.reportABugToolStripMenuItem.Text = "&Report a Bug";
            this.reportABugToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(167, 6);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "&Check for updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(167, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.messagesLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 245);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(224, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // messagesLabel
            // 
            this.messagesLabel.Name = "messagesLabel";
            this.messagesLabel.Size = new System.Drawing.Size(86, 17);
            this.messagesLabel.Text = "messagesLabel";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(3, 248);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 15);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn1.FillWeight = 30F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 31;
            this.dataGridViewTextBoxColumn1.MinimumWidth = 11;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 254;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // Downloader
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(224, 267);
            this.Controls.Add(this.groupBoxStep1);
            this.Controls.Add(this.groupBoxStep2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBoxStatistics);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.groupBoxPassword);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::crf.Properties.Resources.CRFIcon;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Downloader";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.downloader_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            this.groupBoxStep2.ResumeLayout(false);
            this.groupBoxStatistics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.warningPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBoxPassword.ResumeLayout(false);
            this.groupBoxPassword.PerformLayout();
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.groupBoxStep1.ResumeLayout(false);
            this.groupBoxStep1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdDownload;
        private System.Windows.Forms.Button cmdErase;
        private System.Windows.Forms.Button cmdPassword;
        private System.Windows.Forms.Button cmdClock;
        private System.Windows.Forms.Button cmdLogOut;
        private System.Windows.Forms.Button cmdReboot;
        private System.Windows.Forms.Button cmdDecode;
        private System.Windows.Forms.Button cmdBluetooth;
        private System.Windows.Forms.GroupBox groupBoxStep2;
        private System.Windows.Forms.GroupBox groupBoxStatistics;
        private System.Windows.Forms.Button cmdOkPass;
        private System.Windows.Forms.Button cmdCancelPass;
        private System.Windows.Forms.CheckBox checkBoxShowPass;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxPassword;
        private System.Windows.Forms.Button cmdCancelClock;
        private System.Windows.Forms.Button cmdOkClock;
        private System.Windows.Forms.Button cmdSyncClock;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataValue;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox groupBoxStep1;
        private System.Windows.Forms.Button cmdLogIn;
        private System.Windows.Forms.RadioButton radioScanUsb;
        private System.Windows.Forms.ComboBox cboPort;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.RadioButton radioScanBluetooth;
        private System.Windows.Forms.Button cmdRescan;
        private System.Windows.Forms.RadioButton radioScanHID;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.RadioButton radioScanAll;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem deviceManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem newModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oldModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel messagesLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox warningPictureBox;
        private System.Windows.Forms.PictureBox errorPictureBox;
        private System.Windows.Forms.Button cmdUpgrade;
        private System.Windows.Forms.ToolStripMenuItem firmwareUpdatefromInternetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem firmwareUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportABugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem installToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxLedStartup;
        private System.Windows.Forms.CheckBox checkBoxLedSwipe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem recoveryOpenToolStripMenuItem;

    }
}
