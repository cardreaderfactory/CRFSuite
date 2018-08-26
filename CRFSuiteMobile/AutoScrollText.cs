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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CRFSuite
{
    public partial class AutoScrollText : TextBox
    {
        public AutoScrollText()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                int length = value.Length;

                if (length > 1024)
                {
                    base.Text = value.Substring(1024 - 256);
                    length = base.Text.Length;
                }
                else
                {
                    base.Text = value;
                }


                if (length > 0)
                {
                    base.SelectionStart = length - 1;
                    base.SelectionLength = 0;

                    base.ScrollToCaret();
                }
            }
        }
    }
}
