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

namespace crf.CustomsControls
{
    public class ToolStripTextBoxAcceptDelKey : ToolStripTextBox
    {
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)msg.WParam & Keys.KeyCode;

            if ((msg.Msg == WM_KEYDOWN || msg.Msg == WM_KEYUP)
                 && keyCode == Keys.Delete)
            {
                if (base.SelectedText == string.Empty)
                    base.Select(base.SelectionStart, 1);
                base.SelectedText = "";
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
