using Cognex.InSight.Web.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeVerificationSystem.Model
{
    /// <summary>
    /// @Author: DungLe
    /// @Email: dung.le@rynantech.com
    /// @Date created: October 17, 2022
    /// </summary>
    public class DetectModel
    {
        private int _Index = 0;
        private int _OldIndex = 0;
        private Bitmap _Image = null;
        private ComparisonResult _CompareResult = ComparisonResult.Valid;
        private long _CompareTime = 0; //milliseconds
        private string _ProcessingDateTime = "";
        private string _Text = "";
        private RoleOfStation _RoleOfCamera = RoleOfStation.ForProduct;

        public int Index { get => _Index; set => _Index = value; }
        public int OldIndex { get => _OldIndex; set => _OldIndex = value; }
        public Bitmap Image { get => _Image; set => _Image = value; }
        public ComparisonResult CompareResult { get => _CompareResult; set => _CompareResult = value; }
        public long CompareTime { get => _CompareTime; set => _CompareTime = value; }
        public string ProcessingDateTime { get => _ProcessingDateTime; set => _ProcessingDateTime = value; }
        public string Text { get => _Text; set => _Text = value; }
        public RoleOfStation RoleOfCamera { get => _RoleOfCamera; set => _RoleOfCamera = value; }
        public CvsDisplay CvsDisplayImage {get;set;}

    }
}
