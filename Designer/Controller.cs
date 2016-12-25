using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Designer.Acquaintance;
using Designer.Domain.Entity;
using Designer.Domain.Mediator;
using Designer.Presentation;

namespace Designer
{
    /// <summary>
    /// Контроллер
    /// </summary>
    public class Controller
    {
        IModel model;
        IPresenter presenter;
        ModelChanger modelChanger;
        Camera3D camera3d;
        Camera2D camera2d;
        SaveData saveData;
        /// <summary>
        /// Конструктор класса Controller
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="presenter"></param>
        public Controller(int width, int height,IPresenter presenter)
        {
            modelChanger = new ModelChanger();
            camera2d = new Camera2D();
            saveData = new SaveData();

            this.presenter = presenter;
            presenter.SurfaceMouseClick += View_MouseClick;
            presenter.SurfaceMouseMove += View_MouseMove;
            presenter.ListIndexChanged += List_SelectedIndexChanged;
            presenter.ButtonClick += View_ButtonClick;
            presenter.EventKeyPress += View_EventKeyPress;
            presenter.EvenMouseWheel += View_EvenMouseWheel;
            presenter.EventPropertyChanged += Presenter_EventPropertyChanged;

            model = modelChanger.GetStartModel(width, height);
            model.EventChangeModel += Model_ChangeModel;
            model.EventUpdateModel += Model_EventUpdateModel;

            presenter.Properties = model.Properts;
            presenter.NameProperty = model.NameProperty;
            presenter.NameButton = model.NameButton;

            presenter.SetDrawList(model.DrawList);
            camera3d = model.StartCamera;
            presenter.Camera3D = camera3d;
        }
        
        private void Presenter_EventPropertyChanged(string nameProp, string value)
        {
            model.ChangeProperty(new Property(nameProp, value));
        }
        
