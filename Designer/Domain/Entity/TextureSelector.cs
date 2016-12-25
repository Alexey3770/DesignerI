using Designer.Acquaintance;
using Designer.Domain.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Designer.Domain.Entity
{
    /// <summary>
    /// Класс представляет собой модель выбора покрытия для поверхностей
    /// </summary>
    class TextureSelector : IModel
    {
        Room planSideRoom;

        TextureLibFloor textureLibFloor;
        TextureLibSide textureLibSide;

        public Room Room
        {
            get { return planSideRoom; }
        }
        public IDrawableList DrawList
        {
            get;
            private set;
        }
        public Room PlanSideRoom
        {
            get { return planSideRoom; }
        }
        public List<IDrawableObj> DrawObj
        {
            get;
            private set;
        }

        public string NameButton
        {
            get { return "Продолжить"; }
        }

        public string NameProperty
        {
            get
            {
                return "";
            }
        }

        public List<Property> Properts
        {
            get
            {
                return null;
            }
        }

        public Camera3D StartCamera
        {
            get;
            private set;
        }

        public event EventHandler EventChangeModel;
        public event EventEmpty EventUpdateModel;
        public event EventEmpty EventChangeProperty;

        public TextureSelector(Room planSideRoom,TextureLibFloor tLF,TextureLibSide tLS)
        {
            this.planSideRoom = planSideRoom;
            DrawObj = new List<IDrawableObj>();
            textureLibFloor = tLF;
            textureLibSide = tLS;
            DrawList = new DrawableListElement(null,"Выбор покрытия",new List<IDrawableList>(new IDrawableList[] { textureLibFloor,textureLibSide }));
            DrawObj.Add(planSideRoom);

            
            
        }


        public void ChangeProperty(Property prop)
        {
        }

        public void SelectElement(string namme,bool isSelected)
        {
            string s = textureLibSide.GetPath(namme);
            if (s != "")
            {
                if (isSelected)
                    planSideRoom.AddTexture(s, "side");
                else
                    planSideRoom.SetBaseTexture("side");
            }
            else
            {
                s = textureLibFloor.GetPath(namme);
                if (isSelected)
                    planSideRoom.AddTexture(s, "floor");
                else
                    planSideRoom.SetBaseTexture("floor");
            }
            EventUpdateModel?.Invoke();
        }

        public void Move(Direct direct)
        {
        }

        public void ButtonClick()
        {
            EventChangeModel?.Invoke(this, null);
        }
        

        public void SurfaceMouseClick(int x, int y)
        {
        }

        public void MoveElement(int x, int y)
        {
        }

        public void RotateElement(int x)
        {
        }
    }
}
