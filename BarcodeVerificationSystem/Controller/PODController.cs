using BarcodeVerificationSystem.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarcodeVerificationSystem.Controller
{
    /// <summary>
    /// @Author: TrangDong
    /// @Email: trang.dong@rynantech.com
    /// @Date created: July 21, 2021
    /// </summary>
    public class PODController
    {
        // Properties
        private string _ServerIP = "127.0.0.1";
        private int _Port = 1997;
        private RoleOfStation _RoleOfPrinter = RoleOfStation.ForProduct;
        private int _TimeOutOfConnection = 1000;
        private int _SendTimeout = 1000;
        // Start package of POD Message: STX
        private byte _StartPackage = 0x02;
        // End package of POD message: ETX
        protected byte _EndPackage = 0x03;

        //Edit by DungLe
        // Version POD Protocol R20
        private bool _IsVersion = false;
        //
        private TcpClient _TcpClient;
        private NetworkStream _NetworkStream;
        private StreamReader _StreamReader = null;
        private StreamWriter _StreamWriter = null;
        private Thread _ThreadReceiveData = null;
        // END Properties

        public string ServerIP
        {
            get { return _ServerIP; }
            set { _ServerIP = value; Disconnect(); }
        }

        public int Port
        {
            get { return _Port; }
            set { _Port = value; Disconnect(); }
        }

        public RoleOfStation RoleOfPrinter
        {
            get { return _RoleOfPrinter; }
            set { _RoleOfPrinter = value; }
        }

        // Constructor
        public PODController(string serverIP, int port, int timeOutOfConnection, int sendTimeout)
        {
            _ServerIP = serverIP;
            _Port = port;
            _TimeOutOfConnection = timeOutOfConnection;
            _SendTimeout = sendTimeout;
        }
        // Edit by DungLe
        // Added isVersion
        public PODController(string serverIP, int port, RoleOfStation roleOfPrinter, int timeOutOfConnection, int sendTimeout, bool isVersion)
        {
            _ServerIP = serverIP;
            _Port = port;
            _RoleOfPrinter = roleOfPrinter;
            _TimeOutOfConnection = timeOutOfConnection;
            _SendTimeout = sendTimeout;
            _IsVersion = isVersion;

            _IsVersion = false; // Remove this if using IsVersion
        }
        // END Constructor

        // Methods
        public bool Connect()
        {
            try
            {
                //Console.WriteLine("TCP connecting...");
                _TcpClient = new TcpClient();
                //_TcpClient.Connect(_ServerIP, _Port);
                var task = _TcpClient.ConnectAsync(_ServerIP, _Port);
                //task.Wait(5000);
                task.Wait(_TimeOutOfConnection);
                if (!task.IsCompleted)
                {
                    Disconnect();
                    return false;
                }

                //Console.WriteLine("TCP connected!!!");
                _TcpClient.SendTimeout = _SendTimeout;
                _NetworkStream = _TcpClient.GetStream();
                _StreamReader = new StreamReader(_NetworkStream);
                _StreamWriter = new StreamWriter(_NetworkStream);
                _StreamWriter.AutoFlush = true;

                uint dummy = 0;
                byte[] inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
                //set keepalive on
                BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);
                //interval time between last operation on socket and first checking. example:5000ms=5s
                BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
                //after first checking, socket will check serval times by 1000ms.
                BitConverter.GetBytes((uint)1000).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);

                _TcpClient.Client.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);

                //KillThreadReceiveData();
                _ThreadReceiveData = new Thread(ReceiveData);
                _ThreadReceiveData.IsBackground = true;
                _ThreadReceiveData.Priority = ThreadPriority.Normal;
                _ThreadReceiveData.Start();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void KillThreadReceiveData()
        {
            if (_ThreadReceiveData != null && _ThreadReceiveData.IsAlive)
            {
                // Release thread
                _ThreadReceiveData.Abort();
                _ThreadReceiveData = null;
            }
        }

        public bool Disconnect()
        {
            try
            {
                KillThreadReceiveData();

                if (_StreamReader != null)
                {
                    _StreamReader.Close();
                    _StreamReader = null;
                }

                if (_StreamWriter != null)
                {
                    _StreamWriter.Close();
                    _StreamWriter = null;
                }

                if (_NetworkStream != null)
                {
                    _NetworkStream.Close();
                    _NetworkStream = null;
                }

                if (_TcpClient != null)
                {
                    _TcpClient.Client.Close();
                    _TcpClient.Client = null;
                    _TcpClient.Close();
                    _TcpClient = null;
                }
            }
            catch(Exception)
            { }
            return true;
        }

        public bool IsConnected()
        {
            try
            {
                if (_TcpClient != null)
                {
                    if (_TcpClient.Client.Connected)
                    {
                        return _TcpClient.Client.Connected;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // Edit by TrangDong
        // Date 23/09/2022
        private void ReceiveData()
        {
            if (_IsVersion)
            {
                //Support software R20 1.0.6.G
                Byte[] bytes;
                int counter;
                while (true)
                {
                    try
                    {
                        if (_TcpClient.ReceiveBufferSize > 0)
                        {
                            bytes = new byte[_TcpClient.ReceiveBufferSize];
                            _NetworkStream.Read(bytes, 0, bytes.Length);

                            // Get length has data
                            counter = 0;
                            for (int index = 0; index < bytes.Length; index++)
                            {
                                if (bytes[index] > 0x00)
                                {
                                    counter++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            // END Get length has data
                            //string dataRead = Encoding.ASCII.GetString(bytes); //the message incoming
                            string dataRead = Encoding.ASCII.GetString(bytes, 0, counter);
                            Console.WriteLine("Received: {0}", dataRead);
                            RaiseOnPODReceiveMessageEvent(dataRead);
                            RaiseOnPODReceiveDataEventEvent(new PODDataModel { IP = _ServerIP, Port = _Port, RoleOfPrinter = _RoleOfPrinter, Text = dataRead });
                            if (counter == 0)
                            {
                                OnDisconnect();
                            }
                        }
                        else
                        {
                            Thread.Sleep(5);
                        }
                    }
                    catch (Exception) { }
                    Thread.Sleep(10);
                }
            }
            else
            {
                // Update later
                while (true)
                {
                    try
                    {
                        //string dataRead = _StreamReader.ReadLine();
                        StringBuilder currentLine = new StringBuilder();
                        int code;
                        char charRead;
                        while ((code = _StreamReader.Read()) >= 0)
                        {
                            charRead = (char)code;
                            if (charRead == '\r' || charRead == '\n' || charRead == _EndPackage)
                            {
                                break;
                            }

                            if (charRead != _StartPackage && charRead != _EndPackage)
                            {
                                currentLine.Append(charRead);
                            }
                        }

                        string dataRead = currentLine.ToString();
                        Console.WriteLine("Receive data: {0}", dataRead);
                        RaiseOnPODReceiveMessageEvent(dataRead);
                        RaiseOnPODReceiveDataEventEvent(new PODDataModel { IP = _ServerIP, Port = _Port, RoleOfPrinter = _RoleOfPrinter, Text = dataRead });
                        if (dataRead != null && dataRead == "")
                        {
                            OnDisconnect();
                        }
                    }
                    catch (Exception) { }
                    Thread.Sleep(1);
                }
            }
        }

        public bool Send(string message, int sendTimeout = 0)
        {
            try
            {
                //_StreamWriter.WriteLine(message);
                _StreamWriter.Write((char)_StartPackage + message + (char)_EndPackage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private async void OnDisconnect()
        {
            await Task.Run(() =>
            {
                Disconnect();
            });
        }
        // Methods

        // Events
        public event EventHandler OnPODReceiveMessageEvent;
        public void RaiseOnPODReceiveMessageEvent(object data)
        {
            OnPODReceiveMessageEvent?.Invoke(data, EventArgs.Empty);
        }

        public event EventHandler OnPODReceiveDataEvent;
        public void RaiseOnPODReceiveDataEventEvent(PODDataModel data)
        {
            OnPODReceiveDataEvent?.Invoke(data, EventArgs.Empty);
        }
        // END Events
    }
}