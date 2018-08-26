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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using crf.Properties;

namespace crf.Forms
{
    public partial class Preferences : Form
    {
        private string maximumPrivacyTooltip = 
                "If enabled, this option will:\n\n" +
                " a) Remove any functionality to update the device's firmware or this software.\n" +
                " b) Disable any communication over the network.\n" +
                " c) Remove any reference to '" + Program.name + "' or to 'CardReaderFactory' from the whole program.\n"+
                " d) Remove all the menus that provide the functionality mentioned above.\n\n" +               
                "In order to do this '" + Program.name + ".exe' will be renamed into the name of your choice.\n\n" +
                "Please note that this menu will also disappear and the only way to reenable the disabled functionality is to rename the file back to '" + Program.name + ".exe' manually.";
        private string editDisabled = "To prevent anybody from peeking over your shoulder, View & Edit Keys is currently disabled. To enable this feature, click on the \"Edit Keys\" check box from the right hand side of this text box.";
        private bool _allowApply;
        private bool _isAssociated;
        int keyCount = 0;

        ToolTip keysToolTip = new ToolTip();

        public Preferences()
        {
            InitializeComponent();
            askToChangeDefaultPassCheckBox.Checked = Settings.Default.askToChangeDefaultPass;
            switch ((DownloadBehaviour)Settings.Default.openAfterDownload)
            {
                case DownloadBehaviour.Ask:
                    radioDownloadAsk.Checked = true;
                    break;
                case DownloadBehaviour.Open:
                    radioDownloadYes.Checked = true;
                    break;
                case DownloadBehaviour.DoNothing:
                    radioDownloadNo.Checked = true;
                    break;
            }
            keepFileAssociationsCheckBox.Checked = Settings.Default.checkFileAssociations;
            isAssociated = FileAssociation.isAssociated(Program.extension, Program.executablePath);
            updateKeyCount();
            if (Program.enableInternetUpdates)
            {
                updatePrivacy();
                toolTip.SetToolTip(removeOEMcheckBox, Utils.wordWrap(maximumPrivacyTooltip, 65));
                toolTip.IsBalloon = true;
            }
            else
            {
                tabControl1.Controls.Remove(this.tabPage3);
            }

            editKeysCheckBox_CheckedChanged(null, null);
            allowApply = false;
        }

        public bool allowApply
        {
            get { return _allowApply; }
            set
            {
                _allowApply = value;
                cmdApply.Enabled = value;
            }
        }

        public bool isAssociated
        {
            get { return _isAssociated; }
            set
            {
                _isAssociated = value;
                if (isAssociated)
                {
                    associationStatusLabel.Text = Program.name + " is currently associated with ." + Program.extension.ToUpper() + " files";
                    associationStatusLabel.ForeColor = Color.Green;
                    cmdAssociateCRF.Text = "Deassociate from ." + Program.extension.ToUpper() + " files";
                }
                else
                {
                    associationStatusLabel.Text = Program.name + " is not currently associated with ." + Program.extension.ToUpper() + " files";
                    associationStatusLabel.ForeColor = Color.Red;
                    cmdAssociateCRF.Text = "Associate with ." + Program.extension.ToUpper() + " files";
                }
            }
        }
      
        private void keepFileAssociationsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            allowApply = true;
            Settings.Default.checkFileAssociations = keepFileAssociationsCheckBox.Checked;
        }

