using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Designer.Domain.Entity;
using Designer.Acquaintance;
using Designer.Domain.Mediator;

namespace Designer
{
    /// <summary>
    /// Класс представлеят собой модель создания комнаты
    /// </summary>
    class RoomCreater : IModel
    {
        public event EventHandler EventChangeModel;
        public event EventEmpty EventUpdateModel;
        public event EventEmpty EventChangeProperty;
        #region Properties
        /// <summary>
        /// Список выбираемых элементов
        /// </summary>
        public IDrawableList DrawList
        {
            get { return currentDrawList; }
        }

        public Room Room
        {
            get { return planSideRoom; }
        }

        /// <summary>
        /// Список отображаемых элементов
        /// </summary>
        public List<IDrawableObj> DrawObj
        {
            get { return drawObj; }
        }

        public Camera3D StartCamera
        {
            get
            {
                return camera;
            }
        }

        public List<Property> Properts
        {
            get { return props; }
        }

        public string NameProperty
        {
            get
            {
                return "Размеры";
            }
        }

        public string NameButton
        {
            get
            {
                return "Продолжить";
            }
        }

        public Room PlanSideRoom
        {
            get { return planSideRoom; }
        }
        #endregion
        #region Private Field
        int furwidth = 2, furlength = 2;
        List<IDrawableObj> drawObj;
        IDrawableList currentDrawList;
        Bitmap b = new Bitmap(20, 20);
        Camera3D camera;
        Point? oldMousePoint;
        List<Property> props;
        Room planSideRoom;
        #endregion

        public RoomCreater()
        {
            InitDrawList();
            InitPropetyList();
            drawObj = new List<IDrawableObj>();
            camera = new Camera3D(new Point3D(1,1, 1));
            
            Graphics g = Graphics.FromImage(b);
            g.DrawImage(Properties.Resources.Round, 0, 0, 20, 20);
        }
        public RoomCreater(Room room)
        {
            drawObj = new List<IDrawableObj>();
            planSideRoom = room;
            DrawObj.Add(room);
            furwidth = planSideRoom.Width;
            furlength = planSideRoom.Length;
            InitDrawList();
            InitPropetyList();
            
            camera = new Camera3D(new Point3D(0, 0, 3));
            
            Graphics g = Graphics.FromImage(b);
            g.DrawImage(Properties.Resources.Round, 0, 0, 20, 20);
        }
        /// <summary>
        /// Инициализация списка выбираемых элементов
        /// </summary>
        private void InitDrawList()
        {
            List<IDrawableList> subList = new List<IDrawableList>();

            foreach (string s in Room.TypesPlanRoom)
            {
                subList.Add(new DrawableListElement(Properties.Resources.buthome, s, null));
            }
            currentDrawList = new DrawableListElement(null, "Планировка", subList);
        }
        /// <summary>
        /// Инициализация свойств
        /// </summary>
        private void InitPropetyList()
        {
            props = new List<Property>();
            props.Add(new Property("Длина", Convert.ToString(furwidth),false));
            props.Add(new Property("Ширина", Convert.ToString(furlength), false));
            
        }
        public void ChangeProperty(Property prop)
        {
            switch (prop.Name)
            {
                case "Длина":  furlength = Convert.ToInt32(prop.Value); break;
                case "Ширина": furwidth = Convert.ToInt32(prop.Value); break;
            }
            if (planSideRoom != null)
            {
                planSideRoom.Length = furlength;
                planSideRoom.Width = furwidth;
                EventUpdateModel?.Invoke();
            }
        }
        public void SurfaceMouseClick(int x, int y)
        {

        }
        public void RotateCamera(int x, int y)
        {
            if (oldMousePoint == null)
                oldMousePoint = new Point(x, y);
            camera.Rotate((x - oldMousePoint.Value.X) / 5, (y - oldMousePoint.Value.Y) / 5);
            oldMousePoint = new Point(x, y);
        }

        public void SelectElement(string name, bool isSelected)
        {
            if (!isSelected)
                drawObj.Clear();
            if (Room.TypesPlanRoom.Contains(name))
            {
                planSideRoom = new Room(name,furwidth,furlength);
                drawObj.Add(planSideRoom);
            }
            EventUpdateModel?.Invoke();
        }

        public void ChangeCamera(string type)
        {
            
            EventUpdateModel?.Invoke();
            switch (type)
            {
                case "3d":
                    camera.UpVector.X = 0; camera.UpVector.Y = 0; camera.UpVector.Z = 1;
                    camera.Position.Y = 300;
                    camera.Position.Z = 300;
                    break;
                case "2d":
                    camera.Position.X = 0;
                    camera.Position.Y = 0;
                    camera.Position.Z = 400;
                    camera.Target.X = camera.Target.Y = camera.Target.Z = 0;
                    camera.UpVector.X = 0; camera.UpVector.Y = 1; camera.UpVector.Z = 0;
                    break;
                default: throw new Exception();
            }
        }

        public void Move(Direct direct)
        {
            EventUpdateModel?.Invoke();
        }

        public void ButtonClick()
        {
            EventChangeModel?.Invoke(this, null);
        }

        public void MoveElement(int x, int y)
        {
        }

        public void RotateElement(int x)
        {
        }
    }
}
