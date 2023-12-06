using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeVerificationSystem.Model
{
    public class ExportResultFileModel
    {
        private string _FileName = null;
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        private List<string[]> _ValueList = new List<string[]>();

        public List<string[]> ValueList
        {
            get { return _ValueList; }
            set { _ValueList = value; }
        }
    }
}
