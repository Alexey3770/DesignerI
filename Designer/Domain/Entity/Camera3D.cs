using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Designer
{
    /// <summary>
    /// Класс определяет позицию и направление просмотра камеры
    /// </summary>
    public class Camera3D
    {
        /// <summary>
        /// Точка наблюдателя.
        /// </summary>
        public Point3D Position
        {
            get;
            set;
        }
        /// <summary>
        /// Точка, на которую смотрит наблюдатель.
        /// </summary>
        public Point3D Target
        {
            get;
            set;
        }
        /// <summary>
        /// Направление верха камеры.
        /// </summary>
        public Point3D UpVector
        {
            get;
            set;
        }

        
        float angleX_Y, //Угол между вектором в плоскости xy и осью x
            angleZ_XY;//Угол между вектором и плоскостью xy
        float r = 30;//Длина вектора
        /// <summary>
        /// Конструктор класса Camera3D с параметрами
        /// </summary>
        /// <param name="position">Начальное положение камеры</param>
        /// <param name="target">Точка просмотра</param>
        public Camera3D(Point3D position,Point3D target = null)
        {
            Position = position;
            Target = Target != null ? Target : new Point3D(0, 0,0);
            float rs = (float)Math.Sqrt(Math.Pow(Position.X, 2) + Math.Pow(Position.Y, 2) + Math.Pow(Position.Z, 2));
            float asp = (rs - r) / rs;
            Target.X = Position.X * asp;
            Target.Y = Position.Y * asp;
            Target.Z = Position.Z * asp;

            UpVector = new Point3D(0, 0, 1);
            angleX_Y = 270;
        }
        /// <summary>
        /// Пермещение камеры.
        /// </summary>
        /// <param name="dx">Сдвиг по оси Х</param>
        /// <param name="dy">Сдвиг по оси Y</param>
        /// <param name="dz">Сдвиг по оси Z</param>
        public void Move(float dx,float dy,float dz)
        {
            float vX = Target.X - Position.X;
            float vY = Target.Y - Position.Y;

            float l = (float)Math.Sqrt(Math.Pow(vX, 2) + Math.Pow(vY, 2));
            float aspX = dx / l;
            float aspY = dy / l;

            Position.X += vX * aspX - vY * aspY;
            Position.Y += vY * aspX + vX * aspY;
            Position.Z += dz;

            Target.X += vX * aspX - vY * aspY;
            Target.Y += vY * aspX + vX * aspY;
            Target.Z += dz;

        }
        int ls = 1,lc = 1;
        /// <summary>
        /// Поворот камеры
        /// </summary>
        /// <param name="axisXd">Угол по оси X</param>
        /// <param name="axisYd">Угол по оси Y</param>
        public void Rotate(float axisXd,float axisYd)
        {
            if (angleZ_XY + axisYd > 85 || angleZ_XY + axisYd < -85)
                return;
            angleX_Y += axisXd;
            angleZ_XY += axisYd;
            if (angleX_Y > 360)
                angleX_Y = angleX_Y - 360;
            

            Target.X = Position.X + (float)(r * Math.Cos(angleX_Y*Math.PI/180) * Math.Cos(angleZ_XY * Math.PI / 180));
            Target.Y = Position.Y + (float)(r * Math.Sin(angleX_Y * Math.PI / 180) * Math.Cos(angleZ_XY * Math.PI / 180));
            Target.Z = Position.Z + (float)(r * Math.Sin(angleZ_XY * Math.PI / 180));
        }
        
    }
}
