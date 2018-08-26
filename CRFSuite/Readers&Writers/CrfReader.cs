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
    class CrfReader : FileStream
    {
        public CrfReader(string path) : base(path, FileMode.Open, FileAccess.Read)
        {
        }

        private bool hasValidData(byte[] buffer)
        {
            if ((buffer[0] == 'S') && (buffer[1] == 'C') && (buffer[2] == 'R'))
                return true;
            else
                return false;
        }

        

        /**
         * Checks if pass is valid
         * returns null if no key has been found
         * return "" if any key will work
         */
        public string tryKnownKeys()
        {
            byte[] buffer = new byte[16];
            byte[] decrypted = new byte[16];

            try
            {
                int len = this.Read(buffer, 0, 16);

                //if file is not encrypted just open it.
                if (hasValidData(buffer))
                    return "";

                string[] keys = KeyManager.Instance.getKeys();
                for (int i = 0; i < keys.Length; i++)
                {
                    buffer.CopyTo(decrypted, 0);
                    Aes.decryptData(decrypted, keys[i], Aes.Mode.CBC);
                    if (hasValidData(decrypted))
                        return keys[i];

                    buffer.CopyTo(decrypted, 0);
                    Aes.decryptData(decrypted, keys[i], Aes.Mode.ECB);
                    if (hasValidData(decrypted))
                        return keys[i];
                }
            }
            catch
            {
            };

            return null;
        }

        private static bool hasFirstFlag(byte b)
        {
            if (b == 'S')
                return true;
            else
                return false;
        }

        private static int findAllCards(byte[] buffer)
        {
            int cards = 0;


            for (int i = Array.FindIndex(buffer, 0, hasFirstFlag);
                i != -1;
                i = Array.FindIndex(buffer, i+1, hasFirstFlag))
            {
                if (i >= buffer.Length - 2)
                    break;

                if (buffer[i + 1] == 'C' && buffer[i + 2] == 'R')
                {
                    cards++;
                    i += 2;
                }

            }
            return cards;
        }

        private static byte[] findBestSolution(byte[] buffer, string password)
        {
            byte[] solution = new byte[buffer.Length];
            int maxCount = 0;          

            for (int i = 0; i < buffer.Length-16; i++)
            {
                for (int a = (int)Aes.Mode.CBC; a <= (int)Aes.Mode.ECB; a++)
                {
                    byte[] decrypted = new byte[buffer.Length - i];
                    Array.Copy(buffer, i, decrypted, 0, buffer.Length - i);
                    Aes.decryptData(decrypted, password, (Aes.Mode)(a));
                    int count = findAllCards(decrypted);
                    if (count > maxCount)
                    {
                        solution = decrypted;
                        maxCount = count;
                    }

                }
            }

            return solution;
        }

        /**
         * Deserializes an array of cards from a file
         */
        public List<Card> Read(string password, bool recoveryMode)
        {
            byte[] buffer = new byte[this.Length];
            int len = this.Read(buffer, 0, (int)this.Length);

            //if file is not encrypted just open it.
            if (hasValidData(buffer))
                return BuildCardsFromStream(buffer);

            if (password == null)
                return BuildCardsFromStream(buffer);

            byte[] decrypted;

            if (recoveryMode)
            {
                decrypted = findBestSolution(buffer, password);
            }
            else
            {
                decrypted = new byte[this.Length];
                buffer.CopyTo(decrypted, 0);
                Aes.decryptData(decrypted, password, Aes.Mode.CBC);
                if (!hasValidData(decrypted))
                {
                    buffer.CopyTo(decrypted, 0);
                    Aes.decryptData(decrypted, password, Aes.Mode.ECB);
#if DEBUG
                    if (hasValidData(decrypted))
                        System.Diagnostics.Debug.WriteLine("Used EBC decoding mode");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Used CBC decoding mode");
#endif
                }

            }

            return BuildCardsFromStream(decrypted);
        }

        /**
         * Builds an array of cards from a stream of bytes.
         * 
         * @param buffer Stream of bytes used to build the cards. Function
         *               will look for cards until the end of the buffer
         *               
         * @return Array with all created cards. Can be empty but it will
         *         never be null.
         */
        public List<Card> BuildCardsFromStream(byte[] buffer)
        {
            List<Card> cards = new List<Card>();
            uint cardNumber = 0;

            Card previousCard = null;

            for (int i = 0; (i + 3) < buffer.Length; )
            {
                if ((buffer[i] == (byte)'S') &&
                    (buffer[i + 1] == (byte)'C') &&
                    (buffer[i + 2] == (byte)'R'))
                {
                    // build a card and skip till next card
                    try
                    {
                        Card card = CardBuilder(buffer, ref i, cardNumber, previousCard);
                        if (null != card)
                        {
                            cards.Add(card);
                            cardNumber++;
                            previousCard = card;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    i++;
                }
            }

            return cards;
        }

        /**
         * Reads the time diff with the previous card if it is present. It should be at the end of
         * the 3 tracks and before than next card.
         * 
         * @param cardBytes    bytes read from the file. all cards.
         * @param position     first byte for the time diff.
         * @param timeDiff     time diff calculated. 0 if no present.
         * @param timeDiffSize number of bytes read used from cardBytes to calculate the time diff.
         */
        void ReadTimeDiff(byte[] cardBytes, int position, out double timeDiff, out int timeDiffSize)
        {
            timeDiff = 0;
            timeDiffSize = 0;

            if (cardBytes.Length < (position + 4))
                return;

            char[] charDiff = new char[4];
            for (int i = 0; i < 4; i++)
                charDiff[i] = (char)cardBytes[position + i];

            try
            {
                string time = new string(charDiff, 0, 4);

                if ((time == "SCRD") || (time == " SCR") || (time == "  SC") || (time == "   S") || (time == "    "))
                    return;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            timeDiff = ((uint)(cardBytes[position + 3] << 24)) +
                       ((uint)(cardBytes[position + 2] << 16)) +
                       ((uint)(cardBytes[position + 1] << 8)) +
                       ((uint)cardBytes[position]);
            if (timeDiff > 0)
            {
                timeDiff = timeDiff * 1000;
                timeDiff = timeDiff / 256;
            }

            timeDiffSize = 4;
        }

        /**
         * Function used to build a card.
         * 
         * @param cardBytes    Bytes used to build the card
         * @param position     In value is the first byte that must be used to build the card
         *                     Out value is the last card byte + 1 (probably the first
         *                     byte of the next card)
         * @param cardNumber   Number to use in swipes fields in case the card is type 1
         * @param previousCard previous card. Needed to calculate time diffs. Can be null if this
         *                     is the first card on the file. If it is null diff time is defined with
         *                     when reader was started.
         *                 
         * @return Card created. Can return null is card cannot be created.
         */
        private Card CardBuilder(byte[] cardBytes, ref int position, uint cardNumber, Card previousCard)
        {
            uint timeStamp1;
            uint timeStamp2;
            double timeDiff;
            int timeDiffSize;
            uint swipes;
            ushort track1Len;
            ushort track2Len;
            ushort track3Len;

            switch (cardBytes[position + 3])
            {
                case (byte)'D':
                    if (cardBytes.Length < (position + 13))
                    {
                        position += 4;
                        return null;
                    }

                    timeStamp1 = ((uint)(cardBytes[position + 7] << 24)) +
                                 ((uint)(cardBytes[position + 6] << 16)) +
                                 ((uint)(cardBytes[position + 5] << 8)) +
                                 ((uint)cardBytes[position + 4]);
                    timeStamp2 = 0;
                    swipes = ((uint)(cardBytes[position + 8] << 8)) +
                                 ((uint)cardBytes[position + 9]);
                    track1Len = (ushort)cardBytes[position + 10];
                    track2Len = (ushort)cardBytes[position + 11];
                    track3Len = (ushort)cardBytes[position + 12];

                    //skip header and move to first track
                    position += 13;

                    //try to read time diff with the previous card
                    ReadTimeDiff(cardBytes, position + track1Len + track2Len + track3Len, 
                                 out timeDiff, out timeDiffSize);
                    if ((timeDiff > 0) && (previousCard != null))
                    {
                        uint timeStamp1Aux = (uint)((previousCard.TimeAsDouble + timeDiff) / 1000);

                        // difference cannot be higher than 1 second. so seconds timeStamp must be the same.
                        // if this is not true it is probably the device has been restarted
                        if (timeStamp1Aux == timeStamp1)
                            timeStamp2 = (uint)((previousCard.TimeAsDouble + timeDiff) % 1000);
                    }
                    break;

                case 1:
                    if (cardBytes.Length < (position + 12))
                    {
                        position += 4;
                        return null;
                    }

                    timeStamp1 = ((uint)(cardBytes[position + 7] << 24)) +
                                 ((uint)(cardBytes[position + 6] << 16)) +
                                 ((uint)(cardBytes[position + 5] << 8)) +
                                 ((uint)cardBytes[position + 4]);
                    timeStamp2 = (uint) (((ushort)cardBytes[position + 8]) * 1000 + 255) / 256; // adding 255 to round near the correct integer
                    swipes = cardNumber;
                    track1Len = (ushort)cardBytes[position + 9];
                    track2Len = (ushort)cardBytes[position + 10];
                    track3Len = (ushort)cardBytes[position + 11];

                    //skip header and move to first track
                    position += 12;

                    //no time diff
                    timeDiff = 0;
                    timeDiffSize = 0;
                    break;

                default:
                    position += 3;
                    return null;
            } // switch (cardBytes[position + 3])

            //check that lengths are correct.
            //not needed. application has to work with chard that do not complaint with the standard
            /*
            if ((((track1Len * 8) / VariantSettings.bpc[0]) > VariantSettings.maxChars[0]) ||
                (((track2Len * 8) / VariantSettings.bpc[1]) > VariantSettings.maxChars[1]) ||
                (((track3Len * 8) / VariantSettings.bpc[2]) > VariantSettings.maxChars[2]))
                return null;
            */

            //build the card
            Card retValue = new Card(swipes, cardNumber,
                                     timeStamp1, timeStamp2,
                                     cardBytes, position,
                                     track1Len, track2Len,
                                     track3Len, timeDiffSize);
            if (previousCard == null)
                retValue.timeDiff = retValue.TimeAsDouble;
            else
                retValue.timeDiff = retValue.TimeAsDouble - previousCard.TimeAsDouble;
            //skip tracks
            position += track1Len + track2Len + track3Len + timeDiffSize;

            return retValue;
        }
    }
}
