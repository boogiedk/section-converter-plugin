using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SectionConverterPlugin.Forms
{
    public partial class InputPointNumberDialog : Form
    {
        int _pointNumber;

        private bool _dataReverted;

        public InputPointNumberDialog()
        {
            this.Enabled = false;

            InitializeComponent();

            retb_PointNumber.SetRegExp(new Regex(@"^\d+$"));
            retb_PointNumber.Value = "0";
            _dataReverted = false;

            this.Enabled = true;
            this.ActiveControl = retb_PointNumber;
        }

        private int StringToUInt(string s)
        {
            return Int32.Parse(s,CultureInfo.InvariantCulture);
        }

        private void UpdatePointNumber()
        {
            _dataReverted = retb_PointNumber.Reverted;

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

        private bool TrySetActiveAnyRevertedInputControl()
        {
            bool result = false;

            if (retb_PointNumber.Reverted)
            {
                this.ActiveControl = retb_PointNumber;
                result = true;
            }

            return result;
        }

        // update station from forms metods
        private void retb_PointNumber_ValueChanged(object sender, EventArgs e)
        {
            UpdatePointNumber();
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
