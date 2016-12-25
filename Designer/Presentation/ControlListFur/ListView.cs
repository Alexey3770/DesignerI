using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Designer.Acquaintance;
using Designer.Domain.Entity;

namespace Designer.Presentation.ControlListFur
{
    public delegate void PropertyChanged(string nameProp, string value);
    /// <summary>
    /// Контрол отображающий элементы, реализующие интерфейс IDrawableListFur.
    /// </summary>
    public partial class ListView : UserControl
    {
        /// <summary>
        /// Событие происходит, когда свойства изменены пользователем
        /// </summary>
        public event PropertyChanged EventPropertyChanged;
        const int INTERVALMIDELEM = 10;
        
        public delegate void SelectedIndexChanged(string name,bool isSelect);
        /// <summary>
        /// Событие происходит, когда изменился выбраный элемент
        /// </summary>
        public event SelectedIndexChanged EventSelectIndChanged;
           
        IDrawableList currnetDrawObj;
        List<IDrawableList> history;
        List<Control> property;
        
        /// <summary>
        /// Отображаемый элемент.
        /// </summary>
        public IDrawableList DrawObjects
        {
            set
            {
                currnetDrawObj = value;
                history.Clear();
                history.Add(currnetDrawObj);
                Update();
            }
        }
        /// <summary>
        /// Отображаемая кнопка
        /// </summary>
        public Button Button
        {
            get { return listbut; }
        }
        /// <summary>
        /// Кнопка перехода в 2д режим
        /// </summary>
        public Control Btn_2d
        {
            get { return btn_2d; }
        }
        /// <summary>
        /// Кнопка перехода в 3д режим
        /// </summary>
        public Control Btn_3d
        {
            get { return btn_3d; }
        }
        /// <summary>
        /// Название свойств
        /// </summary>
        public string NameProperty
        {
            set { label2.Text = value; }
        }
        /// <summary>
        /// Название кнопки
        /// </summary>
        public string ButtonName
        {
            set
            {
                listbut.Text = value;
                if (listbut.Text != "")
                {
                    listbut.Visible = true;
                    panel1.Height = listbut.Location.Y + listbut.Height + INTERVALMIDELEM;
                    ListFurniture_Resize(null, null);
                }
                else
                {
                    listbut.Visible = false;
                    panel1.Height = listbut.Location.Y + INTERVALMIDELEM;
                    ListFurniture_Resize(null, null);
                }
            }
        }
       
        public ListView()
        {
            InitializeComponent();
            history = new List<IDrawableList>();
            property = new List<Control>();
            listbut.Width = Width - 2 * INTERVALMIDELEM;
            listbut.Location = new Point(INTERVALMIDELEM,listbut.Location.Y);
            
        }
        /// <summary>
        /// Добавление контролов для отображения свойств
        /// </summary>
        /// <param name="props"></param>
        public void AddPropeties(List<Property> props)
        {
            //if(props == null)
            //{
            //    panel1.Visible = false;
            //    pnl_listFurs.Height = Height - (btn_back.Height + label1.Height);
            //    return;                
            //}
            
            Point startPoint = new Point( INTERVALMIDELEM,label2.Location.Y + label2.Height+INTERVALMIDELEM);
            foreach (Control c in property)
                    panel1.Controls.Remove(c);
                property.Clear();
                UpdateSize(startPoint);
            if (props == null)
            {
                label2.Text = "";
                
                return;
            }
            foreach (Property pr in props)
            {
                Label l = new Label() { Font = label1.Font, Text = pr.Name, AutoSize = true, Location = startPoint };
                panel1.Controls.Add(l);
                property.Add(l);
                TextBox t = new TextBox()
                {
                    ReadOnly = pr.IsLock,
                    Name = pr.Name,
                    Font = label1.Font,
                    Text = pr.Value,
                    Size = new Size(panel1.Width - (startPoint.X + l.Size.Width + 2 * INTERVALMIDELEM), 40),
                    Location = new Point(startPoint.X + l.Width + INTERVALMIDELEM, startPoint.Y),
                    TextAlign = HorizontalAlignment.Right
                };
                if (pr.IsLock)
                    t.ReadOnly = true;
                t.TextChanged += T_TextChanged;
                panel1.Controls.Add(t);
                property.Add(t);
                startPoint.Y += INTERVALMIDELEM + l.Height;
            }
            UpdateSize(startPoint);
        }

