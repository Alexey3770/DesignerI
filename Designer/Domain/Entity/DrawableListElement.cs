using Designer.Acquaintance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Designer.Domain.Entity
{
    /// <summary>
    /// Класс используется ListView в качестве отображаемого элемента
    /// </summary>
    class DrawableListElement : IDrawableList
    {
        Bitmap img;
        string name;
        List<IDrawableList> subDrawObj;

        public Bitmap Img
        {
            get
            {
                return img;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public List<IDrawableList> SubDrawObj
        {
            get
            {
                return subDrawObj;
            }
        }

        public DrawableListElement(Bitmap img, string name, List<IDrawableList> subDrawObj)
        {
            this.img = img;
            this.name = name;
            this.subDrawObj = subDrawObj;
        }
    }
}
