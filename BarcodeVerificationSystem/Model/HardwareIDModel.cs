using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EncrytionFile.Model
{
    [DataContract]//For serialization and deserialization
    public class HardwareIDModel
    {
        #region Properties
        [DataMember(Name = "_HardwareID")]
        public string HardwareID { get; set; }
        #endregion Properties
    }
}
