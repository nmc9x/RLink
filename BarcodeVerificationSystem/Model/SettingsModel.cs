using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace BarcodeVerificationSystem.Model
{
    public class SettingsModel
    {
        #region Properties
        // Compare type
        private CompareType _CompareType = CompareType.CanRead;
        public CompareType CompareType { get => _CompareType; set => _CompareType = value; }
        // Compare type

        private string _JobFileExtension = ".rvis";

        // Camera settings
        private List<CameraModel> _CameraList = new List<CameraModel>();
        public List<CameraModel> CameraList { get => _CameraList; set => _CameraList = value; }
        // END Camera settings

        // Printer settings
        private List<PrinterModel> _PrinterList = new List<PrinterModel>();
        public List<PrinterModel> PrinterList { get => _PrinterList; set => _PrinterList = value; }
        private bool _IsPrinting = true;
        public bool IsPrinting { get => _IsPrinting; set => _IsPrinting = value; }



        // END Printer settings

        // Sensor controller
        private bool _SensorControllerEnable = true;
        private string _SensorControllerIP = "192.168.1.100";
        private int _SensorControllerPort = 2001;
        private int _SensorControllerPulseEncoder = 3600;
        private float _SensorControllerEncoderDiameter = 48.51f;
        private int _SensorControllerDelayBefore = 0;
        private int _SensorControllerDelayAfter = 0;

        public bool SensorControllerEnable { get => _SensorControllerEnable; set => _SensorControllerEnable = value; }
        public string SensorControllerIP { get => _SensorControllerIP; set => _SensorControllerIP = value; }
        public int SensorControllerPort { get => _SensorControllerPort; set => _SensorControllerPort = value; }
        public int SensorControllerPulseEncoder { get => _SensorControllerPulseEncoder; set => _SensorControllerPulseEncoder = value; }
        public float SensorControllerEncoderDiameter { get => _SensorControllerEncoderDiameter; set => _SensorControllerEncoderDiameter = value; }
        public int SensorControllerDelayBefore { get => _SensorControllerDelayBefore; set => _SensorControllerDelayBefore = value; }
        public int SensorControllerDelayAfter { get => _SensorControllerDelayAfter; set => _SensorControllerDelayAfter = value; }
        // END Sensor controller

        // System settings
        private string _ExportCheckedResultPath = @"C:\Users\Public\Exports\CheckedResult";
        private string _DataCheckedFileName = "20191220_164200_DataChecked.txt";
        private bool _ExportImageEnable = false;
        private string _ExportImagePath = @"C:\Users\Public\Exports\Images";
        private string _FailedDataSentToPrinter = @"Failure";
        private List<PODModel> _PrintFieldForVerifyAndPrint = new List<PODModel>();
        public string ExportCheckedResultPath { get => _ExportCheckedResultPath; set => _ExportCheckedResultPath = value; }
        public string DataCheckedFileName { get => _DataCheckedFileName; set => _DataCheckedFileName = value; }
        public bool ExportImageEnable { get => _ExportImageEnable; set => _ExportImageEnable = value; }
        public string ExportImagePath { get => _ExportImagePath; set => _ExportImagePath = value; }
        public string FailedDataSentToPrinter { get => _FailedDataSentToPrinter; set => _FailedDataSentToPrinter = value; }
        public List<PODModel> PrintFieldForVerifyAndPrint { get => _PrintFieldForVerifyAndPrint; set => _PrintFieldForVerifyAndPrint = value; }
        // END System settings

        // Language
        private string _Language = "en-US";
        public string Language { get => _Language; set => _Language = value; }

        private string _DateTimeFormatOfResult = "yyyy/MM/dd HH:mm:ss";
        public string DateTimeFormatOfResult { get => _DateTimeFormatOfResult; set => _DateTimeFormatOfResult = value; }

        private bool _OutputEnable = true;
        public bool OutputEnable { get => _OutputEnable; set => _OutputEnable = value; }
        private bool _TotalCheckEnable = true;
        public bool TotalCheckEnable { get => _TotalCheckEnable; set => _TotalCheckEnable = value; }
        private bool _VerifyAndPrintBasicSentMethod = true;
        public bool VerifyAndPrintBasicSentMethod { get => _VerifyAndPrintBasicSentMethod; set => _VerifyAndPrintBasicSentMethod = value; }

        private string _ExportNamePrefixFormat = "yyyyMMdd_HHmmss";
        public string ExportNamePrefixFormat { get => _ExportNamePrefixFormat; set => _ExportNamePrefixFormat = value; }

        // END Language
        private string _JobDateTimeFormat = "yyyyMMdd_HHmmss";
        public string JobDateTimeFormat { get => _JobDateTimeFormat; set => _JobDateTimeFormat = value; }
        private string _JobFileNameDefault = "Template";
        public string JobFileNameDefault { get => _JobFileNameDefault; set => _JobFileNameDefault = value; }
        public string JobFileExtension { get => _JobFileExtension; set => _JobFileExtension = value; }

        #endregion Properties

        #region Methods
        public virtual void SaveSettings(String fileName)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, this);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception)
            {
                //Log exception here
            }
        }

        public static SettingsModel LoadSetting(String fileName)
        {
            SettingsModel info = null;
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(SettingsModel);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        info = (SettingsModel)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception)
            {
                return new SettingsModel();
            }

            return info;
        }

        #endregion Methods
    }
}
