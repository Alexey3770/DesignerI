using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Designer.Domain.Entity
{
    class Side : IDrawableObj
    {
        const float height = 200;

        public Bitmap Image2D
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public PointF Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {

            }
        }

        Point3D[] points;
        Bitmap texture;
        Bitmap image2D;
        Object3D model;

        public Side(PointF p1,PointF p2)
        {
            points = new Point3D[4];
            points[0] = new Point3D(p1.X, p1.Y, 0);
            points[1] = new Point3D(p2.X, p2.Y, 0);
            points[2] = new Point3D(p1.X, p1.Y, height);
            points[3] = new Point3D(p2.X, p2.Y, height);
        }

        public void Draw3D()
        {
        }
        
    }
}
