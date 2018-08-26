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
    class CardReader
    {
        /**
         * CRF stream to read from
         */
        private CrfReader _crfStream;

        /**
         * Constructor.
         * 
         * @param file File name where to read cards from.
         */
        public CardReader(string filename)
        {
            _crfStream = new CrfReader(filename);
        }

        /**
         * Reads an array of cards from the file
         * 
         * @param password Password used to decrypt file if we are opening a crf file.
         *                 Can be null if file is not encrypted. NeedPassword
         *                 property can be used to know if password is needed.
         * @return Cards read
         */
        public List<Card> Read(string password, bool recoveryMode)
        {
            return _crfStream.Read(password, recoveryMode);
        }

        /**
         * Closes the input stream
         */
        public void Close()
        {
            if (null != _crfStream)
                _crfStream.Close();
        }

        public CardFileFormat.Format FileFormat
        {
            get
            {
                if (_crfStream != null)
                    return CardFileFormat.Format.CRF;
                else
                    return CardFileFormat.Format.NONE;
            }
        }

        public static bool NeedPassword(string filename)
        {
            //all supported input formats need a password
            return true;
        }
    }
}
