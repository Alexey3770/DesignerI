using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Designer.Domain.Entity
{
    /// <summary>
    /// Класс хранит данные о положении камеры для двухмерного представления
    /// </summary>
    public class Camera2D
    {
        /// <summary>
        /// Положение камеры
        /// </summary>
        public Point PositionCamera
        {
            get { return positionCamera; }
            set { positionCamera = value; }
        }
        /// <summary>
        /// Масштаб
        /// </summary>
        public float Scale
        {
            get;
            set;
        }

        Point positionCamera;
        /// <summary>
        /// Конструктор класса Camera2D
        /// </summary>
        public Camera2D()
        {
            PositionCamera = new Point(0, 0);
            Scale = 1;
        }
        /// <summary>
        /// Конструктор класса с параметрами
        /// </summary>
        /// <param name="positionCamera">Начальное положение камеры</param>
        /// <param name="scale">Масштаб</param>
        public Camera2D(Point positionCamera,int scale)
        {
            PositionCamera = positionCamera;
            Scale = scale;
        }
        /// <summary>
        /// Перемещение камеры
        /// </summary>
        /// <param name="dx">Сдвиг по оси X</param>
        /// <param name="dy">Сдвиг по оси Y</param>
        public void MoveCamera(int dx, int dy)
        {
            positionCamera.X += dx;
            positionCamera.Y += dy;
        }
        /// <summary>
        /// Изменение масштаба
        /// </summary>
        /// <param name="dscale"></param>
        public void ChangeScale(int dscale)
        {
            Scale += dscale;
        }
    }
}