        private void askToChangeDefaultPassCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            allowApply = true;
            Settings.Default.askToChangeDefaultPass = askToChangeDefaultPassCheckBox.Checked;
        }

        [DllImport("shell32.dll")]
        static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner,
           [Out] StringBuilder lpszPath, int nFolder, bool fCreate);
        const int CSIDL_COMMON_PROGRAMS = 0x0017;
        const int CSIDL_PROGRAMS = 0x0002;

        private void updateStartMenu(string startMenu, string oldFullPath, string newFullPath)
        {
            string oldName = Path.GetFileNameWithoutExtension(oldFullPath);
            string newName = Path.GetFileNameWithoutExtension(newFullPath);
            string linkName = startMenu + "\\" + oldName + "\\" + oldName + ".lnk";
            if (!File.Exists(linkName))
                return;

            ShellLink shortcut = new ShellLink(linkName);
            if (Path.GetFullPath(shortcut.Target).ToLower() != oldFullPath.ToLower())
                return; /* doesn't refer to us therefore we don't care */

            try
            {
                shortcut.Target = newFullPath;
                shortcut.WorkingDirectory = Path.GetDirectoryName(newFullPath);
                shortcut.Description = newName;
                shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
                shortcut.Save();

                File.Move(linkName, startMenu + "\\" + oldName + "\\" + newName + ".lnk");
                Directory.Move(startMenu + "\\" + oldName, startMenu + "\\" + newName);
            }
            catch
            {};
        }

        private bool renameProgram(string newName)
        {
            try
            {
                newName = Path.GetFullPath(Path.GetFileNameWithoutExtension(newName) + ".exe");                

                /* rename the file */
                System.IO.File.Move(Program.executablePath, newName);

                /* change start menu */
                StringBuilder path = new StringBuilder(260);
                string common, user;
                SHGetSpecialFolderPath(IntPtr.Zero, path, CSIDL_COMMON_PROGRAMS, false);
                common = path.ToString();
                SHGetSpecialFolderPath(IntPtr.Zero, path, CSIDL_PROGRAMS, false);
                user = path.ToString();

                updateStartMenu(common, Program.executablePath, newName);
                updateStartMenu(user, Program.executablePath, newName);

                Program.updatePath(newName);
            }
            catch (Exception ex)
            { 
                MessageBox.Show("There has been an error while renaming the program\n\n" + ex.Message, "Remaing failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            };

            return true;
        }

        private bool apply()
        {
            isAssociated = FileAssociation.isAssociated(Program.extension, Program.executablePath);

            if (removeOEMcheckBox.Checked && programNameTextBox.Text.Length > 0 && !renameProgram(programNameTextBox.Text))
                return false;

            if (editKeysCheckBox.Checked)
                KeyManager.Instance.setKeys(keysTextBox.Lines); /* saves the settings too */
            else
                Settings.Default.Save();

            return true;
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            allowApply = !apply();
            if (!allowApply)                
                updateKeyCount();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (apply())
                this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAssociateCRF_Click(object sender, EventArgs e)
        {
            if (isAssociated)
                FileAssociation.deleteAssociation(Program.extension);
            else
                FileAssociation.Associate(Program.extension, Program.executablePath);

            isAssociated = FileAssociation.isAssociated(Program.extension, Program.executablePath);
        }

        private void removeOEMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            updatePrivacy();
        }

        private void updatePrivacy()
        {
            //swUpdatesCheckBox.Enabled = fwUpdatesCheckBox.Enabled = !checkBox1.Checked;
            label1.Enabled = programNameTextBox.Enabled = removeOEMcheckBox.Checked;
            updateButtons();
            if (removeOEMcheckBox.Checked)
                programNameTextBox.Focus();
        }

        private void updateButtons()
        {
            if ( (programNameTextBox.Text.Trim().Length > 0 && programNameTextBox.Text.Trim().ToLower() != Program.defaultname) ||
                 (!removeOEMcheckBox.Checked) )
                cmdOk.Enabled = cmdApply.Enabled = true;
            else
                cmdOk.Enabled = cmdApply.Enabled = false;
        }

        private void nameChanged(object sender, EventArgs e)
        {
            updateButtons();            
        }

        private void cmdEraseKeys_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to erase " + keyCount + " keys?",
                "Erase keys?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                KeyManager.Instance.eraseKeys();
                updateKeyCount();
            }
        }

        private void updateKeyCount()
        {
            keyCount = KeyManager.Instance.getKeyCount();
            if (keyCount != 1)
                keyCountLabel.Text = keyCount + " keys stored";
            else
                keyCountLabel.Text = keyCount + " key stored";

            if (editKeysCheckBox.Checked)
                keysTextBox.Lines = KeyManager.Instance.getKeys();

            cmdEraseKeys.Enabled = (keyCount != 0);
        }

        private void keysChanged(object sender, EventArgs e)
        {
            allowApply = true;
        }

        private void editKeysCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            keysTextBox.Enabled = editKeysCheckBox.Checked;
            if (editKeysCheckBox.Checked)
            {

                if (Program.keysEncryptionWarning.Length > 0 && Program.showKeysEncryptionWarning)
                {
                    Program.updateKeyEncriptionWarning(this);
                }
                
                keysTextBox.Lines = KeyManager.Instance.getKeys();
            }
            else
            {
                keysTextBox.Text = editDisabled;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.openAfterDownload = (int)DownloadBehaviour.Open;
        }

        private void radioDownloadNo_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.openAfterDownload = (int)DownloadBehaviour.DoNothing;

        }

        private void radioDownloadAsk_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.openAfterDownload = (int)DownloadBehaviour.Ask;
        }



    }
}
