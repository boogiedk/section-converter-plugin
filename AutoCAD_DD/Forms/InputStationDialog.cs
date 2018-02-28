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
    public partial class InputStationDialog : Form
    {
        double _station;
        private bool _dataReverted;

        public InputStationDialog()
        {
            this.Enabled = false;

            InitializeComponent();

            retb_StationValueHundred.SetRegExp(new Regex(@"^\d+$"));
            retb_StationValueUnit.SetRegExp(new Regex(@"^\d{1,2}([,\.]\d+)?$"));

            retb_StationValueHundred.Value = "0";
            retb_StationValueUnit.Value = "0";

            _dataReverted = false;

            this.Enabled = true;
        }

        private double StringToDouble(string s)
        {
            return Double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);
        }

        private void UpdateStation()
        {
            _dataReverted = retb_StationValueHundred.Reverted || 
                retb_StationValueUnit.Reverted;

            var stationValueHundredsString = retb_StationValueHundred.Value;
            var stationValueUnitsString = retb_StationValueUnit.Value;

            // skip for initialization
            if (stationValueHundredsString == null ||
                stationValueUnitsString == null)
            {
                return;
            }

            var stationValueHundreds = StringToDouble(stationValueHundredsString);
            var stationValueUnits = StringToDouble(stationValueUnitsString);

            _station = stationValueHundreds * 100 + stationValueUnits;
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

        public double Station
        {
            get
            {
                return _station;
            }
        }

        // update station from forms metods
        private void retb_stationValueHundred_ValueChanged(object sender, EventArgs e)
        {
            UpdateStation();
        }
        private void retb_stationValueUnit_ValueChanged(object sender, EventArgs e)
        {
            UpdateStation();
        }
    }
}
