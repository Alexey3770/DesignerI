using Designer.Acquaintance;
using Designer.Foundation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using Designer.Domain.Entity;

namespace Designer.Domain.Mediator
{
    /// <summary>
    /// Класс содержит информацию о текстурах для стен полученную из бд
    /// </summary>
    public class TextureLibSide : IDrawableList
    {
        const string nameTableSide = "SideCover";
        const string nameList = "Отделочное покрытие стен";

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
        /// <summary>
        /// Конструктор класса TextureLibSide
        /// </summary>
        public TextureLibSide()
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
            DataTable tSide = dbAccess.GetData(nameTableSide);
            for (int i = 0; i < tSide.Rows.Count; i++)
            {
                SubDrawObj.Add(new DLEWithPath(Convert.ToInt32(tSide.Rows[i][0]),(string)tSide.Rows[i][1], (string)tSide.Rows[i][2],null));
            }

        }
        /// <summary>
        /// Получение пути к текстуре
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetPath(string name)
        {
            foreach(IDrawableList dr in SubDrawObj)
                if(dr.Name == name)
                    return ((DLEWithPath)dr).Path;
            return "";
        }

        
    }
}
