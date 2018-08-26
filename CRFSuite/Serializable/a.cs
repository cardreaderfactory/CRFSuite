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

namespace Serializable
{
    public enum SortOrder { Ascending = -1, Descending = 1, None = 0 };
    /**
     * This class will not be obfuscate to avoid problem between versions.
     * This is why it has this name name and properties names.
     * 
     * It has all data needed to build a card
     */
    [Serializable]
    public class a
    {
        //card data

        //swipes
        public uint _s;

        //card number
        public uint _cN;

        //time stamp1
        public uint _tS1;

        //time stamp2
        public uint _tS2;

        //merged card1
        public a _c1;

        //merged card2
        public a _c2;

        //track information.
        public b[] _t;

        public a(uint s, uint cN, uint tS1, uint tS2, b[] t)
        {
            _s = s;
            _cN = cN;
            _tS1 = tS1;
            _tS2 = tS2;
            _t = t;
        }
    }
}
