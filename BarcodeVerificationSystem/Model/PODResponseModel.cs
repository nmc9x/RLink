using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeVerificationSystem.Model
{
    /// <summary>
    /// @Author: TrangDong
    /// @Email: trang.dong@rynantech.com
    /// @Date created: July 22, 2020
    /// </summary>
    [DataContract]//For serialization and deserialization
    public class PODResponseModel
    {
        [DataMember(Name = "command")]
        public string Command { get; set; }

        [DataMember(Name = "template")]
        public string[] Template { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "error")]
        public string Error { get; set; }
    }
}
