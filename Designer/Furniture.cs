using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Designer
{
    public class Furniture : IDrawableObj, IDrawableListFur
    {
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
        }

        public Bitmap Img
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Draw3D()
        {
            throw new NotImplementedException();
        }

        public Furniture()
        {

        }
    }
}
