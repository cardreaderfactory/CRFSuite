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
using System.Text;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;

namespace crf
{

    class CommunicationManager
    {
        public enum State
        {
            PortClosed,
            Disconnected,
            LoggedOut,
            LoggedIn,
            Blocked
        };
        #region Manager Enums

        private int timeout = 400;

        public enum ReceiveTransmissionType { Sync, Async };

        /// <summary>
        /// enumeration to hold our message types
        /// </summary>
        public enum MessageType { Incoming, Outgoing, Normal, Warning, Error, Debug };
        #endregion

        public delegate void ShowMessage(IconBitmap icon, String message, String tooltip);

        #region Manager Variables
        //property variables
        private string _portName = string.Empty;
        public Form form;
        public ShowMessage showMessage;
        public ProgressBar progressBar;
        private ReceiveTransmissionType _recvTransType = ReceiveTransmissionType.Sync;


        //global manager variables
        private Color[] MessageColor = { Color.Blue, Color.Green, Color.Black, Color.Orange, Color.Red, Color.Gray };
        private FixedSerialPort comPort = new FixedSerialPort();

        private int blockMaxTime;
        private int blockedCurrentTime;
        public string blockTimeMessage;
        private State lastState = State.LoggedOut;

        #endregion

        #region Manager Properties

        /// <summary>
        /// property to hold the PortName
        /// of our manager class
        /// </summary>
        public string PortName
        {
            get { return _portName; }
            set { _portName = value; }
        }

        #endregion

        #region Manager Constructors

        /// <summary>
        /// Comstructor to set the properties of our
        /// serial port communicator to nothing
        /// </summary>
        public CommunicationManager()
        {
            _portName = string.Empty;
            //add event handler
            //comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
        }
        ~CommunicationManager()
        {
            //_displayLabel = null;
            ClosePort();
        }
        #endregion

        private static void debugLog(string st)
        {
#if DEBUG
//            System.Diagnostics.Debug.WriteLine(st);
#endif
        }

        #region WriteData
        public void WriteData(string msg)
        {
            //first make sure the port is open
            //if its not open then open it
            try
            {
                //send the message to the port
                comPort.Write(msg);
                //display the message
                //DisplayData(MessageType.Outgoing, msg);
            }
            catch (Exception ex)
            {
#if DEBUG                
                DisplayData(MessageType.Error, ex.Message + "\n" +ex.StackTrace);
#else
                DisplayData(MessageType.Error, ex.Message);
#endif
                return;
            }
        }
        #endregion     
       
        #region DisplayData
        /// <summary>
        /// method to display the data to & from the port
        /// on the screen
        /// </summary>
        /// <param name="type">MessageType of the message</param>
        /// <param name="msg">Message to display</param>
        [STAThread]
        private void DisplayData(MessageType type, string msg)
        {
            IconBitmap m;
            if (showMessage == null)
                return;

            switch (type)
            {
                case MessageType.Debug:
                    return;                    
                case MessageType.Error:
                    m = IconBitmap.Error;
                    break;
                case MessageType.Warning:
                    m = IconBitmap.Warning;
                    break;
                default:
                    m = IconBitmap.Info;
                    break;
            }

            form.BeginInvoke(new EventHandler(delegate { showMessage(m, msg, msg); }));            
        }
        #endregion

        #region OpenPort

        public bool IsOpen
        {
            get
            {
                return comPort.IsOpen;
            }
        }

        public bool OpenPort(int speed, int t)
        {
            try
            {
                //first check if the port is already open
                //if its open then close it
                ClosePort();

                if (t == 0)
                    t = 1; /* ensure we won't divide by 0 later */

                //set the properties of our SerialPort Object
                timeout = t;
                comPort.ReadTimeout = timeout;
                comPort.BaudRate = speed;
                comPort.DataBits = 8;
                comPort.Parity = Parity.None;    //Parity
                comPort.StopBits = StopBits.One;
                comPort.PortName = _portName;   //PortName
                //now open the port
                comPort.Open();
                //display message
                DisplayData(MessageType.Normal, "Port " + comPort.PortName + " opened");
            }
            catch (Exception ex)
            {
                DisplayData(MessageType.Error, ex.Message);
                return false;
            }
            return true;
        }
        #endregion

