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
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace crf.Algorithm
{

    /* Calculates the most probable blocks of bits.
     * 
     * Input: A string of 1 and 0 with separators and the bits per trackId of the card.
     *   example: .100111111001111000.0.101000...01.110000110111100011..100.00110010100.0000111010. 
     *   
     *   bpc 5, direction 0, 000000001101001101110010010000001000010010000001010000100010000101010110110011100111010101000110011000010110000010000100001000011001110101010001100110000000010000100001000010000100001000010000100001111111110000000000
     *   bpc 5, direction 1, 000000001111111110000100001000010000100001000010000100001000000001100110001010101110011000010000100001000001101000011001100010101011100111001101101010100001000100001010000001001000010000001001001110110010110000000000
     *   should be ;634004022156995231=000095231000000000?7
     *    
     * Output: Creates a list with valid and invalid strings (BitSolutionAtoms)
     *    it supports lost bits as synchronizes based the parity bit and should return the best solution
     */
    public class BitSolution
    {
        #region constants

        public const int Auto = -1;
        public const int Unchanged = -2;

        public const int MinBpc = 4;                    // minimum Bits Per Character supported
        public const int MaxBpc = 9;                    // maximum Bits Per Character supported
        public const int BpcRange = MaxBpc - MinBpc; 

        #endregion

        #region variables

        /* 1. creating a BitSolution will immediately make the rate available but will not initialize anything else      
         * 2. call findSolutions() to generate the list with solutions 
         */

        public int idx;                             // index - used for debugging to locate the solution
        public int add;                             // automatically detected if not set
        public int track;                           // not required but it might increase the chances of calculating the accurate rate
        public int direction;                       // ignored by the class. useful if the callers wants to know what direction this was.
        public char ss;                             // the start sentinel; if you are using custom sentinels, you need to reinitialize this every time after changing bpc or binaryString
        public char es;                             // the end sentinel; if you are using custom sentinels, you need to reinitialize this every time after changing bpc or binaryString
        public char[] charsToSplit = { 'v', '^', '?', '.', ' ' }; // used to split the binaryForm into multiple atoms before calculation
        
        private string binaryString;                // main binaryString. all the class works on this

        /* cache */
        string asciiForm;                           // caches the human readable form
        bool startSentinel;       
        bool endSentinel;         

        /* private stuff */
        int bpc;                                    // bits per char (config param)
        int validCharsCount;                        // the number of valid characters in this solution
        int rate;
        
        int level;                                  // if level 0, the solution was immediatelly determined; level 1 was extracted from invalid level 0, etc.
        string[] subData;                           // bit binaryString
        int[] subStart;                             // starting bit for valid binaryString
        int[] subLength;                            // length of valid binaryString (in bpc)
        List<BitSolutionAtom> atoms;                // internal storage for calculated solutions.

        #endregion

        #region construtors

        public BitSolution()
        {

        }
        
        public BitSolution(string binaryString)
        {
            init(binaryString, Auto, Auto);
        }

        public BitSolution(string binaryString, int bpc)
        {
            init(binaryString, bpc, Auto);
        }

        public BitSolution(string binaryString, int bpc, int add)
        {
            init(binaryString, bpc, add);
        }

        #endregion

        #region properties

        public int Bpc
        {
            get
            {
                return bpc;
            }
            set
            {
                init(binaryString, value, Unchanged);
            }
        }

        public string BinaryString
        {
            get
            {
                return binaryString;
            }
            set
            {
                init(value, Unchanged, Unchanged);
            }
        }

        public List<BitSolutionAtom> Atoms
        {
            get
            {
                if (atoms == null)
                    calculateAtoms();
                return atoms;
            }
        }

        public int Rate
        {
            get
            {
                return rate;
            }
        }

        public bool HasSS 
        {
            get 
            {
                return startSentinel;
            }
        }

        public bool HasES
        {
            get
            {
                return endSentinel;
            }
        }

        
        #endregion

        #region overrides

        public override int GetHashCode()
        {
            if (binaryString == null)
                return this.GetHashCode();
            else
                return binaryString.GetHashCode();
        }

        //public override string ToString()
        //{
        //    return String.Format("validCharsCount: {0}, rate: {1}", validCharsCount, rate);
        //}

        /* returns the Ascii form based on the user set direction */
        public override string ToString()
        {
            return ToString(add);
        }


        public string ToString(int add)
        {
            return asciiForm;
        }

        #endregion

        #region implementation
        
        public virtual void init(string binaryString, int bpc, int add)
        {

            if (bpc != Unchanged)
                this.bpc = bpc;

            if (add != Unchanged)
                this.add = add;

            if (binaryString == null)
                return;

            this.binaryString = binaryString;   
            this.validCharsCount = 0;
            this.rate = 0;
            this.subData = binaryString.Split(charsToSplit);
            this.subStart = new int[subData.GetLength(0)];
            this.subLength = new int[subData.GetLength(0)];
            this.atoms = null;

            if (bpc == Auto ||                                          // automatic calculation requested
                (bpc != Unchanged && (bpc < MinBpc || bpc > MaxBpc))    // invalid bpc requested => Auto
               )
            {
                this.bpc = calcBestBpc(subData);
            }


            if (add == Auto)
                this.add = getAdd(this.bpc);

            for (int i = 0; i < subData.GetLength(0); i++)
            {
                if (subData[i].Length == 0)
                    continue;
                if (subData[i].Length >= 2 * bpc)
                {
                    calcBestStart(subData[i], bpc, ref subStart[i], ref subLength[i]);
                    validCharsCount += subLength[i];
                    rate += subLength[i] * subLength[i];
                }
            }

            asciiForm = calculateAsciiForm(Atoms, this.add);
            configureSentinels(this.bpc);
            calculateSentinels();
            rate += smartRate(bpc, track);
        }

        private void configureSentinels(int bpc)
        {
            if (ss == 0)
            {
                if (bpc == 7)
                    ss = '%';
                else if (bpc == 5)
                    ss = ';';
            }

            if (es == 0)
                es = '?';
        }

        public void calculateSentinels()
        {
            string s = asciiForm;
            for (int i = 0; i < 3 && i < s.Length; i++)
                if (s[i] == ss)
                    startSentinel = true;

            for (int i = s.Length - 1; i >= 0 && i >= s.Length - 3; i--)
                if (s[i] == es)
                    endSentinel = true;
        }


        private void addAtom(string st, bool parity, int rate, int level)
        {
            if (st.Length == 0)
                return;
            BitSolutionAtom a = new BitSolutionAtom() { data = st, level = level, rate = rate, parityOk = parity, bpc = bpc, add = add};
            atoms.Add(a);
        }

        private void addInvalid(string st)
        {
            if (st.Length == 0)
                return;

            level++;

            if (st.Length > bpc)
            {
                int start = 0;
                int rate = 0;
                int l;
                calcBestStart(st, bpc, ref start, ref rate);
                if (rate > 0)
                {
                    addInvalid(st.Substring(0, start));
                    l = rate * bpc;
                    addAtom(st.Substring(start, l), true, rate, level);
                    addInvalid(st.Substring(start + l, st.Length - start - l));
                }
                else
                {
                    addAtom(st, false, 0, level - 1);
                }

            }
            else
            {
                addAtom(st, false, 0, level - 1);
            }

            level--;
        }

        private void calculateAtoms()
        {
            if (subData == null)
                return;

            atoms = new List<BitSolutionAtom>();

            for (int i = 0; i < subData.GetLength(0); i++)
            {
                string st = subData[i];
                /* create an atom from the invalid part of the current string of bits */
                BitSolutionAtom a = new BitSolutionAtom();
                if (subStart[i] > 0)
                    addInvalid(st.Substring(0, subStart[i]));
                else if (subLength[i] == 0)
                    addInvalid(st.Substring(0, st.Length));

                /* when there's a valid part, add it.*/
                if (subLength[i] > 0)
                {
                    int validLen = subLength[i] * bpc;
                    addAtom(st.Substring(subStart[i], validLen), true, subLength[i], 0);
                    /* if there's anything left after the valid part, add it to an invalid atom */
                    if (subStart[i] + validLen < st.Length)
                        addInvalid(st.Substring(subStart[i] + validLen, st.Length - subStart[i] - validLen));
                }
            }
        }
        
        #endregion

        #region implementation - static functions

        public static string Reverse(string data)
        {
            /* reverse string */
            StringBuilder s = new StringBuilder();
            for (int i = data.Length - 1; i >= 0; i--)
                s.Append(data[i]);

            return s.ToString();
        }

        public static byte[] Reverse(byte[] data)
        {
            /* reverse string */
            if (data == null)
                return null;
            int len = data.GetLength(0);

            byte[] b = new byte[len];

            for (int i = 0, j = len - 1; i < len; i++, j--)
            {
                // Reverse the bits in a byte with 3 operations (64-bit multiply and modulus division)
                // http://graphics.stanford.edu/~seander/bithacks.html#ReverseByteWith64BitsDiv
                b[j] = (byte)((data[i] * 0x0202020202 & 0x010884422010) % 1023); 
            }

            return b;
        }

        /* calculates the Ascii form of the bit solution in the specified direction */
        private static string calculateAsciiForm(List<BitSolutionAtom> atomsList, int add)
        {
            // work in progress
            StringBuilder sb = new StringBuilder("");

            for (int i = 0; i < atomsList.Count; i++)
                sb.Append(atomsList[i].ToString(add));

            return sb.ToString().Trim(' ');
        }

        /* converts the characters '1' and '0' from binaryString string to ascii form
         * 
         * bpc          bits per character
         * direction    0 for left to right
         *              1 for right to left
         * add          the value to "add" in order to obtain the correct ascii value
         * 
         */
        public static string binaryStringToString(string data, int bpc, int add)
        {

            if (bpc < MinBpc)
                throw new Exception("invalid bpc");

            byte ch = 0;

            StringBuilder sb = new StringBuilder();
            for (int i = 0, bitId = 0; i < data.Length; i++)
            {
                if (data[i] == '1')
                {
                    ch |= (byte)(1 << bitId);
                    bitId++;
                }
                else if (data[i] == '0')
                {
                    bitId++;
                } // else skip; as binary form does not support spaces

                if (bitId == bpc - 1)
                {
                    sb.Append((char)(ch + add));
                    ch = 0;
                    bitId = 0;
                    i++; // skip parity bit
                }
            }

            return sb.ToString();
        }


        /* calculates the longest valid parity for this string
         * note: the string is in binary form ('1' and '0's)
         * 
         * note: if the string is very long and 1 or few bits were 
         * lost it can leave substantial binaryString as shown with parity fail
         * 
         * example:
         *  kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk_kkkkkkkkkkkkkk will report only
         *  kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk 
         *  
         *          
         * 
         */
        public static void calcMaxParity(string binaryString, int startOffset, int bpc, ref int validChars, ref int offset)
        {
            int tempValidChars = 0;
            int tempOffset = 0;
            bool ourParity = true;
            bool theirParity;

            validChars = 0;
            offset = startOffset;
            tempOffset = startOffset;
            //            Debug.Write(String.Format("testing {0}\r\n", st.Substring(x,st.Length-x)));
            for (int i = startOffset; i + bpc - 1 < binaryString.Length; i += bpc)
            {
                //                Debug.Write(String.Format(" {0}", st.Substring(i,bpc)));
                ourParity = true;
                for (int j = 0; j < bpc - 1; j++)
                {
                    // note: if this asserts here you need to fix this function to skip these bits.
                    Debug.Assert(binaryString[i + j] == '1' || binaryString[i + j] == '0');

                    if (binaryString[i + j] == '1')
                        ourParity = !ourParity;
                }

                if (binaryString[i + bpc - 1] == '1')
                    theirParity = true;
                else
                    theirParity = false;


                if (ourParity != theirParity)
                {
                    //                    Debug.Write(String.Format("!\r\n")); 
                    tempValidChars = 0;
                    tempOffset = i + bpc;
                }
                else
                {
                    tempValidChars++;

                    if (tempValidChars > validChars)
                    {
                        validChars = tempValidChars;
                        offset = tempOffset;
                    }
                }
            }
        }

        /* calculates the parity of a binary string ('1' and '0')
         * 
         * returns: true if the parity is 1
         *          false if the parity is 0
         */
        public static bool calcParity(string binaryString)
        {
            bool parity = true;

            for (int i = 0; i < binaryString.Length; i++)
            {
                // note: if this asserts here you need to fix this function to skip these bits.
                Debug.Assert(binaryString[i] == '1' || binaryString[i] == '0');
                if (binaryString[i] == '1')
                    parity = !parity;
            }

            return parity;
        }

        /* calculates the best start offset for the st (st is in binary representation form - ('1' and '0's)
         * also returns the number of consecutive valid characters for the calculated offset 
         * 
         * binaryString:
         *  st               string in binary form
         *  bpc              bits per character
         * 
         * output:
         *  bestStart        best value for start offset
         *  bestValidChars   the number of valid characters when using bestStart
         */
        public static void calcBestStart(string binaryString, int bpc, ref int bestStart, ref int bestValidChars)
        {
            int validChars = 0;
            int offset = 0;

            bestValidChars = 0;
            bestStart = 0;
            for (int i = 0; i < bpc; i++)
            {
                //                Debug.Write(String.Format("i={0}\r\n", i));
                calcMaxParity(binaryString, i, bpc, ref validChars, ref offset);
                Debug.Write(String.Format("i={0}, valid={1}, start={2}, binaryString=[{3}]\r\n", i, validChars, offset, binaryStringToString(binaryString.Substring(i, binaryString.Length - i), bpc, 0x30)));

                if (validChars > bestValidChars)
                {
                    bestValidChars = validChars;
                    bestStart = offset;
                }
            }
            //            Debug.Write(String.Format("best i={0}, valid={1}\r\n", bestStart, bestValidChars));
        }

        /* calculates the best bpc for the bit binaryString from [] bits        
         * uses a x*x function in order to favorize the longest characters sequence.
         * 
         * binaryString:
         * binaryString         an array of strings in binary form
         *
         * returns      the best bpc
         */
        public static int calcBestBpc(string[] binaryStrings)
        {
            if (binaryStrings == null)
                return -1;

            int bestBpc = 5;
            int maxRate = 0;

            for (int bpc = MinBpc; bpc <= MaxBpc; bpc++)
            {
                int rate = 0;
                for (int i = 0; i < binaryStrings.GetLength(0); i++)
                {
                    if (binaryStrings[i].Length == 0)
                        continue;

                    if (binaryStrings[i].Length >= 2 * bpc)
                    {
                        int start = 0;
                        int length = 0;
                        calcBestStart(binaryStrings[i], bpc, ref start, ref length);
                        rate += length * length; // using x*x as graph so we can favorise 
                    }
                }

                if (rate > maxRate)
                {
                    rate = maxRate;
                    bestBpc = bpc;
                }
            }

            return bestBpc;
        }

        /* calculates the add value based on industry existing standards */
        public static int getAdd(int bpc)
        {
            int add = BitSolution.Auto;
            switch (bpc)
            {
                case 3:
                    goto case 4;
                case 4:
                    add = 0x40;
                    break;

                case 5:
                    add = 0x30;
                    break;
                case 6:
                    goto case 5;

                case 7:
                    add = 0x20;
                    break;
                case 8:
                    goto case 7;

                case 9:
                    add = 0x00;
                    break;
            }

            return add;
        }

        /* calculates the rate for the decoded string
         * 
         * binaryString:
         * st       decoded string (warning: NOT binary form!)
         * bpc      bits per character
         * rate     the calculated rate
         * trackId    the trackId number
         * 
         */
        public int smartRate(int bpc, int track)
        {
            int retVal = 0;

            string asciiString = ToString();

            if (startSentinel)
                retVal += 3;

            if (endSentinel)
                retVal += 3;

            if (bpc == 5)
            {                 
                /* Subtract the number of matches of 0x3a, 0x3b, 0x3c, 0x3e, 0x3f. 
                 * This mean: :;<>? */
                retVal -= Regex.Matches(asciiString, @"[\x3A\x3B\x3C\x3E\x3F]").Count;

                int l = asciiString.Length;
                if (track != 2) /* not track 3 */
                {
                    /* we are expecting just one equal on this trackId */
                    int c = 0;
                    for (int i = 0; i < l; i++)
                        if (asciiString[i] == '=')
                            c++;
                    if (c == 1)
                        retVal++;
                    else
                        retVal -= c;
                }
                else /* track 3 */
                {
                    /* we expect double equals; just one equal is not fine */
                    for (int i = 0; i < l; i++)
                    {
                        if (asciiString[i] == '=')
                            if (i + 1 < l && asciiString[i + 1] == '=')
                            {
                                retVal += 2;
                                i++;
                            }
                            else
                            {
                                retVal--;
                            }
                    }
                }
            }
            else if (bpc == 7)
            {
                /* Subtract the number of matches of 0x21, 0x3a, 0x3b, 0x3c, 0x3e, 0x3f, 0x40, 0x5b, 0x5c, 0x5d and range between # and ,
                    * This means: !#$%&'()*+,:;<>?@[\] : [\x21\x3A\x3B\x3C\x3E\x3F\x40\x5B\x5C\x5D#-,]*/
                retVal -= Regex.Matches(asciiString, @"[\x21\x3A\x3B\x3C\x3E\x3F\x40\x5B\x5C\x5D#-,]").Count;
            }
            return retVal;
        }

        #endregion

    }

    // this class is used as a container by BitSolution

    public class BitSolutionAtom
    {
        public int level;       // the level in solution
        public string data;     // the binaryForm of the BitSolutionAtom
        public int rate;        // the calculated rate
        public bool parityOk;      // true if this Atom contains valid data (no parity error)
        public int bpc;         // the bpc used to calculate the level, rate, 
        public int add;         // for debug purposes (used in Debuger)

        // for debug purposes
        public override string ToString()
        {
            return String.Format("{0}, level: {1}: rate: {2}, binaryString: {3}, len: {4}, [{5}]", parityOk ? "CRC ok" : "CRC fail", level, rate, data, data.Length, ToString(add));
        }

        // used by BitSolution to retrieve the data
        public string ToString(int add)
        {
            if (!parityOk)
                return ""; // fix me: not implemented.

            return BitSolution.binaryStringToString(data, bpc, add);
        }


    }

}
