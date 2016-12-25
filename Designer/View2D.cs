using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Designer
{
    public partial class View2D : UserControl,IView
    {
        public View2D()
        {
            InitializeComponent();
        }

        public void DrawObjects(IDrawableObj[] objs)
        {
        }

        public void SetPosCamera(int x, int y)
        {

        }
    }
}
