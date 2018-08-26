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
    public class CardWriter
    {
        /**
         * Possible data to save for a card
         */
        public const int READER_CARD = 0x0001;
        public const int CARD_NUMBER = 0x0002;
        public const int DIRECTION = 0x0004;
        public const int TIME = 0x0008;
        public const int TRACK1 = 0x0010;
        public const int TRACK2 = 0x0020;
        public const int TRACK3 = 0x0040;

        /**
         * Array with all posible fields, so fields ID can be used in for expressions
         */
        //public readonly int[] Fields = { READER_CARD, CARD_NUMBER, TIME, TRACK1, TRACK2, TRACK3, DIRECTION };
        public readonly int[] Fields = { READER_CARD,CARD_NUMBER, DIRECTION, TIME, TRACK1, TRACK2, TRACK3 };

        /**
         * CRF stream where to write
         */
        private CrfWriter _crfStream;


        /**
         * Csv stream where to write
         */
        private CsvWriter _csvStream;

        /**
         * Csv stream where to write
         */
        private TxtWriter _txtStream;

        /**
         * Constructor.
         * 
         * @param file File name where cards will be saved. If file exists its content will be erased.
         *             If file does not exists it will be created.
         * @param saveFormat Format to save file. If NONE format will be chosen depending on file extension.
         *             
         * @warning Only csv, crf and text formats are supported at the moment. If file has any other format
         *          it will be saved as text.
         */
        public CardWriter(string file, CardFileFormat.Format saveFormat)
        {
            _csvStream = null;
            _txtStream = null;
            _crfStream = null;

            //what format do we have to save?
            if (saveFormat == CardFileFormat.Format.NONE)
            {
                if (file.EndsWith(Program.extension))
                {
                    saveFormat = CardFileFormat.Format.CRF;
                }
                else if (file.EndsWith(".csv"))
                {
                    saveFormat = CardFileFormat.Format.CSV;
                }
                else
                {
                    saveFormat = CardFileFormat.Format.TXT;
                }
            }

            if (saveFormat == CardFileFormat.Format.CRF)
            {
                _crfStream = new CrfWriter(file);
            }
            else if (saveFormat == CardFileFormat.Format.CSV)
            {
                _csvStream = new CsvWriter(file);
            }
            else //use txt as default
            {
                _txtStream = new TxtWriter(file);
            }
        }

        /**
         * Constructor.
         * 
         * @param outputStream Stream where to save cards.
         * @param saveFormat Format to save file. Only TXT is supported at the moment.
         *             
         * @warning Only csv, crf and text formats are supported at the moment. If file has any other format
         *          it will be saved as text.         */
        public CardWriter(MemoryStream outputStream, CardFileFormat.Format saveFormat)
        {
            _csvStream = null;
            _txtStream = null;
            _crfStream = null;

            //what format do we have to save?
            if (saveFormat != CardFileFormat.Format.TXT)
            {
                throw new ArgumentException("Only CardFileFormat.Format.TXT is supported with stream output");
            }

            _txtStream = new TxtWriter(outputStream);
        }

        /**
         * Constructor.
         * 
         * @param outputStream Stream where to save cards.
         * @param saveFormat Format to save file. Only TXT is supported at the moment.
         *             
         * @warning Only csv, crf and text formats are supported at the moment. If file has any other format
         *          it will be saved as text.         */
        public CardWriter(MemoryStream outputStream)
        {
            _txtStream = new TxtWriter(outputStream);
        }


        /**
         * Writes an array of cards to the stream.
         *
         * @param passowrd Password to encrypt file. Only needed if output format is CRF. 
         *                 In other cases can be null as it is not used.
         * @param cards  Array of cards to save
         * @param fields Mask of fields to save (i.e. CARD_NUMBER | TIME).
         */
        public void Write(string password, IList<Card> cards, int fields)
        {
            //crf format? => serialize array
            if (null != _crfStream)
            {
                //check password is correct
                if (password != "")
                    if ((password == null) || (password.Length != 32))
                        throw new ArgumentException("Incorrect password length");

                //in this case all fields are saved.
                _crfStream.Write(password, new List<Card>(cards));
                return;
            }

            //save as txt or csv
            int numberOfItems = 1;

            for (int i = 0; i < Fields.Length; i++)
            {
                if (Fields[i] == (fields & Fields[i]))
                    numberOfItems++;
            }

            object[] items = new object[numberOfItems];
            int[] itemsLength = null;
            if (null != _txtStream)
            {
                itemsLength = new int[numberOfItems];
            }

            //write headers names
            numberOfItems = 1;
            for (int i = 0; i < Fields.Length; i++)
            {
                if (Fields[i] == (fields & Fields[i]))
                {
                    items[numberOfItems] = Card.ColumnName(i);
                    if (null != itemsLength)
                        itemsLength[numberOfItems] = Card.ColumnName(i).Length;
                    numberOfItems++;
                }
            }

            //before writing headers longest items have to be calculated.
            if (null != itemsLength)
            {
                foreach (Card card in cards)
                {
                    numberOfItems = 0;
                    itemsLength[numberOfItems] = 0;
                    numberOfItems++;
                    if (READER_CARD == (fields & READER_CARD))
                    {
                        int length = card.ReaderCard.Length;
                        if (itemsLength[numberOfItems] < length)
                            itemsLength[numberOfItems] = length;
                        numberOfItems++;
                    }

                    if (CARD_NUMBER == (fields & CARD_NUMBER))
                    {
                        int length = card.CardNumber.Length;
                        if (itemsLength[numberOfItems] < length)
                            itemsLength[numberOfItems] = length;
                        numberOfItems++;
                    }
                    if (DIRECTION == (fields & DIRECTION))
                    {
                        int length = card.DirectionAsString.Length;
                        if (itemsLength[numberOfItems] < length)
                            itemsLength[numberOfItems] = length;
                        numberOfItems++;
                    }
                    if (TIME == (fields & TIME))
                    {
                        int length = card.Time.Length;
                        if (itemsLength[numberOfItems] < length)
                            itemsLength[numberOfItems] = length;
                        numberOfItems++;
                    }

                    if (TRACK1 == (fields & TRACK1))
                    {                       
                        int length = card.TrackAsAlignedString(0).Length;
                        if (itemsLength[numberOfItems] < length)
                            itemsLength[numberOfItems] = length;
                        numberOfItems++;
                    }

                    if (TRACK2 == (fields & TRACK2))
                    {
                        int length = card.TrackAsAlignedString(1).Length;
                        if (itemsLength[numberOfItems] < length)
                            itemsLength[numberOfItems] = length;
                        numberOfItems++;
                    }

                    if (TRACK3 == (fields & TRACK3))
                    {
                        int length = card.TrackAsAlignedString(2).Length;
                        if (itemsLength[numberOfItems] < length)
                            itemsLength[numberOfItems] = length;
                        numberOfItems++;
                    }
                }
            }

            //finally write headers
            if (null != _csvStream)
                _csvStream.Write(items);
            else
                _txtStream.Write(items, itemsLength);

            /* generate the card separator line */
            int maxLineLength = numberOfItems - 1; /* fields are separated by 1 char */
            int? previousColor = cards[0].TimeGroupColor;
            String separator = "";

            if (null != itemsLength)
                for (int i = 0; i < numberOfItems; i++)
                    maxLineLength += itemsLength[i];

            for (int i = 0; i < maxLineLength; i++)
                separator += "-";
            separator += "\r\n ";
            //write cards
            foreach (Card card in cards)
            {
                numberOfItems = 0;

                if (card.TimeGroupColor != previousColor)
                {
                    items[numberOfItems] = separator;
                    numberOfItems++;
                    previousColor = card.TimeGroupColor;
                }
                else
                {
                    items[numberOfItems] = "";
                    numberOfItems++;
                }


                if (READER_CARD == (fields & READER_CARD))
                {
                    items[numberOfItems] = card.ReaderCard;
                    numberOfItems++;
                }

                if (CARD_NUMBER == (fields & CARD_NUMBER))
                {
                    items[numberOfItems] = card.CardNumber;
                    numberOfItems++;
                }
                if (DIRECTION == (fields & DIRECTION))
                {
                    items[numberOfItems] = card.DirectionAsString;
                    numberOfItems++;
                }
                if (TIME == (fields & TIME))
                {
                    items[numberOfItems] = card.Time;
                    numberOfItems++;
                }

                if (TRACK1 == (fields & TRACK1))
                {
                    items[numberOfItems] = card.TrackAsAlignedString(0);
                    numberOfItems++;
                }

                if (TRACK2 == (fields & TRACK2))
                {
                    items[numberOfItems] = card.TrackAsAlignedString(1);
                    numberOfItems++;
                }

                if (TRACK3 == (fields & TRACK3))
                {
                    items[numberOfItems] = card.TrackAsAlignedString(2);
                    numberOfItems++;
                }


                if (null != _csvStream)
                {
                    _csvStream.Write(items);
                    _csvStream.Flush();
                }
                else
                {
                    _txtStream.Write(items, itemsLength);
                    _txtStream.Flush();
                }
            }
        }

        /**
         * Closes the output stream
         */
        public void Close()
        {
            if (null != _csvStream)
                _csvStream.Close();
            if (null != _txtStream)
                _txtStream.Close();
            if (null != _crfStream)
                _crfStream.Close();
        }

        public CardFileFormat.Format FileFormat
        {
            get
            {
                if (null != _csvStream)
                    return CardFileFormat.Format.CSV;
                else if (null != _crfStream)
                    return CardFileFormat.Format.CRF;
                else if (_txtStream != null)
                    return CardFileFormat.Format.TXT;
                else
                    return CardFileFormat.Format.NONE;
            }
        }

        /**
         * This function is static because caller needs to know if a password is needed for a file
         * before opening the file. once file is open it is deleted if it already exists, then user
         * can cancel password popup but file is already deleted.
         */
        public static bool NeedPassword(CardFileFormat.Format format)
        {
            //password is only needed for crf format.
            return (format == CardFileFormat.Format.CRF);
        }
    }
}
