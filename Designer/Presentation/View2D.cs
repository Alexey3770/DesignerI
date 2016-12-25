using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Designer.Domain.Entity;
using System.Drawing.Drawing2D;

namespace Designer
{
    public partial class View2D : UserControl
    {
        public Camera2D Camera
        {
            get { return camera; }
        }

        List<IDrawableObj> objs;
        Camera2D camera;

        public View2D()
        {
            InitializeComponent();
            camera = new Camera2D();
        }

        public void DrawObjects(List<IDrawableObj> objs = null)
        {
            if(objs != null)
                this.objs = objs;
            if (this.objs == null)
                return;
            float widthM = 0, heightM = 0;
            IDrawableObj ds;
            foreach(IDrawableObj d in this.objs)
            {
                if(d.Image2D.Width > widthM)
                {
                    widthM = d.Image2D.Width;
                    ds = d;
                }
                if (d.Image2D.Height > heightM)
                {
                    heightM = d.Image2D.Height;
                    ds = d;
                }
            }
            if(widthM > heightM)
            {
                camera.Scale = (float)Width * 0.7f / widthM;
            }
            else
            {
                camera.Scale = (float)Height * 0.7f / heightM;
            }
            camera.PositionCamera = new Point((int)(Width / 2 - widthM*camera.Scale / 2), (int)(Height / 2 - heightM*camera.Scale / 2));
            Invalidate();
        }        
        
        
        protected override void OnPaint(PaintEventArgs e)
        {
            if (objs != null)
            {
                Matrix m = new Matrix();
                m.Translate(camera.PositionCamera.X, camera.PositionCamera.Y);
                m.Scale(camera.Scale, camera.Scale);
                
                foreach (IDrawableObj ob in objs)
                {
                    m.RotateAt(ob.Angle, ob.Position);
                    e.Graphics.Transform = m;
                    e.Graphics.DrawImage(ob.Image2D, ob.Position);
                    
                    m.RotateAt(-ob.Angle, ob.Position);
                    //e.Graphics.Transform = m;
                }
            }
        }
    }
}
