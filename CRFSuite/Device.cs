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
using System.Drawing;
using crf;

namespace crf
{

    class HardwareErrors
    {
        public int code;
        public String message;
        public String tooltip;
        public HardwareErrors(int c, String m, String t)
        {
            code = c;
            message = m;
            tooltip = t;
        }
    }
    class Device
    {
        enum HwErr
        {
            Clock = 0,
            Asic,
            Asic2,
            Memory,
            Bluetooth
        };

        enum LedState
        {
            Blink_Startup = 1<<0,
            Blink_Swipe = 1<<1,
        };

        /* do not modify these fields from outside of this class; everything is updated by calling updateStats() */
        public string stats;             /* the statistics line read from the device */
        public string name;               /* the name of the device */
        public string url;
        public string build;              /* the build number of the device */
        public string firmware;           /* the firmware version */
        public string bluetoothName;      /* the bluetooth device name */
        public string bluetoothPeering;   /* the bluetooth peering password */
        public string readMode;           /* the read mode as reported by the device. eg: "0" or "1" */
        public string hardwareMessage;    /* short message regarding the status of the device */
        public string hardwareTooltip;    /* detailed message regarding the status of the device */
        public string cpu;                /* the cpu name */
        public string fuses;               /* device's fuses */
        public int totalMem;              /* the total amount of the memory */
        public int freeMem;               /* the free amount of memory */
        public int currentSwipes;         /* the swipes that are present in the device memory */
        public int allSwipes;             /* all the swipes that this device has ever read */
        public int timestamp;             /* the timestamp of this device */
        public int hw;              /* the hardware status */
        public bool isDemo = false;       /* true if the device is in demo mode */
        public bool hasReadMode = false;  /* true if the device can change read modes */
        public bool hasBluetooth = false; /* true if the device has bluetooth capabilities */
        public bool canChangeKey = false;
        public bool canChangeLeds = false;
        public bool ledStartup = false;
        public bool ledSwipe = false;


        private HardwareErrors[] hwErrors;
        private DataGridView dataGrid;
        private int idName;
        private int idBuild;
        private int idHardware;
        private int idClock;
        private int idSwipes;
        private int idMemory;
        private bool _newDevice;

        private readonly int bigIconSize = 32;
        private Bitmap bitmapWarning, bitmapError;
        private PictureBox pictureWarning, pictureError;
        private Point pictureLocation;
        private ToolTip pictureBoxTootltip = new ToolTip(); 

        public static int wrapLimit = 50;
        public static string demoWarning = Utils.wordWrap("This device is in demo mode!\n\n" +
                     "This means that it only reads 10 swipes or 10 minutes since power up, whichever comes first. To remove these limitations you have to upgrade it.\n\n" +
                     "If you have bought a full device, please ask your distributor for the upgrade code as soon as possible. If they are unable to provide you this code, please ask for a refund straight away.\n\n" +
                     "If you have bought a demo device, please note that you will need to purchase the upgrade code in order to remove the limitations. This code is about 9 times more expensive than the device itself.\n\n\n", wrapLimit);

        public static string unknown = "Unknown";

        public bool newDevice
        {
            get { return _newDevice; }
            set 
            { 
                _newDevice = value; 
                if (_newDevice) 
                    updateStats("_demo", false); 
            }
        }

        public Device(DataGridView grid, PictureBox pW, PictureBox pE)
        {
            initErrors();
            dataGrid = grid;
            grid.Rows.Clear();
            idName = gridAddValue("Name", unknown, "The name of the connected device");
            idBuild = gridAddValue("Build", unknown, "The build number and the current firmware version of your device");
            idHardware = gridAddValue("State", unknown, "The hardware check of your device");
            idClock = gridAddValue("Clock", unknown, "The device's date and time. This field is updated only when you issue commands or if you click in this part of the window.");
            idSwipes = gridAddValue("Swipes", unknown, "The number of swipes stored in memory and the number of swipes that have been read since this device has left the factory.");
            idMemory = gridAddValue("Memory", unknown, "Used and available memory. This is shown in KiloBytes (1KB = 1024bytes)");
            grid.AutoResizeColumns();

            /* set up pictures */
            pictureWarning = pW;
            pictureError = pE;

            bitmapWarning = Downloader.resizeIcon(SystemIcons.Warning, bigIconSize, bigIconSize);
            bitmapError = Downloader.resizeIcon(SystemIcons.Error, bigIconSize, bigIconSize);
            pictureWarning.Image = bitmapWarning;
            pictureWarning.Width = bitmapWarning.Width;
            pictureWarning.Height = bitmapWarning.Height;
            
            pictureError.Image = bitmapError;
            pictureError.Width = bitmapError.Width;
            pictureError.Height = bitmapError.Height;

            pictureLocation = new Point(grid.Location.X + grid.Width - bitmapWarning.Width, grid.Location.Y);
            pictureWarning.Location = pictureLocation;
            pictureError.Location = pictureLocation;

        }

        public void updateMenu(string menu)
        {
            hasBluetooth = menu.Contains("Bluetooth");
            hasReadMode = menu.Contains("ReadMode");
            canChangeKey = menu.Contains("setKey");
            canChangeLeds = menu.Contains("blInk");
        }


