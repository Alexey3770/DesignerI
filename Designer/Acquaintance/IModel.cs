using Designer.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Designer.Acquaintance
{
    public delegate void EventEmpty();
    public enum Direct
    {
        front,back,left,right,up,down
    }
    /// <summary>
    /// Интерфейс прденазначен для отделения слоя Model и Controller
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Событие происходит, когда данные в модели изменились
        /// </summary>
        event EventEmpty EventUpdateModel;
        /// <summary>
        /// Событие происходит, когда необходимо заменить модель
        /// </summary>
        event EventHandler EventChangeModel;
        /// <summary>
        /// Событие происходит, когда изменились отображаемые свойства
        /// </summary>
        event EventEmpty EventChangeProperty;
        /// <summary>
        /// Список отображаемых элементов
        /// </summary>
        List<IDrawableObj> DrawObj { get; }
        /// <summary>
        /// Список выбираемых элементов
        /// </summary>
        IDrawableList DrawList { get; }
        /// <summary>
        /// Начальное положение камеры
        /// </summary>
        Camera3D StartCamera { get; }
        /// <summary>
        /// Список с отображемыми свойствами
        /// </summary>
        List<Property> Properts { get; }
        /// <summary>
        /// Название списка со свойствами
        /// </summary>
        string NameProperty { get; }
        /// <summary>
        /// Название кнопки
        /// </summary>
        string NameButton { get; }
        /// <summary>
        /// Планировка комнаты
        /// </summary>
        Room Room { get; }
        /// <summary>
        /// Нажатие клавишей мыши
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void SurfaceMouseClick(int x, int y);
        /// <summary>
        /// Перемещение выбранного элемента
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void MoveElement(int x, int y);
        /// <summary>
        /// Поворот выбранного элемента
        /// </summary>
        /// <param name="x"></param>
        void RotateElement(int x);
        /// <summary>
        /// Изменение выбранного элемента
        /// </summary>
        /// <param name="namme">Имя нового элемента</param>
        void SelectElement(string namme,bool isSelected);
        
        /// <summary>
        /// Изменение отображаемых свойств
        /// </summary>
        /// <param name="prop"></param>
        void ChangeProperty(Property prop);
        /// <summary>
        /// Нажатие кнопки
        /// </summary>
        void ButtonClick();
    }
}
