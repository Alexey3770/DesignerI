using Designer.Acquaintance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Designer.Foundation;
using System.Data;
using Designer.Domain.Entity;

namespace Designer.Domain.Mediator
{
    /// <summary>
    /// Класс содержит информацию полученную из бд о мебели
    /// </summary>
    class FurnitureLib : IDrawableList
    {
        const string nameTableFurniture = "Furniture";
        const string nameTableCategory = "Category";

        DBAccess dbAccess;
        List<Furniture> furs;

        public Bitmap Img
        {
            get;
            private set;
        }

        public string Name
        {
            get
            {
                return "Мебель";
            }
        }

        public List<IDrawableList> SubDrawObj
        {
            get;
            private set;
        }

        public FurnitureLib()
        {
            dbAccess = new DBAccess();
            SubDrawObj = new List<IDrawableList>();
            furs = new List<Furniture>();
            LoadData();
        }
        /// <summary>
        /// Загрузка данных из БД
        /// </summary>
        private void LoadData()
        {
            DataTable tableFur = dbAccess.GetData(nameTableFurniture);
            DataTable tableCat = dbAccess.GetData(nameTableCategory);
            for(int i =0;i < tableCat.Rows.Count; i++)
            {
                List<IDrawableList> drlist = new List<IDrawableList>();
                for(int g = 0;g < tableFur.Rows.Count; g++)
                {
                    if ((int)tableFur.Rows[g][8] == (int)tableCat.Rows[i][0])
                    {
                        Furniture f = new Furniture((int)tableFur.Rows[g][0],(string)tableFur.Rows[g][1], (string)tableFur.Rows[g][2], (string)tableFur.Rows[g][3], (string)tableFur.Rows[g][4], (float)tableFur.Rows[g][5], (float)tableFur.Rows[g][6], (float)tableFur.Rows[g][7], (float)tableFur.Rows[g][9]);
                        drlist.Add(f);
                        furs.Add(f);
                    }
                }
                SubDrawObj.Add(new DrawableListElement(null, (string)tableCat.Rows[i][1],drlist));
            }
        }

        /// <summary>
        /// Получение объекта мебель
        /// </summary>
        /// <param name="name">Название мебели</param>
        /// <returns></returns>
        public Furniture GetFurniture(string name)
        {
            foreach (Furniture f in furs)
                if (name == f.Name)
                    return f.Copy();
            return null;
        }

        /// <summary>
        /// Получение пути к файлу с 3д моделью
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetPath(string name)
        {
            return "";
        }
    }
}
