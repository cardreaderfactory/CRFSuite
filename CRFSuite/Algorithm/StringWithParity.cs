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
using System.Text;

namespace crf.Algorithm
{
    /**
     * string that can hold a string and a buffer with bools indicating for each character if parity is ok or not.
     */
    public class StringWithParity
    {
        private byte[] st = null;
        private bool[] parity = null;

        public byte[] data 
        { 
            get 
            { 
                return st; 
            } 
        }

        public static StringWithParity Empty = new StringWithParity("");

        public StringWithParity(StringWithParity swp)
        {
            if (swp.st != null)
            {
                int len = swp.st.GetLength(0);
                this.st = new byte[len];
                Array.Copy(swp.st, this.st, len);
            }
            if (swp.parity != null)
            {
                int len = swp.st.GetLength(0);
                this.parity = new bool[len];
                Array.Copy(swp.parity, this.parity, len);
            }
        }

        public StringWithParity(string sValue)
        {
            if ((sValue != null) && (sValue.Length > 0))
            {
                UTF8Encoding _UTF8 = new UTF8Encoding();
                st = _UTF8.GetBytes(sValue);

                parity = new bool[st.Length];
                for (int i = 0; i < parity.Length; i++)
                    parity[i] = true;
            }
            else
            {
                parity = null;
                st = null;
            }
        }


        public StringWithParity(byte[] sValue, bool[] parity)
        {
            /* not sure why if I add this exception application crashes.
            if (((crcValue == null) && (sValue != string.Empty)) ||
                (crcValue.Length != sValue.Length))
                throw new ArgumentException("Both string and CRCs must have same length");
            */

            st = sValue;
            this.parity = parity;

        }


        public static bool IsNullOrEmpty(StringWithParity crcString)
        {
            return ((crcString.st == null) || (crcString.parity == null));
        }

        public int Length { get { return ToString().Length; } }

        public string ToActualString()
        {
            if (st != null)
            {
                UTF8Encoding _UTF8 = new UTF8Encoding();

                return _UTF8.GetString(st);
            }
            else
                return string.Empty;
        }

        public override string ToString()
        {
            if (st == null)
                return string.Empty;

            UTF8Encoding _UTF8 = new UTF8Encoding();

            //show only valid bytes
            // get_decodeMethod is a bottle neck!!
            if (Variant._DecodeMethod == (int)Forms.DecodeSettings.ValidChars.ValidCRC)
                return getStringWithValidCRCChars(st, parity);

            //show everything
            return _UTF8.GetString(st);
        }

        private string getStringWithValidCRCChars(byte[] variantBytes, bool[] parityCheck)
        {
            char[] validCRCChars = new char[variantBytes.Length];

            for (int i = 0; i < variantBytes.Length; i++)
            {
                if (parityCheck[i])
                    validCRCChars[i] = (char)variantBytes[i];
                else
                    validCRCChars[i] = '_';
            }

            return new string(validCRCChars, 0, validCRCChars.Length);
        }

        public static explicit operator bool[](StringWithParity swp)
        {
            if (StringWithParity.IsNullOrEmpty(swp))
                return null;

            bool[] retValue = null;

            //show only valid bytes

            if (Variant._DecodeMethod == (int)Forms.DecodeSettings.ValidChars.LongestValidCRC)
            {
                retValue = new bool[swp.parity.GetLength(0)];
                for (int i = 0; i < retValue.Length; i++)
                    retValue[i] = true;
            }
            else
            {
                retValue = swp.parity;
            }

            return retValue;
        }

    }

}