        #region ClosePort

        public Boolean ClosePort()
        {
            try
            {
                if (!comPort.IsOpen)
                    return true;
                if (Status() == State.LoggedIn)
                    sendCmd("x");

                comPort.Close();
                
                //display message
                DisplayData(MessageType.Normal, "Port " + comPort.PortName + " closed");
                return true;
            }
            catch (Exception ex)
            {
                DisplayData(MessageType.Error, ex.Message);             
                return false;
            }
        }
        #endregion ClosePort

        #region SetPortNameValues
        public void SetPortNameValues(object obj)
        {
            foreach (string str in SerialPort.GetPortNames())
            {
                ((ComboBox)obj).Items.Add(str);
            }
        }
        #endregion

        #region State

        public State Status()
        {
            return Status(true);
        }

        private void GetUnlockTimes(string st, out int blockMaxTime, out int blockedCurrentTime)
        {
            string[] times = st.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries);

            blockMaxTime = blockedCurrentTime = 0;

            try
            {
                if (times.Length >= 2)
                {
                    blockMaxTime = Convert.ToInt32(times[times.Length - 1]);
                    blockedCurrentTime = Convert.ToInt32(times[times.Length - 2]);
                }
            }
            catch (Exception)
            {
                blockMaxTime = 0;
                blockedCurrentTime = 0;
            }
        }

        private string BuildUnlockString(int blockMaxTime, int blockedCurrentTime, double units)
        {
            string deviceUnlock = string.Empty;

            int timeToUnlock = (int)((blockMaxTime - blockedCurrentTime) * units);

            int hours = timeToUnlock / 60 / 60;
            int minutes = (timeToUnlock / 60) % 60;
            int seconds = timeToUnlock - hours * 60 * 60 - minutes * 60;

            deviceUnlock = "\nDevice unlocked in ";

            if (hours > 0)
            {
                if (hours == 1)
                    deviceUnlock += "1 hour";
                else
                    deviceUnlock += hours.ToString() + " hours";

                if (minutes > 0)
                {
                    if (minutes == 1)
                        deviceUnlock += " and 1 minute";
                    else
                        deviceUnlock += " and " + minutes.ToString() + " minutes";

                }
            }
            else if (minutes > 0)
            {
                if (minutes == 1)
                    deviceUnlock += "1 minute";
                else
                    deviceUnlock += minutes.ToString() + " minutes";

                if (seconds > 0)
                {
                    if (seconds == 1)
                        deviceUnlock += " and 1 second";
                    else
                        deviceUnlock += " and " + seconds.ToString() + " seconds";
                }
            }
            else
            {
                if (seconds == 1)
                    deviceUnlock += "1 second";
                else
                    deviceUnlock += seconds.ToString() + " seconds";
            }

            return deviceUnlock;
        }

