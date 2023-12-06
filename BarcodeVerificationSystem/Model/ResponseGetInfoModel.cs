using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeVerificationSystem
{
    [DataContract]
    public class ResponseGetInfoModel
    {
        [DataMember(Name = "success")]
        public string success { get; set; }
        [DataMember(Name = "message")]
        public string message { get; set; }
        [DataMember(Name = "data")]
        public GetInfoDataResponseModel data { get; set; }

    }
}
