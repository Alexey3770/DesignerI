using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Designer
{
    public partial class ListFurniture : UserControl
    {
        private const int INTERVALMIDELEM = 10;

        Dictionary<string,Category> furs;
        List<Control> currnetDeleteList;
        Bitmap imgElem;

        public Dictionary<string, Category> Furnitures
        {
            set
            {
                furs = value;
                Update(furs,"Мебель");
            }
        }
        
       
        public ListFurniture()
        {
            InitializeComponent();
            furs = new Dictionary<string, Category>();
            currnetDeleteList = new List<Control>();
            imgElem = new Bitmap(Width - 2 * INTERVALMIDELEM,30);
            using (Graphics gr = Graphics.FromImage(imgElem))
            {
                gr.DrawImage(Properties.Resources.ElemList, 0, 0, imgElem.Width, imgElem.Height);
            }
            
        }
        public ListFurniture(int width,int height)
        {
            InitializeComponent();
            Width = width;
            Height = height;

        }

        private void Update(Dictionary<string, Category> drawList,string nameMainCat)
        {
            Size sizeElem = new Size(Width - 2 * INTERVALMIDELEM, 30);
            Point locationElem = new Point(INTERVALMIDELEM, label1.Location.Y + label1.Height + INTERVALMIDELEM);
            label1.Text = nameMainCat;
            locationElem.Y = label1.Location.Y + label1.Height + INTERVALMIDELEM;
            int color = 0;
            foreach(KeyValuePair<string,Category> cat in drawList)
            {
                ElementFurList l = new ElementFurList(sizeElem.Width,sizeElem.Height,color,cat.Value.Name) { Location = locationElem ,Name = cat.Value.Name };
                Controls.Add(l);
                currnetDeleteList.Add(l);
                l.Click += P_Click;
                locationElem.Y += INTERVALMIDELEM;
            }
        }
        private void Update(Dictionary<string, IDrawableListFur> drawList, string nameMainCat)
        {
            Size sizePicture = new Size((Width - 15) / 2, (Width - 15) / 2);
            Point locationPicture = new Point(INTERVALMIDELEM, label1.Location.Y + label1.Height + INTERVALMIDELEM);
            label1.Text = nameMainCat;
            locationPicture.Y = label1.Location.Y + label1.Height + INTERVALMIDELEM;
            foreach(KeyValuePair<string,IDrawableListFur> dr in drawList)
            {
                Controls.Add(new DrawElemLF(dr.Value) { Size = sizePicture,Location = locationPicture });
                locationPicture.Y += INTERVALMIDELEM;
                if (locationPicture.X == INTERVALMIDELEM)
                    locationPicture.X = Width / 2 + INTERVALMIDELEM / 2;
                else
                    locationPicture.X = INTERVALMIDELEM;

            }
            //foreach (KeyValuePair<string, Category> cat in drawList)
            //{
            //    Controls.Add(new Label() { Text = cat.Value.Name, Location = locationElem, Size = sizeElem, AutoSize = false, TextAlign = ContentAlignment.MiddleCenter });
            //    PictureBox p = new PictureBox() { Name = cat.Value.Name, Location = locationElem, Image = Properties.Resources.ElemList, Size = sizeElem };
            //    p.Click += P_Click;
            //    Controls.Add(p);
            //}
        }
        private void P_Click(object sender, EventArgs e)
        {
            string name = ((Control)sender).Name;
            ClearList();
            if (furs[name].SubCategory != null)
                Update(furs[name].SubCategory, name);
            else
                Update(furs[name].Furnitures, name);
            
        }

        private void ClearList()
        {
            foreach(Control s in currnetDeleteList)
            {
                Controls.Remove(s);
            }
            currnetDeleteList.Clear();
        }
        
    }
}