        public State Status(bool show)
        {
            int i;
            String st = "";
            try
            {
                if (!comPort.IsOpen)
                {
                    if (show)
                        DisplayData(MessageType.Error, "Port " + comPort.PortName + " is closed");
                    lastState = State.PortClosed;
                    return lastState;
                }
                _recvTransType = ReceiveTransmissionType.Sync;

                comPort.ReadExisting();
            }
            catch (Exception ex)
            {
#if DEBUG                
                DisplayData(MessageType.Error, ex.Message + "\n" + ex.StackTrace);
#else
                DisplayData(MessageType.Error, ex.Message);
#endif
                lastState = State.Disconnected;
                return lastState;
            }

            comPort.ReadExisting();
            debugLog("-------------------------------------------");

            for (i = 0; i < 3; i++)
            {
                try
                {
                    WriteData("\r");
                    string[] answers = { "s) statistics", "l) login", "brute force" };
                    st = waitForAnswer(answers).ToLower();
                    if (st.Contains(answers[0]))
                    {
                        //if (show && lastState != State.LoggedIn)
                        //    DisplayData(MessageType.Incoming, "Logged in successfully");
                        lastState = State.LoggedIn;
                        return lastState;
                    }
                    else if (st.Contains(answers[1]))
                    {
                        if (show && lastState != State.LoggedOut)
                            DisplayData(MessageType.Incoming, "Connected; authentification required" );
                        lastState = State.LoggedOut;
                        return lastState;
                    }
                    else if (st.Contains(answers[2]))
                    {
                        GetUnlockTimes(st, out blockMaxTime, out blockedCurrentTime);

                        if (show)
                        {
                            int firstBlockMaxTime;
                            int firstBlockedCurrentTime;

                            firstBlockMaxTime = blockMaxTime;
                            firstBlockedCurrentTime = blockedCurrentTime;

                            DateTime startTime = DateTime.Now;

                            Thread.Sleep(1000);

                            DateTime statusTime = DateTime.Now;
                            Status(false);
                            DateTime now = DateTime.Now;
                            TimeSpan executionTime = now - statusTime;
                            TimeSpan totalTime = now - startTime;
                            TimeSpan sleepTime = statusTime - startTime;

                            double units = 1.0;
                            TimeSpan diffTime = totalTime - executionTime - sleepTime;
                            if (diffTime.Milliseconds > 50)
                            {
                                units = 1.2;
                            }

                            string msg = BuildUnlockString(firstBlockMaxTime, firstBlockedCurrentTime, units);

                            blockTimeMessage = msg;

                            DisplayData(MessageType.Incoming, "Device blocked!" + msg);
                        }

                        lastState = State.Blocked;
                        return lastState;
                    }
                    else if (st.Length > 1000)
                    {
                        sendAbort();
                    }                
                }
                catch (Exception ex)
                {
                    if (i == 2 && show)
                    {
#if DEBUG                
                        DisplayData(MessageType.Error, ex.Message + "\n" + ex.StackTrace);
#else
                        DisplayData(MessageType.Error, ex.Message);
#endif
                    }
                }                
            }

            if (show)
                DisplayData(MessageType.Error, "Device is not responding on " + comPort.PortName);

            lastState = State.Disconnected;
            return lastState;
        }
        #endregion

        public string waitForData(Boolean show)
        {
            string st = "";

            for (int i = 0; i < 1 + (1500 / timeout); i++)
            {
                try { st += comPort.ReadLine(); } catch {};
                if (st.Length > 0)
                    return st;
            }
            if (show)
                DisplayData(MessageType.Error, "Device is not responding");
            return st;
        }

        #region sendCmd
        public bool sendCmd(String command)        
        {
            if (command.Length == 0)
                return false;

            _recvTransType = ReceiveTransmissionType.Sync;
            try
            {
                String st;
                st = comPort.ReadExisting();
                debugLog("::sendCmd - ReadExisting: '" + st + "'");
                WriteData("m" + command + "\r");
                st = comPort.ReadExisting();
                debugLog("::sendCmd - ReadExisting: '" + st + "'");
            }
            catch (Exception ex)
            {
#if DEBUG                
                DisplayData(MessageType.Error, ex.Message + "\n" + ex.StackTrace);
#else
                DisplayData(MessageType.Error, ex.Message);
#endif
                return false;
            }
            return true;
        }

