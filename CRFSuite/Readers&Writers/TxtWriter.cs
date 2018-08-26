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
using System.IO;

namespace crf
{
    class TxtWriter : StreamWriter
    {
        public TxtWriter(string path) : base(path, false)
        {
        }

        public TxtWriter(MemoryStream outputStream) : base(outputStream)
        {
        }

        public void Write(object[] row, int []itemsLength)
        {
            int i = 0;

            //write all items but last one
            while (i < (row.Length - 1))
            {
                WriteItem(row[i], itemsLength[i]);
                i++;
            }

            //write last item
            WriteItem(row[i], itemsLength[i]);
            //end line with \r\n so even notepad can open the file correctly.
            Write("\r\n");
        }

        private void WriteItem(object item, int itemLength)
        {
            if (item == null)
                return;

            //add 1 to leave at least 1 space between items
            Write(item.ToString().PadRight(itemLength +1));
        }
    }
}
