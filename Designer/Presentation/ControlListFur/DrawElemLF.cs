using Designer.Acquaintance;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Designer.Presentation.ControlListFur
{
    /// <summary>
    /// Графический элемент отображающий IDrawableListFur
    /// </summary>
    class DrawElemLF : PictureBox
    {
        /// <summary>
        /// Выбран ли текущий элемент
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value;Invalidate(); }
        }
        /// <summary>
        /// Отображаемый объект
        /// </summary>
        public IDrawableList Furniture
        {
            get { return idlf; }
        }

        bool isSelected;
        Image img;
        string name;
        IDrawableList idlf;
        Pen o = new Pen(Brushes.Red, 3);

        public DrawElemLF(IDrawableList idlf)
        {
            this.idlf = idlf;
            img = Properties.Resources.ramka;
            Image = idlf.Img;
            name = idlf.Name;
            SizeMode = PictureBoxSizeMode.StretchImage;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
            base.OnPaint(e);
            //e.Graphics.FillRectangle(Brushes.White, new Rectangle(0, Height - 20, Width, 20));
           e.Graphics.DrawString(name, new Font(DefaultFont, FontStyle.Bold), Brushes.Black, new Point(Width / 2 - (int)e.Graphics.MeasureString(name,DefaultFont).Width/2, Height -20));
            
            if (isSelected)
                e.Graphics.DrawRectangle(o, new Rectangle(0, 0, Width - 1, Height - 1));//e.Graphics.DrawImage(img, 0, 0, Width - 1, Height - 1);
        }
    }
}
