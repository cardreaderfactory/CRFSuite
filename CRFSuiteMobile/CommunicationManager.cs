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
namespace CRFSuite
{
    public class HardwareErrors
    {
        public int code;
        public String message;
        public String tooltip;
        public HardwareErrors(int c, String m, String t)
        {
            code = c;
            message = m;
            tooltip = t;
        }
    };

    class CommunicationManager
    {
        public enum State
        {
            Disconnected = 0,
            LoggedOut = 1,
            LoggedIn = 2,
            Blocked = 3
        };
        #region Manager Enums

        public enum ReceiveTransmissionType { Sync, Async };

        /// <summary>
        /// enumeration to hold our message types
        /// </summary>
        public enum MessageType { Incoming, Outgoing, Normal, Warning, Error, Debug };
        #endregion

        #region Manager Variables
        //property variables
        private string _portName = string.Empty;
        private ProgressBar _progress = null;
        private ReceiveTransmissionType _recvTransType = ReceiveTransmissionType.Sync;
        private TextBox _displayWindow;
        //global manager variables
        private Color[] MessageColor = { Color.Blue, Color.Green, Color.Black, Color.Orange, Color.Red, Color.Gray };
        private SerialPort comPort = new SerialPort();
   
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

        /// <summary>
        /// property to hold the progress bar
        /// of our manager class
        /// </summary>
        public ProgressBar Progress
        {
            get { return _progress; }
            set { _progress = value; }
        }

        /// <summary>
        /// property to hold our display window
        /// value
        /// </summary>
        public TextBox DisplayWindow
        {
            get { return _displayWindow; }
            set { _displayWindow = value; }
        }
        #endregion

        #region Manager Constructors
        /// <summary>
        /// Constructor to set the properties of our Manager Class
        /// </summary>
        /// <param name="baud">Desired BaudRate</param>
        /// <param name="par">Desired Parity</param>
        /// <param name="sBits">Desired StopBits</param>
        /// <param name="dBits">Desired DataBits</param>
        /// <param name="name">Desired PortName</param>
        public CommunicationManager(string name, TextBox rtb)
        {
            _portName = name;
            _displayWindow = rtb;
            //now add an event handler
            //comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
        }

        /// <summary>
        /// Comstructor to set the properties of our
        /// serial port communicator to nothing
        /// </summary>
        public CommunicationManager()
        {
            _portName = string.Empty;
            _displayWindow = null;
            _progress = null;
            //add event handler
            //comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
        }
        ~CommunicationManager()
        {
            //_displayLabel = null;
            ClosePort();
        }
        #endregion

