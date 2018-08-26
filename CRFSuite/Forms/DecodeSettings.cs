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
using crf.Presentation;
using TaskDialogDLL;

namespace crf.Forms
{
    public partial class DecodeSettings : Form
    {
        private enum DecodeFormat
        {
            ISO_7811,
            AAMVA,
            California,
            Reverse,
            ABA,
            Custom
        }

        private enum Intervals
        {
            Minutes = 0,
            Seconds = 1,
            Miliseconds = 2
        }

        public enum ValidChars
        {
            All = 0,
            ValidCRC = 1,
            LongestValidCRC = 2
        };

        public enum Change
        {
            DecoderType,
            ShowDirection,
            ShowTime,
            TimeFormat,
            DecodeMethod,
            ShowTrack,
            GroupSwipes,
            AlignChars,
            TrackBPC,
            SmartDecoding,
            SettingsClosed
        }

        struct EncodingStandard
        {
            public byte t1Bpc, t2Bpc, t3Bpc, t1Start, t2Start, t3Start;
            public EncodingStandard(byte t1Bpc, byte t2Bpc, byte t3Bpc, byte t1Start, byte t2Start, byte t3Start)
            {
                this.t1Bpc = t1Bpc;
                this.t2Bpc = t2Bpc;
                this.t3Bpc = t3Bpc;
                this.t1Start = t1Start;
                this.t2Start = t2Start;
                this.t3Start = t3Start;
            }
        };
          
        EncodingStandard[] standards = new EncodingStandard[]
        { 
            /* ISO_7811 */      new EncodingStandard ( 7, 5, 5, 0x20, 0x30, 0x30 ),
            /* AAMVA */         new EncodingStandard ( 7, 5, 7, 0x20, 0x30, 0x20 ),
            /* California */    new EncodingStandard ( 6, 5, 6, 0x20, 0x30, 0x20 ),
            /* Reverse */       new EncodingStandard ( 5, 5, 7, 0x30, 0x30, 0x20 ),
            /* ABA */           new EncodingStandard ( 5, 5, 5, 0x30, 0x30, 0x30 ),
        };

        private bool hasChanges;
        private int lastStandard;
        private DecodeSettingsTrack t1, t2, t3;
        private bool changingStandards = false;
        private int t1Count = 0, t2Count = 0, t3Count = 0, total = 0;
        public delegate void SettingChangedHandle(Change type, object value);
        public SettingChangedHandle applyChange;
        private string simpleDecoderWarning = "The Simple Decoder has limited functionality:\n\n" +
            "1. If the card cannot be interpreted correctly you might see incorrect interpreted data " +
            "(error that is easily corrected in the advanced decoder).\n\n2. Any changes made in the " +
            "Simple Decoder can only be saved in text mode therefore you will not be able to edit your " +
            "changes later in the advanced decoder.\n\n3. If you decide to return later to the Advanced " +
            "Decoder, you will loose any changes that you have made while working with the Simple Decoder.";

        #region Singleton implementation

        /** Returns the Settings instance */
        private static DecodeSettings _instance = null; /** Singleton instance. Each decode form will create each own settings instance */
        public static DecodeSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DecodeSettings();

                return _instance;
            }

