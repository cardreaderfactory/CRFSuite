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
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace crf
{
    class FileAssociation
    {
        static string getRegistryValue(RegistryKey root, string path)
        {
            RegistryKey reg;
            string value;

            try
            {
                reg = root.OpenSubKey(path, false);
                if (reg == null)
                    return "";
                value = (string)reg.GetValue("");
                if (value == null)
                    return "";
            }
            catch
            {
                return "";
            }

            return value;
        }

        static void registerAssociation(RegistryKey root, string extension, string progId, string path)
        {
            try
            {
                root.CreateSubKey(extension).SetValue("", progId);
                RegistryKey key = Registry.ClassesRoot.CreateSubKey(progId);
                key.SetValue("", progId); /* description */
                key.CreateSubKey(@"Shell\Open\Command").SetValue("", "\"" + path + "\" \"%1\"");
                key.CreateSubKey("DefaultIcon").SetValue("", path + ",0");
            }
            catch (Exception ex)
            {
                string a;
                a = ex.ToString();

            };
        }

        static void deleteAssociation(RegistryKey root, string key)
        {
            try
            {
                root.DeleteSubKeyTree(key);
            }
            catch
            {
            };
        }

        public static bool isAssociated(string extension, string executablePath)
        {
            RegistryKey root = Registry.ClassesRoot;
            string value;            

            if (extension[0] != '.')
                extension = "." + extension;

            value = getRegistryValue(root, extension);
            if (value == "")
                return false;

            value = getRegistryValue(root, value + @"\Shell\Open\Command");
            if (value == "")
            {
                return false;
            }
            else
            {
                if (value.IndexOf("\" \"") != -1)
                    value = value.Substring(1, value.IndexOf("\" \"") - 1);

                return (value.ToLower() == executablePath.ToLower());
            }
        }

        public static void Associate(string extension, string executablePath)
        {
            if (extension[0] != '.')
                extension = "." + extension;

            if (!isAssociated(extension, executablePath))
            {
                RegistryKey root = Registry.ClassesRoot;
                string oldProgId = getRegistryValue(root, extension);
                string programName = Path.GetFileNameWithoutExtension(executablePath);

                if (oldProgId != "")
                    deleteAssociation(root, oldProgId);

                registerAssociation(root, extension, programName, executablePath);
            }
        }

        public static void deleteAssociation(string extension)
        {
            if (extension == "")
                return;

            if (extension[0] != '.')
                extension = "." + extension;

            RegistryKey root = Registry.ClassesRoot;
            string oldProgId = getRegistryValue(root, extension);
            if (oldProgId != "")
                deleteAssociation(root, oldProgId);

            deleteAssociation(root, extension);
        }

    }
}
