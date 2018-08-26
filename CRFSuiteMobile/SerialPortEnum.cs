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
using System.Windows.Forms;
using System.IO.Ports;
using Microsoft.Win32;

public class CSerialPortEnum : List<CSerialPortInfo>
{
    private CSerialPortEnum()
	{
    }

    public enum PortType { Serial = 0, Bluetooth = 1, All = 2, All2 = 3 };

    private readonly string []PortTypeString = { "Serial", "Bluetooth", "", "" };

    public static CSerialPortEnum GetPorts(PortType portType)
    {
        CSerialPortEnum ports = new CSerialPortEnum();

        switch (portType)
        {
            case PortType.Serial:
            case PortType.Bluetooth:
            case PortType.All:
                ports.EnumeratePorts(portType);
                break;

            case PortType.All2:
                ports.EnumerateAllPorts();
                break;
        }

        //needed because comboxes in pocket pc does not have sort property.
        ports.Sort(new CSerialPortInfoComparer());

        return ports;
    }

    private int EnumerateAllPorts()
    {
        foreach (string str in SerialPort.GetPortNames())
        {
            CSerialPortInfo pInfo = new CSerialPortInfo(str, "");
            Add(pInfo);
        }

        return this.Count;
    }

    private int EnumeratePorts(PortType portType)
    {
        RegistryKey hKey = Registry.LocalMachine.OpenSubKey("Drivers\\Active");

		if (hKey != null)
		{
            string [] subKeyNames = hKey.GetSubKeyNames();

			foreach (string szKeyName in subKeyNames)
			{
                RegistryKey	hKey2 = hKey.OpenSubKey(szKeyName);

                if (hKey2 != null)
                {
				    string strDriverName = string.Empty;
                    string strDriverKey = string.Empty;
                    string strFriendlyName = string.Empty;

                    strDriverName = hKey2.GetValue("Name", string.Empty).ToString();
                    strDriverKey = hKey2.GetValue("Key", string.Empty).ToString();

                    hKey2.Close();

				    // Check if this is a COM port
                    if (strDriverName.StartsWith("COM"))
                    {
                        hKey2 = Registry.LocalMachine.OpenSubKey(strDriverKey);

                        if (hKey2 != null)
                        {
                            strFriendlyName = hKey2.GetValue("FriendlyName", string.Empty).ToString();
                            hKey2.Close();
                        }

                        if ((PortTypeString[(int)portType].Length == 0) ||
                            strFriendlyName.StartsWith(PortTypeString[(int)portType], StringComparison.CurrentCultureIgnoreCase))
                        {
                            CSerialPortInfo pInfo = new CSerialPortInfo(strDriverName, strFriendlyName);
                            Add(pInfo);
                        }
                    }
                }
			}

			hKey.Close();
		}

        //read bluetooth ports only if type is bluetooth
        if ((PortType.Bluetooth == portType) || (PortType.All == portType))
		    AddMsBtPorts();

        return this.Count;
	}

	// Adds any Microsoft Bluetooth Serial Ports
	private void AddMsBtPorts()
	{
        RegistryKey	hKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Bluetooth\\Serial\\Ports");

		if (hKey != null)
		{
            string [] subKeyNames = hKey.GetSubKeyNames();

            foreach (string szKeyName in subKeyNames)
            {
                RegistryKey hKey2 = hKey.OpenSubKey(szKeyName);

                if (hKey2 != null)
                {
                    string strPortName = hKey2.GetValue("Port", string.Empty).ToString();
                    hKey2.Close();

                    CSerialPortInfo pInfo = new CSerialPortInfo(strPortName + ": ", "Bluetooth");
                    Add(pInfo);
                }
            }

			hKey.Close();
		}
    }
}
