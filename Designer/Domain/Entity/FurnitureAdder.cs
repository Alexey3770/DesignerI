using Designer.Acquaintance;
using Designer.Domain.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Designer.Domain.Entity
{
    /// <summary>
    /// Класс представляеет собой модель поведия - добавление мебели
    /// </summary>
    class FurnitureAdder : IModel
    {
        Room planSideRoom;
        FurnitureLib furLib;
        Furniture selectedFur;

        public IDrawableList DrawList
        {
            get { return furLib; } 
            
        }

        public List<IDrawableObj> DrawObj
        {
            get;
            private set;
        }
        public Room Room
        {
            get { return planSideRoom; }
        }

        public string NameButton
        {
            get { return "Удалить"; }
        }

        public string NameProperty
        {
            get { return "Размеры мебели"; }
        }

        public List<Property> Properts
        {
            get;
            private set;
        }

        public Camera3D StartCamera
        {
            get;
            private set;
        }

        public event EventHandler EventChangeModel;
        public event EventEmpty EventUpdateModel;
        public event EventEmpty EventChangeProperty;

        public FurnitureAdder(Room planSideRoom,FurnitureLib furLib)
        {
            this.planSideRoom = planSideRoom;
            this.furLib = furLib;
            DrawObj = new List<IDrawableObj>();
            DrawObj.Add(planSideRoom);
            InitProperty();
        }

        private void InitProperty()
        {
            Properts = new List<Property>();
            Properts.Add(new Property("Длина (м)","", true));
            Properts.Add(new Property("Ширина (м)","", true));
            Properts.Add(new Property("Высота (м)","", true));
        }

        public void ChangeProperty(Property prop)
        {
        }

        public void SelectElement(string namme, bool isSelected)
        {
            if (isSelected)
            {
                DrawObj.Remove(selectedFur);
                Furniture f = furLib.GetFurniture(namme);
                selectedFur = f;
                SetProperty(f);
                selectedFur.Position = planSideRoom.PositionAdd;
                DrawObj.Add(selectedFur);
                
            }
            else
            {
                DrawObj.Remove(selectedFur);
                selectedFur = null;
                ClearProperty();
            }
            EventUpdateModel?.Invoke();
        }
        /// <summary>
        /// Установка отображемых свойств
        /// </summary>
        /// <param name="f"></param>
        private void SetProperty(Furniture f)
        {
            Properts[0].Value = Convert.ToString(f.Length);
            Properts[1].Value = Convert.ToString(f.Width);
            Properts[2].Value = Convert.ToString(f.Height);
            EventChangeProperty?.Invoke();
        }
        /// <summary>
        /// Обнуление отображаемых свойств
        /// </summary>
        private void ClearProperty()
        {
            Properts[0].Value = "";
            Properts[1].Value = "";
            Properts[2].Value = "";
            EventChangeProperty?.Invoke();
        }
        public void Move(Direct direct)
        {
            throw new NotImplementedException();
        }

        public void ButtonClick()
        {
            if (selectedFur != null)
            {
                DrawObj.Remove(selectedFur);
                selectedFur = null;
                ClearProperty();
                EventUpdateModel?.Invoke();
            }
        }
        bool isMoving = false;
        public void SurfaceMouseClick(int x, int y)
        {
            if (selectedFur != null)
            {
                planSideRoom.AddFurniture(selectedFur);
                DrawObj.Remove(selectedFur);
                if (!isMoving)
                {
                    selectedFur = selectedFur.Copy();

                    DrawObj.Add(selectedFur);
                }
                else
                {
                    isMoving = false;
                    selectedFur = null;
                    ClearProperty();
                }
            }
            else
            {
                //foreach(IDrawableObj d in DrawObj)                
                //    if(d is Furniture)                    
                //        if(((Furniture)d).Contain(x,y))
                //        {
                //            selectedFur = (Furniture)d;
                //            SetProperty((Furniture)d);
                //            isMoving = true;
                //            break;
                //        }
                    
                
                selectedFur = planSideRoom.GetObject(x, y);
                DrawObj.Add(selectedFur);
                if (selectedFur != null)
                {
                    SetProperty(selectedFur);
                    isMoving = true;
                }

            }
            EventUpdateModel?.Invoke();
        }

        public void MoveElement(int x, int y)
        {
            if (selectedFur != null)
            {
                selectedFur.Position = new System.Drawing.PointF(x , y );
                EventUpdateModel?.Invoke();
            }
        }

        public void RotateElement(int x)
        {
            selectedFur.Angle += x;
            EventUpdateModel?.Invoke();
        }
    }
}
