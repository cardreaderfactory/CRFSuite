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

using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Collections.Specialized;
using System.Security.Cryptography;
using crf.Properties;
using crf.Forms;
using TaskDialogDLL;

namespace crf
{
    public enum IconBitmap
    {
        NoIcon,
        Info,
        Warning,
        Error,
        All
    }

    public enum PassBoxType
    {
        UserPassword,
        Bluetooth,
        Upgrade,
        Key
    }

    public enum DownloadBehaviour
    {
        Ask = 0,
        Open = 1,
        DoNothing = 2
    }


    public partial class Downloader : Form
    {
        enum KeyModifiers
        {
            None,
            Shift,
            CtrlAltShift
        }

        enum TimeOut
        {
            USB = 400,
            BlueTooth = 1000
        }

        enum Speed
        {
            USB = 250000,
            BlueTooth = 115200
        }

        CommunicationManager comm = new CommunicationManager();
        Device device;
        bool downloading = false;
        PassBoxType passBoxMode = PassBoxType.UserPassword;
        string recoveryCode = "";
        string currentFile = "";
        private string getfirmwareLink = "https://www.cardreaderfactory.com/crfsuite/getfirmware.php";
        private string getsoftwareLink = "https://www.cardreaderfactory.com/crfsuite/getsoftware.php";

//        private string getfirmwareLink = "http://localhost/getfirmware.php";
//        private string getsoftwareLink = "http://localhost/getsoftware.php";

        KeyModifiers keyPressed = KeyModifiers.None;
        float displayRatioX, displayRatioY;
        Point statisticsBoxLocation = new System.Drawing.Point(3, 26);
        Point commandBoxLocation = new System.Drawing.Point(3, 146);
        Point progressLocation = new System.Drawing.Point(6, 68);

        readonly int iconSize = 20;
        Bitmap [] bitmaps;

        static public Bitmap resizeIcon(Icon icon, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
                g.DrawImage(icon.ToBitmap(), 0, 0, nWidth, nHeight);
            return result;
        }

        System.Drawing.Point newPoint(int x, int y)
        {
            return new System.Drawing.Point((int)(x*displayRatioX), (int)(y*displayRatioY));
        }

        void fixPoint(ref System.Drawing.Point point)
        {
            point = newPoint(point.X, point.Y);
        }

        public Downloader()
        {
            InitializeComponent();
        }

        private void downloader_Load(object sender, EventArgs e)
        {
            this.Text = Program.name;

            /* internet functions are enabled only if the executable name is crfsuite */
            if (!Program.enableInternetUpdates)
            {
                firmwareUpdatefromInternetToolStripMenuItem.Visible = false;
                firmwareUpdateToolStripMenuItem.Text = "Firmware &update";
                firmwareUpdatefromInternetToolStripMenuItem.Text = "N/A";
                helpToolStripMenuItem.Visible = false;
            }

            /* rename the menu that contains CRFSuite */
            installToToolStripMenuItem.Text = "&Install " + Program.name + " to flash drive ...";

            /* window size adjustments */
            displayRatioX = (float)groupBoxStep1.Width / (float)218;
            displayRatioY = (float)groupBoxStep1.Height / (float)216;
            this.Size = new System.Drawing.Size((int)(1 * displayRatioX), (int)(1 * displayRatioY));
            this.MaximumSize = new System.Drawing.Size(this.Width, this.Height);

            /* demo warning image */
            bitmaps = new Bitmap[(int)IconBitmap.All];
            bitmaps[(int)IconBitmap.NoIcon] = null;
            bitmaps[(int)IconBitmap.Info] = resizeIcon(SystemIcons.Information, iconSize, iconSize);
            bitmaps[(int)IconBitmap.Warning] = resizeIcon(SystemIcons.Warning, iconSize, iconSize);
            bitmaps[(int)IconBitmap.Error] = resizeIcon(SystemIcons.Error, iconSize, iconSize);

            device = new Device(dataGridView, warningPictureBox, errorPictureBox);

            /* message warning icons */
            messagesLabel.ImageScaling = ToolStripItemImageScaling.None;

            cboPort.Sorted = true;
            comm.form = this;
            comm.progressBar = progressBar;
            comm.showMessage = showMessage;
            //progressBar.Visible = false;
            progressBar.Maximum = 100;
            progressBar.Value = 0;

            dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            showMessage(IconBitmap.Info, "Please select a Port or Open a file", "");
            fixPoint(ref commandBoxLocation);
            fixPoint(ref progressLocation);
            fixPoint(ref statisticsBoxLocation);

            dateTimePicker.CustomFormat = "hh:mm:ss / dd MMM yyyy";

            gotoStep(1);
            /* set up file associations if required */
            if (Settings.Default.checkFileAssociations &&
                !FileAssociation.isAssociated(Program.extension, Program.executablePath))
            {
                DialogResult r = TaskDialog.MessageBox(this,
                            "File association",
                            Program.name + " is not associated with *." + Program.extension + " files.",
                            "Would you like to associate it?",
                            "Don't show this again",
                            TaskDialogButtons.YesNo,
                            SysIcons.Warning
                            );
                Settings.Default.checkFileAssociations = !TaskDialog.VerificationChecked;

                if (r == DialogResult.Yes)
                    FileAssociation.Associate(Program.extension, Program.executablePath);
            }
        }

        private void cmdDownload_Click(object sender, EventArgs e)
        {
            if (downloading)
            {
                showMessage(IconBitmap.Info, "Canceling ...", "");
                cmdDownload.Enabled = false;
                comm.finaliseDownload(false);
                showMessage(IconBitmap.Info, "Download aborted", "");
            }
            else
            {
                //full download?
                Boolean full = (keyPressed == KeyModifiers.Shift);

                enableProgress(true);
                cmdDownload.Text = "&Cancel";
                cmdDownload.Visible = true;
                cmdDownload.Enabled = true;
                groupBoxStep2.Refresh();

                saveFileDialog1.Filter = Utils.getFilter(Program.extension) + "|All Files (*.*)|*.*";
                saveFileDialog1.CheckFileExists = false;
                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    downloadComplete(false);
                    return;
                }
                
                currentFile = saveFileDialog1.FileName;

                //remember directory name.
                try
                {
                    saveFileDialog1.InitialDirectory = (new FileInfo(saveFileDialog1.FileName)).DirectoryName;
                }
                catch(Exception)
                {
                }

                /* save file */
                if (currentFile != "")
                    downloading = comm.Download(full, currentFile, downloadComplete);
            }
        }
        
