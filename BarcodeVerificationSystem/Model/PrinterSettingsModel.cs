using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeVerificationSystem.Model
{
    [DataContract]
    public class PrinterSettingsModel
    {
        [DataMember(Name = "podMode")]
        public int podMode { get; set; } // 0 print all, 1 print last, 2 print last and repeat
        [DataMember(Name = "podDataType")]
        public int podDataType { get; set; }// 0 json, 1 Raw data, 2 Customise
        [DataMember(Name = "monitorResponse")]
        public int monitorResponse { get; set; } // 0 Timed Interval, 1 Each print
        [DataMember(Name= "enablePOD")]
        public bool enablePOD { get; set; } // Enable POD
        [DataMember(Name = "responsePODData")]
        public bool responsePODData { get; set; } // Response POD data
        [DataMember(Name = "responsePODCommand")]
        public bool responsePODCommand { get; set; } // Response POD commnand
        [DataMember(Name = "enableMonitor")]
        public bool enableMonitor { get; set; } // Enable monitor
    }
}
