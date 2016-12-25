using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Designer.Presentation.ControlListFur
{
    /// <summary>
    /// Текстовый элемент ListView
    /// </summary>
    public partial class ElementFurList : UserControl
    {
        private Brush brush;
        private Brush mainRectBrush;
        private string text;
        /// <summary>
        /// Конструктор класса ElementFurList
        /// </summary>
        /// <param name="width"></param>
        /// <param name="hegiht"></param>
        /// <param name="color"></param>
        /// <param name="text"></param>
        public ElementFurList(int width,int hegiht,int color,string text)
        {
            InitializeComponent();
            Width = width;
            Height = hegiht;
            this.text = text;
            mainRectBrush = Brushes.White;
            switch (color)
            {
                case 0: brush = Brushes.Red; break;
                case 1: brush = Brushes.Orange; break;
                case 2: brush = Brushes.Yellow; break;
                case 3: brush = Brushes.Green; break;
                case 4: brush = Brushes.Aqua; break;
                case 5: brush = Brushes.Blue; break;
                case 6: brush = Brushes.BlueViolet; break;
                default: brush = Brushes.Gray; break;
            }
            MouseEnter += ElementFurList_MouseEnter;
            MouseLeave += ElementFurList_MouseLeave;
        }

        private void ElementFurList_MouseLeave(object sender, EventArgs e)
        {
            timerEnter.Stop();
            timerLeave.Start();
        }

        private void ElementFurList_MouseEnter(object sender, EventArgs e)
        {
            timerLeave.Stop();
            timerEnter.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(mainRectBrush, 0, 0, Width, Height);
            e.Graphics.FillRectangle(brush, 0, 0, Width * 0.05f, Height);
            e.Graphics.DrawString(text, Font, Brushes.Black, new PointF(Width * 0.1f, Height / 2 - Font.Height / 2));
        }

        private float blue = 255;

        private void timerEnter_Tick(object sender, EventArgs e)
        {
            blue -= 20f;
            if (blue < 0)
            {
                timerEnter.Stop();
                blue = 0;
            }
            mainRectBrush = new SolidBrush(Color.FromArgb(255, 255, (int)blue));            
            Invalidate();
        }

        private void timerLeave_Tick(object sender, EventArgs e)
        {
            blue += 20f;
            if (blue > 255)
            {
                timerLeave.Stop();
                blue = 255;
            }
            mainRectBrush = new SolidBrush(Color.FromArgb(255, 255, (int)blue));
            
            Invalidate();
        }
    }
}
