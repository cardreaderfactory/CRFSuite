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
using System.Security.Cryptography; 

namespace crf
{
    class StringUtil
    {
        private static byte[] salt = new byte[] { 0x39, 0x77, 0x92, 0xd7, 0x4b, 0xd1, 0x8c, 0x6a, 0x3a, 0x24, 0xf3, 0xa0, 0xcc, 0xe1, 0xe9, 0x63 };

        public static bool isValidKey(string key)
        {
            //not needed because text if changed to upper but just in case
            if (key.Length != 32)
                return false;

            key = key.ToLower();

            foreach (char character in key)
            {
                if (!((character >= '0' && character <= '9') ||
                      (character >= 'a' && character <= 'f')))
                    return false;
            }

            return true;
        }


        /**
         * Copied from http://en.wikibooks.org/wiki/Algorithm_Implementation/Strings/Longest_common_substring.
         * It has been modified to return indexes of the common sequence on input strings.
         * this function is a bottle neck!!!
         */
        public static bool LongestCommonSubstring(string str1, string str2, int alignChars,
                                                 out int str1Index, out int str2Index, 
                                                 out string sequence)
        {
            str1Index = -1;
            str2Index = -1;
            sequence = string.Empty;
            if (String.IsNullOrEmpty(str1) || String.IsNullOrEmpty(str2))
                return false;

            int[,] num = new int[str1.Length, str2.Length];

            for (int i = 0; i < str1.Length; i++)
            {
                for (int j = 0; j < str2.Length; j++)
                {
                    if (str1[i] != str2[j])
                        num[i, j] = 0;
                    else
                    {
                        if ((i == 0) || (j == 0))
                            num[i, j] = 1;
                        else
                            num[i, j] = 1 + num[i - 1, j - 1];
                        if (num[i, j] > alignChars)
                        {
                            str1Index = i;
                            str2Index = j;
                            return true;
                        }
                    }
                }
            }


            return false;
        }


        public static string Encrypt(string clearText, string password)
        {
            try
            {
                byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);

                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, salt);

                // Now get the key/IV and do the encryption using the function that accepts byte arrays. 
                // Using Rfc2898DeriveBytes object we are first getting  32 bytes for the Key 
                // (the default Rijndael key length is 256bit = 32bytes)
                // and then 16 bytes for the IV. IV should always be the block size, which is by default 
                // 16 bytes (128 bit) for Rijndael. 
                // You can also read KeySize/BlockSize properties off
                // the algorithm to find out the sizes. 

                byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));

                // Now we need to turn the resulting byte array into a string. 
                // A common mistake would be to use an Encoding class for that.
                // It does not work because not all byte values can be
                // represented by characters. 
                // We are going to be using Base64 encoding that is designed
                //exactly for what we are trying to do. 

                return Convert.ToBase64String(encryptedData);
            }
            catch
            {
                return "";
            }
        }

        public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            // Create a MemoryStream to accept the encrypted bytes 

            MemoryStream ms = new MemoryStream();

            // Create a symmetric algorithm. 
            // We are going to use Rijndael because it is strong and
            // available on all platforms. 

            Rijndael alg = Rijndael.Create();

            // Now set the key and the IV. 
            // We need the IV (Initialization Vector) because
            // the algorithm is operating in its default 
            // mode called CBC (Cipher Block Chaining).
            // The IV is XORed with the first block (8 byte) 
            // of the data before it is encrypted, and then each
            // encrypted block is XORed with the
            // following block of plaintext.
            // This is done to make encryption more secure. 
            // There is also a mode called ECB which does not need an IV,
            // but it is much less secure. 

            alg.Key = Key;
            alg.IV = IV;

            // Create a CryptoStream through which we are going to be
            // pumping our data. 
            // CryptoStreamMode.Write means that we are going to be
            // writing data to the stream and the output will be written
            // in the MemoryStream we have provided. 

            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);

            // Write the data and make it do the encryption 

            cs.Write(clearData, 0, clearData.Length);

            // Close the crypto stream (or do FlushFinalBlock). 
            // This will tell it that we have done our encryption and
            // there is no more data coming in, 
            // and it is now a good time to apply the padding and
            // finalize the encryption process. 

            cs.Close();

            // Now get the encrypted data from the MemoryStream.
            // Some people make a mistake of using GetBuffer() here,
            // which is not the right way. 

            byte[] encryptedData = ms.ToArray();

            return encryptedData;
        }

        public static string Decrypt(string cipherText, string Password)
        {
            try
            {
                // First we need to turn the input string into a byte array. 
                // We presume that Base64 encoding was used 

                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                // Then, we need to turn the password into Key and IV 
                // We are using salt to make it harder to guess our key
                // using a dictionary attack - 
                // trying to guess a password by enumerating all possible words. 

                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Password, salt);


                // Now get the key/IV and do the decryption using
                // the function that accepts byte arrays. 
                // Using PasswordDeriveBytes object we are first
                // getting 32 bytes for the Key 
                // (the default Rijndael key length is 256bit = 32bytes)
                // and then 16 bytes for the IV. 
                // IV should always be the block size, which is by
                // default 16 bytes (128 bit) for Rijndael. 
                // You can also read KeySize/BlockSize properties off
                // the algorithm to find out the sizes. 

                byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));

                // Now we need to turn the resulting byte array into a string. 
                // A common mistake would be to use an Encoding class for that.
                // It does not work 
                // because not all byte values can be represented by characters. 
                // We are going to be using Base64 encoding that is
                // designed exactly for what we are trying to do. 

                return System.Text.Encoding.Unicode.GetString(decryptedData);
            }
            catch
            {
                return "";
            }
        }

        // Decrypt a byte array into a byte array using a key and an IV 

        public static byte[] Decrypt(byte[] cipherData,
                                    byte[] Key, byte[] IV)
        {
            // Create a MemoryStream that is going to accept the
            // decrypted bytes 

            MemoryStream ms = new MemoryStream();

            Rijndael alg = Rijndael.Create();

            // Now set the key and the IV. 
            // We need the IV (Initialization Vector) because the algorithm

            // is operating in its default 
            // mode called CBC (Cipher Block Chaining). The IV is XORed with
            // the first block (8 byte) 
            // of the data after it is decrypted, and then each decrypted
            // block is XORed with the previous 
            // cipher block. This is done to make encryption more secure. 
            // There is also a mode called ECB which does not need an IV,
            // but it is much less secure. 

            alg.Key = Key;
            alg.IV = IV;
            //alg.Mode = CipherMode.CBC;

            // Create a CryptoStream through which we are going to be
            // pumping our data. 
            // CryptoStreamMode.Write means that we are going to be
            // writing data to the stream 
            // and the output will be written in the MemoryStream
            // we have provided. 

            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);

            // Write the data and make it do the decryption 

            cs.Write(cipherData, 0, cipherData.Length);

            // Close the crypto stream (or do FlushFinalBlock). 
            // This will tell it that we have done our decryption
            // and there is no more data coming in, 
            // and it is now a good time to remove the padding
            // and finalize the decryption process. 

            cs.Close();

            // Now get the decrypted data from the MemoryStream.
            // Some people make a mistake of using GetBuffer() here,
            // which is not the right way. 

            byte[] decryptedData = ms.ToArray();

            return decryptedData;
        }

    }
}
