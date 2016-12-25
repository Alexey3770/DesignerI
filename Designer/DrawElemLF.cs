using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Designer
{
    class DrawElemLF : PictureBox
    {
        public bool IsSelected
        {
            get { return isSelected; }
        }

        private bool isSelected;
        private Image img;
        private string name;

        public DrawElemLF(IDrawableListFur idlf)
        {
            img = Properties.Resources.ramka;
            Image = idlf.Img;
            name = idlf.Name;
            Click += DrawElemLF_Click;
            SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DrawElemLF_Click(object sender, EventArgs e)
        {
            isSelected = !isSelected;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if(isSelected)
                e.Graphics.DrawImage(img, 0, 0, Width - 1, Height - 1);
            base.OnPaint(e);
            e.Graphics.DrawString(name, DefaultFont, Brushes.Black, new Point(Width / 2, Height - 20));
        }
    }
}
