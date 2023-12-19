using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeVerificationSystem.Model
{
    /// <summary>
    /// @Author: TrangDong
    /// @Email: trang.dong@rynantech.com
    /// @Date created: November 26, 2019
    /// </summary>
    public class ExportImageModel
    {
        private Bitmap _Image = null;
        public Bitmap Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        private int _Index = 0;
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        public ExportImageModel(Bitmap image, int index)
        {
            _Image = image;
            _Index = index;
        }
    }  
}
