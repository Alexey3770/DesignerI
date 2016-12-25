using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Designer.Acquaintance
{
    /// <summary>
    /// Интерфейс используется классом ListView в качестве элементов для отображения
    /// </summary>
    public interface IDrawableList
    {
        /// <summary>
        /// Изображение элемента списка (возможно Null)
        /// </summary>
        Bitmap Img { get; }
        /// <summary>
        /// Название отображемого элемента списка 
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Подсписок отображемых элементов (возможно Null)
        /// </summary>
        List<IDrawableList> SubDrawObj { get; }
    }
}
