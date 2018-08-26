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
    public class CrfWriter : FileStream
    {
        private byte[] header = new byte[] { (byte)'S', (byte)'C', (byte)'R', 1 };
        private byte[] buf = new byte[1024];

        public CrfWriter(string path) : base(path, FileMode.Create, FileAccess.Write)
        {
        }

        public void Write(string password, List<Card> cards)
        {
            if (cards == null)
                return;
            MemoryStream stream = new MemoryStream();

            int count = cards.Count;

            for (int i = 0; i < count; i++)
                WriteCard(stream, cards[i]);

            byte[] data;
            if (password != "")
                data = Aes.encryptData(stream, password);
            else
                data = stream.ToArray();

            if (data != null)    
                Write(data, 0, data.Length);
            else
                throw new Exception("error encrypting data");
        }

        private void WriteCard(MemoryStream stream, Card card)
        {
            if (card == null)
                return;

            int cardLength = 12;

            header.CopyTo(buf, 0);            
            buf[4] = (byte)card._timeStamp1;
            buf[5] = (byte)(card._timeStamp1>>8);
            buf[6] = (byte)(card._timeStamp1>>16);
            buf[7] = (byte)(card._timeStamp1>>24);
            buf[8] = (byte)(card._timeStamp2 * 256 / 1000);
            if (card._tracks == null)
            {
                buf[9] = buf[10] = buf[11] = (byte)0;
            }
            else
            {
                int index = cardLength;
                for (int i = 0; i < 3; i++)
                {
                    if (card._tracks[i] != null)
                    {
                        buf[9 + i] = (byte)copyTrack(card._tracks[i].trackBytes, buf, index);
                        index += buf[9 + i];
                    }
                    else
                    {
                        buf[9 + i] = 0;
                    }
                 }
                cardLength += buf[9] + buf[10] + buf[11];

                /* and meta data */
                buf[index++] = (byte)'M'; /* card serialise version */
                buf[index++] = (byte)'T'; /* card serialise version */
                buf[index++] = (byte)'D'; /* card serialise version */
                buf[index++] = 0; /* card serialise version */
                cardLength += 4;
                for (int i = 0; i < 3; i++)
                {
                    cardLength++;
                    if (card._tracks[i] == null)
                        buf[index++] = 0;
                    else
                    {
                        buf[index++] = (byte)card._tracks[i].serialiseMetaDataSize();
                        cardLength += card._tracks[i].serialiseMetaDataSize();
                    }
                }

                for (int i = 0; i < 3; i++)
                    if (card._tracks[i] != null)
                        card._tracks[i].serialiseMetaData(ref buf, ref index);


            }

            stream.Write(buf, 0, cardLength);
        }

        private int copyTrack(byte[] source, byte[] destination, int destinationIndex)
        {
            int start, stop = 0;

            for (start = 0; start < source.Length && source[start] == 0; start++) ; /* strip leading 0s */
            for (stop = source.Length - 1; start <= stop && source[stop] == 0; stop--) ; /* strip ending 0s */

            if (start <= stop)
                Array.Copy(source, start, destination, destinationIndex, stop - start + 1);

            return stop - start + 1;
        }
    }
}