        private void View_EvenMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (camera3d.Position.Z > 3)
                    camera3d.Move(0, 0, -2);
                camera2d.Scale -= 0.1f;
            }
            else
            {
                if (camera3d.Position.Z <= 5)
                    camera3d.Move(0, 0, 2);
                camera2d.Scale += 0.1f;
            }
            presenter.Camera3D = camera3d;
        }

        private void View_EventKeyPress(bool isPressed,char key)
        {
            switch (key)
            {
                case 'w': case 'W': case 'Ц': case 'ц': camera3d.Move(2, 0,0);  break;
                case 's': case 'S': case 'ы': case 'Ы': camera3d.Move(-2, 0,0); break;
                case 'a': case 'A': case 'ф': case 'Ф': camera3d.Move(0, 2,0); break;
                case 'd': case 'D': case 'в': case 'В': camera3d.Move(0, -2,0); break;
            }
            presenter.Camera3D = camera3d;
        }

        private void Model_EventUpdateModel()
        {
            //presenter.Camera3D = model.StartCamera;
            presenter.DrawObjects(model.DrawObj);
        }

        private void View_ButtonClick(string name)
        {
            switch (name)
            {
                case "btn_3d": presenter.Mode = ModeView.mode3d;  break;
                case "btn_2d": presenter.Mode = ModeView.mode2d; break;
                case "listbut": model.ButtonClick(); break;
                case "load":
                    LoadForm l = new LoadForm(saveData.GetRooms());
                    if(l.ShowDialog() == DialogResult.OK)
                    {
                        model = modelChanger.GetModel("FurnitureAdder",saveData.LoadRoom(l.SelectedRoom));
                        model.EventChangeModel += Model_ChangeModel;
                        model.EventUpdateModel += Model_EventUpdateModel;
                        presenter.Properties = model.Properts;
                        presenter.NameButton = model.NameButton;
                        presenter.SetDrawList(model.DrawList);
                        presenter.DrawObjects(model.DrawObj);
                    }
                    break;
                case "save":
                    SaveForm s = new SaveForm();
                    if (s.ShowDialog() == DialogResult.OK)
                    {
                        if (saveData.SaveRoom(model.Room,s.RoomName))
                            presenter.ShowMessage("Комната успешно сохранена");
                        else
                            presenter.ShowMessage("Ошибка записи");
                    }

                    break;
                case "cover":
                    model = modelChanger.GetModel("TextureSelector", model.Room);
                    model.EventChangeModel += Model_ChangeModel;
                    model.EventUpdateModel += Model_EventUpdateModel;
                    presenter.Properties = model.Properts;
                    presenter.NameButton = model.NameButton;
                    presenter.SetDrawList(model.DrawList);
                    presenter.DrawObjects(model.DrawObj); break;
                case "size":
                    model = modelChanger.GetModel("RoomCreater", model.Room);
                    model.EventChangeModel += Model_ChangeModel;
                    model.EventUpdateModel += Model_EventUpdateModel;
                    presenter.Properties = model.Properts;
                    presenter.NameButton = model.NameButton;
                    presenter.SetDrawList(model.DrawList);
                    presenter.DrawObjects(model.DrawObj); break;
            }
        }

        private void Model_ChangeModel(object sender, EventArgs e)
        {
            model = modelChanger.ChangeModel(model);
            model.EventChangeModel += Model_ChangeModel;
            model.EventUpdateModel += Model_EventUpdateModel;
            model.EventChangeProperty += Model_EventChangeProperty;
            presenter.Properties = model.Properts;
            presenter.NameButton = model.NameButton;
            presenter.SetDrawList(model.DrawList);
            presenter.DrawObjects(model.DrawObj);
        }

        private void Model_EventChangeProperty()
        {
            presenter.Properties = model.Properts;
        }

        public void View_MouseClick(int x, int y,bool isLeftButtonPress,bool isRightButtonPress)
        {
            if(presenter.Mode == ModeView.mode2d && isLeftButtonPress)
                model.SurfaceMouseClick((int)((x - presenter.Camera2D.PositionCamera.X) / presenter.Camera2D.Scale), (int)((y - presenter.Camera2D.PositionCamera.Y) / presenter.Camera2D.Scale));
        }

        public void List_SelectedIndexChanged(string name, bool isSelected)
        {
            model.SelectElement(name,isSelected);
        }

        Point? oldMousePoint;
        public void View_MouseMove(int x, int y, bool isLeftButtonPress, bool isRightButtonPress)
        {
            if(presenter.Mode == ModeView.mode2d && !isRightButtonPress)
            {
                model.MoveElement((int)((x - presenter.Camera2D.PositionCamera.X)/presenter.Camera2D.Scale), (int)((y - presenter.Camera2D.PositionCamera.Y) / presenter.Camera2D.Scale));
            }
            if (isLeftButtonPress)
            {
                if (oldMousePoint == null)
                    oldMousePoint = new Point(x, y);
                if (presenter.Mode == ModeView.mode3d)
                    camera3d.Rotate(((float)(x - oldMousePoint.Value.X)) / 4, ((float)(y - oldMousePoint.Value.Y)) / 4);
                else
                    camera2d.MoveCamera(x - oldMousePoint.Value.X, y - oldMousePoint.Value.Y);
                oldMousePoint = new Point(x, y);
            }
            else if (isRightButtonPress)
            {
                if (presenter.Mode == ModeView.mode2d)
                {
                    model.RotateElement((int)((x - oldMousePoint.Value.X)));
                }
                if (oldMousePoint == null)
                    oldMousePoint = new Point(x, y);
                if (presenter.Mode == ModeView.mode3d)
                    camera3d.Move(((float)(y - oldMousePoint.Value.Y))/30, ((float)(x - oldMousePoint.Value.X))/30, 0);
                else
                    camera2d.MoveCamera(x - oldMousePoint.Value.X, y - oldMousePoint.Value.Y);
                oldMousePoint = new Point(x, y);
            }
            else
                oldMousePoint = new Point(x, y);
            presenter.Camera3D = camera3d;
        }
    }
}
