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

            retb_Height.SetRegExp(new Regex(@"^[-\+]?\d+([,\.]\d+)?$"));

            retb_Height.Value = "0";
            _dataReverted = false;

            this.Enabled = true;
            this.ActiveControl = retb_Height;
        }
    
        private double StringToDouble(string s)
        {
            return Double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);
        }

        private void UpdateHeight()
        {
            _dataReverted = retb_Height.Reverted;
            
            var heightValuesString = retb_Height.Value;

            // skip for initialization
            if (heightValuesString == null)
            {
                return;
            }

             _height = StringToDouble(heightValuesString);
        }

        // TODO:
        // rename to PointHeight
        public double PointHeight
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

        // TODO:
        // работает почему-то только в дебаге.
        private bool TrySetActiveAnyRevertedInputControl()
        {
            bool result = false;

            if (retb_Height.Reverted)
            {
                this.ActiveControl = retb_Height;
                result = true;
            }
            
            return result;
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            this.ActiveControl = btn_Ok;

            if (_dataReverted == true)
            {
                _dataReverted = false;

                MessageBox.Show("Invalid input");

                TrySetActiveAnyRevertedInputControl();

                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
