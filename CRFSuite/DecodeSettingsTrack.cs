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
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using crf.Properties;
using crf.Forms;

namespace crf
{

    class DecodeSettingsTrack
    {
        int trackNumber;
        CheckBox showCheckBox;
        ComboBox bpcComboBox;
        TextBox startTextBox;
        DecodeSettings parent;
        byte lastValue = 0;

        public bool show
        {
            get { return showCheckBox.Checked; }
            //set { showCheckBox.Checked = value; updateBoxes(); }
        }

        public byte bpc
        {
            get { return readCombo(bpcComboBox); }
            set { bpcComboBox.SelectedIndex = bpcComboBox.FindStringExact(value.ToString()); updateBoxes(); }
        }

        public byte start
        {
            get { return readHexFromTextBox(startTextBox, lastValue, getMaxStartValue(readCombo(bpcComboBox)), false); }
            set { startTextBox.Text = "0x" + value.ToString("x"); }
        }


        byte readCombo(ComboBox c)
        {
            byte value = 5;
            try
            {
                value = Convert.ToByte((string)c.SelectedItem);
            }
            catch { };
            return value;
        }

        public DecodeSettingsTrack(ref CheckBox s, ref ComboBox b, ref TextBox st,
                                   bool sShow, int sBpc, int sStart,
                                   DecodeSettings p, int t)
        {

            trackNumber = t;
            showCheckBox = s;
            bpcComboBox = b;
            startTextBox = st;
            parent = p;
            lastValue = (byte)sStart;

            showCheckBox.Checked = sShow;
            bpcComboBox.SelectedIndex = bpcComboBox.FindStringExact(sBpc.ToString());
            startTextBox.Text = "0x" + sStart.ToString("x");

            updateBoxes();

            showCheckBox.CheckedChanged += new System.EventHandler(this.showTrack_CheckedChanged);
            bpcComboBox.SelectedIndexChanged += new System.EventHandler(this.BPCChanged);
            startTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.startValidating);
        }

        void updateBoxes()
        {
            bpcComboBox.Enabled = showCheckBox.Checked;
            startTextBox.Enabled = showCheckBox.Checked && (getMaxStartValue(readCombo(bpcComboBox)) != 0);
        }

        private void showTrack_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
            switch (trackNumber)
            {
                case 0:
                    Settings.Default.showTrack1 = showCheckBox.Checked;
                    break;
                case 1:
                    Settings.Default.showTrack2 = showCheckBox.Checked;
                    break;
                case 2:
                    Settings.Default.showTrack3 = showCheckBox.Checked;
                    break;
            }

            if (parent != null)
                parent.onChange(DecodeSettings.Change.ShowTrack, trackNumber);
            
        }

        private void BPCChanged(object sender, EventArgs e)
        {
            updateBoxes();
            byte value = readCombo(bpcComboBox); 
            switch (value)
            {
                case 5:
                    startTextBox.Text = "0x30";
                    VariantSettings.add[trackNumber] = 0x30;
                    break;
                case 6:
                case 7:
                    startTextBox.Text = "0x20";
                    VariantSettings.add[trackNumber] = 0x20;
                    break;
                case 9:
                    startTextBox.Text = "0x00";
                    VariantSettings.add[trackNumber] = 0x00;
                    break;
            }

            VariantSettings.bpc[trackNumber] = value;

            if (parent != null)
                parent.trackChanged(trackNumber);
        }


        private void startValidating(object sender, CancelEventArgs e)
        {
            byte value;
            value = readHexFromTextBox(startTextBox, lastValue, getMaxStartValue(readCombo(bpcComboBox)), false);
            if (lastValue != value)
            {
                VariantSettings.add[trackNumber] = value;

                if (parent != null)
                    parent.trackChanged(trackNumber);
                lastValue = value;
            }
        }

        public static int getMaxStartValue(int bits)
        {
            return (0x100 - (1 << (bits - 1)));
        }

        private static byte readHexFromTextBox(TextBox t, byte lastValue, int maxValue, bool quiet)
        {
            byte r;
            try
            {
                int len = t.Text.Length;
                if (len > 2 && t.Text.Substring(0, 2).ToLower() == "0x")
                {
                    r = Convert.ToByte(t.Text.Substring(2, len - 2), 16);
                }
                else
                {
                    r = Convert.ToByte(t.Text);
                }

                if (r > maxValue)
                    throw new Exception("out of range");
                t.Text = "0x" + r.ToString("x");
            }
            catch
            {
                r = lastValue;
                t.Text = "0x" + lastValue.ToString("x");
                if (!quiet)
                    MessageBox.Show("Invalid start value\nValid range: 0x00 .. 0x" + maxValue.ToString("x") + " ( 0 .. " + maxValue + " )", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return r;
        }


    }
}
