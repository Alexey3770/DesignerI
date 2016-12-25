using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Designer.Presentation
{
    public partial class LoadForm : Form
    {
        public string SelectedRoom
        {
            get { return (string)listBox1.SelectedItem; }
        }

        public LoadForm(List<string> rooms)
        {
            InitializeComponent();
            listBox1.DataSource = rooms;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
