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
using crf.Properties;
using System.Diagnostics;
using TaskDialogDLL;

namespace crf
{
    public partial class EnterKey : Form
    {
        /**
         * if users presses accept button but key is not correct this member will be false so form will not be closed.
         */
        private bool _canClose;
        private bool _initializing;
        private string build = "";

        public EnterKey(string b)
        {
            _initializing = true;
            InitializeComponent();
            _canClose = true;
            build = b;

            showKey.Checked = Settings.Default.showKey;
            rememberKey.Checked = Settings.Default.rememberKey;

            cmdRequestKey.Visible = Program.enableInternetUpdates;
            _initializing = false;
        }

        private void checkShow_CheckedChanged(object sender, EventArgs e)
        {
            key.UseSystemPasswordChar = !showKey.Checked;
            key.Focus();
        }

        private void EnterKey_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_canClose)
            {
                e.Cancel = true;
                _canClose = true;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (key.Text.Length != 32)
            {
                MessageBox.Show("Password does not have 32 characters.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _canClose = false;
            }
            else if (!StringUtil.isValidKey(key.Text))
            {
                MessageBox.Show("Password has invalid characters.\nValid characters are numbers (0..9) and letters from A to F.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _canClose = false;
            }
            else
            {
                Settings.Default.showKey = showKey.Checked;
                Settings.Default.rememberKey = rememberKey.Checked;
                if (rememberKey.Checked)
                    KeyManager.Instance.addKey(key.Text); /* saves the settings too */
                else
                    Settings.Default.Save();
            }
        }

        private void rememberKey_CheckedChanged(object sender, EventArgs e)
        {
            if ( !_initializing && rememberKey.Checked && 
                 Program.keysEncryptionWarning.Length > 0 && 
                 Program.showKeysEncryptionWarning )
            {
                Program.updateKeyEncriptionWarning(this);
            }
        }

        private void cmdRequestKey_Click(object sender, EventArgs e)
        {
            if (Program.enableInternetUpdates)
            {
                string st = "";
                if (build != "")
                    st = "Your device build number is: " + build + "\n\n";
                if (MessageBox.Show("For insurance purposes, we ship all our devices without a key. "+
                    "Please click YES to go to our website and complete the form with the build number of your device " +
                    "so we can confirm that is has been delivered. We will send the key via email " +
                    "within 24 hours. This procedure allows us to replace any lost packages without " + 
                    "having to charge anything to the customer.\n\n" + st +
                    "Would you like to request your key now?\nNote: Internet connection is required.", 
                    "Where is my key?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process p;
                    p = Process.Start("http://www.cardreaderfactory.com/shop/key-request.html");
                }
            }
            else
            {
                MessageBox.Show(Utils.wordWrap("Your key is provided by your distribuitor togheter with your reader; " +
                    "it has 32 characters and contains numbers from '0' to '9' and letters from 'A' to 'F'." +
                    "\n\nExample (this is not your key!): 87651f70af61eff4791041e7b27e148e ", 60), 
                    "Where is my key?", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

    }
}
