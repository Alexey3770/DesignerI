using Designer.Acquaintance;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Designer.Domain.Entity
{
    /// <summary>
    /// Класс хранит информацию о мебели и отображает её
    /// </summary>
    public class Furniture : IDrawableObj, IDrawableList
    {
        Bitmap img;
        string name;
        Object3D model3d;
        string path;
        PointF position;
        float scale;
        float angle;
        PointF posRadCont;
        /// <summary>
        /// ID мебели
        /// </summary>
        public int ID
        {
            get;
            private set;
        }
        /// <summary>
        /// Ширина мебели
        /// </summary>
        public float Width
        {
            get;
            private set;
        }
        /// <summary>
        /// Высота мебели
        /// </summary>
        public float Height
        {
            get;
            private set;
        }
        /// <summary>
        /// Длина мебели
        /// </summary>
        public float Length
        {
            get;
            private set;
        }
        /// <summary>
        /// Двухмерной изображение мебели
        /// </summary>
        public Bitmap Image2D
        {
            get;
            private set;
        }
        /// <summary>
        /// Положение мебели в пространстве
        /// </summary>
        public PointF Position
        {
            get { return position; }
            set
            {
                position = value;
                UpdatePosRad();
                model3d.Position.X = position.X / Sizes.WIDTH2D*Sizes.WIDTH3D;
                model3d.Position.Y = position.Y / Sizes.WIDTH2D * Sizes.WIDTH3D;
            }
        }
        /// <summary>
        /// Угол мебели
        /// </summary>
        public float Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                model3d.Position.AngleAxisZ = angle;
                UpdatePosRad();
            }
        }
        /// <summary>
        /// Изображение для listfurniture
        /// </summary>
        public Bitmap Img
        {
            get
            {
                return img;
            }
        }
        /// <summary>
        /// Под каталог отображаемых элементов
        /// </summary>
        public List<IDrawableList> SubDrawObj
        {
            get;
            set;
        }
        /// <summary>
        /// Название мебели
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        private float RaduisContain
        {
            get
            {
                if (Length > Width)
                    return Length * Sizes.WIDTH2D/2;
                else
                    return Width * Sizes.WIDTH2D / 2;
            }
        }

        /// <summary>
        /// Конструктор класса Furniture
        /// </summary>
        /// <param name="name">Название мебели</param>
        /// <param name="path">Путь к файлу 3D объектом</param>
        /// <param name="pathImg2d">Путь к к изображению для 2-х мерного представления</param>
        /// <param name="pathImg">Путь к картинке для отображение в списке</param>
        /// <param name="width">Ширина мебели</param>
        /// <param name="height">Высота мебели</param>
        /// <param name="length">Длина мебели</param>
        /// <param name="scale">Масштаб</param>
        public Furniture(int id,string name, string path, string pathImg2d,string pathImg,float width,float height,float length,float scale)
        {
            ID = id;
            this.name = name;
            this.scale = scale;
            model3d = new Object3D();
            this.path = path;
            model3d.LoadModelFromPath(path);
            model3d.Scale = scale;
            Image2D = (Bitmap)Image.FromFile(pathImg2d);
            
            img = (Bitmap)Image.FromFile(pathImg);
            
            Width = width;
            Height = height;
            Length = length;
            UpdatePosRad();
            ScaleImgs();
        }
        private Furniture(int id,string name, string path, Bitmap img2d, Bitmap img, float width, float height, float length,float scale)
        {
            ID = id;
            this.name = name;
            model3d = new Object3D();
            this.scale = scale;
            this.path = path;
            model3d.LoadModelFromPath(path);
            model3d.Scale = scale;
            Image2D = img2d;
            this.img = img;
            Width = width;
            Height = height;
            Length = length;
            UpdatePosRad();
        }
        private void ScaleImgs()
        {
            Bitmap b = new Bitmap((int)(Width * Sizes.WIDTH2D), (int)(Height * Sizes.WIDTH2D));
            Graphics g = Graphics.FromImage(b);
            g.DrawImage(Image2D, 0, 0, Width * Sizes.WIDTH2D, Height * Sizes.WIDTH2D);
            Image2D = b;
        }
        /// <summary>
        /// Отрисовка трёхмерного представления мебели
        /// </summary>
        public void Draw3D()
        {
            model3d.DrawModel();
        }
        /// <summary>
        /// Создание копии мебели
        /// </summary>
        /// <returns></returns>
        public Furniture Copy()
        {
            return new Furniture(ID,name, path, Image2D, Img, Width, Height, Length,scale);
        }
        /// <summary>
        /// Проверка на принадлежность точки к мебели
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Contain(int x,int y)
        {

            if (Math.Sqrt(Math.Pow(x - posRadCont.X,2) + Math.Pow(y - posRadCont.Y,2)) < RaduisContain)
                return true;
            return false;

            //if (x > position.X && y > position.Y && x < position.X + Image2D.Width && y < position.Y + Image2D.Height)
            //    return true;
            //else
            //    return false;
        }

        private void UpdatePosRad()
        {
            posRadCont = new PointF((float)(position.X + RaduisContain * Math.Cos((Angle + 45)*Math.PI/180)),(float)( position.Y + RaduisContain * Math.Sin((Angle + 45) * Math.PI / 180)));
        }
    }
}
