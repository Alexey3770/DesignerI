using Designer.Acquaintance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Designer.Domain.Entity
{
    /// <summary>
    /// Класс используется для отображения в ListView
    /// </summary>
    class DLEWithPath : IDrawableList
    {
        public Bitmap Img
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public List<IDrawableList> SubDrawObj
        {
            get;
            private set;
        }
        /// <summary>
        /// ИД объекта
        /// </summary>
        public int ID
        {
            get;
            private set;
        }
        /// <summary>
        /// Путь к данным
        /// </summary>
        public string Path
        {
            get;
            private set;
        }
        /// <summary>
        /// Конструктор класс DLEWithPath с параметрами
        /// </summary>
        /// <param name="id">ид</param>
        /// <param name="name">Название</param>
        /// <param name="path">Путь к файлу</param>
        /// <param name="subDrawObj">Подсписок</param>
        public DLEWithPath(int id,string name,string path, List<IDrawableList> subDrawObj)
        {
            Img = (Bitmap)Image.FromFile(path);
            Name = name;
            SubDrawObj = subDrawObj;
            ID = id;
            Path = path;
        }
    }
}
