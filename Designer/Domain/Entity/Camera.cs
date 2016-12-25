using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Designer
{
    public class Camera
    {
        public Point3D Position
        {
            get { return position; }
            set { position = value; }
        }
        public Point3D Target
        {
            get { return target; }
            set { target = value; }
        }
        public Point3D UpVector
        {
            get { return upVector; }
            set { upVector = value; }
        }

        Point3D position;
        Point3D target;
        Point3D upVector;
        float angleX_Y, //Угол между вектором в плоскости xy и осью x
            angleZ_XY;//Угол между вектором и плоскостью xy
        float r = 30;//Длина вектора

        public Camera(Point3D position,Point3D target = null)
        {
            this.position = position;
            this.target = target != null ? target : new Point3D(0, 0,0);
            float rs = (float)Math.Sqrt(Math.Pow(position.X, 2) + Math.Pow(position.Y, 2) + Math.Pow(position.Z, 2));
            float asp = (rs - r) / rs;
            this.target.X = position.X * asp;
            this.target.Y = position.Y * asp;
            this.target.Z = position.Z * asp;

            upVector = new Point3D(0, 0, 1);
            angleX_Y = 270;
        }

        public void Move(float dx,float dy,float dz)
        {
            float vX = target.X - position.X;
            float vY = target.Y - position.Y;

            float l = (float)Math.Sqrt(Math.Pow(vX, 2) + Math.Pow(vY, 2));
            float aspX = dx / l;
            float aspY = dy / l;

            position.X += vX * aspX - vY * aspY;
            position.Y += vY * aspX + vX * aspY;
            position.Z += dz;

            target.X += vX * aspX - vY * aspY;
            target.Y += vY * aspX + vX * aspY;
            target.Z += dz;

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
            

            target.X = position.X + (float)(r * Math.Cos(angleX_Y*Math.PI/180) * Math.Cos(angleZ_XY * Math.PI / 180));
            target.Y = position.Y + (float)(r * Math.Sin(angleX_Y * Math.PI / 180) * Math.Cos(angleZ_XY * Math.PI / 180));
            target.Z = position.Z + (float)(r * Math.Sin(angleZ_XY * Math.PI / 180));
        }
        
    }
}