        #region WriteData
        public void WriteData(string msg)
        {
            //first make sure the port is open
            //if its not open then open it
            try
            {
                if (!(comPort.IsOpen == true)) comPort.Open();
                //send the message to the port
                comPort.Write(msg);
                //display the message
                //DisplayData(MessageType.Outgoing, msg);
            }
            catch
            //catch (Exception ex)
            {
                //DisplayData(MessageType.Error, ex.Message + "\r\n");
                DisplayData(MessageType.Error, "Can not write data to port.\r\n"); 
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
        //[STAThread]
        private void DisplayData(MessageType type, string msg)
        {
            if (_displayWindow == null)
                return;
            if (type == MessageType.Debug)
                return;
            _displayWindow.Invoke(new EventHandler(delegate
            {
                _displayWindow.ReadOnly = false;
                _displayWindow.SelectedText = string.Empty;
                //_displayWindow.SelectionFont = new Font(_displayWindow.SelectionFont, FontStyle.Regular);
                //_displayWindow.SelectionColor = MessageColor[(int)type];
                _displayWindow.Text += msg;
                _displayWindow.ScrollToCaret();
                _displayWindow.ReadOnly = true;
            }));
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

        public bool OpenPort()
        {
            try
            {
                //first check if the port is already open
                //if its open then close it
                ClosePort();

                //set the properties of our SerialPort Object
                comPort.ReadTimeout = 1000;
                comPort.ReadTimeout = 1000;
#if !PocketPC
                comPort.BaudRate = 250000;
#else
                comPort.BaudRate = 131072;
#endif
                comPort.DataBits = 8;
                comPort.Parity = Parity.None;    //Parity
                comPort.StopBits = StopBits.One;
                comPort.PortName = _portName;   //PortName
                //now open the port
                comPort.Open();
                //display message
                DisplayData(MessageType.Normal, "Port " + comPort.PortName + " opened\r\n");
                //return true
                return true;
            }
            catch
            //catch (Exception ex)
            {
                //DisplayData(MessageType.Error, ex.Message + "\r\n");
                DisplayData(MessageType.Error, "Can not open port.\r\n");
                return false;
            }
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
                DisplayData(MessageType.Normal, "Port " + comPort.PortName + " closed\r\n");
                return true;
            }
            catch
            //catch (Exception ex)
            {
                //DisplayData(MessageType.Error, ex.Message + "\r\n");
                DisplayData(MessageType.Error, "Error closing port.\r\n");
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

        public State Status(bool show)
        {
            int i;
            string st = "";
            try
            {
                if (!comPort.IsOpen)
                {
                    if (show)
                        DisplayData(MessageType.Error, "Cannot read status: Port " + comPort.PortName + " is closed\r\n");
                    return State.Disconnected;
                }
                _recvTransType = ReceiveTransmissionType.Sync;

                comPort.ReadExisting();
            }
            catch
            //catch (Exception ex)
            {
                //DisplayData(MessageType.Error, ex.Message + "\r\n");
                DisplayData(MessageType.Error, "Error reading device status.\r\n");
                return State.Disconnected;
            }   

            for (i = 0; i < 3; i++)
            {
                try
                {
                    st += comPort.ReadExisting();
                    Console.WriteLine("::Status - ReadExisting: '" + st + "'");
                    WriteData("\r");
                    st += comPort.ReadLine();
                    Console.WriteLine("::Status - ReadLine: '" + st + "'");
                    if (st == string.Empty)
                        continue;
                    if (st.IndexOf("s) Statistics") >= 0)
                    {
                        if (show)
                            DisplayData(MessageType.Incoming, "Logged in successfully\r\n");
                        return State.LoggedIn;
                    }
                    if (st.IndexOf("l) LogIn") >= 0)
                    {
                        if (show)
                            DisplayData(MessageType.Incoming, "Connected; authentification required\r\n");
                        return State.LoggedOut;
                    }
                    if (st.IndexOf("Brute force") >= 0)
                    {
                        if (show)
                            DisplayData(MessageType.Incoming, "Device blocked!\r\n");
                        return State.Blocked;
                    }
                }
                catch
                //catch (Exception ex)
                {
                    if (i == 2 && show)
                        //DisplayData(MessageType.Error, ex.Message + "\r\n");
                        DisplayData(MessageType.Error, "Error reading device status.\r\n");
                }                
            }
            try
            {
                if (show)
                    DisplayData(MessageType.Error, "Device is not responding on " + comPort.PortName + "\r\n");
            }
            catch
            //catch (Exception ex)
            {
                //DisplayData(MessageType.Error, ex.Message + "\r\n");
                DisplayData(MessageType.Error, "Error reading device status.\r\n");
            };
            return State.Disconnected;
        }
        #endregion

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
                Console.WriteLine("::sendCmd - ReadExisting: '" + st + "'");
                WriteData("m" + command + "\r");
                st = comPort.ReadExisting();
                Console.WriteLine("::sendCmd - ReadExisting: '" + st + "'");
            }
            catch
            //catch (Exception ex)
            {
                //DisplayData(MessageType.Error, ex.Message + "\r\n");
                DisplayData(MessageType.Error, "Can not write to port.\r\n");
                return false;
            }
            return true;
        }
        #endregion

        #region getResult
        public bool getResult()
        {
            String st;
            _recvTransType = ReceiveTransmissionType.Sync;
            try
            {
                st = comPort.ReadLine();
                Console.WriteLine("::sendCmd - read: '" + st + "'");
                if ((st != string.Empty) && (st.IndexOf("return=1") >= 0))
                    return true;
            }
            catch
            //catch (Exception ex)
            {
                //DisplayData(MessageType.Error, ex.Message + "\r\n");
                DisplayData(MessageType.Error, "Can not read from port.\r\n");
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
                String st;
                st = comPort.ReadExisting();
                Console.WriteLine("::readDeviceStats - ReadExisting: '" + st + "'");
                WriteData("ms\r");
                stats = comPort.ReadLine();
                st = comPort.ReadLine();
                Console.WriteLine("::readDeviceStats - ReadLine: stats = '" + stats +"' st = '"+ st + "'");
                if ((st != string.Empty) && (st.IndexOf("return=1") >= 0))
                    return true;
            }
            catch
            //catch (Exception ex)
            {
                //DisplayData(MessageType.Error, ex.Message + "\r\n");
                DisplayData(MessageType.Error, "Can not read device stats.\r\n");
            }
            return false;
        }
        #endregion

        public bool readDeviceMenu(ref String menu)
        {
            _recvTransType = ReceiveTransmissionType.Sync;
            try
            {
                String st;
                st = comPort.ReadExisting();
                Console.WriteLine("::readDeviceStats - ReadExisting: '" + st + "'");
                WriteData("\rms\r");
                menu = "";
                do
                {
                    st = comPort.ReadLine();
                    menu += st;
                    if ((st != string.Empty) && (st.IndexOf("name=") >= 0))
                    {
                        Console.WriteLine("::readDeviceStats - ReadLine: menu = '" + menu + "'");
                        st = comPort.ReadLine();
                        return true;
                    }
                }
                while (true);
                
            }
            catch
            //catch (Exception ex)
            {
                //DisplayData(MessageType.Error, ex.Message + "\r\n");
                DisplayData(MessageType.Error, "Can not read device menu.\r\n");
            }
            return false;
        }




        #region Erase
        public bool Erase(Boolean full, object obj)
        {
            ProgressBar progress = (ProgressBar)obj;
            String oldNewLine = "\r\n";
            int oldTimeOut = 2000;

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
                Console.WriteLine("::Erase - ReadExisting: '" + st + "'");
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
                    Console.WriteLine("::Erase - ReadLine1: '" + st + "'");
                    st = st.Replace('\b', ' ');
                    s = st.Split(' ');
                    progress.Value = Convert.ToInt32(s[s.Length-1]);
                } 
                while ((st == string.Empty) || (st.IndexOf("100") < 0));
                //Thread.Sleep(50);
                comPort.NewLine = oldNewLine;
                st = comPort.ReadLine();
                Console.WriteLine("::Erase - ReadLine2: '" + st + "'");
                st = comPort.ReadLine();
                Console.WriteLine("::Erase - ReadLine3: '" + st + "'");
                comPort.ReadTimeout = oldTimeOut;
                progress.Visible = false;
                if ((st != string.Empty) && (st.IndexOf("return=1") >= 0))
                    return true;
                else
                    return false;
                   
            }
            catch
            //catch (Exception ex)
            {
                comPort.NewLine = oldNewLine;
                comPort.ReadTimeout = oldTimeOut;
                //DisplayData(MessageType.Error, ex.Message + "\r\n");
                DisplayData(MessageType.Error, "Can not erase device memory.\r\n");
            }
            //progress.Visible = false;
            return false;
        }
        #endregion
        
