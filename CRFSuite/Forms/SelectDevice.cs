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

namespace crf.Forms
{
    public partial class SelectDevice : Form
    {
        public string deviceName = "";
        public string deviceCpu = "";
        public string recoveryCode = "";
        public string deviceBuild = "";
        public DialogResult result = DialogResult.Cancel;

        private bool isDeviceSelected = false;
        private bool isCpuSelected = false;

        public SelectDevice()
        {
            InitializeComponent();
        }

        private void deviceSelected(object sender, EventArgs e)
        {
            isDeviceSelected = true;

            cmdOk.Enabled = (isDeviceSelected && isCpuSelected);
            deviceName = ((RadioButton)sender).Name;
        }

        private void cpuSelected(object sender, EventArgs e)
        {
            isCpuSelected = true;
            cmdOk.Enabled = (isDeviceSelected && isCpuSelected);
            deviceCpu = ((RadioButton)sender).Text;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            int build = -1;

            recoveryCode = recoveryCodeTextBox.Text.Trim().ToLower();

            if (recoveryCode.Length == 0)
            {
                this.Close();
                return;
            }

            char lastchar = recoveryCode[recoveryCode.Length-1];            
            try
            {
                /* strip 'f' or 'd' at the end in case the users writes what is written on the sticker */
                if (lastchar == 'f' || lastchar == 'd')
                {
                    build = Convert.ToInt32(recoveryCode.Substring(0, recoveryCode.Length - 1));
                    deviceBuild = recoveryCode.Substring(0, recoveryCode.Length - 1);
                }
                else
                {
                    build = Convert.ToInt32(recoveryCode);
                    deviceBuild = recoveryCode;
                }                
            }
            catch { };

            if (build != -1)
                recoveryCode = "";

            deviceCpu = "";
            deviceName = "";

            this.Close();
        }

        private void recoveryCodeChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = (recoveryCodeTextBox.Text.Length == 0);
            cmdOk.Enabled = (recoveryCodeTextBox.Text.Length != 0) || (isDeviceSelected && isCpuSelected);
        }
    }
}