        public void downloadComplete(Boolean success)
        {
            enableProgress(false);

            cmdDownload.Text = "&Download";

            if (!success)
                currentFile = "";
            downloading = false;
            comm.Status(false);
            updateStats();

            //ask password to open downloaded file.
            if (!success)
                return;

            switch (Settings.Default.openAfterDownload)
            {
                case (int)DownloadBehaviour.Open:
                    decodeFile(currentFile);
                    return;
                case (int)DownloadBehaviour.DoNothing:
                    return;
            }


            DialogResult r = TaskDialog.ShowTaskDialogBox(this,
                    "Decode", /* Task Dialog Title */
                    "Do you want to open this file?", /* The main instruction text */
                    "To re-enable this dialog, go to File/Preferences (CTRL-P)", /* The content text for the task dialog */
                    "If you choose not to show this dialog again and press 'YES', I will always open the file after download. If you press 'NO', I will not do anything after download.\n\nIf you later decide that you want this dialog to be displayed again you can enable it by going to File/Preferences or pressing CTRL-P while in Downloader section of the software.", /* Any expanded content text */
                    "", /* Optional footer text with an icon */
                    "Don't show this dialog again",
                    "", /* "Radio Option 1 | Radio Option 2" */
                    "Yes and automatically open downloaded files|No and never open downloaded files", /* Command &Button 1|Command Button 2 */
                    TaskDialogButtons.YesNo,
                    SysIcons.Question,
                    SysIcons.Information
                    );

            switch (r)
            {
                case DialogResult.OK:
                    if (TaskDialog.CommandButtonResult == 0)
                    {
                        Settings.Default.openAfterDownload = (int)DownloadBehaviour.Open;
                        decodeFile(currentFile);
                    }
                    else
                    {
                        Settings.Default.openAfterDownload = (int)DownloadBehaviour.DoNothing;
                    }
                    break;
                case DialogResult.Yes:
                    if (TaskDialog.VerificationChecked)
                        Settings.Default.openAfterDownload = (int)DownloadBehaviour.Open;
                    decodeFile(currentFile);
                    break;
                case DialogResult.No:
                    if (TaskDialog.VerificationChecked)
                        Settings.Default.openAfterDownload = (int)DownloadBehaviour.DoNothing;
                    break;
                default:
                    break;
            }

        }

        private void decodeFile(string file)
        {
            frmDecode f = new frmDecode();
            if (device != null)
                f.deviceBuild = device.build;
            f.Show(this, file);
        }

        private void cmdErase_Click(object sender, EventArgs e)
        {
            Boolean full;
            full = (keyPressed == KeyModifiers.Shift);

            enableProgress(true);
            cmdErase.Visible = true;            
            groupBoxStep2.Refresh();
            showMessage(IconBitmap.Info, "Erasing ...", "");

            if (!comm.Erase(full, progressBar))
                showMessage(IconBitmap.Error, "Erase failed.", "The erase process has failed!");
            else
                showMessage(IconBitmap.Info, "Erase successful.", "The erase process has been completed.");


            enableProgress(false);
            updateStats();
        }

        public void showMessage(IconBitmap type, String message, String tooltip)
        {            
            //Console.WriteLine("["+message+"]");
            messagesLabel.Image = bitmaps[(int)type];
            message = message.TrimEnd('\r', '\n');
            int i = message.IndexOf("\n");

            if (i == -1)
            {
                messagesLabel.Text = message;
                messagesLabel.ToolTipText = Utils.wordWrap(tooltip, Device.wrapLimit);                
            }
            else
            {
                messagesLabel.Text = message.Substring(0, i - 1);
                messagesLabel.ToolTipText = Utils.wordWrap(message+"\n\n"+tooltip, Device.wrapLimit);
            }
            statusStrip1.Refresh();
        }
        
        private void selectFirstPort(RadioButton radio)
        {
            if (cboPort.Items.Count == 0)
            {
                cboPort.Text = "";
                cmdConnect.Enabled = false;
                showMessage(IconBitmap.Error, "Can't find a suitable communication port.",
                            "Cannot find a suitable communication port.\n\n" +
                            " - If you are using a USB device, please ensure that the cable driver is installed and the cable is plugged into of this computer's USB port.\n\n" +
                            " - If you are using a Bluetooth device, please try the ports listed in 'All ports'");
                radio.Checked = false;
            }
            else
            {
                cmdConnect.Enabled = true;
                cboPort.SelectedIndex = 0;
                if (cboPort.Items.Count > 1)
                    showMessage(IconBitmap.Warning, cboPort.Items.Count + " ports detected.", "");
                else
                    showMessage(IconBitmap.Info, cboPort.Items.Count + " port detected.", "");
            }
        }


        private void addPorts(String name)
        {
            StringBuilder devices = new StringBuilder("");
            UInt32 Index = 0;
            int result = 0;

            while (true)
            {
                String st;
                result = DevInfo.DeviceInfo.EnumerateDevices(Index, "Ports", devices);
                Index++;    //increment index for next device
                if (result == -2)
                {
                    cboPort.Items.Add("Incorrect name of Class");
                    break;
                }
                if (result == -1) break;  //no next device
                st = devices.ToString();
                if (result == 0 && st.Contains(name))
                    cboPort.Items.Add(st);   //put device in listbox
                cboPort.Refresh();     //refresh listbox
            }
        }

        private void cmdDeviceManager_Click(object sender, EventArgs e)
        {
            Process myProc;
            myProc = Process.Start("devmgmt.msc");
        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            if (!(radio.Checked))
                return;

            gotoStep(1);
            cboPort.Items.Clear();

            if (radio == radioScanUsb)
                addPorts("USB Serial Port");
            else if (radio == radioScanBluetooth)
                addPorts("Bluetooth");
            else if (radio == radioScanHID)
                addPorts("");
            else /* if (radio == radioScanAll) */
                comm.SetPortNameValues(cboPort);            

            selectFirstPort(radio);
            gotoStep(1);

        }

        private void updateStats()
        {
            updateStats(false);
        }

        private void showCmdUpgrade(bool visible)
        {
            if (!visible)
            {
                cmdUpgrade.Visible = false;
                return;
            }
            /* demo menus */
            if (device.isDemo)
            {
                cmdUpgrade.Text = "&Upgrade";
                cmdUpgrade.Visible = true;
                cmdUpgrade.ForeColor = System.Drawing.SystemColors.ControlText;
            }
            else if (device.canChangeKey)
            {
                cmdUpgrade.Text = "&Key";
                cmdUpgrade.Visible = true;
                cmdUpgrade.ForeColor = Color.Blue;
            }
            else
            {
                cmdUpgrade.Visible = false;
            }               

        }

        private void updateStats(bool showName)
        {
            string menu = string.Empty;
            string stats = string.Empty;

            if (Program.enableInternetUpdates)
                showName = true;

            this.Refresh();
            if (device.newDevice)
            {
                comm.readDeviceMenu(ref menu); 
                device.updateMenu(menu); /* determines the device capabilities */
                newModeToolStripMenuItem.Enabled = oldModeToolStripMenuItem.Enabled = device.hasReadMode;
                newModeToolStripMenuItem.Visible = oldModeToolStripMenuItem.Visible = toolStripSeparator7.Visible = device.hasReadMode;
                checkBoxLedStartup.Enabled = checkBoxLedSwipe.Enabled = device.canChangeLeds;
            }

            comm.readDeviceStats(ref stats);
            if (stats.Length == 0)
            {
                checkState();
                this.Refresh();
                return;
            }

            device.updateStats(stats, showName && Program.enableInternetUpdates); /* updates every stat about this device */

            showCmdUpgrade(true);

            /* read mode menus */
            newModeToolStripMenuItem.Checked = (device.readMode == "1");
            oldModeToolStripMenuItem.Checked = (device.readMode != "1");

            if (device.newDevice)
            {
                if (device.isDemo)
                    showMessage(IconBitmap.Warning, "WARNING: This is a demo device!", Device.demoWarning);

                if (device.hw != 0)
                    showMessage(IconBitmap.Error, "Hardware errors detected!", "We have detected hardware errors on this device.\n" + device.hardwareTooltip);
            }

            device.newDevice = false;
            this.Refresh();
        }

