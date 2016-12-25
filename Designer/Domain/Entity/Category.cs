using Designer.Acquaintance;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Designer.Domain.Entity
{
    /// <summary>
    /// Класс предназначен для хранения информации о категории
    /// </summary>
    public class Category :IDrawableList
    {
        List<Category> subCategory;
        List<IDrawableList> furs;
        List<IDrawableList> subDrawObj;
        string name;
        int id;

        /// <summary>
        /// Название категории
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Подкатегории
        /// </summary>
        public List<Category> SubCategory
        {
            get { return subCategory; }
        }
        /// <summary>
        /// Мебель
        /// </summary>
        public List<IDrawableList> Furnitures
        {
            get { return furs; }
        }
        public Bitmap Img
        {
            get
            {
                return null;
            }
        }
        /// <summary>
        /// Объекты для отображение
        /// </summary>
        public List<IDrawableList> SubDrawObj
        {
            get
            {
                return subDrawObj;
            }
        }
        /// <summary>
        /// Конструтор класса Category с пармаетрами
        /// </summary>
        /// <param name="name">Название категории</param>
        /// <param name="subCat">Подкатегории</param>
        /// <param name="furs">Список отображемых элементов</param>
        public Category(string name,List<Category> subCat, List<IDrawableList> furs)
        {
            
            if (subCat != null)
            {
                subDrawObj = new List<IDrawableList>();
                subCategory = subCat;
                foreach (Category cat in subCat)
                    subDrawObj.Add(cat);

            }
            else
                subDrawObj = this.furs = furs;
            this.name = name;
        }

    }
}
