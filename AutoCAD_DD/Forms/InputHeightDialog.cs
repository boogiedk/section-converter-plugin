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
        public InputHeightDialog()
        {
            this.Enabled = false;

            InitializeComponent();

            retb_heightDouble.SetRegExp(new Regex(@"^\d{1,4}([,\.]\d+)?$"));

            retb_heightDouble.Value = "0";
            this.Enabled = true;
        }

        double _height;
    
        private double StringToDouble(string s)
        {
            return Double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);
        }

        private void UpdateHeight()
        {
            var heightValuesString = retb_heightDouble.Value;

            // skip for initialization
            if (heightValuesString == null)
            {
                return;
            }

             _height = StringToDouble(heightValuesString);
        }

        public double Height
        {
            get
            {
                return _height;
            }
        }

        // update station from forms metods
        private void retb_heightDouble_ValueChanged(object sender, EventArgs e)
        {
            UpdateHeight();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
        }
    }
}