        private void enableDeviceMenu(bool all)
        {
            //deviceToolStripMenuItem.Visible = value;
            refreshInfoToolStripMenuItem.Enabled = all;
            newModeToolStripMenuItem.Enabled = all;
            newModeToolStripMenuItem.Checked &= all;
            oldModeToolStripMenuItem.Enabled = all;
            oldModeToolStripMenuItem.Checked &= all;

            firmwareUpdateToolStripMenuItem.Enabled = comm.IsOpen;
            firmwareUpdatefromInternetToolStripMenuItem.Enabled = comm.IsOpen;
        }

        private void gotoStep(int stepNumber)
        {
            cmdRescan.Enabled = radioScanSelected();
            switch (stepNumber)
            {
                case 1:
                    comm.ClosePort();
                    enableDeviceMenu(false);
                    device.newDevice = true;
                    dataGridView.Visible = false;
                    groupBoxStep1.Visible = true;
                    
                    cmdConnect.Enabled = (cboPort.Items.Count > 0);
                    cboPort.Visible = cmdConnect.Enabled;
                    cmdConnect.Visible = cmdConnect.Enabled;
                    cmdRescan.Visible = cmdConnect.Enabled;
                    cmdLogIn.Visible = false;
                    txtPassword.Visible = false;
                    labelPassword.Visible = false;

                    groupBoxStep2.Enabled = false;
                    groupBoxStep2.Visible = false;
                    groupBoxStatistics.Visible = false;
                    break;

                case 2:
                    enableDeviceMenu(false);
                    device.newDevice = true;
                    dataGridView.Visible = false;
                    groupBoxStep1.Visible = true;
                    cmdLogIn.Visible = true;
                    txtPassword.Visible = true;
                    labelPassword.Visible = true;

                    cmdLogIn.Enabled = true;
                    groupBoxStep2.Enabled = false;
                    groupBoxStep2.Visible = false;
                    groupBoxPassword.Visible = false;
                    groupBoxStatistics.Visible = false;
                    break;

                case 3:
                    updateStats();
                    enableDeviceMenu(true);
                    //try { Clipboard.SetDataObject(device.build, true); } catch { };
                    dataGridView.Visible = true;
                    groupBoxStep1.Visible = false;

                    groupBoxStatistics.Location = statisticsBoxLocation;
                    groupBoxStatistics.Visible = true;
                    groupBoxStatistics.Refresh();

                    groupBoxStep2.Enabled = true;
                    groupBoxStep2.Location = commandBoxLocation;
                    groupBoxStep2.Visible = true;
                    groupBoxSettings.Visible = false;
                    groupBoxPassword.Visible = false;

                    enableButtons(true);
                    break;

                default:
                    break;
            }
        }

        String getPortName(String text)
        {
            int index = text.IndexOf("(COM");
            if (index != -1)
            {
                return text.Substring(index + 1, text.Length - index - 2);   //put device in listbox
            }

            return text;
        }

