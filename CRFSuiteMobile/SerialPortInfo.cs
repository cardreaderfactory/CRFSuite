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

public class CSerialPortInfo
{
	private string _strPortName;    // Serial port name (COM1:)
	private string _strDescription;	 // Serial port description if available
    private int _portNumber;

	public CSerialPortInfo(string pszPortName, string pszDescription)
	{
        //not sure why port name has \0 characters
        _strPortName = pszPortName.Replace("\0", "");
        _strDescription = pszDescription;

        int index = pszPortName.IndexOf(':');
        //if (index > 3)
            //index -= 3; //COMx:
        if (index < 0)
            index = pszPortName.Length; //COMx (without :)

        _portNumber = Convert.ToInt32(pszPortName.Substring(3, index - 3));
	}

    public string PortName
    {
        get
        {
            return _strPortName;
        }
    }

    public string PortDescription
    {
        get
        {
            return _strDescription;
        }
    }

    public int PortNumber
    {
        get
        {
            return _portNumber;
        }
    }

    public override string ToString()
    {
        if (_strDescription != string.Empty)
            return _strPortName + " " + _strDescription;
        else
            return _strPortName;
    }
}

public class CSerialPortInfoComparer : IComparer<CSerialPortInfo>
{
    public int Compare(CSerialPortInfo x, CSerialPortInfo y)
    {
        //return string.Compare(x.PortName, y.PortName);
        return x.PortNumber - y.PortNumber;
    }
}
