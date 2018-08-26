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
using crf;

namespace Serializable
{
    /**
     * This class will not be obfuscate to avoid problem between versions.
     * This is why it has this name name and properties names.
     * 
     * It has all data needed to build a variant group
     */
    [Serializable]
    public class b
    {
        /**< all calculated variants */
        public List<c> _aV;

        /**< bpc used to create this variant group */
        public byte _bpc;

        /**< add byte used */
        public byte _add;

        /**< index of the preferred variant (variant with the biggest rate) */
        public int _pVI;

        /**< index of the user preferred variant. -1 if none */
        public int _pUVI;

        /**
         * Track bytes with a 0 at the beginning an another at the end
         */
        public byte[] _tB;

        /** not 100% needed but it will not increase the size of file too much
         *  and user sorting order will be saved
         */
        public Serializable.SortOrder _sO;

        public b(List<c> aV, byte bpc, byte add, int pVI, int pUVI, byte[] tB, Serializable.SortOrder sO)
        {
            _aV = aV;
            _bpc = bpc;
            _add = add;
            _pVI = pVI;
            _pUVI = pUVI;
            _tB = tB;
            _sO = sO;
        }
    }
}
