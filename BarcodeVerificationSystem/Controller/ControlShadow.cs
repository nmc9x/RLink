using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace BarcodeVerificationSystem.Controller
{
    public class ControlShadow
    {
        public static void ApplyShadow(System.Windows.Forms.Control ctrl, Color pColor, int blur)
        {
            ctrl.BackColor = (ctrl.Parent).BackColor;
            ctrl.Paint += (sender, eventArgs) =>
            {
                var brush = new LinearGradientBrush(new Point(0, 0), new Point(blur, 0), Color.Transparent, pColor);

                // left
                eventArgs.Graphics.FillRectangle(brush, 0, blur, blur, ctrl.Height - blur * 2);

                // up
                brush.RotateTransform(90);
                eventArgs.Graphics.FillRectangle(brush, blur, 0, ctrl.Width - blur * 2, blur);

                // right
                // make sure parttern is currect
                brush.ResetTransform();
                brush.TranslateTransform(ctrl.Width % blur, ctrl.Height % blur);

                brush.RotateTransform(180);
                eventArgs.Graphics.FillRectangle(brush, ctrl.Width - blur, blur, blur, ctrl.Height - blur * 2);
                // down
                brush.RotateTransform(90);
                eventArgs.Graphics.FillRectangle(brush, blur, ctrl.Height - blur, ctrl.Width - blur * 2, blur);

                var gp = new GraphicsPath();
                //gp.AddPie(0,0,blur*2,blur*2, 180, 90);
                gp.AddEllipse(0, 0, blur  *2, blur  *2);


                var pgb = new PathGradientBrush(gp);
                pgb.CenterColor = pColor;
                pgb.SurroundColors = new[] { Color.Transparent };
                pgb.CenterPoint = new Point(blur, blur);

                var w = ctrl.Width;
                var h = ctrl.Height;
                // lt
                eventArgs.Graphics.FillPie(pgb, 0, 0, blur  *2, blur  *2, 180, 90);
                // rt
                var matrix = new Matrix();
                matrix.Translate(w - blur * 2, 0);

                pgb.Transform = matrix;
                //pgb.Transform.Translate(w-blur*2, 0);
                eventArgs.Graphics.FillPie(pgb, w - blur  *2, 0, blur  *2, blur * 2, 270, 90);
                // rb
                matrix.Translate(0, h - blur * 2);
                pgb.Transform = matrix;
                eventArgs.Graphics.FillPie(pgb, w - blur  *2, h - blur  *2, blur  *2, blur  *2, 0, 90);
                // lb
                matrix.Reset();
                matrix.Translate(0, h - blur * 2);
                pgb.Transform = matrix;
                eventArgs.Graphics.FillPie(pgb, 0, h - blur  *2, blur  *2, blur * 2, 90, 90);
            };
            ctrl.Resize += (sender, eventArgs) =>
            {
                ctrl.Invalidate();
            };
        }
    }
}
