using Designer.Acquaintance;
using Designer.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Designer
{
    public delegate void SetMousePos(int x, int y,bool isButtonPress);
    public delegate void NameChanged(string name);
    public delegate void KeyPress(bool isPressed,char key);
    
    public interface IView
    {
        event SetMousePos SurfaceMouseClick;
        event SetMousePos SurfaceMouseMove;
        event NameChanged ListIndexChanged;
        event NameChanged ButtonClick;
        event KeyPress EventKeyPress;
        Camera Camera { get; set; }
        List<IProperties> Properties { set; }
        string NameProperty { set; }
        string NameButton { set; }
        void DrawObjects(List<IDrawableObj> objs);
        void SetDrawList(IDrawableListFur drawList);
    }
}
