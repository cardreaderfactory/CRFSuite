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

namespace CRFSuite
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.groupBoxStep1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdLogIn = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.cmdRescan = new System.Windows.Forms.Button();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.cboPort = new System.Windows.Forms.ComboBox();
            this.radioScanAll = new System.Windows.Forms.RadioButton();
            this.radioScanHID = new System.Windows.Forms.RadioButton();
            this.radioScanBluetooth = new System.Windows.Forms.RadioButton();
            this.radioScanUsb = new System.Windows.Forms.RadioButton();
            this.rtbDisplay = new CRFSuite.AutoScrollText();
            this.groupBoxStatistics = new System.Windows.Forms.Panel();
            this.deviceInfoDataGrid1 = new CRFSuite.DeviceInfoDataGrid();
            this.dataGrid = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.groupBoxStep2 = new System.Windows.Forms.Panel();
            this.lblStep2 = new System.Windows.Forms.Label();
            this.cmdDecode = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.cmdLogOut = new System.Windows.Forms.Button();
            this.cmdReboot = new System.Windows.Forms.Button();
            this.cmdBluetooth = new System.Windows.Forms.Button();
            this.cmdPassword = new System.Windows.Forms.Button();
            this.cmdClock = new System.Windows.Forms.Button();
            this.cmdErase = new System.Windows.Forms.Button();
            this.cmdDownload = new System.Windows.Forms.Button();
            this.groupBoxClock = new System.Windows.Forms.Panel();
            this.lblClock = new System.Windows.Forms.Label();
            this.textBoxSecond = new System.Windows.Forms.TextBox();
            this.textBoxYear = new System.Windows.Forms.TextBox();
            this.textBoxMinute = new System.Windows.Forms.TextBox();
            this.textBoxMonth = new System.Windows.Forms.TextBox();
            this.textBoxHour = new System.Windows.Forms.TextBox();
            this.textBoxDay = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.cmdOkClock = new System.Windows.Forms.Button();
            this.cmdSyncClock = new System.Windows.Forms.Button();
            this.groupBoxPassword = new System.Windows.Forms.Panel();
            this.lblPassGroup = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBoxShowPass = new System.Windows.Forms.CheckBox();
            this.cmdCancelPass = new System.Windows.Forms.Button();
            this.cmdOkPass = new System.Windows.Forms.Button();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuOptions = new System.Windows.Forms.MenuItem();
            this.menuFullDownloadErase = new System.Windows.Forms.MenuItem();
            this.menuUpgrade = new System.Windows.Forms.MenuItem();
            this.menuClearOutput = new System.Windows.Forms.MenuItem();
            this.menuSeparetor1 = new System.Windows.Forms.MenuItem();
            this.menuAbout = new System.Windows.Forms.MenuItem();
            this.menuSeparetor2 = new System.Windows.Forms.MenuItem();
            this.menuExit = new System.Windows.Forms.MenuItem();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
            this.groupBoxStep1.SuspendLayout();
            this.groupBoxStatistics.SuspendLayout();
            this.groupBoxStep2.SuspendLayout();
            this.groupBoxClock.SuspendLayout();
            this.groupBoxPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxStep1
            // 
            this.groupBoxStep1.Controls.Add(this.label5);
            this.groupBoxStep1.Controls.Add(this.cmdLogIn);
            this.groupBoxStep1.Controls.Add(this.txtPassword);
            this.groupBoxStep1.Controls.Add(this.labelPassword);
            this.groupBoxStep1.Controls.Add(this.cmdRescan);
            this.groupBoxStep1.Controls.Add(this.cmdConnect);
            this.groupBoxStep1.Controls.Add(this.cboPort);
            this.groupBoxStep1.Controls.Add(this.radioScanAll);
            this.groupBoxStep1.Controls.Add(this.radioScanHID);
            this.groupBoxStep1.Controls.Add(this.radioScanBluetooth);
            this.groupBoxStep1.Controls.Add(this.radioScanUsb);
            this.groupBoxStep1.Location = new System.Drawing.Point(3, 3);
            this.groupBoxStep1.Name = "groupBoxStep1";
            this.groupBoxStep1.Size = new System.Drawing.Size(234, 190);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(0, -3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(187, 16);
            this.label5.Text = "Step1: Connect";
            // 
            // cmdLogIn
            // 
            this.cmdLogIn.Location = new System.Drawing.Point(163, 165);
            this.cmdLogIn.Name = "cmdLogIn";
            this.cmdLogIn.Size = new System.Drawing.Size(70, 23);
            this.cmdLogIn.TabIndex = 8;
            this.cmdLogIn.Text = "&Log In";
            this.cmdLogIn.Click += new System.EventHandler(this.cmdLogIn_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(67, 138);
            this.txtPassword.MaxLength = 32;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(166, 21);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.GotFocus += new System.EventHandler(this.txtBox_GotFocus);
            this.txtPassword.LostFocus += new System.EventHandler(this.txtBox_LostFocus);
            // 
            // labelPassword
            // 
            this.labelPassword.Location = new System.Drawing.Point(0, 139);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(67, 20);
            this.labelPassword.Text = "Password:";
            // 
            // cmdRescan
            // 
            this.cmdRescan.Location = new System.Drawing.Point(163, 69);
            this.cmdRescan.Name = "cmdRescan";
            this.cmdRescan.Size = new System.Drawing.Size(70, 23);
            this.cmdRescan.TabIndex = 6;
            this.cmdRescan.Text = "&Rescan";
            this.cmdRescan.Click += new System.EventHandler(this.cmdRescan_Click);
            // 
            // cmdConnect
            // 
            this.cmdConnect.Location = new System.Drawing.Point(88, 69);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(70, 23);
            this.cmdConnect.TabIndex = 5;
            this.cmdConnect.Text = "&Connect";
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // cboPort
            // 
            this.cboPort.Location = new System.Drawing.Point(0, 41);
            this.cboPort.Name = "cboPort";
            this.cboPort.Size = new System.Drawing.Size(233, 22);
            this.cboPort.TabIndex = 4;
            // 
            // radioScanAll
            // 
            this.radioScanAll.Location = new System.Drawing.Point(173, 17);
            this.radioScanAll.Name = "radioScanAll";
            this.radioScanAll.Size = new System.Drawing.Size(58, 20);
            this.radioScanAll.TabIndex = 3;
            this.radioScanAll.Text = "All(2)";
            this.radioScanAll.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // radioScanHID
            // 
            this.radioScanHID.Location = new System.Drawing.Point(131, 17);
            this.radioScanHID.Name = "radioScanHID";
            this.radioScanHID.Size = new System.Drawing.Size(43, 20);
            this.radioScanHID.TabIndex = 2;
            this.radioScanHID.Text = "All";
            this.radioScanHID.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // radioScanBluetooth
            // 
            this.radioScanBluetooth.Location = new System.Drawing.Point(51, 17);
            this.radioScanBluetooth.Name = "radioScanBluetooth";
            this.radioScanBluetooth.Size = new System.Drawing.Size(80, 20);
            this.radioScanBluetooth.TabIndex = 1;
            this.radioScanBluetooth.Text = "Bluetooth";
            this.radioScanBluetooth.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // radioScanUsb
            // 
            this.radioScanUsb.Location = new System.Drawing.Point(3, 17);
            this.radioScanUsb.Name = "radioScanUsb";
            this.radioScanUsb.Size = new System.Drawing.Size(50, 20);
            this.radioScanUsb.TabIndex = 0;
            this.radioScanUsb.Text = "USB";
            this.radioScanUsb.CheckedChanged += new System.EventHandler(this.radioScanUsb_CheckedChanged);
            // 
            // rtbDisplay
            // 
            this.rtbDisplay.Location = new System.Drawing.Point(3, 197);
            this.rtbDisplay.Multiline = true;
            this.rtbDisplay.Name = "rtbDisplay";
            this.rtbDisplay.ReadOnly = true;
            this.rtbDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.rtbDisplay.Size = new System.Drawing.Size(234, 69);
            this.rtbDisplay.TabIndex = 1;
            // 
            // groupBoxStatistics
            // 
            this.groupBoxStatistics.Controls.Add(this.deviceInfoDataGrid1);
            this.groupBoxStatistics.Location = new System.Drawing.Point(243, 3);
            this.groupBoxStatistics.Name = "groupBoxStatistics";
            this.groupBoxStatistics.Size = new System.Drawing.Size(234, 92);
            this.groupBoxStatistics.Visible = false;
            // 
            // deviceInfoDataGrid1
            // 
            this.deviceInfoDataGrid1.BackColor = System.Drawing.SystemColors.Window;
            this.deviceInfoDataGrid1.Location = new System.Drawing.Point(0, 0);
            this.deviceInfoDataGrid1.Name = "deviceInfoDataGrid1";
            this.deviceInfoDataGrid1.Size = new System.Drawing.Size(233, 92);
            this.deviceInfoDataGrid1.TabIndex = 0;
            this.deviceInfoDataGrid1.Click += new System.EventHandler(this.dataGrid_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGrid.ColumnHeadersVisible = false;
            this.dataGrid.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.dataGrid.GridLineColor = System.Drawing.Color.Black;
            this.dataGrid.HeaderBackColor = System.Drawing.SystemColors.WindowText;
            this.dataGrid.HeaderForeColor = System.Drawing.SystemColors.WindowText;
            this.dataGrid.Location = new System.Drawing.Point(483, 3);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.SelectionBackColor = System.Drawing.SystemColors.Window;
            this.dataGrid.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            this.dataGrid.Size = new System.Drawing.Size(230, 85);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.TableStyles.Add(this.dataGridTableStyle1);
            this.dataGrid.Visible = false;
            this.dataGrid.Click += new System.EventHandler(this.dataGrid_Click);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.MappingName = "Stats";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.MappingName = "Name";
            this.dataGridTextBoxColumn1.NullText = "\"\"";
            this.dataGridTextBoxColumn1.Width = 80;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.MappingName = "Value";
            this.dataGridTextBoxColumn2.NullText = "\"\"";
            this.dataGridTextBoxColumn2.Width = 145;
            // 
            // groupBoxStep2
            // 
            this.groupBoxStep2.Controls.Add(this.lblStep2);
            this.groupBoxStep2.Controls.Add(this.cmdDecode);
            this.groupBoxStep2.Controls.Add(this.progressBar);
            this.groupBoxStep2.Controls.Add(this.cmdLogOut);
            this.groupBoxStep2.Controls.Add(this.cmdReboot);
            this.groupBoxStep2.Controls.Add(this.cmdBluetooth);
            this.groupBoxStep2.Controls.Add(this.cmdPassword);
            this.groupBoxStep2.Controls.Add(this.cmdClock);
            this.groupBoxStep2.Controls.Add(this.cmdErase);
            this.groupBoxStep2.Controls.Add(this.cmdDownload);
            this.groupBoxStep2.Location = new System.Drawing.Point(243, 96);
            this.groupBoxStep2.Name = "groupBoxStep2";
            this.groupBoxStep2.Size = new System.Drawing.Size(234, 97);
            this.groupBoxStep2.Visible = false;
            // 
            // lblStep2
            // 
            this.lblStep2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblStep2.Location = new System.Drawing.Point(0, 0);
            this.lblStep2.Name = "lblStep2";
            this.lblStep2.Size = new System.Drawing.Size(187, 18);
            this.lblStep2.Text = "Step2: Command the device";
            // 
            // cmdDecode
            // 
            this.cmdDecode.Location = new System.Drawing.Point(163, 20);
            this.cmdDecode.Name = "cmdDecode";
            this.cmdDecode.Size = new System.Drawing.Size(70, 23);
            this.cmdDecode.TabIndex = 2;
            this.cmdDecode.Text = "Dec&ode";
            this.cmdDecode.Click += new System.EventHandler(this.cmdDecode_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(82, 72);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(70, 23);
            this.progressBar.Visible = false;
            // 
            // cmdLogOut
            // 
            this.cmdLogOut.Location = new System.Drawing.Point(163, 72);
            this.cmdLogOut.Name = "cmdLogOut";
            this.cmdLogOut.Size = new System.Drawing.Size(70, 23);
            this.cmdLogOut.TabIndex = 7;
            this.cmdLogOut.Text = "&Log out";
            this.cmdLogOut.Click += new System.EventHandler(this.cmdLogOut_Click);
            // 
            // cmdReboot
            // 
            this.cmdReboot.Location = new System.Drawing.Point(1, 72);
            this.cmdReboot.Name = "cmdReboot";
            this.cmdReboot.Size = new System.Drawing.Size(70, 23);
            this.cmdReboot.TabIndex = 6;
            this.cmdReboot.Text = "Re&boot";
            this.cmdReboot.Click += new System.EventHandler(this.cmdReboot_Click);
            // 
            // cmdBluetooth
            // 
            this.cmdBluetooth.Location = new System.Drawing.Point(163, 46);
            this.cmdBluetooth.Name = "cmdBluetooth";
            this.cmdBluetooth.Size = new System.Drawing.Size(70, 23);
            this.cmdBluetooth.TabIndex = 5;
            this.cmdBluetooth.Text = "&Bluetooth";
            this.cmdBluetooth.Click += new System.EventHandler(this.cmdBluetooth_Click);
            // 
            // cmdPassword
            // 
            this.cmdPassword.Location = new System.Drawing.Point(82, 46);
            this.cmdPassword.Name = "cmdPassword";
            this.cmdPassword.Size = new System.Drawing.Size(70, 23);
            this.cmdPassword.TabIndex = 4;
            this.cmdPassword.Text = "&Password";
            this.cmdPassword.Click += new System.EventHandler(this.cmdPassword_Click);
            // 
            // cmdClock
            // 
            this.cmdClock.Location = new System.Drawing.Point(1, 46);
            this.cmdClock.Name = "cmdClock";
            this.cmdClock.Size = new System.Drawing.Size(70, 23);
            this.cmdClock.TabIndex = 3;
            this.cmdClock.Text = "&Clock";
            this.cmdClock.Click += new System.EventHandler(this.cmdClock_Click);
            // 
            // cmdErase
            // 
            this.cmdErase.Location = new System.Drawing.Point(82, 20);
            this.cmdErase.Name = "cmdErase";
            this.cmdErase.Size = new System.Drawing.Size(70, 23);
            this.cmdErase.TabIndex = 1;
            this.cmdErase.Text = "&Erase";
            this.cmdErase.Click += new System.EventHandler(this.cmdErase_Click);
            // 
            // cmdDownload
            // 
            this.cmdDownload.Location = new System.Drawing.Point(1, 20);
            this.cmdDownload.Name = "cmdDownload";
            this.cmdDownload.Size = new System.Drawing.Size(70, 23);
            this.cmdDownload.TabIndex = 0;
            this.cmdDownload.Text = "&Download";
            this.cmdDownload.Click += new System.EventHandler(this.cmdDownload_Click);
            // 
            // groupBoxClock
            // 
            this.groupBoxClock.Controls.Add(this.lblClock);
            this.groupBoxClock.Controls.Add(this.textBoxSecond);
            this.groupBoxClock.Controls.Add(this.textBoxYear);
            this.groupBoxClock.Controls.Add(this.textBoxMinute);
            this.groupBoxClock.Controls.Add(this.textBoxMonth);
            this.groupBoxClock.Controls.Add(this.textBoxHour);
            this.groupBoxClock.Controls.Add(this.textBoxDay);
            this.groupBoxClock.Controls.Add(this.label7);
            this.groupBoxClock.Controls.Add(this.label6);
            this.groupBoxClock.Controls.Add(this.label9);
            this.groupBoxClock.Controls.Add(this.label8);
            this.groupBoxClock.Controls.Add(this.label4);
            this.groupBoxClock.Controls.Add(this.label3);
            this.groupBoxClock.Controls.Add(this.button3);
            this.groupBoxClock.Controls.Add(this.cmdOkClock);
            this.groupBoxClock.Controls.Add(this.cmdSyncClock);
            this.groupBoxClock.Location = new System.Drawing.Point(483, 96);
            this.groupBoxClock.Name = "groupBoxClock";
            this.groupBoxClock.Size = new System.Drawing.Size(234, 97);
            this.groupBoxClock.Visible = false;
            // 
            // lblClock
            // 
            this.lblClock.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblClock.Location = new System.Drawing.Point(0, 0);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(153, 18);
            this.lblClock.Text = "Change Date && Time";
            // 
            // textBoxSecond
            // 
            this.textBoxSecond.Location = new System.Drawing.Point(194, 44);
            this.textBoxSecond.MaxLength = 2;
            this.textBoxSecond.Name = "textBoxSecond";
            this.textBoxSecond.Size = new System.Drawing.Size(38, 21);
            this.textBoxSecond.TabIndex = 5;
            this.textBoxSecond.GotFocus += new System.EventHandler(this.txtBox_GotFocus);
            this.textBoxSecond.LostFocus += new System.EventHandler(this.txtBox_LostFocus);
            // 
            // textBoxYear
            // 
            this.textBoxYear.Location = new System.Drawing.Point(194, 20);
            this.textBoxYear.MaxLength = 4;
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.Size = new System.Drawing.Size(38, 21);
            this.textBoxYear.TabIndex = 2;
            this.textBoxYear.GotFocus += new System.EventHandler(this.txtBox_GotFocus);
            this.textBoxYear.LostFocus += new System.EventHandler(this.txtBox_LostFocus);
            // 
            // textBoxMinute
            // 
            this.textBoxMinute.Location = new System.Drawing.Point(117, 46);
            this.textBoxMinute.MaxLength = 2;
            this.textBoxMinute.Name = "textBoxMinute";
            this.textBoxMinute.Size = new System.Drawing.Size(38, 21);
            this.textBoxMinute.TabIndex = 4;
            this.textBoxMinute.GotFocus += new System.EventHandler(this.txtBox_GotFocus);
            this.textBoxMinute.LostFocus += new System.EventHandler(this.txtBox_LostFocus);
            // 
            // textBoxMonth
            // 
            this.textBoxMonth.Location = new System.Drawing.Point(117, 20);
            this.textBoxMonth.MaxLength = 2;
            this.textBoxMonth.Name = "textBoxMonth";
            this.textBoxMonth.Size = new System.Drawing.Size(38, 21);
            this.textBoxMonth.TabIndex = 1;
            this.textBoxMonth.GotFocus += new System.EventHandler(this.txtBox_GotFocus);
            this.textBoxMonth.LostFocus += new System.EventHandler(this.txtBox_LostFocus);
            // 
            // textBoxHour
            // 
            this.textBoxHour.Location = new System.Drawing.Point(32, 46);
            this.textBoxHour.MaxLength = 2;
            this.textBoxHour.Name = "textBoxHour";
            this.textBoxHour.Size = new System.Drawing.Size(38, 21);
            this.textBoxHour.TabIndex = 3;
            this.textBoxHour.GotFocus += new System.EventHandler(this.txtBox_GotFocus);
            this.textBoxHour.LostFocus += new System.EventHandler(this.txtBox_LostFocus);
            // 
            // textBoxDay
            // 
            this.textBoxDay.Location = new System.Drawing.Point(32, 20);
            this.textBoxDay.MaxLength = 2;
            this.textBoxDay.Name = "textBoxDay";
            this.textBoxDay.Size = new System.Drawing.Size(38, 21);
            this.textBoxDay.TabIndex = 0;
            this.textBoxDay.GotFocus += new System.EventHandler(this.txtBox_GotFocus);
            this.textBoxDay.LostFocus += new System.EventHandler(this.txtBox_LostFocus);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(158, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 20);
            this.label7.Text = "Sec";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(158, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 20);
            this.label6.Text = "Year";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(74, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 20);
            this.label9.Text = "Minute";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(74, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 20);
            this.label8.Text = "Month";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 20);
            this.label4.Text = "Hour";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 20);
            this.label3.Text = "Day";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(166, 72);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(67, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Cancel";
            this.button3.Click += new System.EventHandler(this.cmdCancelClock);
            // 
            // cmdOkClock
            // 
            this.cmdOkClock.Location = new System.Drawing.Point(84, 72);
            this.cmdOkClock.Name = "cmdOkClock";
            this.cmdOkClock.Size = new System.Drawing.Size(67, 23);
            this.cmdOkClock.TabIndex = 7;
            this.cmdOkClock.Text = "Ok";
            this.cmdOkClock.Click += new System.EventHandler(this.cmdOkClock_Click);
            // 
            // cmdSyncClock
            // 
            this.cmdSyncClock.Location = new System.Drawing.Point(1, 72);
            this.cmdSyncClock.Name = "cmdSyncClock";
            this.cmdSyncClock.Size = new System.Drawing.Size(67, 23);
            this.cmdSyncClock.TabIndex = 6;
            this.cmdSyncClock.Text = "Sync";
            this.cmdSyncClock.Click += new System.EventHandler(this.cmdSyncClock_Click);
            // 
            // groupBoxPassword
            // 
            this.groupBoxPassword.Controls.Add(this.lblPassGroup);
            this.groupBoxPassword.Controls.Add(this.textBox3);
            this.groupBoxPassword.Controls.Add(this.label2);
            this.groupBoxPassword.Controls.Add(this.label1);
            this.groupBoxPassword.Controls.Add(this.textBox2);
            this.groupBoxPassword.Controls.Add(this.textBox1);
            this.groupBoxPassword.Controls.Add(this.checkBoxShowPass);
            this.groupBoxPassword.Controls.Add(this.cmdCancelPass);
            this.groupBoxPassword.Controls.Add(this.cmdOkPass);
            this.groupBoxPassword.Location = new System.Drawing.Point(723, 96);
            this.groupBoxPassword.Name = "groupBoxPassword";
            this.groupBoxPassword.Size = new System.Drawing.Size(234, 97);
            this.groupBoxPassword.Visible = false;
            // 
            // lblPassGroup
            // 
            this.lblPassGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPassGroup.Location = new System.Drawing.Point(0, 0);
            this.lblPassGroup.Name = "lblPassGroup";
            this.lblPassGroup.Size = new System.Drawing.Size(175, 18);
            this.lblPassGroup.Text = "Change Password";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(105, 20);
            this.textBox3.MaxLength = 32;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(128, 21);
            this.textBox3.TabIndex = 1;
            this.textBox3.Visible = false;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            this.textBox3.GotFocus += new System.EventHandler(this.txtBox_GotFocus);
            this.textBox3.LostFocus += new System.EventHandler(this.txtBox_LostFocus);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "&Verify password:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "&Enter password:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(105, 46);
            this.textBox2.MaxLength = 32;
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(128, 21);
            this.textBox2.TabIndex = 2;
            this.textBox2.GotFocus += new System.EventHandler(this.txtBox_GotFocus);
            this.textBox2.LostFocus += new System.EventHandler(this.txtBox_LostFocus);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(105, 20);
            this.textBox1.MaxLength = 32;
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(128, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.GotFocus += new System.EventHandler(this.txtBox_GotFocus);
            this.textBox1.LostFocus += new System.EventHandler(this.txtBox_LostFocus);
            // 
            // checkBoxShowPass
            // 
            this.checkBoxShowPass.Location = new System.Drawing.Point(0, 74);
            this.checkBoxShowPass.Name = "checkBoxShowPass";
            this.checkBoxShowPass.Size = new System.Drawing.Size(100, 20);
            this.checkBoxShowPass.TabIndex = 3;
            this.checkBoxShowPass.Text = "&Show pass";
            this.checkBoxShowPass.CheckStateChanged += new System.EventHandler(this.checkBoxShowPass_CheckStateChanged);
            // 
            // cmdCancelPass
            // 
            this.cmdCancelPass.Location = new System.Drawing.Point(178, 72);
            this.cmdCancelPass.Name = "cmdCancelPass";
            this.cmdCancelPass.Size = new System.Drawing.Size(55, 23);
            this.cmdCancelPass.TabIndex = 5;
            this.cmdCancelPass.Text = "Cancel";
            this.cmdCancelPass.Click += new System.EventHandler(this.cmdCancelPass_Click);
            // 
            // cmdOkPass
            // 
            this.cmdOkPass.Location = new System.Drawing.Point(105, 72);
            this.cmdOkPass.Name = "cmdOkPass";
            this.cmdOkPass.Size = new System.Drawing.Size(55, 23);
            this.cmdOkPass.TabIndex = 4;
            this.cmdOkPass.Text = "Ok";
            this.cmdOkPass.Click += new System.EventHandler(this.cmdOkPass_Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuOptions);
            // 
            // menuOptions
            // 
            this.menuOptions.MenuItems.Add(this.menuFullDownloadErase);
            this.menuOptions.MenuItems.Add(this.menuUpgrade);
            this.menuOptions.MenuItems.Add(this.menuClearOutput);
            this.menuOptions.MenuItems.Add(this.menuSeparetor1);
            this.menuOptions.MenuItems.Add(this.menuAbout);
            this.menuOptions.MenuItems.Add(this.menuSeparetor2);
            this.menuOptions.MenuItems.Add(this.menuExit);
            this.menuOptions.Text = "Options";
            // 
            // menuFullDownloadErase
            // 
            this.menuFullDownloadErase.Text = "Full erase";
            this.menuFullDownloadErase.Click += new System.EventHandler(this.menuFullDownloadErase_Click);
            // 
            // menuUpgrade
            // 
            this.menuUpgrade.Text = "Upgrade";
            this.menuUpgrade.Click += new System.EventHandler(this.menuUpgrade_Click);
            // 
            // menuClearOutput
            // 
            this.menuClearOutput.Text = "Clear output";
            this.menuClearOutput.Click += new System.EventHandler(this.menuClearOutput_Click);
            // 
            // menuSeparetor1
            // 
            this.menuSeparetor1.Text = "-";
            // 
            // menuAbout
            // 
            this.menuAbout.Text = "About";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // menuSeparetor2
            // 
            this.menuSeparetor2.Text = "-";
            // 
            // menuExit
            // 
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1057, 285);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.groupBoxPassword);
            this.Controls.Add(this.groupBoxClock);
            this.Controls.Add(this.groupBoxStep2);
            this.Controls.Add(this.groupBoxStatistics);
            this.Controls.Add(this.rtbDisplay);
            this.Controls.Add(this.groupBoxStep1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "CRFSuiteMobile";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Closed += new System.EventHandler(this.FormMain_FormClosed);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormMain_Closing);
            this.groupBoxStep1.ResumeLayout(false);
            this.groupBoxStatistics.ResumeLayout(false);
            this.groupBoxStep2.ResumeLayout(false);
            this.groupBoxClock.ResumeLayout(false);
            this.groupBoxPassword.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel groupBoxStep1;
        private System.Windows.Forms.RadioButton radioScanUsb;
        private System.Windows.Forms.RadioButton radioScanBluetooth;
        private System.Windows.Forms.RadioButton radioScanAll;
        private System.Windows.Forms.RadioButton radioScanHID;
        private System.Windows.Forms.ComboBox cboPort;
        private System.Windows.Forms.Button cmdRescan;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.Panel groupBoxStatistics;
        private System.Windows.Forms.Panel groupBoxStep2;
        private System.Windows.Forms.Button cmdPassword;
        private System.Windows.Forms.Button cmdClock;
        private System.Windows.Forms.Button cmdErase;
        private System.Windows.Forms.Button cmdDownload;
        private System.Windows.Forms.Button cmdLogOut;
        private System.Windows.Forms.Button cmdReboot;
        private System.Windows.Forms.Button cmdBluetooth;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel groupBoxClock;
        private System.Windows.Forms.Panel groupBoxPassword;
        private System.Windows.Forms.Button cmdLogIn;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button cmdOkClock;
        private System.Windows.Forms.Button cmdSyncClock;
        private System.Windows.Forms.Button cmdCancelPass;
        private System.Windows.Forms.Button cmdOkPass;
        private System.Windows.Forms.CheckBox checkBoxShowPass;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxHour;
        private System.Windows.Forms.TextBox textBoxDay;
        private System.Windows.Forms.TextBox textBoxMinute;
        private System.Windows.Forms.TextBox textBoxMonth;
        private System.Windows.Forms.TextBox textBoxSecond;
        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.DataGrid dataGrid;
        private System.Windows.Forms.Button cmdDecode;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.TextBox textBox3;
#if PocketPC
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
#endif
        private System.Windows.Forms.MenuItem menuOptions;
        private System.Windows.Forms.MenuItem menuFullDownloadErase;
        private System.Windows.Forms.MenuItem menuUpgrade;
        private System.Windows.Forms.MenuItem menuClearOutput;
        private System.Windows.Forms.Label lblPassGroup;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Label lblStep2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MenuItem menuExit;
        private System.Windows.Forms.MenuItem menuSeparetor2;
        private AutoScrollText rtbDisplay;
        private DeviceInfoDataGrid deviceInfoDataGrid1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.MenuItem menuSeparetor1;
        private System.Windows.Forms.MenuItem menuAbout;
    }
}