        public int getLedValue()
        {
            int val = 0;
            if (ledSwipe)
                val |= (int)LedState.Blink_Swipe;
            if (ledStartup)
                val |= (int)LedState.Blink_Startup;
            return val;
        }

        public void updateStats(string msg, bool showName)
        {
            String[] s;
            string hardwareSt;

            stats = msg;

            isDemo = false;
            name = build = firmware = hardwareSt = bluetoothName = bluetoothPeering = "";
            totalMem = freeMem = currentSwipes = allSwipes = timestamp = -1;

            try
            {
                msg = msg.Replace("={", " ");
                msg = msg.Replace('}', ' ');
                s = msg.Split(',','\n');

                name = getValue(s, "name");
                url = getUrl(name);
                int u = name.IndexOf('_');
                if (u != -1)
                {
                    isDemo = true;
                    name = name.Substring(0, u);
                }

                build = getValue(s, "build");
                firmware = getValue(s, "fw");
                hardwareSt = getValue(s, "hw");
                bluetoothName = getValue(s, "bt");
                bluetoothPeering = getValue(s, "peer");
                readMode = getValue(s, "rmode");
                cpu = getValue(s, "uc");
                fuses = getValue(s, "fs");
                int.TryParse(getValue(s, "total"), out totalMem);
                int.TryParse(getValue(s, "free"), out freeMem);
                int.TryParse(getValue(s, "current"), out currentSwipes);
                int.TryParse(getValue(s, "all"), out allSwipes);
                int.TryParse(getValue(s, "time"), out timestamp);
                int led = 0;
                int.TryParse(getValue(s, "led"), out led);
                ledStartup = ((led & (int)LedState.Blink_Startup) != 0);
                ledSwipe = ((led & (int)LedState.Blink_Swipe) != 0);
            }
            catch
            {
                //showMessage(Color.Red, "updateStats: " + ex.Message, "updateStats: " + ex.Message);
            }
            finally
            {
                updateName(showName, name, build, cpu);
                setWarningIcon(isDemo, demoWarning);
                updateSwipes(allSwipes, currentSwipes);
                updateMemory(totalMem, freeMem);
                updateTimestamp(timestamp);
                updateHardware(hardwareSt, build, totalMem);
                updateBuild(build, firmware);
            }
        }

        private void updateBuild(string build, string firmware)
        {
            if (build == "")
            {
                gridUpdateValue(ref idBuild, unknown, "");
            }
            else
            {
                if (firmware.Length == 0)
                    firmware = unknown;
                gridUpdateValue(ref idBuild, build + ", Firmware: " + firmware, "");
            }
        }

        private void updateSwipes(int allSwipes, int currentSwipes)
        {
            if (allSwipes == -1 || currentSwipes == -1)
                gridUpdateValue(ref idSwipes, unknown, "");
            else
                gridUpdateValue(ref idSwipes, int2String(currentSwipes) + " in memory, " + int2String(allSwipes) + " in life time", "");
        }

        private String int2String(int value)
        {
            String st = value.ToString();
            String st2 = "";
            int i;
            int pad = (st.Length - 1) % 3;

            for (i = 0; i < st.Length; i++)
            {
                st2 += st[i];
                if ((i - pad) % 3 == 0 && i + 1 != st.Length)
                    st2 += ",";
            }
            return st2;
        }


        private void initErrors()
        {
            hwErrors = new HardwareErrors[5];
            hwErrors[(int)HwErr.Clock] = new HardwareErrors(0x01, "Clock", "The clock has failed (all timestamps will be incorrect).");
            hwErrors[(int)HwErr.Asic] = new HardwareErrors(0x02, "Reader", "Cannot communicate with the head (switching to old read mode might fix this).");
            hwErrors[(int)HwErr.Asic2] = new HardwareErrors(0x04, "Reader2", "Cannot communicate with the head (switching to old read mode might fix this).");
            hwErrors[(int)HwErr.Memory] = new HardwareErrors(0x08, "Memory", "Cannot access the memory (this reader will not read anything until memory is fixed).");
            hwErrors[(int)HwErr.Bluetooth] = new HardwareErrors(0x10, "Bluetooth", "Cannot program the bluetooth (try disconnecting the device from any power source and connect it back).");
        }

        private void setErrorIcon(bool enableValue, string message)
        {
            pictureError.Visible = enableValue;
            pictureBoxTootltip.SetToolTip(pictureError, message);
        }

        private void setWarningIcon(bool enableValue, string message)
        {
            pictureWarning.Visible = enableValue;
            pictureBoxTootltip.SetToolTip(pictureWarning, message);
        }

        private void configureErrorMessages(String buildSt)
        {
            int b = 7000;

            Int32.TryParse(buildSt, out b);

            if (b < 6299)
            {
                hwErrors[(int)HwErr.Asic2].code = 0x10;
                hwErrors[(int)HwErr.Memory].code = 0x04;
                hwErrors[(int)HwErr.Bluetooth].code = 0x08;
            }
            else
            {
                hwErrors[(int)HwErr.Asic2].code = 0x04;
                hwErrors[(int)HwErr.Memory].code = 0x08;
                hwErrors[(int)HwErr.Bluetooth].code = 0x10;
            }
        }

