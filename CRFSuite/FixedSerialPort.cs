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
using System.IO.Ports;

public partial class FixedSerialPort : SerialPort
{
    public new void Open()
    {
        try
        {
            base.Open();
            /*
            ** because of the issue with the FTDI USB serial device,
            ** the call to the stream's finalize is suppressed
            **
            ** it will be un-suppressed in Dispose if the stream
            ** is still good
            */
            GC.SuppressFinalize(BaseStream);
        }
        catch (Exception ex)
        {
            throw (ex);
        }

    }

    public new void Dispose()
    {
        Dispose(true);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && (base.Container != null))
        {
            base.Container.Dispose();
        }

        try
        {
            /*
            ** because of the issue with the FTDI USB serial device,
            ** the call to the stream's finalize is suppressed
            **
            ** an attempt to un-suppress the stream's finalize is made
            ** here, but if it fails, the exception is caught and
            ** ignored
            */
            GC.ReRegisterForFinalize(BaseStream);
        }
        catch
        {
        }

        base.Dispose(disposing);
    }
}