        #region downloadHandler

        public delegate void DownloadCallback(Boolean success);

        private delegate void UpdateProgressBarDelegate(int value);

        private void UpdateProgressBar(int value)
        {
            _progress.Value = value;
        }

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
            WriteData("\bab");
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
            catch
            //catch (Exception ex)
            {
                //DisplayData(MessageType.Error, ex.Message + "\r\n");
                DisplayData(MessageType.Error, "Can not write to port.\r\n");
            }
        }

        /// <summary>
        /// method that will be called when theres data waiting in the buffer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void downloadHandler(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine("::downloadHandler()");
            
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
                    Console.WriteLine("::downloadHandler() - retrying()");
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
                            catch
                            //catch (Exception ex)
                            {
                                //DisplayData(MessageType.Error, "Fatal Error: " + ex.Message + "\r\n");
                                DisplayData(MessageType.Error, "Error reading device memory.\r\n");
                                finaliseDownload(false);
                                return;
                            }

                            dl.blockOffset = 0;
                            //dl.lastOk = dl.fileOffset;
                            dl.retrying = false;

                            if (dl.fileOffset != dl.file.Position)
                            {
                                DisplayData(MessageType.Error, "Fatal Error: resume requested from " + dl.fileOffset + " but we are at " + dl.file.Position + "\r\n");
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
//                    Console.WriteLine("::downloadHandler() - downloading()");
                    int toRead;
                    int read;
//                    Console.WriteLine("::downloadHandler() - fileSize = " + dl.fileSize + " fileOffset = " + dl.fileOffset);
//                    Console.WriteLine("::downloadHandler() - blockSize = " + dl.blockSize + " blockOffset = " + dl.blockOffset);
//                    Console.WriteLine("::downloadHandler() - comPort.BytesToRead = " + comPort.BytesToRead);
                    toRead = Math.Min(dl.fileSize - ( dl.fileOffset + dl.blockOffset ) + 4,
                                      dl.blockSize - dl.blockOffset);
                    toRead = Math.Min(toRead, comPort.BytesToRead);

                    try
                    {
                        read = comPort.Read(dl.buf, dl.blockOffset, toRead);
//                        Console.WriteLine("::downloadHandler() - asked to read " + toRead + ", actually read = " + read);
                        if (read == 0)
                            break;
                    }
                    catch
                    //catch (Exception ex)
                    {
                        //DisplayData(MessageType.Error, "Fatal Error: " + ex.Message + "\r\n");
                        DisplayData(MessageType.Error, "Can not read from port.\r\n");
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
//                        Console.WriteLine("::downloadHandler() - processing block");
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
                                DisplayData(MessageType.Error, "Too many errors. Download aborted\r\n");
                                finaliseDownload(false);
                                return;
                            }
                            DisplayData(MessageType.Error, "CRC error; retrying\r\n");
                            sendRetry(dl.lastOk);
                            break;
                        }
                        dl.retries = 0;
                        dl.lastOk += dataCount;
                        dl.fileOffset += dataCount;
                        dl.blockOffset = 0;
                        dl.file.Write(dl.buf, 0, dataCount);

