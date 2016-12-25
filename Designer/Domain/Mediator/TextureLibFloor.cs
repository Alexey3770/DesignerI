using Designer.Acquaintance;
using Designer.Domain.Entity;
using Designer.Foundation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Designer.Domain.Mediator
{
    /// <summary>
    /// Класс содержит информацию о текстурах для пола полученную из бд
    /// </summary>
    class TextureLibFloor:IDrawableList
    {
        const string nameTableFloor = "FloorCover";
        const string nameList = "Отделочное покрытие пола";

        DBAccess dbAccess;

        public List<string> TexturesForSide
        {
            get;
            private set;
        }

        public Bitmap Img
        {
            get;
            private set;
        }
        public string Name
        {
            get { return nameList; }
        }
        public List<IDrawableList> SubDrawObj
        {
            get;
            private set;
        }

        public TextureLibFloor()
        {
            SubDrawObj = new List<IDrawableList>();
            TexturesForSide = new List<string>();
            dbAccess = new DBAccess();
            LoadTextures();
        }
        /// <summary>
        /// Загрузка текстур из БД
        /// </summary>
        private void LoadTextures()
        {
            DataTable tSide = dbAccess.GetData(nameTableFloor);
            for (int i = 0; i < tSide.Rows.Count; i++)
            {

                SubDrawObj.Add(new DLEWithPath(Convert.ToInt32(tSide.Rows[i][0]), (string)tSide.Rows[i][1], (string)tSide.Rows[i][2], null));
            }

        }
        /// <summary>
        /// Получение пути к текстуре
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetPath(string name)
        {
            foreach (IDrawableList dr in SubDrawObj)
                if (dr.Name == name)
                    return ((DLEWithPath)dr).Path;
            return "";
        }
    }
}
