using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Designer
{
    public interface IDrawableObj
    {
        Bitmap Image2D
        {
            get;
        }
        PointF Position
        {
            get;
        }
        void Draw3D();
    }
}
