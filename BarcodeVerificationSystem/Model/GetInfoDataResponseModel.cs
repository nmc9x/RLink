using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeVerificationSystem
{
    [DataContract]
    public class GetInfoDataResponseModel
    {
        [DataMember(Name = "model")]
        public string model { get; set; }
        [DataMember(Name = "headNumber")]
        public string headNumber { get; set; }
        [DataMember(Name = "software")]
        public string software { get; set; }
        [DataMember(Name = "buildDate")]
        public string buildDate { get; set; }
        [DataMember(Name = "buildTime")]
        public string buildTime { get; set; }
        [DataMember(Name = "activeExtFunc")]
        public string activeExtFunc { get; set; }
        [DataMember(Name = "kernelVersion")]
        public string kernelVersion { get; set; }
        [DataMember(Name = "firmware")]
        public string firmware { get; set; }
        [DataMember(Name = "hardware")]
        public string hardware { get; set; }
        [DataMember(Name = "serialNumber")]
        public string serialNumber { get; set; }
        [DataMember(Name = "uuid")]
        public string uuid { get; set; }
        [DataMember(Name = "dateActive")]
        public string dateActive { get; set; }
        [DataMember(Name = "printStatus")]
        public int printStatus { get; set; }
        [DataMember(Name = "printerName")]
        public string printerName { get; set; }
    }
}