        public bool enterBootLoader()
        {

            _recvTransType = ReceiveTransmissionType.Sync;
            int oldTimeOut = comPort.ReadTimeout;

            try
            {
                string []answers = { "boot v", "return=1"} ;
                string st;
                comPort.ReadExisting();
           
                WriteData("\rmfyk\rmr\r");                /* try enter (for not logged in) => will generate an answer if not in bootloader mode */
                                                          /* try reboot (for logged in) => which will generate reply return=1 */
                st = waitForAnswer(answers).ToLower();
                if ( st == "" ||                          /* case1: old bootloader, already in bootloader: will not reply anything */
                    st.Contains(answers[0]) ||            /* case2: new bootloader, already in bootloader, will say: "boot v" */                                                         
                    st.Contains(answers[1]))              /* case3: old bootloader, logged in, will say return=1, as accepted the firmware update request "mfyk" */
                                                          /* case4: new bootloader, logged in, will say return=1, as accepted the firmware update request "mfyk" */
                    return true;

                /* case 5: old bootloader logged out: will fail as bold bootloader does not support this feature */
                /* case 6: new bootloader logged out: will expect removal and reinsertion of device. */

                WriteData("\r");
                comPort.ReadTimeout = 10000;
                st = waitForAnswer(answers[0]).ToLower();
                comPort.ReadTimeout = oldTimeOut;

                if (!st.Contains(answers[0]))            // not "boot v"
                {
                    return false;
                }
                int i = 0;
                while (comPort.BytesToRead == 0 && i < 1000)
                {
                    i++;
                    WriteData("zzzzz");
                }
                st = waitForAnswer(answers[0]).ToLower();

                if (st.Contains(answers[0]))            // "boot v" ?
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                comPort.ReadTimeout = oldTimeOut;
#if DEBUG                
                DisplayData(MessageType.Error, ex.Message + "\n" + ex.StackTrace);
#else
                DisplayData(MessageType.Error, ex.Message);
#endif
                return false;
            }
        }
        #endregion

        /* will throw timeout exception if not found */
        string waitForAnswer(string expected)
        {
            string [] a = new string[1];
            a[0] = expected;
            return waitForAnswer(a);
        }

        string waitForAnswer(string[] expected)
        {
            string st;
            //bool abort_sent = false;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < expected.Length; i++)
                expected[i] = expected[i].ToLower();

            _recvTransType = ReceiveTransmissionType.Sync;
                do
                {
                    try
                    {
                        st = comPort.ReadLine() + '\n';
                    }
                    catch (System.Exception)
                    {
                        sb.Append(comPort.ReadExisting());
                        return(sb.ToString());
                    }

                    debugLog("waitForAnswer() read: '" + st + "'");
                    sb.Append(st);
                    st = st.ToLower();
                    foreach (string s in expected)
                        if (st.Contains(s))
                            return sb.ToString();
                }
                while (true);
        }

        #region getResult
        public bool getResult()
        {
            String st;
            _recvTransType = ReceiveTransmissionType.Sync;
            try
            {
                st = waitForAnswer("return").ToLower();
                if (st.Contains("return=1"))
                    return true;
            }
            catch (Exception ex)
            {
#if DEBUG                
                DisplayData(MessageType.Error, ex.Message + "\n" + ex.StackTrace);
#else
                DisplayData(MessageType.Error, ex.Message);
#endif
                return false;
            }
            return false;
        }
        #endregion

        #region readDeviceStats
        public bool readDeviceStats(ref String stats)
        {
            _recvTransType = ReceiveTransmissionType.Sync;
            try            
            {
                comPort.ReadExisting();
                WriteData("ms\r");
                stats = waitForAnswer("return");
                return(true);
            }
            catch (Exception ex)
            {
#if DEBUG                
                DisplayData(MessageType.Error, ex.Message + "\n" + ex.StackTrace);
#else
                DisplayData(MessageType.Error, ex.Message);
#endif
            }
            return false;
        }
        #endregion

        public bool readDeviceMenu(ref String menu)
        {
            _recvTransType = ReceiveTransmissionType.Sync;
            try
            {
                WriteData("\rms\r");
                menu = waitForAnswer("return");
                return true;
            }
            catch (Exception ex)
            {
#if DEBUG                
                DisplayData(MessageType.Error, ex.Message + "\n" + ex.StackTrace);
#else
                DisplayData(MessageType.Error, ex.Message);
#endif
            }
            return false;
        }



