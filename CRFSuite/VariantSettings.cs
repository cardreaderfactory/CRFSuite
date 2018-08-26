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
    class VariantSettings
    {
        /**
         * Maximum number of chars on every track.
         * Add two for the start and end character.
         */
        public readonly static int[] maxChars = { 79 + 2, 40 + 2, 107 + 2 };

        /**
         * Char to add in every track.
         */
        public readonly static byte[] add = { 0x20, 0x30, 0x30 };

        /**
         * Bits per char for every track.
         */
        public readonly static byte[] bpc = { 7, 5, 5 };

        /**
         * Start char for every track
         */
        public readonly static char[] ss = { '%', ';', ';' };

        /**
         * End char for every track
         */
        public readonly static char[] es = { '?', '?', '?' };
    }
}
