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
using SectionConverterPlugin;

namespace SectionConverterPlugin.Forms
{
    public partial class InputPointNumberDialog : Form
    {
        public InputPointNumberDialog()
        {
            this.Enabled = false;

            InitializeComponent();

            retb_PointNumber.SetRegExp(new Regex(@"^\d+$"));

            retb_PointNumber.Value = "0";
            this.Enabled = true;
        }

        int _pointNumber;

        private int StringToUInt(string s)
        {
            return Int32.Parse(s,CultureInfo.InvariantCulture);
        }

        private void UpdatePointNumber()
        {
            var PointNumberValuesString = retb_PointNumber.Value;

            // skip for initialization
            if (PointNumberValuesString == null)
            {
                return;
            }

            _pointNumber = StringToUInt(PointNumberValuesString);
        }

        public int PointNumber
        {
            get
            {
                return _pointNumber;
            }
        }

        // update station from forms metods
        private void retb_PointNumber_ValueChanged(object sender, EventArgs e)
        {
            UpdatePointNumber();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {

        }
    }
}