            set
            {
                _instance = value;
            }
        }
        #endregion



        public DecodeSettings(int t1, int t2, int t3, int t)
        {
            InitializeComponent();
            this.t1Count = t1;
            this.t2Count = t2;
            this.t3Count = t3;
            this.total = t;
            loadSettings();
        }

        public DecodeSettings()
        {
            InitializeComponent();
            loadSettings();
        }

        private void loadSettings()
        {
            groupSwipes.Text = Settings.Default.groupSwipesInterval.ToString();
            groupSwipesUnits.SelectedIndex = Settings.Default.groupSwipesIntervalUnit;
            alignChars.Text = Settings.Default.alignChars.ToString();
            direction.Checked = Settings.Default.showDirection;
            radioAdvancedMode.Checked = Settings.Default.showAdvancedDecoder;
            radioSimpleMode.Checked = !Settings.Default.showAdvancedDecoder;
            time.Checked = Settings.Default.showTime;
            timeFormat.Enabled = time.Checked;
            timeFormat.SelectedIndex = Settings.Default.showTimeFormat;
            decodeMethod.SelectedIndex = Settings.Default.decodeMethod;
            t1 = new DecodeSettingsTrack(ref showTrack1, ref track1BPC, ref track1Start, Settings.Default.showTrack1, VariantSettings.bpc[0], VariantSettings.add[0], this, 0);
            t2 = new DecodeSettingsTrack(ref showTrack2, ref track2BPC, ref track2Start, Settings.Default.showTrack2, VariantSettings.bpc[1], VariantSettings.add[1], this, 1);
            t3 = new DecodeSettingsTrack(ref showTrack3, ref track3BPC, ref track3Start, Settings.Default.showTrack3, VariantSettings.bpc[2], VariantSettings.add[2], this, 2);
            decodeFormat.SelectedIndex = Settings.Default.decodeFormat;

            if (total > 0)
            {
                track1Stats.Text = t1Count + " cards (" + (t1Count * 100 / total) + "%)";
                track2Stats.Text = t2Count + " cards (" + (t2Count * 100 / total) + "%)";
                track3Stats.Text = t3Count + " cards (" + (t3Count * 100 / total) + "%)";
            }

            setStandard();

            showDecodeSettings.Checked = Settings.Default.showDecodeSettings;
            hasChanges = false;
        }

        public static int groupSwipesInterval
        {
            get
            {
                int multiplier;
                switch (Properties.Settings.Default.groupSwipesIntervalUnit)
                {
                    case (int)Intervals.Minutes: /* minutes */
                        multiplier = 60 * 1000;
                        break;
                    case (int)Intervals.Seconds: /* seconds */
                        multiplier = 1000;
                        break;
                    default: /* miliseconds */
                        multiplier = 1;
                        break;
                }
                return Properties.Settings.Default.groupSwipesInterval * multiplier;
            }
            set
            {
                if (value % (60 * 1000) == 0)
                {
                    Properties.Settings.Default.groupSwipesIntervalUnit = (int)Intervals.Minutes;
                    Properties.Settings.Default.groupSwipesInterval = value / (60 * 1000);
                }
                else if ((value % 1000) == 0)
                {
                    Properties.Settings.Default.groupSwipesIntervalUnit = (int)Intervals.Seconds;
                    Properties.Settings.Default.groupSwipesInterval = value / 1000;
                }
                else
                {
                    Properties.Settings.Default.groupSwipesIntervalUnit = (int)Intervals.Miliseconds;
                    Properties.Settings.Default.groupSwipesInterval = value;
                }
            }
        }

        public bool getShowTrack(int track)
        {
            switch (track)
            {
                case 0:
                    return Settings.Default.showTrack1;
                case 1:
                    return Settings.Default.showTrack2;
                case 2:
                    return Settings.Default.showTrack3;
                default:
                    return false;
            }
        }

        public byte getBPC(int track)
        {
            return VariantSettings.bpc[track];
        }

        public byte getStart(int track)
        {
            return VariantSettings.add[track];
        }

        public void trackChanged(int trackNumber)
        {
            if (!changingStandards)
            {
                decodeFormat.SelectedIndex = (int)DecodeFormat.Custom;
                applyChange(DecodeSettings.Change.TrackBPC, trackNumber);
            }
        }

        private void setStandard()
        {
            changingStandards = true;
            int i = decodeFormat.SelectedIndex;
            if (i != (int)DecodeFormat.Custom)
            {
                //bool b = Properties.Settings.Default.autoResize;
                //Properties.Settings.Default.autoResize = false;
                SettingChangedHandle a = applyChange;
                applyChange = null;
                t1.bpc = standards[i].t1Bpc;
                t2.bpc = standards[i].t2Bpc;
                t3.bpc = standards[i].t3Bpc;
                t1.start = standards[i].t1Start;
                t2.start = standards[i].t2Start;
                t3.start = standards[i].t3Start;
                VariantSettings.add[0] = t1.start;
                VariantSettings.add[1] = t2.start;
                VariantSettings.add[2] = t3.start;
                applyChange = a;
                onChange(Change.TrackBPC, 0);
                onChange(Change.TrackBPC, 1);
                //Properties.Settings.Default.autoResize = b;
                onChange(Change.TrackBPC, 2);
            }
            lastStandard = decodeFormat.SelectedIndex;
            changingStandards = false;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (hasChanges)
            {
                Settings.Default.Reload();
                if (time.Checked != Settings.Default.showTime)
                    onChange(Change.ShowTime, Settings.Default.showTime);

                if (direction.Checked != Settings.Default.showDirection)
                    onChange(Change.ShowDirection, Settings.Default.showDirection);

                if (showTrack1.Checked != Settings.Default.showTrack1)
                    onChange(Change.ShowTrack, 0);
                if (showTrack2.Checked != Settings.Default.showTrack2)
                    onChange(Change.ShowTrack, 1);
                if (showTrack3.Checked != Settings.Default.showTrack3)
                    onChange(Change.ShowTrack, 2);
                if (groupSwipes.Text != Settings.Default.groupSwipesInterval.ToString() || groupSwipesUnits.SelectedIndex != Settings.Default.groupSwipesIntervalUnit)
                    onChange(Change.GroupSwipes, groupSwipesInterval); /* groupSwipesInterval is calculated from Settings.Default.* */
                if (timeFormat.SelectedIndex != Settings.Default.showTimeFormat)
                    onChange(Change.TimeFormat, Settings.Default.showTimeFormat);
                if (decodeMethod.SelectedIndex != Settings.Default.decodeMethod)
                    onChange(Change.DecodeMethod, Settings.Default.decodeMethod);
                if (alignChars.Text != Settings.Default.alignChars.ToString())
                    onChange(Change.AlignChars, Settings.Default.alignChars.ToString());
                if (radioAdvancedMode.Checked != Settings.Default.showAdvancedDecoder)
                    onChange(Change.DecoderType, Settings.Default.showAdvancedDecoder);
            }
            this.Close();
        }


        private void direction_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.showDirection = direction.Checked;
            onChange(Change.ShowDirection, direction.Checked);
        }

        private void time_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.showTime = time.Checked;
            timeFormat.Enabled = time.Checked;
            onChange(Change.ShowTime, time.Checked);
        }

        private void timeFormatChanged(object sender, EventArgs e)
        {
            onChange(Change.TimeFormat, timeFormat.SelectedIndex);
        }

        private void decodeMethodChanged(object sender, EventArgs e)
        {
            Settings.Default.decodeMethod = decodeMethod.SelectedIndex;
            onChange(Change.DecodeMethod, decodeMethod.SelectedIndex);
        }

        private static int readIntFromTextBox(TextBox t, int lastValue)
        {
            int r;
            try
            {
                r = (int)Convert.ToUInt32(t.Text);
            }
            catch
            {
                r = lastValue;
                t.Text = lastValue.ToString();
                MessageBox.Show("Only positive numbers are allowed in this field", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return r;
        }

        private void standardChanged(object sender, EventArgs e)
        {
            if (decodeFormat.SelectedIndex != lastStandard)
            {
                setStandard();
                Settings.Default.decodeFormat = decodeFormat.SelectedIndex;
            }
        }

        private void showDecodeSettings_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.showDecodeSettings = showDecodeSettings.Checked;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            this.Close();
        }

        private void groupSwipes_Validating(object sender, CancelEventArgs e)
        {
            int lastValue = Settings.Default.groupSwipesInterval;
            Settings.Default.groupSwipesInterval = readIntFromTextBox(groupSwipes, lastValue);
            onChange(Change.GroupSwipes, groupSwipesInterval);
            groupSwipesUnits.SelectedIndex = Settings.Default.groupSwipesIntervalUnit;
        }
        private void alignChars_Validating(object sender, CancelEventArgs e)
        {
            int lastValue = Settings.Default.alignChars;
            Settings.Default.alignChars = readIntFromTextBox(alignChars, lastValue);
            onChange(Change.AlignChars, alignChars.Text);
        }

        private void groupSwipesUnitsChanged(object sender, EventArgs e)
        {
            Settings.Default.groupSwipesIntervalUnit = groupSwipesUnits.SelectedIndex;
            onChange(Change.GroupSwipes, groupSwipesInterval);
        }

        private void showAdvanceDecode_Changed(object sender, EventArgs e)
        {

            if (Settings.Default.showAdvancedDecoder != radioAdvancedMode.Checked)
            {

                if (!radioAdvancedMode.Checked && Settings.Default.showDecoderSwitchWarning)
                {
                    DialogResult r = TaskDialog.MessageBox(this, 
                                        "Warning", 
                                        "Are you sure that you want to continue?", 
                                        simpleDecoderWarning, 
                                        "Don't show this message again for this document",
                                        TaskDialogButtons.YesNo,
                                        SysIcons.Warning);

                    Settings.Default.showDecoderSwitchWarning = !TaskDialog.VerificationChecked;
                    if ( r == DialogResult.No)
                    {
                        radioAdvancedMode.Checked = Settings.Default.showAdvancedDecoder;
                        radioSimpleMode.Checked = !Settings.Default.showAdvancedDecoder;
                        return;
                    }
                }

                Settings.Default.showAdvancedDecoder = radioAdvancedMode.Checked;
                onChange(Change.DecoderType, radioAdvancedMode.Checked);
            }
        }

        private void fClosing(object sender, FormClosingEventArgs e)
        {
            onChange(Change.SettingsClosed, null);
        }

        public void onChange(Change type, object o)
        {
            if (applyChange != null)
                applyChange(type, o);
            if (type != Change.SettingsClosed)
                hasChanges = true;
        }

        private void DecodeSettings_Shown(object sender, EventArgs e)
        {
            //this.Refresh();
        }

    }
}