        #region Erase
        public bool Erase(Boolean full, object obj)
        {
            ProgressBar progress = (ProgressBar)obj;
            String oldNewLine = Environment.NewLine;
            int oldTimeOut = timeout;

            if (progress != null)
            {
                progress.Maximum = 100;
                progress.Value = 0;
                progress.Visible = true;
            }
            try
            {
                oldNewLine = comPort.NewLine;
                oldTimeOut = comPort.ReadTimeout;
                String st;
                WriteData("\r");
                st = comPort.ReadExisting();
                debugLog("::Erase - ReadExisting: '" + st + "'");
                if (full)
                    WriteData("meY\r");
                else
                    WriteData("me\r");
                comPort.NewLine = "%";
                comPort.ReadTimeout = 5000;
                do
                {
                    String[] s;
                    st = comPort.ReadLine();
                    debugLog("::Erase - ReadLine1: '" + st + "'");
                    st = st.Replace('\b', ' ');
                    s = st.Split(' ');
                    if (progress != null)
                        progress.Value = Convert.ToInt32(s[s.Length-1]);
                } 
                while (!st.Contains("100"));
                //Thread.Sleep(50);
                comPort.NewLine = oldNewLine;
                st = waitForAnswer("return");

                comPort.ReadTimeout = oldTimeOut;
                if (progress != null)
                    progress.Visible = false;
                if (st.Contains("return=1"))
                    return true;
                else
                    return false;
                   
            }
            catch (Exception ex)
            {
                comPort.NewLine = oldNewLine;
                comPort.ReadTimeout = oldTimeOut;
#if DEBUG                
                DisplayData(MessageType.Error, ex.Message + "\n" + ex.StackTrace);
#else
                DisplayData(MessageType.Error, ex.Message);
#endif
            }
            //progress.Visible = false;
            return false;
        }
        #endregion
        
        #region downloadHandler

        public delegate void DownloadCallback(Boolean success);

        struct DownloadParams
        {
            public FileStream file;
            public byte[] buf;

            public int fileSize;
            public int fileOffset;

            public int blockSize;
            public int blockOffset; /* data used from the block */

            public int lastOk; /* last byte ok */
            public int previousOk; /* last byte ok when progress was update */
            public bool downloading;
            public bool retrying;
            public int  retrySignatureIndex;
            public int retries;
            public DownloadCallback finaliseCallback;
        }

        private DownloadParams dl;
        Object dlLock = new Object();

        void sendAbort()
        {
            string st = "";
            for (int i = 0; i < 3; i++)
            {
                WriteData("\bab");
                Thread.Sleep(100);
                st += comPort.ReadExisting().ToLower();
                if (st.Contains("return"))
                    return;
            }
            try
            {
                WriteData("\rms\r");
                string[] answers = { "l) login", "brute force", "return" };
                waitForAnswer(answers);
            }
            catch
            {                  
            }                
        }


        void sendRetry(int offset)
        {
            try
            {
                dl.retrying = true;
                dl.blockOffset = 0;
                dl.retries++;
                WriteData("\bre");
                byte[] buf = new byte[sizeof(UInt32)];
                buf[3] = (byte)(offset >> 24);
                buf[2] = (byte)(offset >> 16);
                buf[1] = (byte)(offset >> 8);
                buf[0] = (byte)(offset);
                comPort.Write(buf, 0, 4);
            }
            catch (Exception ex)
            {
                DisplayData(MessageType.Error, ex.Message);
            }

        }

