using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SectionConverterPlugin.Forms
{
    public partial class InPutAxisData : Form
    {
        string startText;

        public InPutAxisData()
        {
            InitializeComponent();
            btn_ok.Enabled = false;
            startText = maskedtb_main.Text;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void maskedtb_main_TextChanged(object sender, EventArgs e)
        {
            if (startText==maskedtb_main.Text)
                btn_ok.Enabled = false;
            else
                btn_ok.Enabled = true;
        }

        private void InPutAxisData_Load(object sender, EventArgs e)
        {

        }
    }
}