        private void UpdateSize(Point startPoint)
        {
            listbut.Location = new Point(listbut.Location.X, startPoint.Y);
            if (listbut.Visible)
                panel1.Height = listbut.Location.Y + listbut.Height + INTERVALMIDELEM;
            else
                panel1.Height = listbut.Location.Y + INTERVALMIDELEM;
            panel1.Location = new Point(panel1.Location.X, Height - panel1.Height);

            pnl_listFurs.Height = Height - (btn_back.Height + label1.Height + panel1.Height);
        }

        private void T_TextChanged(object sender, EventArgs e)
        {
            int r = 0;
            if(((Control)sender).Text != "")
                if(int.TryParse(((Control)sender).Text,out r))
                    EventPropertyChanged?.Invoke(((Control)sender).Name, ((Control)sender).Text);
        }

        /// <summary>
        /// Обновление контрола (удаление старых вкладок, добавление текущих)
        /// </summary>
        private new void Update()
        {
            ClearList();
            Size sizeElem = new Size(Width - 3 * INTERVALMIDELEM, 30);
            Point locationElem = new Point(INTERVALMIDELEM, 5);
            Size sizePicture = new Size((pnl_listFurs.Width - 60) / 2, (pnl_listFurs.Width - 60) / 2);
            Point locationPicture = new Point(INTERVALMIDELEM, 5);
            label1.Text = currnetDrawObj.Name;
            int color = 0;
            foreach (IDrawableList cat in currnetDrawObj.SubDrawObj)
            {
                if (cat.Img == null)
                {
                    ElementFurList l = new ElementFurList(sizeElem.Width, sizeElem.Height, color, cat.Name) { Location = locationElem, Name = cat.Name };
                    pnl_listFurs.Controls.Add(l);
                    l.Click += P_Click;
                    color++;
                    locationElem.Y += INTERVALMIDELEM + l.Height;
                }
                else
                {
                    DrawElemLF d = new DrawElemLF(cat) { Size = sizePicture, Location = locationPicture };
                    pnl_listFurs.Controls.Add(d);
                    d.Click += D_Click;

                    if (locationPicture.X == INTERVALMIDELEM)
                        locationPicture.X = pnl_listFurs.Width / 2 + INTERVALMIDELEM / 2;
                    else
                    {
                        locationPicture.X = INTERVALMIDELEM;
                        locationPicture.Y += INTERVALMIDELEM + d.Height;
                    }
                }
            }
        }

        /// <summary>
        /// Обрабочик события нажатия по графическому элементу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void D_Click(object sender, EventArgs e)
        {
            if (((DrawElemLF)sender).IsSelected)
            {
                ((DrawElemLF)sender).IsSelected = false;
                EventSelectIndChanged?.Invoke(((DrawElemLF)sender).Furniture.Name, false);
                return;
            }
            foreach (Control c in pnl_listFurs.Controls)
                ((DrawElemLF)c).IsSelected = false;
            ((DrawElemLF)sender).IsSelected = true;
            EventSelectIndChanged?.Invoke(((DrawElemLF)sender).Furniture.Name, true);
        }

        /// <summary>
        /// Обработчик события нажатия по вкладке.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P_Click(object sender, EventArgs e)
        {
            string name = ((Control)sender).Name;
            for(int i = 0;i < currnetDrawObj.SubDrawObj.Count;i++)
            {
                if(currnetDrawObj.SubDrawObj[i].Name == name)
                {
                    history.Add(currnetDrawObj);
                    if (currnetDrawObj.SubDrawObj[i].SubDrawObj != null)
                    {
                        currnetDrawObj = currnetDrawObj.SubDrawObj[i];
                        Update();
                    }
                    return;
                }
            }
            
            
        }

        /// <summary>
        /// Очистка списка отображаемых элементов.
        /// </summary>
        private void ClearList()
        {
            pnl_listFurs.Controls.Clear();
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Назад".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_back_Click(object sender, EventArgs e)
        {
            if(history.Count != 0)
            {
                currnetDrawObj = history[history.Count-1];
                history.RemoveAt(history.Count - 1);
                Update();
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Домой".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(history.Count != 0)
            {
                currnetDrawObj = history[0];
                history.Clear();
                Update();
            }
        }

        private void ListFurniture_Resize(object sender, EventArgs e)
        {
            panel1.Location = new Point(panel1.Location.X, Height - panel1.Height);
            pnl_listFurs.Height = Height - (btn_back.Height + label1.Height + panel1.Height);
            
        }

        private void btn_2d_Click(object sender, EventArgs e)
        {
            btn_2d.Image = Properties.Resources._2dbuton;
            btn_3d.Image = Properties.Resources._3dbut;
        }

        private void btn_3d_Click(object sender, EventArgs e)
        {
            btn_3d.Image = Properties.Resources._3dbuton;
            btn_2d.Image = Properties.Resources._2dbut;
        }
    }
}
