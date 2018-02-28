using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SectionConverterPlugin
{
    public partial class InputHeightDialog : Form
    {
        double _height;
        private bool _dataReverted;

        public InputHeightDialog()
        {
            this.Enabled = false;

            InitializeComponent();

            retb_height.SetRegExp(new Regex(@"^[-\+]?\d+([,\.]\d+)?$"));

            retb_height.Value = "0";
            _dataReverted = false;

            this.Enabled = true;
        }
    
        private double StringToDouble(string s)
        {
            return Double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);
        }

        private void UpdateHeight()
        {
            _dataReverted = retb_height.Reverted;
            
            var heightValuesString = retb_height.Value;

            // skip for initialization
            if (heightValuesString == null)
            {
                return;
            }

             _height = StringToDouble(heightValuesString);
        }

        // TODO:
        // rename
        public double Height
        {
            get
            {
                return _height;
            }
        }

        // update station from forms metods
        private void retb_height_ValueChanged(object sender, EventArgs e)
        {
            UpdateHeight();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (_dataReverted == true)
            {
                _dataReverted = false;

                MessageBox.Show("Invalid input");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
