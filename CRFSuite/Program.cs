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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using TaskDialogDLL;

namespace crf
{
    static class Program
    {
        
        public static string defaultname = "crfsuite";
        public static string extension = "crf";
        public static bool enableInternetUpdates; /* when disabled some functionality is disabled to ensure maximum privacy */
        public static string name; /* the filename of the executable file that started the application, excluding the extension */
        public static string fileName; /* the filename of the executable file that started the application, excluding the extension */
        public static string directoryName ; /* the directory of the executable file that started the application */
        public static string executablePath; /* the executable file that started the application, including the executable name */
        public static string keysEncryptionWarning = "";
        public static bool showKeysEncryptionWarning = true;
        private static string _defaultPass = "_N0C}nUvBG-79qLE$q3R79fvO^n9UhnROUQN";


        static private Timer timer = new Timer();
        static private int tries = 0;

        static public void updatePath(string fullName)
        {
            executablePath = fullName; /* useful as the user has the option to change the name of the file */
            directoryName = Path.GetDirectoryName(executablePath);
            fileName = Path.GetFileNameWithoutExtension(executablePath);

            int u = fileName.IndexOf('_');
            if (u > 0)
                name = fileName.Substring(0, u);
            else
                name = fileName;

            enableInternetUpdates = (name.ToLower() == defaultname.ToLower());
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool fileOpened = false;

//            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

            updatePath(Application.ExecutablePath);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Properties.Settings.Default.showDecoderSwitchWarning = true;

            string[] commandLine = Environment.GetCommandLineArgs();
            /*
            string[] commandLine = new string[3];
            commandLine[1] = "-del";
            commandLine[2] = "c:\\149243.crf";
             * */

            processParams();

            for (int i = 1; i < commandLine.Length; i++)
            {
                if (commandLine[i] == "-del" || commandLine[i] == "-p")
                {
                    i++; /* skip next param as it is specifing the file to delete */
                }
                else if (File.Exists(commandLine[i]))
                {
                    (new frmDecode()).ShowDialog(commandLine[i]);
                    fileOpened = true;
                }
            }

            if (!fileOpened)
                Application.Run(new Downloader());
        }
        
        static private void processParams()
        {
            bool hasPass = false;
            string[] commandLine = Environment.GetCommandLineArgs();

            for (int i = 1; i < commandLine.Length - 1; i++)
            {
                if (commandLine[i] == "-del")
                {
                    deleteFiles(timer, null);
                    i++;
                }
                else if (commandLine[i] == "-p")
                {
                    if (commandLine[i + 1] != "")
                    {
                        hasPass = true;
                        if (!KeyManager.Instance.logIn(commandLine[i + 1]))
                            KeyManager.Instance.changePass(commandLine[i + 1]);
                    }
                    i++;
                }
            }

            if (!hasPass)
            {
                if (!KeyManager.Instance.logIn(_defaultPass))
                    KeyManager.Instance.changePass(_defaultPass);
                keysEncryptionWarning =
                   "The stored keys are encrypted by using a pre-set password. " +
                   "If you use this feature and a professional attacker gains access to this computer, " +
                   "he might be able to decrypt your keys.\n\n" +
                   "To prevent this from happening, please use your own password by launching " +
                   "this program with the -p parameter. We recommend that you use a password that is at least 16 characters long.\n\nExample:\n"
                   + executablePath + " -p \"your password\"";                   
            }
        }

        static public void updateKeyEncriptionWarning(IWin32Window Owner)
        {
            TaskDialog.MessageBox(Owner,
                    "Security warning", /* window title */
                    "Warning!", /* title */
                    Program.keysEncryptionWarning, /* content */
                    "Don't show this again",
                    TaskDialogButtons.OK,
                    SysIcons.Warning
                    );

            Program.showKeysEncryptionWarning = !TaskDialog.VerificationChecked;
        }

        static private void deleteFiles(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            timer.Enabled = false;
            timer.Stop();

            string[] commandLine = Environment.GetCommandLineArgs();
            for (int i = 1; i < commandLine.Length - 1; i++)
            {
                if (commandLine[i] != "-del")
                    continue;
                
                i++; /* as we are deleting the next param */

                if (!File.Exists(commandLine[i]))
                    continue;

                try
                {
                    File.Delete(commandLine[i]);
                }
                catch 
                {
                    tries++;
                    if (tries < 10 && timer.Enabled == false)
                    {
                        timer.Interval = 500;
                        timer.Tick += deleteFiles;
                        timer.Enabled = true;
                        timer.Start();
                    }
                }                
            }
        }

    }
}