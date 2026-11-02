using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IDCard
{
    // Public controls also work in the Visual Studio Form Designer.
    public class CardPanel : Panel
    {
        public CardPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.FromArgb(224, 235, 248);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle r = new Rectangle(1, 1, Width - 3, Height - 3);
            const int diameter = 26;
            using (GraphicsPath path = new GraphicsPath())
            using (Pen pen = new Pen(Color.FromArgb(57, 116, 194), 2))
            {
                path.AddArc(r.Left, r.Top, diameter, diameter, 180, 90);
                path.AddArc(r.Right - diameter, r.Top, diameter, diameter, 270, 90);
                path.AddArc(r.Right - diameter, r.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(r.Left, r.Bottom - diameter, diameter, diameter, 90, 90);
                path.CloseFigure();
                using (SolidBrush outside = new SolidBrush(Parent != null ? Parent.BackColor : SystemColors.Control))
                    e.Graphics.FillRectangle(outside, ClientRectangle);
                using (SolidBrush fill = new SolidBrush(BackColor))
                    e.Graphics.FillPath(fill, path);
                e.Graphics.DrawPath(pen, path);
            }
        }
    }

    public class UnderlinedLabel : Label
    {
        public UnderlinedLabel()
        {
            AutoSize = false;
            AutoEllipsis = true;
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen pen = new Pen(Color.FromArgb(126, 162, 209)))
                e.Graphics.DrawLine(pen, 0, Height - 1, Width - 1, Height - 1);
        }
    }
}
