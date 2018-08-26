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
    class CsvWriter : StreamWriter
    {
        /**
         * true if all items must be delemited with " at the beginning and at the end.
         * If false only those items with " or line breaks will be delimited.
         * It can be easily moved to Write(object[] object) as a parameter is needed.
         */
        private const bool _quoteAll = true;

        public CsvWriter(string path) : base(path, false)
        {
        }

        public void Write(object[] row)
        {
            int i = 0;

            //write all items but last one
            while (i < (row.Length - 1))
            {
                WriteItem(row[i], _quoteAll);
                Write(',');

                i++;
            }

            //Write last item
            WriteItem(row[i], _quoteAll);
            Write('\n');
        }

        private void WriteItem(object item, bool quoteAll)
        {
            if (item == null)
                return;

            string s = item.ToString();

            if (quoteAll || s.IndexOfAny("\",\x0A\x0D".ToCharArray()) > -1)
                Write("\"" + s.Replace("\"", "\"\"") + "\"");
            else
                Write(s);
        }
    }
}
