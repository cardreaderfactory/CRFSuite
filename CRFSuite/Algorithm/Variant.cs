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
using System.Drawing;
using System.Text;

namespace crf.Algorithm
{
    // extends BitSolution and adds support for byte[] and StringWithParity
    public class Variant : BitSolution, IVariant
    {

        /* alignment to apply in chars while drawing the variant. */
        public int? Alignment = null;

        /*cache of Properties.Settings.Default.decodeMethod */
        public static int _DecodeMethod = 2;

        /* cache */
        byte[] byteArray;                                   // caches the byte form of bits
        StringWithParity swp;   // caches the string with parity

        #region constructor

        public Variant(Variant v)
        {
            this.track = v.track;
            this.idx = v.idx;
            this.direction = v.direction;
            init(v.BinaryString, Bpc, add);
        }

        public Variant(byte[] input)
        {
            init(input, Auto, Auto);
        }

        public Variant(byte[] input, int bpc)
        {
            init(input, bpc, Auto);
        }

        public Variant(byte[] input, int bpc, int add)
        {
            init(input, bpc, add);
        }

        public Variant(StringWithParity input, int bpc, int add)
        {
            init(input, bpc, add);
        }

        #endregion


        #region properties

        public byte[] ByteArray
        {
            get
            {
                if (byteArray == null)
                    byteArray = binaryStringToByteArray(BinaryString);
                return byteArray;
            }
            set
            {
                init(value, Auto, Auto);
            }
        }

        public Image DirectionAsImage
        {
            get
            {
                return ResourcesLoader.directionToImage(direction);
            }
        }

        public StringWithParity ToStringWithParity
        {
            get
            {
                return toStringWithParity(add);
            }            
        }

        public StringWithParity toStringWithParity()
        {
            return toStringWithParity(add);
        }


        public StringWithParity toStringWithParity(int add)
        {
            if (swp == null)
                swp = calculateStringWithParity(Atoms, add);

            return swp;
        }


        #endregion

        #region implementation

        /* main initialization function. erase our cache too if needed */
        public override void init(string binaryString, int bpc, int add)
        {
            base.init(binaryString, bpc, add);
            if (binaryString != null)
            {
                swp = null;
                byteArray = null;
            }
        }

        /* initialization through StringWithParity */
        public void init(StringWithParity input, int bpc, int add)
        {
            init(stringWithParityToBinaryString(input, bpc, add), bpc, add);
        }

        /* initialization with byteArray */
        public void init(byte[] input, int bpc, int add)
        {
//            byte[] ba = null;
            string binaryString = null;

            if (input != null)
            {
//                ba = new byte[input.GetLength(0)];
//                input.CopyTo(ba, 0);
                binaryString = byteArrayToBinaryString(input);
            }

            init(binaryString, bpc, add);   // call init to initialize everything
            byteArray = input;                 // cannot initialize this before init() as init() erases byteArray            
        }

        public static string stringWithParityToBinaryString(StringWithParity swp, int bpc, int add)
        {
            if (swp == null)
                return null;

            StringBuilder sb = new StringBuilder();
            byte [] data = swp.data;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                bool parity = true;
                byte encodedChar = (byte)(Convert.ToByte(data[i]) - add);
                for (int j = 0; j < bpc - 1; j++)
                {
                    if ((encodedChar & (1 << j)) != 0)
                    {
                        sb.Append('1');
                        parity = !parity;
                    }
                    else
                    {
                        sb.Append('0');
                    }
                }
                if (parity)
                    sb.Append('1');
                else
                    sb.Append('0');               

            }

            return sb.ToString();
        }

        /* calculates the Ascii form of the bit solution */
        private static StringWithParity calculateStringWithParity(List<BitSolutionAtom> atomsList, int add)
        {
            // work in progress
            StringBuilder sb = new StringBuilder("");

            for (int i = 0; i < atomsList.Count; i++)
                sb.Append(atomsList[i].ToString(add));

            return new StringWithParity(sb.ToString().Trim(' '));
        }


        /* converts the byte array into the binary string representation form: '1' and '0's */
        public static string byteArrayToBinaryString(byte[] input)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((input[i] & (1 << j)) != 0)
                        sb.Append('1');
                    else
                        sb.Append('0');
                }
            }

            return sb.ToString();
        }

        #endregion

        #region implementation - static functions

        /* converts the the binary string representation form ('1' and '0's) to a byte array         
         * note: the unknown characters are skipped as they never existed 
         */
        public static byte[] binaryStringToByteArray(string data)
        {
            if (data == null)
                return null;

            int bufSize = data.Length / 8;
            if (data.Length % 8 != 0)
                bufSize++;

            byte[] buffer = new byte[bufSize];

            for (int idxData = 0, idxByte = 0, idxBit = 0;
                idxData < data.Length && idxByte < bufSize;
                idxData++)
            {
                if (data[idxData] == '1')
                {
                    buffer[idxByte] |= (byte)(1 << idxBit);
                    idxBit++;
                }
                else if (data[idxData] == '0')
                {
                    idxBit++;
                } // else skip; as binary form does not support spaces

                if (idxBit == 8)
                {
                    idxByte++;
                    idxBit = 0;
                }
            }

            return buffer;
        }

        #endregion

    }

    class VariantComparer : IComparer<Variant>
    {
        private Serializable.SortOrder sortOrder;

        public VariantComparer(Serializable.SortOrder sortOrder)
        {
            this.sortOrder = sortOrder;
        }
        public int Compare(Variant a, Variant b)
        {
            return (b.Rate - a.Rate)*(int)sortOrder;
        }
    }
}
