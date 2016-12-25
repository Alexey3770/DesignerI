using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Designer
{
    interface IView
    {
        void DrawObjects(IDrawableObj[] objs);
        void SetPosCamera(int x,int y);
    }
}