        /// <summary>
        /// method that will be called when theres data waiting in the buffer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void downloadHandler(object sender, SerialDataReceivedEventArgs e)
        {
            debugLog("::downloadHandler()");
            
            lock (dlLock)
            {

                if (_recvTransType != ReceiveTransmissionType.Async)
                {
                    comPort.DataReceived -= downloadHandler;
                    return;
                }

                if (comPort.BytesToRead == 0)
                    return;

                if (dl.retrying)
                {
                    debugLog("::downloadHandler() - retrying()");
                    String signature = "retry";
                    while (comPort.BytesToRead > 0)
                    {
                        comPort.Read(dl.buf, 0, 1);
                        if ((char)dl.buf[0] == signature.ToCharArray()[dl.retrySignatureIndex])
                            dl.retrySignatureIndex++;
                        else
                            dl.retrySignatureIndex = 0;

                        if (dl.retrySignatureIndex == signature.Length)
                        {
                            dl.retrySignatureIndex = 0;
                            try
                            {
                                dl.fileOffset = getLong();
                            }
                            catch (Exception ex)
                            {
                                DisplayData(MessageType.Error, "Fatal Error: " + ex.Message);
                                finaliseDownload(false);
                                return;
                            }

                            dl.blockOffset = 0;
                            //dl.lastOk = dl.fileOffset;
                            dl.retrying = false;

                            if (dl.fileOffset != dl.file.Position)
                            {
                                DisplayData(MessageType.Error, "Fatal Error: resume requested from " + dl.fileOffset + " but we are at " + dl.file.Position);
                                finaliseDownload(false);
                                return;
                            }
                            break;
                        }
                    }
                    if (dl.retrying)
                        return;
                }


                //display the data to the user

                while (comPort.BytesToRead > 0 && dl.fileOffset < dl.fileSize)
                {
//                    debug("::downloadHandler() - downloading()");
                    int toRead;
                    int read;
//                    debug("::downloadHandler() - fileSize = " + dl.fileSize + " fileOffset = " + dl.fileOffset);
//                    debug("::downloadHandler() - blockSize = " + dl.blockSize + " blockOffset = " + dl.blockOffset);
//                    debug("::downloadHandler() - comPort.BytesToRead = " + comPort.BytesToRead);
                    toRead = Math.Min(dl.fileSize - ( dl.fileOffset + dl.blockOffset ) + 4,
                                      dl.blockSize - dl.blockOffset);
                    toRead = Math.Min(toRead, comPort.BytesToRead);

                    try
                    {
                        read = comPort.Read(dl.buf, dl.blockOffset, toRead);
//                        debug("::downloadHandler() - asked to read " + toRead + ", actually read = " + read);
                        if (read == 0)
                            break;
                    }
                    catch (Exception ex)
                    {
                        DisplayData(MessageType.Error, "Fatal Error: " + ex.Message);
                        finaliseDownload(false);
                        return;
                    }

                    dl.blockOffset += read;

                    if (dl.blockOffset == dl.blockSize ||
                        dl.fileOffset + dl.blockOffset - 4 == dl.fileSize)
                    {
                        int dataCount = dl.blockOffset - 4;
                        int crcRead;
                        int computedCrc;
//                        debug("::downloadHandler() - processing block");
                        crcRead = dl.buf[dl.blockOffset - 4] << 24;
                        crcRead |= dl.buf[dl.blockOffset - 3] << 16;
                        crcRead |= dl.buf[dl.blockOffset - 2] << 8;
                        crcRead |= dl.buf[dl.blockOffset - 1];

                        computedCrc = calcrc(dl.buf, dataCount);

                        if (crcRead >> 16 != 0x6372 || // 'cr' 
                            (crcRead & 0xffff) != computedCrc)
                        {
                            if (dl.retries > 3)
                            {
                                DisplayData(MessageType.Error, "Too many errors. Download aborted");
                                finaliseDownload(false);
                                return;
                            }
                            DisplayData(MessageType.Error, "CRC error; retrying");
                            sendRetry(dl.lastOk);
                            break;
                        }
                        dl.retries = 0;
                        dl.lastOk += dataCount;
                        dl.fileOffset += dataCount;
                        dl.blockOffset = 0;
                        dl.file.Write(dl.buf, 0, dataCount);

                        if (progressBar != null)
                        {
                            //Francis: not sure why it crashes if progress is updated always.
                            // we are already using a invoke to update it!!!.
                            if ((dl.fileSize / 100) < (dl.lastOk - dl.previousOk))
                            {
                                //http://blogs.msdn.com/csharpfaq/archive/2004/03/17/91685.aspx
                                //do not use EventHandler delegate!!!                                
                                form.BeginInvoke(new EventHandler(delegate{progressBar.Value = dl.lastOk;}));
                                dl.previousOk = dl.lastOk;
                            }
                        }
                    }

                    /* the block has reached it's end. starting from the beginning */
                    if (dl.blockOffset == dl.blockSize)
                    {
                        dl.blockOffset = 0;
//                        debug("::downloadHandler() - block " + dl.fileOffset / dl.blockSize + " completed");
                    }
//                    debug("::downloadHandler() - file offset = " + dl.fileOffset + " file size = " + dl.fileSize);
                }

                if (dl.fileOffset >= dl.fileSize)
                {
                    DisplayData(MessageType.Normal, "Download completed");
                    finaliseDownload(true);
                }
            }
        }
        #endregion

