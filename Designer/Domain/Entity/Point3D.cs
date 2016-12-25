using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Designer
{
    /// <summary>
    /// Хранит информацию о положении в трёхмерном пространстве
    /// </summary>
    public class Point3D
    {
        /// <summary>
        /// Координата X
        /// </summary>
        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        /// <summary>
        /// Координата Y
        /// </summary>
        public float Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }
        /// <summary>
        /// Координата Z
        /// </summary>
        public float Z
        {
            get
            {
                return z;
            }

            set
            {
                z = value;
            }
        }
        /// <summary>
        /// Угол поворота относительно оси X
        /// </summary>
        public float AngleAxisX
        {
            get
            {
                return angleAxisX;
            }

            set
            {
                angleAxisX = value;
            }
        }
        /// <summary>
        /// Угол поворота относительно оси Y
        /// </summary>
        public float AngleAxisY
        {
            get
            {
                return angleAxisY;
            }

            set
            {
                angleAxisY = value;
            }
        }
        /// <summary>
        /// Угол поворота относительно оси Z
        /// </summary>
        public float AngleAxisZ
        {
            get
            {
                return angleAxisZ;
            }

            set
            {
                angleAxisZ = value;
            }
        }

        float x, y, z;
        float angleAxisX, angleAxisY, angleAxisZ;
        /// <summary>
        /// Конструктор класса Point3D
        /// </summary>
        public Point3D()
        {
            x = y = z = 0;
        }
        /// <summary>
        /// Конструктор класса Point3D с заданием начального положение
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Point3D(float x,float y,float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        /// <summary>
        /// Конструктор класса Point3D с заданием начального положение и угла поворота
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="angleAxisX">Угол поворота относительно оси X</param>
        /// <param name="angleAxisY">Угол поворота относительно оси Y</param>
        /// <param name="angleAxisZ">Угол поворота относительно оси Z</param>
        public Point3D(float x, float y, float z,float angleAxisX,float angleAxisY, float angleAxisZ) : this(x, y, z)
        {
            this.angleAxisX = angleAxisX;
            this.angleAxisY = angleAxisY;
            this.angleAxisZ = angleAxisZ;
        }

        
    }
}
