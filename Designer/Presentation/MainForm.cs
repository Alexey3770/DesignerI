using Designer.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;
using Designer.Acquaintance;
using Designer.Presentation.ControlListFur;
using Designer.Presentation;

namespace Designer
{
    /// <summary>
    /// Главная форма
    /// </summary>
    public partial class MainForm : Form, IPresenter
    {
        const int INETRVALMIDELEM = 30;

        View3D view3D;
        View2D view2D;
        bool isLeftButtonPress = false;
        bool isRightButtonPress = false;
        ModeView modeView;
        /// <summary>
        /// Получает или задает положение 3d камеры
        /// </summary>
        public Camera3D Camera3D
        {
            get { return view3D.Camera; }
            set { view3D.Camera = value; }
        }
        /// <summary>
        /// Получает или задает положение 2d камеры
        /// </summary>
        public Camera2D Camera2D
        {
            get { return view2D.Camera; }
        }
        /// <summary>
        /// Получает или задает отображаемые свойства
        /// </summary>
        public List<Property> Properties
        {
            set { listFurniture1.AddPropeties(value); }
        }
        /// <summary>
        /// Получает или задает название отображаемых свойств
        /// </summary>
        public string NameProperty
        {
            set { listFurniture1.NameProperty = value; }
        }
        /// <summary>
        /// Получает или задает название кнопки
        /// </summary>
        public string NameButton
        {
            set { listFurniture1.ButtonName = value; }
        }
        /// <summary>
        /// Получает или задает режим отображения
        /// </summary>
        public ModeView Mode
        {
            get
            {
                return modeView;
            }

            set
            {
                modeView = value;
                if(modeView == ModeView.mode3d)
                {
                    view3D.Visible = true;
                    view2D.Visible = false;
                }
                else
                {
                    view3D.Visible = false;
                    view2D.Visible = true;
                }
            }
        }
        

        public event SetMousePos SurfaceMouseClick;
        public event SetMousePos SurfaceMouseMove;
        public event SelectedNameChanged ListIndexChanged;
        public event NameChanged ButtonClick;
        public event KeyPress EventKeyPress;
        public event MouseEventHandler EvenMouseWheel;
        public event PropertyChanged EventPropertyChanged;

        public MainForm()
        {
            InitializeComponent();
            Init();
            SignProperties();
            MainForm_Resize(null,null);
        }

        private void Init()
        {
            view3D = new View3D() { Location = new Point(10, listFurniture1.Location.Y), Width = 700, Height = 700 };
            view2D = new View2D() { Location = new Point(10, listFurniture1.Location.Y), Width = 700, Height = 700 };
            view3D.InitializeContexts();
            view3D.Init();
            view3D.Visible = false;
            Controls.Add(view2D);
            Controls.Add(view3D);
            Mode = ModeView.mode2d;
        }

        private void SignProperties()
        {
            listFurniture1.EventSelectIndChanged += ListFurniture1_EventSelectIndChanged;
            listFurniture1.Button.Click += Button_Click;
            listFurniture1.EventPropertyChanged += ListFurniture1_EventPropertyChanged;
            listFurniture1.Btn_2d.Click += Button_Click;
            listFurniture1.Btn_3d.Click += Button_Click;

            view3D.MouseClick += View2D1_MouseClick;            
            view3D.MouseMove += View2D1_MouseMove;
            view3D.MouseDown += View3D_MouseDown;
            view3D.MouseUp += View3D_MouseUp;

            view2D.MouseClick += View2D1_MouseClick;
            view2D.MouseMove += View2D1_MouseMove;
            view2D.MouseDown += View3D_MouseDown;
            view2D.MouseUp += View3D_MouseUp;

            foreach (Control cont in Controls)
            {
                cont.KeyDown += MainForm_KeyDown;
                cont.KeyUp += MainForm_KeyUp;
            }
        }
        

        private void ListFurniture1_EventPropertyChanged(string nameProp, string value)
        {
            EventPropertyChanged?.Invoke(nameProp, value);
        }

        private void View3D_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isLeftButtonPress = false;
            if (e.Button == MouseButtons.Right)
                isRightButtonPress = false;
        }

        private void View3D_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
                isLeftButtonPress = true;
            if (e.Button == MouseButtons.Right)
                isRightButtonPress = true;
        }

        private void ListFurniture1_EventSelectIndChanged(string name,bool isSelected)
        {
            ListIndexChanged?.Invoke(name,isSelected);
        }
        Point? p;
        private void View2D1_MouseMove(object sender, MouseEventArgs e)
        {
            SurfaceMouseMove?.Invoke(e.Location.X, e.Location.Y,isLeftButtonPress,isRightButtonPress);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            EvenMouseWheel?.Invoke(this, e);
        }
        private void View2D1_MouseClick(object sender, MouseEventArgs e)
        {
            SurfaceMouseClick?.Invoke(e.X, e.Y,isLeftButtonPress,isRightButtonPress);
        }

        public void DrawObjects(List<IDrawableObj> drawObjs)
        {
            view2D.DrawObjects(drawObjs);
            view3D.DrawObjects(drawObjs);
        }
        public void SetDrawList(IDrawableList drawList)
        {
            listFurniture1.DrawObjects = drawList;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ButtonClick?.Invoke(((Control)sender).Name);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

        }
        public void ShowMessage(string text)
        {
            MessageBox.Show(text, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            EventKeyPress?.Invoke(true,e.KeyData.ToString()[0]);
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            EventKeyPress?.Invoke(false, e.KeyData.ToString()[0]);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            listFurniture1.Size = new Size(listFurniture1.Size.Width, Height - 90);
            listFurniture1.Location = new Point(Width - listFurniture1.Width - INETRVALMIDELEM, listFurniture1.Location.Y);

            view2D.Size = new Size(Width - listFurniture1.Width - 2 * INETRVALMIDELEM, Height - 90);

            view3D.Size = new Size(Width - listFurniture1.Width - 2*INETRVALMIDELEM, Height - 90);
            view3D.ChangeViewPort();
                view3D.Draw();
            view2D.DrawObjects();
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButtonClick?.Invoke("load");
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButtonClick?.Invoke("save");
        }

        private void save_Click(object sender, EventArgs e)
        {
            ButtonClick?.Invoke(((ToolStripMenuItem)sender).Name);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Presentation.Help().ShowDialog();
        }
    }
}