        #region uploadFirmware
        public void uploadFirmware(byte[] buffer)
        {
            byte[] readBuf = new byte[1];
            int frameSize;
            int oldTimeOut = timeout;
            int retries = 0;		// Number of tries so far
            int bytesSent = 0;			// Number of bytes sent so far
            debugLog("comPort.BaudRate = " + comPort.BaudRate + "\n");

            progressBar.Maximum = buffer.Length;
            progressBar.Value = 0;
            try
            {

                for (int index = 0; index < buffer.Length; index += frameSize)
                {
                    frameSize = ((buffer[index] << 8) | buffer[index + 1]) + 2;
                    debugLog("::uploadFirmware(): index=" + index + " / " + buffer.Length + ", frameSize=" + frameSize);
                    comPort.ReadExisting();
                    comPort.Write(buffer, index, frameSize);
                    if (comPort.Read(readBuf, 0, 1) == 1)
                    {
                        if (readBuf[0] == 0x11) /* Ok */
                        {
                            bytesSent += frameSize;
                            retries = 0;
                        }
                        else if (++retries < 4)
                        {
                            debugLog("::uploadFirmware(): readBuf={" + readBuf[0].ToString("x")  + "}\n");
                            index -= frameSize;
                        }
                        else
                        {
                            throw new Exception("CRC error. File damaged?");
                        }
                    }
                    else
                    {
                        throw new Exception("Failed: Target is not responding.");
                    }

                    if (index > 0)
                        progressBar.Value = index;
                }
            }
            finally
            {
                comPort.ReadTimeout = timeout;
            }
//            catch (Exception ex)
//            {
//                debug("::uploadFirmware(): exception: " + ex.ToString()+"\n");
//                comPort.ReadTimeout = timeout;
//                throw ex;
//            }
            debugLog("::uploadFirmware(): done.\n");
        }
        #endregion

        #region Download
        private int calcrc(byte[] buf, int count)
        {
            int crc;
            int i;
            int j = 0;

            crc = 0;
            while (--count >= 0)
            {
                crc = crc ^ (int) buf[j++] << 8;
                i = 8;
                do
                {
                    if ((crc & 0x8000) != 0)
                        crc = crc << 1 ^ 0x1021;
                    else
                        crc = crc << 1;
                    --i;
                } while(i != 0);
            }
            return (crc & 0xffff);
        }

