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
using System.IO;
using System.Security.Cryptography;

namespace crf
{
    class Utils
    {
        static public string wordWrap(string text, int maxLength)
        {
            // Return empty list of strings if the text was empty
            if (text.Length == 0)
                return text;

            string[] words = text.Split(' ');
            string lines = "";
            string currentLine = "";
            string currentWord;

            foreach (var st in words)
            {
                currentWord = st;
                if (currentWord.Contains("\n"))
                {
                    int lastNewLine = currentWord.LastIndexOf("\n");
                    if (currentLine.Length + 1 + currentWord.IndexOf("\n") > maxLength)
                        lines += "\n" + currentLine + "\n" + currentWord.Substring(0, lastNewLine);
                    else
                        lines += "\n" + currentLine + " " + currentWord.Substring(0, lastNewLine);

                    currentWord = currentWord.Substring(lastNewLine + 1, currentWord.Length - lastNewLine - 1);
                    currentLine = "";
                }

                if ((currentLine.Length > maxLength) ||
                    ((currentLine.Length + currentWord.Length) > maxLength))
                {
                    lines += "\n" + currentLine;
                    currentLine = "";
                }

                if (currentLine.Length > 0)
                    currentLine += " " + currentWord;
                else
                    currentLine += currentWord;
            }

            if (currentLine.Length > 0)
                lines += "\n" + currentLine;

            if (lines[0] == '\n')
                return lines.Substring(1, lines.Length - 1);

            return lines;
        }


        static public string getFilter(string extension)
        {
            return extension.ToLower() + " Files (*." + extension.ToLower() + ")|*." + extension;
        }
    }

    public class Aes
    {
        public enum Mode
        {
            CBC,
            ECB
        }

        public static readonly int[] acceptedBits = { 128, 192, 256 };

        private static readonly byte[] IV = new byte[] { 0x7F, 0x83, 0x71, 0x0D, 0x4F, 0x1F, 0x21, 0x71, 0x2a, 0xDC, 0x4A, 0x7f, 0x74, 0x33, 0x75, 0xC8 };

        public static byte[] convertKey(string textKey)
        {
            int binaryKeyBits = 0;
            int i, j;
            int textKeyBits = textKey.Length * 4; /* when key is written in hex, each byte is 4 bits. f = 4 bits, ff = 8 bits, etc */

            for (i = 0; i < acceptedBits.Length; i++)
                if (textKeyBits == acceptedBits[i])
                    binaryKeyBits = acceptedBits[i];

            if (binaryKeyBits == 0)
                throw new Exception("invalid key length");

            byte[] buf = new byte[binaryKeyBits / 8];

            for (i = 0, j = 0; j < textKey.Length; i++, j += 2)
            {
                buf[i] = (byte)Convert.ToInt16(textKey.Substring(j, 2), 16);
            }

            return buf;
        }

        public static void decryptData(byte[] readBuf, string keyText, Mode mode)
        {
            byte[] binaryKey;
            binaryKey = convertKey(keyText);

            RijndaelManaged aes = new RijndaelManaged();
            aes.BlockSize = 128;
            aes.KeySize = binaryKey.Length * 8; /* in bits */
            if (mode == Mode.CBC)
            {
                aes.IV = IV;
                aes.Mode = CipherMode.CBC;
            }
            else
            {
                aes.Mode = CipherMode.ECB;
            }
            aes.Padding = PaddingMode.None;
            aes.Key = binaryKey;

            /* method1: no extra memory */
            ICryptoTransform transform = aes.CreateDecryptor();
            int inputCount = readBuf.Length - (readBuf.Length % transform.InputBlockSize);
            int num2 = transform.TransformBlock(readBuf, 0, inputCount, readBuf, 0);
            if (num2 != inputCount)
            {
                throw new Exception(string.Concat(new object[] { "Decryption incomplete: ", num2, " of ", inputCount, " bytes.\nPlease contact the developer." }));
            }
            // method2: using extra memory
            // MemoryStream stream = new MemoryStream();
            // CryptoStream cstream = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Write);
            // cstream.Write(readBuf, 0, readBuf.Length);
            // cstream.Close();
            // stream.ToArray().CopyTo(readBuf, 0);
        }

        public static byte[] encryptData(MemoryStream stream, string keyText)
        {
            byte[] binaryKey;
            byte[] buf = null;
            binaryKey = convertKey(keyText);

            RijndaelManaged aes = new RijndaelManaged();
            aes.BlockSize = 128;
            aes.KeySize = binaryKey.Length * 8; /* in bits */
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.None;
            aes.Key = binaryKey;

            ICryptoTransform transform = aes.CreateEncryptor();
            byte[] padding = new byte[transform.InputBlockSize];
            Array.Clear(padding, 0, transform.InputBlockSize);
            int left = transform.InputBlockSize - (int)stream.Length % transform.InputBlockSize;
            stream.Write(padding, 0, left);
            buf = stream.ToArray();

            /* method1: no extra memory */
            int inputCount = (int)stream.Length;
            int num2 = transform.TransformBlock(buf, 0, inputCount, buf, 0);
            if (num2 != inputCount)
            {
                throw new Exception(string.Concat(new object[] { "Encryption incomplete: ", num2, " of ", inputCount, " bytes.\nPlease contact the developer." }));
            }
            return buf;
        }
    }

}
