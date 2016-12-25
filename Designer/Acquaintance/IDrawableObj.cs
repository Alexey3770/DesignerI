using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Designer
{
    /// <summary>
    /// Интерфейс используется классами View2D, View3D в качестве элементов для отображения
    /// </summary>
    public interface IDrawableObj
    {
        /// <summary>
        /// Изображение для двухмерного представления
        /// </summary>
        Bitmap Image2D
        {
            get;
        }
        /// <summary>
        /// Угол
        /// </summary>
        float Angle
        {
            get;
        }
        /// <summary>
        /// Точка для двухмерного отображения
        /// </summary>
        PointF Position
        {
            get; set;
        }
        /// <summary>
        /// Отрисовка для трёхмерного представления
        /// </summary>
        void Draw3D();
    }
}