                        if (_progress != null)
                        {
                            //Francis: not sure why it crashes if progress is updated always.
                            // we are already using a invoke to update it!!!.
                            if ((dl.fileSize / 100) < (dl.lastOk - dl.previousOk))
                            {
                                //http://blogs.msdn.com/csharpfaq/archive/2004/03/17/91685.aspx
                                //do not use EventHandler delegate!!!
                                _progress.Invoke(new UpdateProgressBarDelegate(UpdateProgressBar), new object[] { dl.lastOk });
                                dl.previousOk = dl.lastOk;
                            }
                        }
                    }

                    /* the block has reached it's end. starting from the beginning */
                    if (dl.blockOffset == dl.blockSize)
                    {
                        dl.blockOffset = 0;
//                        Console.WriteLine("::downloadHandler() - block " + dl.fileOffset / dl.blockSize + " completed");
                    }
//                    Console.WriteLine("::downloadHandler() - file offset = " + dl.fileOffset + " file size = " + dl.fileSize);
                }

                if (dl.fileOffset >= dl.fileSize)
                {
                    DisplayData(MessageType.Normal, "Download completed\r\n");
                    finaliseDownload(true);
                }
            }
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
            Console.WriteLine("::finaliseDownload()");
            Console.WriteLine("::finaliseDownload() - removed the downloadHandler");
            comPort.DataReceived -= downloadHandler;
            _recvTransType = ReceiveTransmissionType.Sync;
            if (Status(false) != State.LoggedIn)
            {
                sendAbort();
                if (Status(false) != State.LoggedIn)
                    Console.WriteLine("Problem aborting download");
            }
            dl.file.Close();
            dl.downloading = false;
            dl.finaliseCallback(success);
        }
      
        public bool Download(bool full, String filename, DownloadCallback finaliseCallback )
        {
            String st;
            int length = 0;
            int block = 0;

            Console.WriteLine("::Download()");

            //Assert(dl.downloading == false);
            dl.finaliseCallback = finaliseCallback;

            _recvTransType = ReceiveTransmissionType.Sync;
            try
            {
                st = comPort.ReadExisting();
                Console.WriteLine("::Download - ReadExisting: '" + st + "'");
                if (full)
                    WriteData("mdA\r");
                else
                    WriteData("md\r");
                bool sigOk = getString("dl");
                length = getLong();
                block = getLong();

                if (!sigOk || block == 0)
                {
                    DisplayData(MessageType.Error, "Fatal error while talking with the device...\r\n");
                    dl.finaliseCallback(false);
                    return false;
                }
                if (length == 0)
                {
                    DisplayData(MessageType.Error, "Device is empty\r\n");
                    dl.finaliseCallback(false);
                    return false;
                }              
            }
            catch
            //catch (Exception ex)
            {
                //DisplayData(MessageType.Error, ex.Message + "\r\n");
                DisplayData(MessageType.Error, "Can not read device memory.\r\n");
                dl.finaliseCallback(false);
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

                Console.WriteLine("::Download - filesize = " + dl.fileSize + " in blocks of = " + dl.blockSize);

                if (_progress != null)
                {
                    _progress.Maximum = length;
                }
                DisplayData(MessageType.Normal, "Downloading " + length + " bytes ...\r\n");
                downloadHandler(null, null);
                if (dl.downloading)
                {
                    Console.WriteLine("::Download() - added the download handler");
                    comPort.DataReceived += new SerialDataReceivedEventHandler(downloadHandler);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            //catch (Exception ex)
            {
                if (dl.file != null)
                    dl.file.Close();

                //DisplayData(MessageType.Error, ex.Message + "\r\n");
                DisplayData(MessageType.Error, "Can not write to port.\r\n");
                dl.finaliseCallback(false);
                return false;
            }
        }
        #endregion

    
    }
}
