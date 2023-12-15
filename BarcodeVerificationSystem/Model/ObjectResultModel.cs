using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeVerificationSystem.Model
{
    public class ObjectResultModel
    {
       
            [JsonProperty("$type")]
            public string Type { get; set; }
            public string Location { get; set; }
            public string Name { get; set; }
            public object Data { get; set; }
        
    }
}
