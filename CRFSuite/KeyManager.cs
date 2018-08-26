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
using crf.Properties;
using System.Collections;

namespace crf
{
    class KeyManager
    {
        private string _password = "";
        private string[] _keys = { };
        #region Generic functions

        public static bool keyExists(string[] keys, string key)
        {
            for (int i = 0; i < keys.Length; i++)
                if (key == keys[i])
                    return true;

            return false;
        }

        public static string[] removeDuplicateAndInvalidKeys(string[] s)
        {
            string item;
            string[] uniques;
            Hashtable table = new Hashtable();
            for (int i = 0; i < s.Length; i++)
            {
                item = s[i].ToLower();
                if (!table.Contains(item) && isValidKey(item))
                    table.Add(item, 0);
            }
            uniques = new string[table.Count];
            table.Keys.CopyTo(uniques, 0);
            return uniques;
        }

        public static bool isValidKey(string key)
        {
            if (key.Length != 32)
                return false;

            foreach (char character in key)
            {
                if (!((character >= '0' && character <= '9') ||
                      (character >= 'a' && character <= 'f')))
                    return false;
            }
            return true;
        }

        public static string array2string(string[] keys)
        {
            string k = "";

            for (int i = 0; i < keys.Length; i++)
            {
                if (k.Length == 0)
                    k = keys[i];
                else
                    k += "," + keys[i];
            }

            return k;
        }

        public static string[] getValidKeys(string[] keys)
        {
            if (keys == null || keys.Length == 0)
                return new string[0];

            keys = removeDuplicateAndInvalidKeys(keys); /* will also put everything to lower case */
            return keys;
        }

        #endregion

        #region Singleton implementation

        /** Returns the Settings instance */
        private static KeyManager _instance = null; /** Singleton instance */
        public static KeyManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new KeyManager();

                return _instance;
            }

            set
            {
                _instance = value;
            }
        }
        #endregion

        public KeyManager()
        {
        }

        ~KeyManager()
        {
            save();
        }

        public int dataSize 
        {
            get
            {
                return (Settings.Default.keys.Length);
            }
        }

        private void save()
        {
            if (_password.Length == 0)
                return;

            Settings.Default.keys = StringUtil.Encrypt(array2string(_keys), _password);
            Settings.Default.Save();
        }

        public bool logIn(string st)
        {
            if (st.Length == 0)
                return false;

            save();

            /* if there are no keys to load, st is accepted as new password */
            if (Settings.Default.keys.Length == 0)
            {
                _password = st;
                return true;
            }

            string k = StringUtil.Decrypt(Settings.Default.keys, st);
            _keys = getValidKeys(k.Split(','));

            if (_keys.Length > 0)
            {
                _password = st;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool changePass(string st)
        {
            if (st.Length == 0)
                return false;
            _password = st;
            save();
            return true;
        }         

        public bool addKey(string key)
        {
            if (_password == "")
                throw new Exception("do not call this function without setting the password");

            if (!isValidKey(key))
                return false;

            string[] _newKeys = new string[_keys.Length + 1];

            _newKeys[0] = key;
            _keys.CopyTo(_newKeys, 1);
            _keys = _newKeys;
            save();

            return true;
        }
        
        public string[] getKeys()
        {
            return _keys;
        }

        public void setKeys(string[] keys)
        {
            if (_password == "")
                throw new Exception("do not call this function without setting the password");

            _keys = getValidKeys(keys);
            save();
        }
 
        public int getKeyCount()
        {
            return _keys.Length;
        }

        public void eraseKeys()
        {
            _keys = new string[0];
            save();
        }

    }
}
