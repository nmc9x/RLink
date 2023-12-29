using System;
using System.Xml.Serialization;

namespace BarcodeVerificationSystem.Model
{
    [Serializable]
    public class CameraModel
    {
        private CameraType _CameraType = CameraType.UKN;
        public CameraType CameraType
        {
            get => _CameraType; 
            set => _CameraType = value;
        }

        private string _Port = "80";
        public string Port
        {
            get { return _Port; }
            set { _Port = value; }
        }
        //
        private int _ObjectSelectNum;
        public int ObjectSelectNum
        {
            get { return _ObjectSelectNum; }
            set { _ObjectSelectNum = value; }
        }

        public bool[] IsSymbol { get; set; } = new bool[5];

        private int _Index = 0;
        private RoleOfStation _RoleOfCamera = RoleOfStation.ForProduct;
        private string _IP = "192.168.0.2";
        private string _UserName = "";
        private string _Password = "";
        private string _NoReadOutputString = "";
        private bool _AutoReconnect = true;
        private bool _OutputEnable = true;
        private bool _IsEnable = true;
        private string _Name = "";
        private string _SerialNumber = "";
        private bool _IsConnected = false;
        private int _CountTimeReconnect = 0;
        [XmlIgnore] 
        public string Name { get => _Name; set => _Name = value; }
        [XmlIgnore] 
        public string SerialNumber { get => _SerialNumber; set => _SerialNumber = value; }
        [XmlIgnore] 
        public bool IsConnected { get => _IsConnected; set => _IsConnected = value; }
        [XmlIgnore]
        public int CountTimeReconnect { get => _CountTimeReconnect; set => _CountTimeReconnect = value; }
        public int Index { get => _Index; set => _Index = value; }
        public RoleOfStation RoleOfCamera { get => _RoleOfCamera; set => _RoleOfCamera = value; }
        public string IP { get => _IP; set => _IP = value; }
        public string UserName { get => _UserName; set => _UserName = value; }
        public string Password { get => _Password; set => _Password = value; }
        public string NoReadOutputString { get => _NoReadOutputString; set => _NoReadOutputString = value; }
        public bool AutoReconnect { get => _AutoReconnect; set => _AutoReconnect = value; }
        public bool OutputEnable { get => _OutputEnable; set => _OutputEnable = value; }
        public bool IsEnable { get => _IsEnable; set => _IsEnable = value; }
    }
    public enum CameraType
    {
        UKN,
        DM,
        IS
    }
}