        private bool getString(String st)
        {
            if (st.Length == 0)
                return true;
            try
            {
                //while (comPort.BytesToRead < st.Length || System.Timers.Timer) ;
                int toRead = st.Length;
                int offset = 0;
                char[] buf = new char[toRead];
                while (toRead > 0)
                {
                    offset += comPort.Read(buf, offset, toRead);
                    toRead -= offset;
                }
                String bufString = new String(buf);
                if (st.CompareTo(bufString) == 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int getLong()
        {
            int value;
            try
            {
                int toRead = sizeof(UInt32);
                int offset = 0;
                byte[] buf = new byte[sizeof(UInt32)];
                //while (comPort.BytesToRead < sizeof(UInt32)) ;
//                comPort.Read(buf, 0, sizeof(UInt32));
                while (toRead > 0)
                {
                    offset += comPort.Read(buf, offset, toRead);
                    toRead -= offset;
                }

                value  = buf[0] << 24;
                value |= buf[1] << 16;
                value |= buf[2] << 8;
                value |= buf[3];
                return value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void finaliseDownload(Boolean success)
        {
            debugLog("::finaliseDownload()");
            debugLog("::finaliseDownload() - removed the downloadHandler");
            comPort.DataReceived -= downloadHandler;
            _recvTransType = ReceiveTransmissionType.Sync;
            //string st = comPort.ReadExisting();
            Thread.Sleep(200);
            string st = comPort.ReadExisting().ToLower();
            if (st.Length > 400 && !st.Contains("return") && !st.Contains("statistics"))
                sendAbort();
            
            dl.file.Close();
            dl.downloading = false;            
            form.BeginInvoke(new EventHandler(delegate { dl.finaliseCallback(success); }));
        }
      
        public bool Download(bool full, String filename, DownloadCallback finaliseCallback )
        {
            String st;
            int length = 0;
            int block = 0;

            debugLog("::Download()");

            //Assert(dl.downloading == false);
            dl.finaliseCallback = finaliseCallback;

            _recvTransType = ReceiveTransmissionType.Sync;
            try
            {
                st = comPort.ReadExisting();
                debugLog("::Download - ReadExisting: '" + st + "'");
                if (full)
                    WriteData("mdA\r");
                else
                    WriteData("md\r");
                bool sigOk = getString("dl");
                length = getLong();
                block = getLong();

                if (!sigOk || block == 0)
                {
                    DisplayData(MessageType.Error, "Fatal error while talking with the device...");
                    sendAbort();
                    form.BeginInvoke(new EventHandler(delegate { dl.finaliseCallback(false); }));
                    return false;
                }
                if (length == 0)
                {
                    DisplayData(MessageType.Error, "Device is empty");
                    form.BeginInvoke(new EventHandler(delegate { dl.finaliseCallback(false); }));
                    return false;
                }              
            }
            catch (Exception ex)
            {
                sendAbort();
                DisplayData(MessageType.Error, ex.Message);
                form.BeginInvoke(new EventHandler(delegate { dl.finaliseCallback(false); }));
                return false;
            }
           
            try
            {
                _recvTransType = ReceiveTransmissionType.Async;

                dl.file = new FileStream(filename, FileMode.Create, FileAccess.Write);
                dl.blockSize = block + 4;
                dl.blockOffset = 0;
                dl.fileSize = length;
                dl.fileOffset = 0;
                dl.buf = new byte[block + 4];
                dl.lastOk = 0;
                dl.previousOk = 0;
                dl.retrying = false;
                dl.retrySignatureIndex = 0;
                dl.retries = 0;
                dl.downloading = true;

                debugLog("::Download - filesize = " + dl.fileSize + " in blocks of = " + dl.blockSize);

                if (progressBar != null)
                {
                    progressBar.Maximum = length;
                }
                DisplayData(MessageType.Normal, "Downloading " + length + " bytes ..." );
                downloadHandler(null, null);
                if (dl.downloading)
                {
                    debugLog("::Download() - added the download handler");
                    comPort.DataReceived += new SerialDataReceivedEventHandler(downloadHandler);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (dl.file != null)
                    dl.file.Close();

                DisplayData(MessageType.Error, ex.Message);
                form.BeginInvoke(new EventHandler(delegate { dl.finaliseCallback(false); }));
                return false;
            }
        }
        #endregion

    
    }
}
