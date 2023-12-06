using BarcodeVerificationSystem.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BarcodeVerificationSystem.Model
{
    /// <summary>
    /// @Author: DungLe
    /// @Email: dung.le@rynantech.com
    /// @Date created: October 17, 2022
    /// </summary>
    [Serializable]
    public class PrinterModel
    {
        private int _Index = 0;
        private string _IP = "192.168.0.2";
        private int _Port = 12500;
        private RoleOfStation _RoleOfPrinter = RoleOfStation.ForProduct;
        private bool _IsEnable = true;
        private bool _IsVersion = false;
        private bool _IsConnected = false;
        private int _CountTimeReconnect = 0;
        private int _NumPortRemote = 80;
        private bool _CheckPrinterSettingsIsEnable = true;
        private PODController _PODController = null;
        [XmlIgnore]//Not save value to xml file
        public bool IsConnected { get => _IsConnected; set => _IsConnected = value; }
        [XmlIgnore]//Not save value to xml file
        public int CountTimeReconnect { get => _CountTimeReconnect; set => _CountTimeReconnect = value; }
        [XmlIgnore]//Not save value to xml file
        public PODController PODController { get => _PODController; set => _PODController = value; }
        public int Index { get => _Index; set => _Index = value; }
        public string IP { get => _IP; set => _IP = value; }
        public int Port { get => _Port; set => _Port = value; }
        public RoleOfStation RoleOfPrinter { get => _RoleOfPrinter; set => _RoleOfPrinter = value; }
        public bool IsEnable { get => _IsEnable; set => _IsEnable = value; }
        public bool IsVersion { get => _IsVersion; set => _IsVersion = value; }
        public int NumPortRemote { get => _NumPortRemote; set => _NumPortRemote = value; }
        public bool CheckAllPrinterSettings { get => _CheckPrinterSettingsIsEnable; set => _CheckPrinterSettingsIsEnable = value; }
    }
}