        private CommunicationManager.State checkState()
        {
            CommunicationManager.State state;
            state = comm.Status(); 
            switch (state)
            {
                case CommunicationManager.State.LoggedOut:
                    gotoStep(2);
                    break;
                case CommunicationManager.State.LoggedIn:
                    gotoStep(3);
                    break;
                case CommunicationManager.State.Disconnected:
                    gotoStep(2);
                    showMessage(IconBitmap.Error, "Device is not responding on " + comm.PortName, "The device is not responding.\n\nPlease ensure that the device" +
                                    "is connected properly and " + comm.PortName + " is the correct port.\n\n" +
                                    "If both of these are correct, try reconnecting.\n\n" +
                                    "If you cannot see your Serial Port in the dropdown list, your " +
                                    "cable is not recognised by this computer. Please disconnect " +
                                    "and reconnect your cable and press the Connect button again.");
                                    
                    break;
                case CommunicationManager.State.PortClosed:
                    gotoStep(1);
                    break;
                case CommunicationManager.State.Blocked:
                    gotoStep(2);
                    showMessage(IconBitmap.Error, "Device blocked", "The device has been blocked because of too many password tries!\n" + comm.blockTimeMessage + "\n" + "Do not remove the power from the device (batery or USB cable) or time count will start from the beginning.");
                    MessageBox.Show("The device has been blocked because of too many password tries!\n" + comm.blockTimeMessage + "\n" + "Do not remove the power from the device (batery or USB cable) or time count will start from the beginning.",
                                    "Connection failed!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    break;

            }
            return state;
        }

        private void cmdConnect_click(object sender, EventArgs e)
        {
            cmdConnect.Enabled = false;
            comm.PortName = getPortName(cboPort.Text);

            /**
             * bluetooth max speed is 115200, but usb serial port speed must be 250.000
             * 
             * There is a bug on the bluetooth device : if we try to open a port using a higher speed
             * than the maximum supported bluetooth device can no longer use serial port until it is switched off/on.
             */
            int speed = 0;
            int timeout = 1;

            if (radioScanUsb.Checked)
            {
                speed = (int)(Speed.USB);
                timeout = (int)(TimeOut.USB);
            }
            else if (radioScanBluetooth.Checked)
            {
                speed = (int)(Speed.BlueTooth);
                timeout = (int)(TimeOut.BlueTooth);
            }
            else
            {
                switch (MessageBox.Show(this, "Are you using an USB device?", Program.name,
                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        speed = (int)(Speed.USB);
                        timeout = (int)(TimeOut.USB);
                        break;

                    case DialogResult.No:
                        speed = (int)(Speed.BlueTooth);
                        timeout = (int)(TimeOut.BlueTooth);
                        break;

                    case DialogResult.Cancel:
                        speed = 0;
                        break;
                }
            }

            if (speed > 0)
            {
                if (comm.OpenPort(speed, timeout))
                {
                    checkState();
                }
                else
                {
                    gotoStep(1);
                }
            }
            cmdConnect.Enabled = true;
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            cmdLogIn.Enabled = false;
            comm.sendCmd("l" + txtPassword.Text);
            comm.getResult();
            switch (checkState())
            {
                case CommunicationManager.State.LoggedOut:
                    showMessage(IconBitmap.Error, "LogIn Failed.", "LogIn Failed. Is the password correct?");
                    break;
                case CommunicationManager.State.LoggedIn:
                    showMessage(IconBitmap.Info, "Logged in.", "Logged in.");
                    if (txtPassword.Text == "1234" && Settings.Default.askToChangeDefaultPass)
                    {
                        DialogResult r = TaskDialog.MessageBox(this,        
                                    "Changing default password",
                                    "Insecure password detected.",
                                    "The password '1234' is the default password on all devices. We strongly recommend that you change this password right now.\n\nWould you like to change the password?",
                                    "Don't show this again",
                                    TaskDialogButtons.YesNo,
                                    SysIcons.Warning
                                    );

                        Settings.Default.askToChangeDefaultPass = !TaskDialog.VerificationChecked;

                        if (r == DialogResult.Yes)
                            cmdPassword_Click(sender, e);
                    }
                    break;
            }
            cmdLogIn.Enabled = true;
        }

        private void cmdLogOut_Click(object sender, EventArgs e)
        {
            CommunicationManager.State state;
            enableButtons(false);
            comm.sendCmd("x");
            state = checkState();

            if (state == CommunicationManager.State.LoggedIn)
            {
                showMessage(IconBitmap.Error, "Loging Out Failed.", "Communication failure: Loging Out Failed.");
            }
            enableButtons(true);
        }

        private void cmdReboot_Click(object sender, EventArgs e)
        {
            enableButtons(false);
            comm.sendCmd("r");
            showMessage(IconBitmap.Info, "Rebooting ...", "");
            comm.waitForData(true);
            checkState();
            enableButtons(true);
        }


        private void enableProgress(Boolean enable)
        {
            enableButtons(!enable);
            cmdDownload.Visible = !enable;
            cmdErase.Visible = !enable;

            cmdDecode.Visible = (!enable);
            cmdClock.Visible = (!enable);
            cmdPassword.Visible = (!enable);
            showCmdUpgrade(!enable);
            //cmdReadMode.Enabled = enable && hasReadMode;

            cmdBluetooth.Visible = (!enable);
            cmdReboot.Visible = (!enable);
            cmdLogOut.Visible = (!enable);
            progressBar.Visible = enable;
            progressBar.Width = groupBoxStep2.Width - (progressLocation.X * 2);
            progressBar.Location = progressLocation;
            progressBar.Value = 0;
        }

        private void enableButtons(Boolean enable)
        {
            cmdDownload.Enabled = enable;
            cmdErase.Enabled = enable;
            cmdDecode.Enabled = enable;

            cmdClock.Enabled = enable;
            cmdPassword.Enabled = enable;
            cmdUpgrade.Enabled = enable;
            //cmdReadMode.Enabled = enable && hasReadMode;

            cmdBluetooth.Enabled = enable && device.hasBluetooth;
            cmdReboot.Enabled = enable;
            cmdLogOut.Enabled = enable;
        }

        #region password

        private void configurePassBox(PassBoxType box)
        {
            passBoxMode = box;
            groupBoxStep2.Visible = false;
            label1.Visible = textBox1.Visible = true;
            switch (box)
            {
                case PassBoxType.UserPassword:

                    groupBoxPassword.Text = "Change login password";

                    label1.Text = "Enter password:";
                    textBox1.Text = "";
                    textBox1.UseSystemPasswordChar = true;

                    label2.Visible = textBox2.Visible = true;
                    label2.Text = "Verify password:";
                    textBox2.Text = "";            
                    textBox2.UseSystemPasswordChar = true;

                    checkBoxShowPass.Text = "&Show pass";
                    checkBoxShowPass.Visible = true;
                    checkBoxShowPass.Checked = false;

                    break;
                case PassBoxType.Upgrade:
                   
                    if (device.isDemo)
                        groupBoxPassword.Text = "Upgrading";
                    else
                        groupBoxPassword.Text = "Downgrading";

                    if (device.isDemo)
                        label1.Text = "Upgrade pass:";
                    else
                        label1.Text = "Downgrade pass:";

                    textBox1.Text = "";
                    textBox1.UseSystemPasswordChar = false;

                    label2.Visible = textBox2.Visible = false;

                    checkBoxShowPass.Text = "&Show pass";
                    checkBoxShowPass.Visible = false;
                    checkBoxShowPass.Checked = true;

                    break;
                case PassBoxType.Bluetooth:

                    groupBoxPassword.Text = "Bluetooth Settings";

                    label1.Text = "Device Name:";
                    textBox1.Text = device.bluetoothName;
                    textBox1.UseSystemPasswordChar = false;

                    label2.Visible = textBox2.Visible = true;
                    label2.Text = "Peering Pass:";
                    textBox2.Text = device.bluetoothPeering;
                    textBox2.UseSystemPasswordChar = true;

                    checkBoxShowPass.Text = "&Show chars";
                    checkBoxShowPass.Visible = true;
                    checkBoxShowPass.Checked = false;

                    break;

                case PassBoxType.Key:

                    groupBoxPassword.Text = "Change key";

                    label1.Text = "Enter key:";
                    textBox1.Text = "";
                    textBox1.UseSystemPasswordChar = false;

                    label2.Visible = textBox2.Visible = true;
                    label2.Text = "Verify key:";
                    textBox2.Text = "";            
                    textBox2.UseSystemPasswordChar = false;

                    checkBoxShowPass.Text = "&CBC";
                    checkBoxShowPass.Visible = true;
                    checkBoxShowPass.Checked = true;
                    break;
            }
            groupBoxPassword.Location = commandBoxLocation;
            groupBoxPassword.Visible = true;
            textBox1.Focus();
        }

        private void cmdBluetooth_Click(object sender, EventArgs e)
        {
            configurePassBox(PassBoxType.Bluetooth);
        }

        private void cmdPassword_Click(object sender, EventArgs e)
        {
            if (keyPressed == KeyModifiers.CtrlAltShift)
                cmdUpgrade_Click(sender, e);
            else
                configurePassBox(PassBoxType.UserPassword);
        }

        private void cmdUpgrade_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text.Contains("Key"))
                configurePassBox(PassBoxType.Key);
            else
                configurePassBox(PassBoxType.Upgrade);
        }

//        int count = 0;
        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers.Equals(Keys.Control | Keys.Alt | Keys.Shift))
                keyPressed = KeyModifiers.CtrlAltShift;
            else if (e.Modifiers.Equals(Keys.Shift))
                keyPressed = KeyModifiers.Shift;
            else
                keyPressed = KeyModifiers.None;

            if (e.KeyCode == Keys.Delete)
                showMessage(IconBitmap.NoIcon, "", "");
        }


        private void configureBluetooth(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 30)
            {
                showMessage(IconBitmap.Error, "Device name too long", "The bluetooth device name cannot exceed 30 characters");
                return;
            }
            if (textBox2.Text.Length > 16)
            {
                showMessage(IconBitmap.Error, "Peering password too long", "The peering password cannot exceed 16 characters");
                return;
            }
            string s = textBox1.Text+textBox2.Text;
            if (s.Contains(",") || s.Contains("="))
            {
                showMessage(IconBitmap.Error, "Chars , and = are not allowed", "The characters , and = are not allowed in bluetooth name or password");
                return;
            }

            DialogResult result;
            result = TaskDialog.MessageBox(this,
                        "Bluetooth settings change confirmation",
                        "Are you sure that you want to save these settings?",
                        "Warning: If you forget the peering password you will be unable to reconnect to the device.\n\nNote: If you choose YES, you will be logged out as this is a required step to update the device settings.",
                        TaskDialogButtons.YesNo,
                        SysIcons.Warning);
            if (result == DialogResult.No)
            {
                showMessage(IconBitmap.Info, "Bluetooth settings not changed.", "");
                return;
            }

            if (comm.sendCmd("b" + textBox2.Text + "," + textBox1.Text) && comm.getResult())
            {
                showMessage(IconBitmap.Info, "Bluetooth settings updated.", "");
                comm.sendCmd("r");
                gotoStep(2);
            }
            else
            {
                showMessage(IconBitmap.Error, "Bluetooth update failed.", "");
                gotoStep(3);
            }
        }

        private bool validateLength(int min, int max)
        {
            if (textBox1.Text.Length < 4)
            {
                showMessage(IconBitmap.Error, "The password is too short.", "The password is too short.\nIt has to contain between 4 and 16 characters.");
                return false;
            }
            if (textBox1.Text.Length > 16)
            {
                showMessage(IconBitmap.Error, "The password is too long.", "The password is too long.\nIt has to contain between 4 and 16 characters.");
                return false;
            }

            return true;

        }

        private bool executeCommand(string command)
        {
            string title = groupBoxPassword.Text;
            string message;
            if (comm.sendCmd(command) && comm.getResult())
            {
                message = title + " succeeded.";
                updateStats();
                showMessage(IconBitmap.Info, message, message);
            }
            else
            {
                message = title + " failed!";
                showMessage(IconBitmap.Error, message, message);
                checkState();
                return false;
            }

            gotoStep(3);
            return true;
        }

        private void configureUpgrade(object sender, EventArgs e)
        {
            DialogResult result;
            string title = groupBoxPassword.Text;
            string message;

            if (!validateLength(4, 16))
                return;

            message = "Are you sure that you want to proceed\n" +
                      "with: " + title + "?";
            if (device.isDemo)
                message += "\n\n" +
                          "Warning: If you enter an incorrect code 3 times,\n" +
                          "you will not be able to use this device anymore.\n";

            result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                showMessage(IconBitmap.Info, "Upgrade canceled.", "");
                gotoStep(3);
                return;
            }

            executeCommand("$" + textBox1.Text + "#" + textBox1.Text);
        }

        private void configureUserPassword(object sender, EventArgs e)
        {
            DialogResult result;
            string title = groupBoxPassword.Text;
            string message;

            if (!checkBoxShowPass.Checked && !textBox1.Text.Equals(textBox2.Text))
            {
                showMessage(IconBitmap.Error, "Password do not match.", "Password do not match.\nMake sure that you type in both boxes the same password!");
                return;
            }

            if (!validateLength(4, 16))
                return;

            message = "Are you sure that you want to change the password?\n" +
                      "If you forget it, you won't be able to use your device anymore.";

            result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                showMessage(IconBitmap.Info, "Password not changed.", "");
                gotoStep(3);
                return;
            }
            executeCommand("p" + textBox1.Text + "#" + textBox1.Text);
        }

        private void configureKey(object sender, EventArgs e)
        {
            if (!StringUtil.isValidKey(textBox1.Text)) /* pass1 != pass2 */
            {
                DialogResult result = TaskDialog.MessageBox(this,
                            "Invalid key.",
                            "The key is invalid",
                            "Would you like to generate a random key?",
                            TaskDialogButtons.YesNoCancel,
                            SysIcons.Question);
                switch (result)
                {
                    case DialogResult.Yes:
                        textBox1.Text = "";
                        byte[] b = new byte[16];
                        RandomNumberGenerator r = RandomNumberGenerator.Create();
                        r.GetBytes(b);

                        for (int i = 0; i < b.Length; i++)
                        {
                            //String.Format("{0:d6}:{1:mm:ss}", (int)elapsedSpan.TotalHours, time)
                            textBox1.Text += String.Format("{0:x2}", b[i]);
                                
                        }
                        textBox2.Text = textBox1.Text;
                        try { Clipboard.SetDataObject(textBox1.Text, true); } catch { };
                        showMessage(IconBitmap.Info, "Key copied to clipboard", "The key has been copied to clipboard. Use CTRL-V to paste it in a text editor. Please store it in a safe place as without it you won't be able to decrypt your data!");
                        return;

                    case DialogResult.Cancel:
                        showMessage(IconBitmap.Info, "Key not changed.", "");
                        gotoStep(3);
                        return;
                    default:
                        return;
                }
            }

            if (!textBox1.Text.Equals(textBox2.Text))
            {
                showMessage(IconBitmap.Error, "Keys don't match.", "The keys don't match.\nMake sure that you type in both boxes the same key!");
                return;
            }

            if (device.totalMem != device.freeMem)
            {
                DialogResult result = TaskDialog.MessageBox(this,
                            "Device not empty",
                            "Would you like to erase the device?",
                            "In order to change the key, the memory must be empty. Would you like to erase the device now?",
                            TaskDialogButtons.YesNo,
                            SysIcons.Question);
                switch (result)
                { 
                    case DialogResult.Yes:
                        showMessage(IconBitmap.Info, "Erasing. Please wait...", "");
                        if (!comm.Erase(false, null))
                        {
                            showMessage(IconBitmap.Error, "Erase failed.", "The erase process has failed!");
                            return;
                        }
                        break;
                    default:
                        showMessage(IconBitmap.Info, "Key not changed.", "");
                        gotoStep(3);
                        return;
                }
            }

            char cbc;

            if (checkBoxShowPass.Checked)
                cbc = '1';
            else
                cbc = '0';

            executeCommand("k" + textBox1.Text + cbc);
        }



        private void cmdOkPass_Click(object sender, EventArgs e)
        {
            textBox1.Text.Trim();
            textBox2.Text.Trim();

            switch (passBoxMode)
            {
                case PassBoxType.Bluetooth:
                    configureBluetooth(sender, e);
                    return;
                case PassBoxType.Upgrade:
                    configureUpgrade(sender, e);
                    return;
                case PassBoxType.UserPassword:
                    configureUserPassword(sender, e);
                    return;
                case PassBoxType.Key:
                    configureKey(sender, e);
                    return;                  
            }

        }

        private void cmdCancelPass_Click(object sender, EventArgs e)
        {
            if (groupBoxPassword.Text.Contains("Bluetooth"))
                showMessage(IconBitmap.Info, "Bluetooth settings not changed.", "");
            else
                showMessage(IconBitmap.Info, "Password not changed.", "");

            gotoStep(3);
        }

        private void checkBoxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            bool passVisible = ((System.Windows.Forms.CheckBox)sender).Checked;

            switch (passBoxMode)
            {
                case PassBoxType.Bluetooth:
                    textBox2.UseSystemPasswordChar = !passVisible;
                    return;

                case PassBoxType.Key:
                    return;

                case PassBoxType.Upgrade:
                case PassBoxType.UserPassword:
                    if (passVisible)
                    {
                        textBox1.UseSystemPasswordChar = false;
                        textBox2.Visible = false;
                        label2.Visible = false;
                    }
                    else
                    {
                        textBox1.UseSystemPasswordChar = true;
                        textBox2.Visible = true;
                        label2.Visible = true;
                    }
                    break;
            }
        }
        #endregion

        #region clock

        bool  dateTimeChanged = false;

        private void event_dateTimeChanged(object sender, EventArgs e)
        {
            dateTimeChanged = true;

        }

        private void cmdSettings_Click(object sender, EventArgs e)
        {
            groupBoxStep2.Visible = false;
            //groupBoxDebug.Visible = false;
            //textBoxDay.Text = textBoxMonth.Text = textBoxSecond.Text = "";
            //textBoxHour.Text = textBoxMinute.Text = textBoxYear.Text = "";

            try
            {
                dateTimePicker.Value = DateTime.Now;
            }
            catch
            {
            }
            checkBoxLedStartup.Checked = device.ledStartup;
            checkBoxLedSwipe.Checked = device.ledSwipe;
            dateTimeChanged = false;

            groupBoxSettings.Location = commandBoxLocation;
            groupBoxSettings.Visible = true;
        }

        private void cmdSyncClock_Click(object sender, EventArgs e)
        {
            System.DateTime dateTime = DateTime.Now;
            setTime(dateTime);
            updateStats((keyPressed == KeyModifiers.Shift));
            gotoStep(3);
        }

        private void cmdOkClock_Click(object sender, EventArgs e)
        {
            if (dateTimeChanged)
                setTime(dateTimePicker.Value);
            setLeds();
            updateStats((keyPressed == KeyModifiers.Shift));
            gotoStep(3);
        }

        private bool setLeds()
        {
            bool ret = true;
            if (!device.canChangeLeds)
                return true;

            device.ledStartup = checkBoxLedStartup.Checked;
            device.ledSwipe = checkBoxLedSwipe.Checked;            
            if (!(comm.sendCmd("i" + device.getLedValue()) && comm.getResult()))
            {
                showMessage(IconBitmap.Error, "Changing leds failed!", "Changing led behaviour failed!");
                ret = false;
            }
            else
            {
                showMessage(IconBitmap.Info, "Led behaviour changed.", "");
            }
            return ret;
        }


        private bool setTime(System.DateTime dateTime)
        {
            bool ret = true;
            System.DateTime unixBase = new DateTime(1970, 1, 1).ToLocalTime();
            uint timestamp = (uint)dateTime.Subtract(unixBase).TotalSeconds;
            if (!(comm.sendCmd("t" + timestamp.ToString()) && comm.getResult()))
            {
                showMessage(IconBitmap.Error, "Changing clock failed!", "Changing clock has failed!");
                ret = false;
            }
            else
            {
                showMessage(IconBitmap.Info, "Clock changed.", "");
            }
            return ret;
        }

        private void cmdCancelClock_Click(object sender, EventArgs e)
        {
            showMessage(IconBitmap.Info, "Clock not changed.", "");
            gotoStep(3);
        }
        #endregion

        private void cmdRescan_Click(object sender, EventArgs e)
        {
            Boolean oldMode = (keyPressed == KeyModifiers.Shift);
            if (radioScanUsb.Checked)
                radioButtons_CheckedChanged(radioScanUsb, null);
            else if (radioScanBluetooth.Checked)
                radioButtons_CheckedChanged(radioScanBluetooth, null);
            else if (radioScanHID.Checked)
                radioButtons_CheckedChanged(radioScanHID, null);
            else if (radioScanAll.Checked)
            {                    
                gotoStep(1);
                cboPort.Items.Clear();
                comm.SetPortNameValues(cboPort);
                selectFirstPort(radioScanHID);
            }
        }

        private bool radioScanSelected()
        {
            return (radioScanUsb.Checked || radioScanBluetooth.Checked || radioScanHID.Checked || radioScanAll.Checked);
        }

        private void launchDeviceManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process myProc;
            myProc = Process.Start("devmgmt.msc");

        }

        private void txtPassword_keyPress(object sender, KeyPressEventArgs e)
        {        
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                cmdConnect_Click(sender, e);
            }
        }

        private void cboPort_keyPress(object sender, KeyPressEventArgs e)
        {        
            if (e.KeyChar == (char)Keys.Return && !((ComboBox)sender).DroppedDown)
            {
                e.Handled = true;
                cmdConnect_click(sender, e);
            }
        }

        private void dataGridView_click(object sender, MouseEventArgs e)
        {
            DataGridView d = (System.Windows.Forms.DataGridView)sender;

            //do it only if download is not in progress.
            if (!downloading)
                updateStats((keyPressed == KeyModifiers.Shift));

            try
            {
                if (Program.enableInternetUpdates && d != null && d.CurrentRow != null && d.CurrentRow.Index == 0 && device != null)
                {
                    Process p;
                    p = Process.Start(device.url);
                }
            }
            catch{};


        }

        private void cmdDecode_Click(object sender, EventArgs e)
        {
            frmDecode f = new frmDecode();
            if (device != null)
                f.deviceBuild = device.build;
            if (sender is ToolStripMenuItem)
                f.recoveryMode = (sender as ToolStripMenuItem).Text.ToLower().Contains("recovery");
            f.Show(this);
        }

        private void cmdReadMode_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem m = (ToolStripMenuItem)sender;

            if (m == newModeToolStripMenuItem && newModeToolStripMenuItem.Checked)
                return;
            if (m == oldModeToolStripMenuItem && oldModeToolStripMenuItem.Checked)
                return;

            if (!(comm.sendCmd("n") && comm.getResult()))
            {
                showMessage(IconBitmap.Error, "Changing read mode failed!", "Changing read mode failed!");
                return;
            }
            else
            {
                if (m == newModeToolStripMenuItem)
                    showMessage(IconBitmap.Info, "Changed to New Read Mode.", "");
                else
                    showMessage(IconBitmap.Info, "Changed to Old Read Mode.", "");
            }

            updateStats();
        }

        private void refreshStats_Click(object sender, EventArgs e)
        {
            updateStats((keyPressed == KeyModifiers.Shift));
        }

        private void clearMessages_Click(object sender, EventArgs e)
        {
            showMessage(IconBitmap.NoIcon, "", "");
        }

        private void decodeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDecode f = new frmDecode();
            if (device != null)
                f.deviceBuild = device.build;
            f.Show(this);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                cmdOkPass_Click(sender, e);
            }
        }

        private void deviceManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process myProc;
            myProc = Process.Start("devmgmt.msc");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /* reads the firmware from a local file */
        string readFirmware()
        {
            string filename;
            string ascii = null;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select firmware";
            string gcreate = Program.directoryName + "\\" + "gcreate.exe";
            bool canReadHex;
            string input = "";
            string output = "";


            canReadHex = (File.Exists(gcreate));
            if (canReadHex)
                dialog.Filter = "AES & HEX Files (*.aes;*.hex)|*.aes;*.hex|AES Files (*.aes)|*.aes|HEX Files (*.hex)|*.hex|All Files (*.*)|*.*";
            else
                dialog.Filter = "AES Files (*.aes)|*.aes|All Files (*.*)|*.*";
            dialog.CheckFileExists = true;
            if (dialog.ShowDialog() != DialogResult.OK)
                return null;
            filename = dialog.FileName;
            try
            {
                if (Path.GetExtension(filename).ToLower() == ".hex")
                {
                    string aesconfig = Path.GetDirectoryName(filename) + "\\" + "aesconfig.txt";

                    if (!File.Exists(aesconfig))
                        aesconfig = Program.directoryName + "\\" + "aesconfig.txt";

                    if (!File.Exists(aesconfig))
                    {
                        showMessage(IconBitmap.Error, "aesconfig.txt is missing", "Cannot update this .hex file without " + aesconfig);
                        return null;
                    }

                    showMessage(IconBitmap.Info, "Generating AES file ...", "");

                    input = filename;
                    output = Path.GetTempFileName();

                    Process p = new Process();
                    p.StartInfo.FileName = gcreate;
                    p.StartInfo.Arguments = " -c \"" + aesconfig + "\" -f \"" + input + "\" -n -o \"" + output + "\"";
                    //p.StartInfo.UseShellExecute = false;
                    //p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.Start();
                    if (!p.WaitForExit(60000))
                    {
                        p.Kill();
                        try { File.Delete(output); } catch {};
                        return null;
                    }
                    else
                    {
                        filename = output;
                    }
                }

                ascii = File.ReadAllText(filename);
            }
            catch (Exception ex)
            {
                showMessage(IconBitmap.Error, ex.Message, "");
                ascii = null;
            }

            if (output != "" && File.Exists(output))
                try { File.Delete(output); } catch {};

            if ((ascii != null) && (ascii.Length == 0 || (ascii.Substring(0, 4) != "0092" && ascii.Substring(0, 4) != "0112")))
            {
                showMessage(IconBitmap.Error, "This file does not contain valid firmware", "");
                ascii = null;
            }

            return ascii;
        }

        public static string CalculateSHA1(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSHA1 =
            new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(
                cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");

            return hash;
        }

        string processDownloadedFirmware(byte[] buf)
        {
            int sha1Length = 40;

            if (buf == null || buf.Length <= sha1Length)
                goto Cleanup;

            string message = System.Text.Encoding.ASCII.GetString(buf);
            string sha1 = message.Substring(0, sha1Length);
            string data = message.Substring(sha1Length, message.Length - sha1Length);            
            
            if (data.Length == 0 || CalculateSHA1(data).ToUpper() != sha1.ToUpper())
                goto Cleanup; 
            
            return (data);

         Cleanup:
            showMessage(IconBitmap.Error, "Invalid firmware", "");
            return null;
        }

        private byte[] sendCommandToServer(string command, string recoveryCode, bool async)
        {
            NameValueCollection values = new NameValueCollection();
            WebClient client = new WebClient();

            client.Headers.Add("user-agent", "CRFSuite (" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")");

            values.Add("command", command);
            values.Add("device", device.name);
            values.Add("functionality", device.newDevice ? "" : device.isDemo ? "demo" : "full");
            values.Add("build", device.build);
            values.Add("cpu", device.cpu);
            values.Add("fuses", device.fuses);
            values.Add("firmware", device.firmware);
            values.Add("memory", device.totalMem.ToString());
            values.Add("hw", device.hw.ToString());
            values.Add("readMode", device.newDevice ? "" : (device.readMode == "1") ? "new" : "old");
            if (recoveryCode.Length > 0)
                values.Add("recoveryCode", recoveryCode);

            if (async)
            {
                Uri addr = new Uri(getfirmwareLink);
                client.UploadValuesAsync(addr, values);
                return null;
            }
            else
            {
                return client.UploadValues(getfirmwareLink, values);
            }
        }

        void notifyUpdate()
        {
            try
            {
                updateStats();
                sendCommandToServer("complete", recoveryCode, true);
            }
            catch
            {
            }
        }

        /* downloads the firmware from the Internet */
        string downloadFirmware()
        {
            string ascii = null;

            if (device.newDevice)
            {
                Forms.SelectDevice d = new Forms.SelectDevice();
                if (d.ShowDialog() == DialogResult.Cancel)
                    return null;
                device.name = d.deviceName;
                device.cpu = d.deviceCpu;
                device.firmware = "unknown";
                device.build = d.deviceBuild;
                recoveryCode = d.recoveryCode;
            }
            else
            {
                recoveryCode = "";
            }

            try
            {
                String[] serverReply;
                string serverFirmware = "";
                string firmwareNotes = "";

                showMessage(IconBitmap.Info, "Checking for updates...", "");                
                serverReply = System.Text.Encoding.ASCII.GetString(sendCommandToServer("check", recoveryCode, false)).Split('^');
                serverFirmware = Device.getValue(serverReply, "fw");
                firmwareNotes = Device.getValue(serverReply, "message");

                if (serverFirmware == "n/a")
                {
                    if (firmwareNotes.Length > 0)
                        showMessage(IconBitmap.Info, firmwareNotes, "");
                    else
                        showMessage(IconBitmap.Info, "You are using the latest firmware", "");
                }
                else if (serverFirmware.Length > 0 && serverFirmware.Length < 6)
                {
                    DialogResult result;
                    result = MessageBox.Show(
                        "There is a new firmware available. Would you like to update your device's firmware?\n\nYour firmware: " + device.firmware +
                        "\nNew firmware: " + serverFirmware + "\n\nFirmware Notes\n----------------------\n" + firmwareNotes, 
                        "New firmware available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        showMessage(IconBitmap.Info, "Downloading firmware ...", "");
                        ascii = processDownloadedFirmware(sendCommandToServer("download", recoveryCode, false));
                    }
                    else
                    {
                        showMessage(IconBitmap.Info, "Firmware update canceled.", "");
                    }
                }
                else
                {
                    showMessage(IconBitmap.Info, "Cannot communicate with the server", "");
                }                
            }
            catch (WebException ex)
            {
                string msg = ex.Message;
                int i = msg.IndexOf("(");
                if (i >= 0)
                    msg = msg.Substring(i, msg.Length-i);
                showMessage(IconBitmap.Error, msg, ex.Message);
                ascii = null;
            }

            if (ascii != null && !device.newDevice && recoveryCode == "")
            {
                MessageBox.Show("IMPORTANT: Please write the following information as you might need it if the update fails:\n\n--->>> Build: " + device.build + ", CPU: " + device.cpu + " <<<---",
                                "Important message!", MessageBoxButtons.OK);
            }

            return ascii;
        }

        private byte[] getFirmware(bool fromFile)
        {
            long size;
            string ascii;
            byte[] binary;

            if (fromFile)
                ascii = readFirmware();
            else
                ascii = downloadFirmware();

            if (ascii == null)
                return null;

            try
            {
                size = ascii.Length / 2;
                binary = new byte[size];

                for (int i = 0, j = 0; i < size; i++, j += 2)
                    binary[i] = (byte)Convert.ToInt16(ascii.Substring(j, 2), 16);
            }
            catch (Exception ex)
            {                
                showMessage(IconBitmap.Error, ex.Message, "");
                return null;
            }

            return binary;
        }


        private bool prepareDevice(CommunicationManager.State state)
        {
            /* the device is connected, therefore we can find out details about it */
            switch (state)
            {
                case CommunicationManager.State.LoggedIn:
                    showMessage(IconBitmap.Info, "Preparing device for update...", "");
                    if (comm.sendCmd("fyk") && comm.getResult())
                    {
                        comm.sendCmd("r");
                        string st = comm.waitForData(false);
                        if (st != "" && !st.Contains("boot v"))
                        {
                            showMessage(IconBitmap.Error, "Firmware update not supported by device.", "");
                            return false;
                        }
                        if (st != "")
                            System.Threading.Thread.Sleep(1000); // wait for the bootloader to start
                    }
                    else
                    {
                        showMessage(IconBitmap.Error, "Preparation for firmware update failed", "");
                        return false;
                    }
                    break;

                case CommunicationManager.State.PortClosed:
                    showMessage(IconBitmap.Error, "The port is closed", "");
                    return false;
            }

            return true;

        }

        private void relogin(bool wasLoggedIn)
        {
            if (!wasLoggedIn)
                return;
            comm.sendCmd("l" + txtPassword.Text);
            if (checkState() == CommunicationManager.State.LoggedOut)
            {
                showMessage(IconBitmap.Error, "LogIn Failed.", "LogIn Failed. Is the password correct?");
            }
        }

        private void firmwareUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            firmwareUpdate(true);
        }

        private void firmwareUpdate(bool fromFile)
        {
            bool wasLoggedIn = false;
            byte [] firmware;
            string oldname = groupBoxStep2.Text;

            /* prepare GUI for choosing firmware */
            cmdLogIn.Enabled = false;
            groupBoxStep2.Enabled = false;

            if (keyPressed == KeyModifiers.Shift)
            {
                showMessage(IconBitmap.Info, "Remove reader and plug it back in.", "");

                bool ok = comm.enterBootLoader();
                groupBoxStep2.Enabled = true;
                cmdLogIn.Enabled = true;
                gotoStep(2);
                if (ok)
                {
                    showMessage(IconBitmap.Info, "Bootloader activated.", "You have 60 seconds to update.");
                    return;
                }
                showMessage(IconBitmap.Info, "Failed to activate bootloader.", "");
                return;
            }

            CommunicationManager.State state = comm.Status();

            if (state == CommunicationManager.State.LoggedOut)
            {
                showMessage(IconBitmap.Error, 
                    "Device requires logging in first.", 
                    "You are trying to update a working device. In order to do this you have to log in first.");
                groupBoxStep2.Enabled = true;
                cmdLogIn.Enabled = true;
                return;
            }
            
            firmware = getFirmware(fromFile);
            if (firmware == null || !prepareDevice(state))
            {
                groupBoxStep2.Enabled = true;
                cmdLogIn.Enabled = true;
                return;
            }

            wasLoggedIn = (state == CommunicationManager.State.LoggedIn);
            /* prepare GUI for upload */
            enableProgress(true);
            groupBoxStep2.Location = commandBoxLocation;
            groupBoxStep2.Text = "Firmware update";
            groupBoxStep2.Enabled = true;

            groupBoxStep1.Visible = false;
            groupBoxStep2.Visible = true;

            showMessage(IconBitmap.Info, "Updating firmware ...", "");
            try 
            {
                comm.uploadFirmware(firmware);
                string st;
                st = comm.waitForData(true);
                showMessage(IconBitmap.Info, "Firmware update complete.", "");
            }
            catch (Exception ex)
            {
                showMessage(IconBitmap.Error, ex.Message, ex.Message);
#if DEBUG
                TaskDialog.MessageBox(this,
                    "Update Error",
                    ex.Message + "\n\n",
                    "",
                    "",
                    TaskDialogButtons.OK,
                    SysIcons.Error);
#endif
            }

            device.newDevice = true; /* force reading new capabilities, which we might have after a firmware update */
            groupBoxStep2.Visible = false;
            relogin(wasLoggedIn);
            enableProgress(false);
            groupBoxStep2.Text = oldname;
            cmdLogIn.Enabled = true;
            updateStats(false);
            CommunicationManager.State status = checkState();
            if (!fromFile && status == CommunicationManager.State.LoggedIn)
                notifyUpdate();
        }

        private void validatingTextBox(object sender, CancelEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Text != t.Text.Trim())
            {
                t.Text = t.Text.Trim();
                showMessage(IconBitmap.Warning, "Some spaces were removed.", "We have removed all occurrences of white space characters from the beginning and end of this textbox as we do not accept them");
            }
            
        }

        private void portChanged(object sender, EventArgs e)
        {
            gotoStep(1);
        }

        private void firmwareUpdatefromInternetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            firmwareUpdate(false);
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process p;
            p = Process.Start("http://www.cardreaderfactory.com/support/index.php?_m=knowledgebase&_a=view&parentcategoryid=1&pcid=0&nav=0");
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process p;
            p = Process.Start("http://www.cardreaderfactory.com/bugtracker/");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.AboutBox box = new Forms.AboutBox();
            box.Show();
        }

        private void downloadUpdate(Version version, string link)
        {
            WebClient client = new WebClient();

            string temp = Path.GetTempFileName();
            string oldExe = Program.directoryName + "\\" + Program.fileName + "_" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ".exe";

            if (MessageBox.Show("New update available: " + version.ToString() + "\n\nIf you choose to continue, the program will restart once the update is complete.\n\nWould you like to proceed with the update?", Program.name + " update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                if (File.Exists(temp))
                    try { File.Delete(temp); } catch {};
                temp = Program.directoryName + "\\" + Path.GetFileName(temp);

                if (File.Exists(temp))
                    try { File.Delete(temp); } catch {} ;
                if (File.Exists(oldExe))
                    try { File.Delete(oldExe); } catch { } ;
                showMessage(IconBitmap.Info, "Downloading update ...", "");

                client.DownloadFile(link, temp);
                File.Move(Program.executablePath, oldExe);
                File.Move(temp, Program.executablePath);

                Process restart = new Process();
                //restart.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                restart.StartInfo.FileName = Program.executablePath;
                restart.StartInfo.Arguments = "-del \"" + oldExe + "\"";
                restart.Start();
                this.Close();
            }
            catch (Exception ex)
            {
                showMessage(IconBitmap.Error, "Update error: " + ex.Message, "Update error: " + ex.Message);
#if DEBUG
                TaskDialog.MessageBox(this, 
                    "Update Error",
                    ex.Message,
                    "",
                    "",
                    TaskDialogButtons.OK,
                    SysIcons.Error);
#endif
            }

            try
            {
                if (File.Exists(temp))
                    try { File.Delete(temp); } catch { };
                if (File.Exists(oldExe))
                    try { File.Delete(oldExe); } catch { };
            }
            catch { };

        }


        private void checkUpdate()
        {
            showMessage(IconBitmap.Info, "Checking for update...", "");
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "CRFSuite (" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")");
            try
            {
                string[] serverReply;
                string link;
                Version version;
                serverReply = client.DownloadString(getsoftwareLink).Split('^');
                version = new Version(Device.getValue(serverReply, "version"));
                link = Device.getValue(serverReply, "link");
                if (version > Assembly.GetExecutingAssembly().GetName().Version)
                {
                    downloadUpdate(version, link);
                }
                else
                {
                    showMessage(IconBitmap.Info, "No updates are available", "");
                }
            }
            catch (WebException ex)
            {
                if (this != null)
                {
                    showMessage(IconBitmap.Error, "Update check failed", "Check for updates:\n\n" + ex.Message);
                    //MessageBox.Show("Check for updates:\n\n" + ex.Message, "Checking update failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //showMessage(IconBitmap.NoIcon, "Ready", "");
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkUpdate();
        }

        private void installToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Please select the folder where you want " + Program.name + " to be installed.";
            dialog.RootFolder = Environment.SpecialFolder.MyComputer;
            
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            string destination = dialog.SelectedPath + "\\" + Path.GetFileName(Program.executablePath);

            try
            {
                File.Copy(Program.executablePath, destination);
            }
            catch (Exception ex)
            {
                showMessage(IconBitmap.Error, ex.Message, "Error: " + ex.Message);
                return;
            }
            showMessage(IconBitmap.Info, Program.name + " installed successfully.", Program.name + " installed successfully in " + dialog.SelectedPath);
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Preferences p = new Forms.Preferences();

            p.ShowDialog();
            
        }

    }
}


