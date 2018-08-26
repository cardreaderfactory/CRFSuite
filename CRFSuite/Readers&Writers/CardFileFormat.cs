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

namespace crf
{
    public class CardFileFormat
    {
        public enum Format
        {
            /**< file will be saved as csv. this format is only supported by the writer. */
            CSV = 1,

            /**< file will be saved as txt. this format is only supported by the writer. */
            TXT = 2,

            /**< file will be read as crf extended. this format is only supported by the reader */
            CRF = 4,

            /**
             * reader/writer will determine file format using file extension.
             * Write will use txt as default if extension is not known and format is NONE.
             * Reader will use crf as default if extension is not known and format is NONE.
             */
            NONE = 5
        };
    }
}
