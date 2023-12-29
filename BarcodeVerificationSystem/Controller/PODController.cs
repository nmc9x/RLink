using BarcodeVerificationSystem.Model;
using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarcodeVerificationSystem.Controller
{

    public class PODController
    {
        private string _ServerIP = "127.0.0.1";
        private int _Port = 1997;
        private RoleOfStation _RoleOfPrinter = RoleOfStation.ForProduct;
        private readonly int _TimeOutOfConnection = 1000;
        private readonly int _SendTimeout = 1000;
        private readonly byte _StartPackage = 0x02;
        protected byte _EndPackage = 0x03;
        private readonly bool _IsVersion = false;
        private TcpClient _TcpClient;
        private NetworkStream _NetworkStream;
        private StreamReader _StreamReader = null;
        private StreamWriter _StreamWriter = null;
        private Thread _ThreadReceiveData = null;
        public event EventHandler OnPODReceiveMessageEvent;
        public event EventHandler OnPODReceiveDataEvent;
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

        public PODController(string serverIP, int port, int timeOutOfConnection, int sendTimeout)
        {
            _ServerIP = serverIP;
            _Port = port;
            _TimeOutOfConnection = timeOutOfConnection;
            _SendTimeout = sendTimeout;
        }
        public PODController(string serverIP, int port, RoleOfStation roleOfPrinter, int timeOutOfConnection, int sendTimeout, bool isVersion)
        {
            _ServerIP = serverIP;
            _Port = port;
            _RoleOfPrinter = roleOfPrinter;
            _TimeOutOfConnection = timeOutOfConnection;
            _SendTimeout = sendTimeout;
            _IsVersion = isVersion;
            _IsVersion = false; 
        }
        public bool Connect()
        {
            try
            {
                _TcpClient = new TcpClient();
                var task = _TcpClient.ConnectAsync(_ServerIP, _Port);
                task.Wait(_TimeOutOfConnection);
                if (!task.IsCompleted)
                {
                    _ = Disconnect();
                    return false;
                }
                _TcpClient.SendTimeout = _SendTimeout;
                _NetworkStream = _TcpClient.GetStream();
                _StreamReader = new StreamReader(_NetworkStream);
                _StreamWriter = new StreamWriter(_NetworkStream)
                {
                    AutoFlush = true
                };

                uint dummy = 0;
                byte[] inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
                BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);
                BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
                BitConverter.GetBytes((uint)1000).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);
                _TcpClient.Client.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
                _ThreadReceiveData = new Thread(ReceiveData)
                {
                    IsBackground = true,
                    Priority = ThreadPriority.Normal
                };
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

                _TcpClient?.Dispose();
                _TcpClient = null;
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
                            string dataRead = Encoding.ASCII.GetString(bytes, 0, counter);
                           // Console.WriteLine("Received: {0}", dataRead);
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
                while (true)
                {
                    try
                    {
                        var currentLine = new StringBuilder();
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
                       // Console.WriteLine("Receive data: {0}", dataRead);
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
                _StreamWriter?.Write((char)_StartPackage + message + (char)_EndPackage);
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

        public void RaiseOnPODReceiveMessageEvent(object data)
        {
            OnPODReceiveMessageEvent?.Invoke(data, EventArgs.Empty);
        }
        
        public void RaiseOnPODReceiveDataEventEvent(PODDataModel data)
        {
            OnPODReceiveDataEvent?.Invoke(data, EventArgs.Empty);
        }
    }
}