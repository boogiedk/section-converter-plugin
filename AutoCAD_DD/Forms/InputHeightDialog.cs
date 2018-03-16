using System;
using System.Globalization;
using System.Text.RegularExpressions;
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
            
            retb_Height.SelectAll();
             
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
            
            var heightValueString = retb_Height.Value;

            // skip for initialization
            if (heightValueString == null)
            {
                return;
            }
            
             _height = StringToDouble(heightValueString);
        }

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
