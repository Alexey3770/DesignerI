using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;

namespace Designer.Domain.Entity
{
    /// <summary>
    /// Класс хранит информацию о комнате
    /// </summary>
    public class Room :IDrawableObj
    {
        const float sideWidth = 1, sideLength = 0.05f, sideHeight = 3;
        const string baseSideTexture = "Textures//baseside.jpg";
        const string baseFloorTexture = "Textures//basefloor.jpg";     

        static string[] typesPlanRoom = new string[] { "Прямоуголная"};
        Object3D side, floor;
        string currentType;
        int width, length;
        /// <summary>
        /// Тип планировки
        /// </summary>
        public static string[] TypesPlanRoom
        {
            get { return typesPlanRoom; }
        }
        /// <summary>
        /// Название планировки
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Ширина комнаты
        /// </summary>
        public int Width
        {
            get { return width; }
            set { width = value; InitImg2D(); }
        }
        /// <summary>
        /// Длина комнаты
        /// </summary>
        public int Length
        {
            get { return length; }
            set { length = value; InitImg2D(); }
        }
        /// <summary>
        /// Название текстуры
        /// </summary>
        public string TextureName
        {
            get;
            set;
        }
        /// <summary>
        /// Изображение для двухмерного представления комнаты
        /// </summary>
        public Bitmap Image2D
        {
            get;
            private set;
        }
        /// <summary>
        /// Положение комнаты
        /// </summary>
        public PointF Position
        {
            get;
            set;
        }
        /// <summary>
        /// Стартовая позиция для добавления мебели
        /// </summary>
        public PointF PositionAdd
        {
            get { return new PointF(Position.X + Sizes.HEIGHT2D, Position.Y + Sizes.HEIGHT2D); }
        }
        /// <summary>
        /// Угол повората комнаты
        /// </summary>
        public float Angle
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// Мебель, содержащаяся в комнате
        /// </summary>
        public List<Furniture> Furnitures
        {
            get;
            private set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Type
        {
            get { return "Square"; }
        }
        /// <summary>
        /// Путь к файлу с текстурой для стены
        /// </summary>
        public string PathTextSide
        {
            get;
            private set;
        }
        /// <summary>
        /// Путь к файлу с текстурой для пола
        /// </summary>
        public string PathTextFloor
        {
            get;
            private set;
        }

        /// <summary>
        /// Конструктор класса Room
        /// </summary>
        /// <param name="type">Тип планировки</param>
        /// <param name="width">Ширина комнаты</param>
        /// <param name="length">Длина комнаты</param>
        public Room(string type,int width,int length)
        {
            LoadSide();
            //var s = side.Size;
            this.width = width;
            this.length = length;
            if(type == "Square")
                currentType = "Прямоуголная";
            currentType = type;
            Furnitures = new List<Furniture>();
            InitImg2D();
        }
        /// <summary>
        /// Отображение трёхмерного представления комнаты
        /// </summary>
        public void Draw3D()
        {
            for (int w = 0; w < width; w++)
            {
                side.Position = new Point3D(w * sideWidth, 0, 0, 0, 0, 0);
                side.DrawModel();
                side.Position = new Point3D(w * sideWidth + sideWidth, sideWidth * length, 0, 0, 0, 180);
                side.DrawModel();
            }
            for (int l = 0; l < length; l++)
            {
                side.Position = new Point3D(0, l * sideWidth + sideWidth, 0, 0, 0, -90);
                side.DrawModel();
                side.Position = new Point3D(width * sideWidth, l * sideWidth, 0, 0, 0, 90);
                side.DrawModel();
            }
            for (int x = 0; x < width; x++)
                for (int y = 0; y < length; y++)
                {
                    floor.Position = new Point3D(x * sideWidth, y * sideWidth, -sideLength, 0, 0, 0);
                    floor.DrawModel();
                }
            
                foreach (Furniture f in Furnitures)
                    f.Draw3D();
        }
        /// <summary>
        /// Отображение тдвухмерного представления комнаты
        /// </summary>
        /// <param name="g"></param>
        public void Draw2D(Graphics g)
        {
            g.FillRectangle(Brushes.Brown, 0, 0,  width * Sizes.WIDTH2D, sideHeight);
            g.FillRectangle(Brushes.Brown, 0, length * Sizes.WIDTH2D,  width * Sizes.WIDTH2D, sideHeight);

            g.FillRectangle(Brushes.Brown, 0, 0, sideHeight,  length * Sizes.WIDTH2D);
            g.FillRectangle(Brushes.Brown, width * Sizes.WIDTH2D, 0, sideHeight , length * Sizes.WIDTH2D);
        }
        /// <summary>
        /// Изменение размеров комнаты
        /// </summary>
        /// <param name="width">Ширина комнаты</param>
        /// <param name="length">Длина комнаты</param>
        public void ChangeSize(int width, int length)
        {
            this.width = width;
            this.length = length;
            InitImg2D();
        }
        /// <summary>
        /// Добавление текстуры
        /// </summary>
        /// <param name="path">Путь к текстуре</param>
        /// <param name="nameType">Тип текстуры (для стен - side, для пола - floor)</param>
        public void AddTexture(string path, string nameType)
        {
            if (nameType == "side")
            {
                side.AddTexture(path);
                PathTextSide = path;
            }
            else if (nameType == "floor")
            {
                floor.AddTexture(path);
                PathTextFloor = path;
            }
        }
        /// <summary>
        /// Установк базовой текстуры
        /// </summary>
        /// <param name="nameType">Тип текстуры (для стен - side, для пола - floor)</param>
        public void SetBaseTexture(string nameType)
        {
            if (nameType == "side")
            {
                side.AddTexture(baseSideTexture);
                PathTextSide = baseSideTexture;
            }
            else if(nameType == "floor")
            {
                floor.AddTexture(baseFloorTexture);
                PathTextFloor = baseFloorTexture;
            }
            
        }
        /// <summary>
        /// Добавление мебели
        /// </summary>
        /// <param name="fur"></param>
        public void AddFurniture(Furniture fur)
        {
            Furnitures.Add(fur);
            InitImg2D();
        }
        /// <summary>
        /// Поулчение мебели, которая содержит точку
        /// </summary>
        /// <param name="x">Координата X точки</param>
        /// <param name="y">Координата Y точки</param>
        /// <returns></returns>
        public Furniture GetObject(int x, int y)
        {
            foreach(Furniture d in Furnitures)
            {
                if (d.Contain(x, y))
                {
                    Furnitures.Remove(d);
                    return d;
                }
            }
            return null;
        }
        /// <summary>
        /// Загрузка 3d модели стен
        /// </summary>
        private void LoadSide()
        {
            side = new Object3D();
            floor = new Object3D();
            StreamReader sw = File.OpenText("Models//side.ase");
            side.LoadModel(sw.ReadToEnd());
            sw.Close();

            sw = File.OpenText("Models//floor.ase");
            floor.LoadModel(sw.ReadToEnd());
            SetBaseTexture();
            sw.Close();
        }
        /// <summary>
        /// Установка базовых текстур
        /// </summary>
        private void SetBaseTexture()
        {
            side.AddTexture(baseSideTexture);
            floor.AddTexture(baseFloorTexture);
        }
        private void InitImg2D()
        {
            Image2D = new Bitmap((int)(Sizes.WIDTH2D * width), (int)(Sizes.WIDTH2D * length));
            Graphics g = Graphics.FromImage(Image2D);
            g.FillRectangle(Brushes.Brown, 0, 0, width * Sizes.WIDTH2D, Sizes.HEIGHT2D);
            g.FillRectangle(Brushes.Brown, 0, length * Sizes.WIDTH2D - Sizes.HEIGHT2D, width * Sizes.WIDTH2D, Sizes.HEIGHT2D);

            g.FillRectangle(Brushes.Brown, 0, 0, Sizes.HEIGHT2D, length * Sizes.WIDTH2D);
            g.FillRectangle(Brushes.Brown, width * Sizes.WIDTH2D - Sizes.HEIGHT2D, 0, Sizes.HEIGHT2D, length * Sizes.WIDTH2D);
            Position = new PointF(0,0);
            Matrix m = new Matrix();

            foreach (Furniture f in Furnitures)
            {
                m.RotateAt(f.Angle, f.Position);
                g.Transform = m;
                g.DrawImage(f.Image2D, f.Position);
                m.RotateAt(-f.Angle, f.Position);
            }
        }
    }
}
