using Designer.Acquaintance;
using Designer.Domain.Entity;
using Designer.Presentation.ControlListFur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Designer
{
    public delegate void SetMousePos(int x, int y,bool isLeftButtonPress,bool isRightButtonPress);
    public delegate void SelectedNameChanged(string name,bool isSelected);
    public delegate void NameChanged(string name);
    public delegate void KeyPress(bool isPressed,char key);
    public enum ModeView
    {
        mode3d, mode2d
    }
    /// <summary>
    /// Интерфейс предназначен для отделения слоя Presentation и Controller
    /// </summary>
    public interface IPresenter
    {
        /// <summary>
        /// Событие происходит, когда произведён клик по View
        /// </summary>
        event SetMousePos SurfaceMouseClick;
        /// <summary>
        /// Событие происходит, когда происходит перемещение курсора по View
        /// </summary>
        event SetMousePos SurfaceMouseMove;
        /// <summary>
        /// Собыитие происходит, когда изменяется выбраный элемент 
        /// </summary>
        event SelectedNameChanged ListIndexChanged;
        /// <summary>
        /// Событие происходит, когда происдоит нажатие кнопки на форме
        /// </summary>
        event NameChanged ButtonClick;
        /// <summary>
        /// Событие происходит, когда происходит нажатие клавиши клавиатуры
        /// </summary>
        event KeyPress EventKeyPress;
        /// <summary>
        /// Событие происходит, когда происходит прокрутка колёсика мыши
        /// </summary>
        event MouseEventHandler EvenMouseWheel;
        /// <summary>
        /// Событие происходит, когда пользователь меняет значение отображаемого свойства
        /// </summary>
        event PropertyChanged EventPropertyChanged;
        /// <summary>
        /// Текущее положение камеры
        /// </summary>
        Camera3D Camera3D { get; set; }
        /// <summary>
        /// Текущее положение камеры
        /// </summary>
        Camera2D Camera2D { get;}
        /// <summary>
        /// Отображаемые свойства
        /// </summary>
        List<Property> Properties { set; }
        /// <summary>
        /// Название отображаемых свойств
        /// </summary>
        string NameProperty { set; }
        /// <summary>
        /// Название кнопки
        /// </summary>
        string NameButton { set; }
        /// <summary>
        /// Режим отображения (2D, 3D)
        /// </summary>
        ModeView Mode
        {
            get;
            set;
        }
        /// <summary>
        /// Отображение списка объектов
        /// </summary>
        /// <param name="objs"></param>
        void DrawObjects(List<IDrawableObj> objs);
        /// <summary>
        /// Задание списка выбираемых объектов
        /// </summary>
        /// <param name="drawList"></param>
        void SetDrawList(IDrawableList drawList);
        /// <summary>
        /// Отображение сообщения
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        void ShowMessage(string text);
    }
}
