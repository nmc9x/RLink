using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeVerificationSystem.Model
{
    [DataContract]
    public class PrinterSettingsResponseModel
    {
        [DataMember(Name = "success")]
        public bool success { get; set; }
        [DataMember(Name = "data")]
        public PrinterSettingsModel data { get; set; }
        [DataMember(Name ="code")]
        public string code { get; set; }
    }
}
