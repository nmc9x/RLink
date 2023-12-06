using BarcodeVerificationSystem.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BarcodeVerificationSystem.Model
{
    public class JobModel
    {
        #region Properties

        private CompareType _CompareType = CompareType.CanRead;
        private string _StaticText = "";
        private string _DirectoryDatabase = "";
        private List<PODModel> _PODFormat;
        private string _FileName = "";
        private bool _AutoLoad = true;
        private string _UserCreate = "";
        private string _DatabaseBufferPath = "";
        private string _CheckedResultPath = "";
        private string _PrintedResponePath = "";
        private bool _PrinterSeries = true;
        private string _TemplatePrint = "";
        private double _NumberTotalsCode = 0;
        private JobType _JobType = JobType.AfterProduction;
        private JobStatus _JobStatus = JobStatus.NewlyCreated;
        public CompareType CompareType { get => _CompareType; set => _CompareType = value; }
        public string StaticText { get => _StaticText; set => _StaticText = value; }
        public string DirectoryDatabase { get => _DirectoryDatabase; set => _DirectoryDatabase = value; }
        public List<PODModel> PODFormat { get => _PODFormat; set => _PODFormat = value; }
        public string FileName { get => _FileName; set => _FileName = value; }
        public bool AutoLoad { get => _AutoLoad; set => _AutoLoad = value; }
        public string UserCreate { get => _UserCreate; set => _UserCreate = value; }
        public string DatabaseBufferPath { get => _DatabaseBufferPath; set => _DatabaseBufferPath = value; }
        public string CheckedResultPath { get => _CheckedResultPath; set => _CheckedResultPath = value; }
        public string PrintedResponePath { get => _PrintedResponePath; set => _PrintedResponePath = value; }
        public string TemplatePrint { get => _TemplatePrint; set => _TemplatePrint = value; }
        public bool PrinterSeries { get => _PrinterSeries; set => _PrinterSeries = value; }
        public double NumberTotalsCode { get => _NumberTotalsCode; set => _NumberTotalsCode = value; }
        public JobType JobType { get => _JobType; set => _JobType = value; }
        public JobStatus JobStatus { get => _JobStatus; set => _JobStatus = value; }

        #endregion Properties


        #region Methods
        public void SaveFile(String fileName)
        {
            try
            {
                var xs = new XmlSerializer(typeof(JobModel));

                using (TextWriter sw = new StreamWriter(fileName))
                {
                    xs.Serialize(sw,this);
                }
            }
            catch
            { }
        }

        public static JobModel LoadFile(String fileName)
        {
            try
            {
                JobModel info = null;
                var xs = new XmlSerializer(typeof(JobModel));

                using (var sr = new StreamReader(fileName))
                {
                    XmlReader xr = XmlReader.Create(sr);
                    info = (JobModel)xs.Deserialize(xr);
                }

                return info;
            }
            catch
            {
                return null;
            }

        }

        #endregion Methods
    }
}
