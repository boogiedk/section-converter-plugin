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

namespace SectionConverterPlugin.Forms
{
    public partial class InputSizeForWindowDialog : Form
    {
        private Size _searchWindowSize;

        private bool _dataReverted;

        public InputSizeForWindowDialog()
        {
            this.Enabled = false;

            _searchWindowSize = new SectionConverterPlugin.Size();

            InitializeComponent();

            retb_heightWindow.SetRegExp(new Regex(@"^[-\+]?\d+([,\.]\d+)?$"));
            retb_widthWindow.SetRegExp(new Regex(@"^[-\+]?\d+([,\.]\d+)?$"));

            _dataReverted = false;
        }

        public void Initialize(Size searchWindowSize)
        {
            _searchWindowSize = searchWindowSize;

            RefreshComponentsValues();

            this.Enabled = true;   
        }

        private void RefreshComponentsValues()
        {
            retb_widthWindow.Value = AcadTools.DoubleToFormattedString(
                _searchWindowSize.Width);
            retb_heightWindow.Value = AcadTools.DoubleToFormattedString(
                _searchWindowSize.Height);
        }

        private double StringToDouble(string s)
        {
            return Double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);
        }

        private void UpdateHeight()
        {
            _dataReverted = retb_heightWindow.Reverted;

            var heightValueString = retb_heightWindow.Value;

            // skip for initialization
            if (heightValueString == null)
            {
                return;
            }

            _searchWindowSize.Height = StringToDouble(heightValueString);
        }
        private void UpdateWidth()
        {
            _dataReverted = retb_widthWindow.Reverted;

            var widthtValueString = retb_widthWindow.Value;

            // skip for initialization
            if (widthtValueString == null)
            {
                return;
            }

            _searchWindowSize.Width = StringToDouble(widthtValueString);
        }

        private void retb_heightWindow_ValueChanged(object sender, EventArgs e)
        {
            UpdateHeight();
        }
        private void retb_widthtWindow_ValueChanged(object sender, EventArgs e)
        {
            UpdateWidth();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            this.ActiveControl = btn_Ok;

            if (_dataReverted == true)
            {
                _dataReverted = false;

                MessageBox.Show("Invalid input");

                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public Size SearchWindowSize
        {
            get => _searchWindowSize;
            set => _searchWindowSize = value;
        }
    }
}
