using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCAD_DD
{
    public partial class PluginN2 : Form
    {
        public String Description = null;
        public PluginN2()
        {
            InitializeComponent();
        }

        private void button_done_Click(object sender, EventArgs e)
        {
            Description = textBoxDesc2.Text;

            textBoxDesc2.Text = "";

            this.Close();
        }

        private void PluginN2_Load(object sender, EventArgs e)
        {

        }
    }
}