        private void updateMemory(int total, int free)
        {
            if (total == -1 || free == -1)
                gridUpdateValue(ref idMemory, unknown, "");
            else
                gridUpdateValue(ref idMemory, int2String(total / 1024) + "K  Used " + int2String((total - free) / 1024) + "K  Free " + int2String(free / 1024) + "K",
                    "Total memory: " + int2String(total) + " bytes\nUsed memory: " + int2String(total - free) + " bytes\nFree memory: " + int2String(free) + " bytes");
        }

        private void updateTimestamp(int timestamp)
        {
            if (timestamp == -1)
            {
                gridUpdateValue(ref idClock, unknown, "");
            }
            else
            {
                System.DateTime dateTime = new DateTime(1970, 1, 1).ToLocalTime();
                dateTime = dateTime.AddSeconds(timestamp);
                gridUpdateValue(ref idClock, dateTime.ToShortDateString() + " " + dateTime.ToLongTimeString(), "Click to refresh this field");
            }
        }

        private void updateName(Boolean showName, String name, String build, String cpu)
        {
            String tooltip = "";

            if (name == "")
            {
                name = unknown;
            }
            else if (!showName)
            {
                if (isDemo)
                {
                    name = "Demo Card Reader";
                    tooltip = demoWarning;
                }
                else
                {
                    name = "Card Reader";
                }
            }
            else if (isDemo)
            {
                name += " Demo";
            }

            if (cpu == "")
                cpu = unknown;

            tooltip += "CPU: " + cpu;

            gridUpdateValue(ref idName, name, tooltip);
        }

        public static String getValue(String[] s, String toFind)
        {
            String[] tmp;
            int i;
            toFind = toFind + "=";
            for (i = 0; i < s.Length; i++)
            {
                if (s[i].Contains(toFind))
                {
                    tmp = s[i].Split('=');
                    return tmp[1];
                }
            }
            return "";
        }
                
        private void updateHardware(String hardwareSt, String buildSt, int totalMem)
        {
            int failures = 0;

            if (buildSt == "" && totalMem == -1)
            {
                gridUpdateValue(ref idHardware, unknown, "");
                return;
            }

            /* devices with build < 6000 do not report hardware errors */
            if (hardwareSt == "")
            {
                if (totalMem == 0)
                {
                    configureErrorMessages(buildSt);
                    hw = hwErrors[(int)HwErr.Memory].code;
                }
                else
                {
                    hw = 0;
                }
            }
            else
            {
                hw = 0;
                /* other devices report hardware errors */
                Int32.TryParse(hardwareSt, out hw);
            }

            if (hw == 0)
            {
                setErrorIcon(false, "");
                hardwareMessage = "Ok";
                hardwareTooltip = "No hardware errors have been detected.";
            }
            else
            {
                configureErrorMessages(buildSt);
                hardwareTooltip = "Hardware failure error code: " + hw + "\n\n";
                for (int i = 0; i < hwErrors.Length; i++)
                {
                    if ((hwErrors[i].code & hw) != 0)
                    {
                        failures++;
                        hardwareTooltip += failures + ". " + hwErrors[i].tooltip + "\n";
                        hardwareMessage = "Failed (" + hwErrors[i].message + ")";
                    }
                }

                if (failures > 1)
                    hardwareMessage = "Multiple (code " + hw + ")";

                setErrorIcon(true, hardwareTooltip);
            }

            gridUpdateValue(ref idHardware, hardwareMessage, hardwareTooltip);
        }

        private int gridAddValue(String name, String value, String tooltip)
        {
            int n = dataGrid.Rows.Add();

            dataGrid.Rows[n].Cells[0].ToolTipText = Utils.wordWrap(tooltip, wrapLimit);
            dataGrid.Rows[n].Cells[1].ToolTipText = Utils.wordWrap(tooltip, wrapLimit);
            dataGrid.Rows[n].Cells[0].Value = name;
            dataGrid.Rows[n].Cells[1].Value = value;
            return n;
        }

        private void gridUpdateValue(ref int id, String value, String tooltip)
        {
            if (id < 0 || id >= dataGrid.Rows.Count)
                return;

            dataGrid.Rows[id].Cells[1].Value = value;
            if (tooltip != "")
                dataGrid.Rows[id].Cells[1].ToolTipText = tooltip;
        }

        private string getUrl(string name)
        {
            if (name.Contains("007"))
                return "https://www.cardreaderfactory.com/shop/msrv007.html";
            else if (name.Contains("008"))
                return "https://www.cardreaderfactory.com/shop/msrv008.html";
            else if (name.Contains("009"))
                return "https://www.cardreaderfactory.com/shop/msrv009.html";
            else if (name.Contains("010"))
                return "https://www.cardreaderfactory.com/shop/msrv010.html";
            else
                return "https://www.cardreaderfactory.com/shop/magnetic-stripe-readers.html";
        }
    }
}
