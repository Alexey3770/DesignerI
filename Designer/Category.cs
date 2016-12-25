using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Designer
{
    public class Category
    {
        private Dictionary<string, Category> subCategory;
        private Dictionary<string, IDrawableListFur> furs;
        private string name;

        public string Name
        {
            get { return name; }
        }
        public Dictionary<string, Category> SubCategory
        {
            get { return subCategory; }
        }
        public Dictionary<string, IDrawableListFur> Furnitures
        {
            get { return furs; }
        }

        public Category(string name,Dictionary<string,Category> subCat, Dictionary<string,IDrawableListFur> furs)
        {
            subCategory = subCat;
            this.furs = furs;
            this.name = name;
        }

    }
}
