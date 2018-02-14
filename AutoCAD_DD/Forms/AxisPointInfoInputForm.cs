using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SectionConverterPlugin
{
    public partial class AxisPointInfoInputForm : Form
    {
        private string _description;

        public AxisPointInfoInputForm()
        {
            InitializeComponent();
        }

        public string Description
        {
            get => _description;
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            _description = tb_Description.Text;
            this.Close();
        }
    }
}
