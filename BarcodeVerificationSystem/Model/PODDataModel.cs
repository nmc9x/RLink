using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeVerificationSystem.Model
{
    /// <summary>
    /// @Author: DungLe 
    /// @Email: dung.le@rynantech.com
    /// @Date create: October, 17 2022
    /// </summary>
    public class PODDataModel
    {
        private string _IP = "";
        private int _Port = 0;
        private RoleOfStation _RoleOfPrinter = RoleOfStation.ForProduct;
        private string _Text = "";

        public string IP { get => _IP; set => _IP = value; }
        public int Port { get => _Port; set => _Port = value; }
        public RoleOfStation RoleOfPrinter { get => _RoleOfPrinter; set => _RoleOfPrinter = value; }
        public string Text { get => _Text; set => _Text = value; }
    }
}
