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
        //
        private int _Index = 0;
        public int Index { get => _Index; set => _Index = value; }
        //
        private int _OldIndex = 0;
        public int OldIndex { get => _OldIndex; set => _OldIndex = value; }
        //
        private Bitmap _Image = null;
        public Bitmap Image { get => _Image; set => _Image = value; }
        //
        private ComparisonResult _CompareResult = ComparisonResult.Valid;
        public ComparisonResult CompareResult { get => _CompareResult; set => _CompareResult = value; }
        //
        private long _CompareTime = 0; //milliseconds
        public long CompareTime { get => _CompareTime; set => _CompareTime = value; }
        //
        private string _ProcessingDateTime = "";
        public string ProcessingDateTime { get => _ProcessingDateTime; set => _ProcessingDateTime = value; }
        //
        private string _Text = "";
        public string Text { get => _Text; set => _Text = value; }
        //
        private RoleOfStation _RoleOfCamera = RoleOfStation.ForProduct;
        public RoleOfStation RoleOfCamera { get => _RoleOfCamera; set => _RoleOfCamera = value; }
        //
        public CvsDisplay CvsDisplayImage {get;set;}

    }
}
