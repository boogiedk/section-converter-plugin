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

        public InputStationDialog()
        {
            this.Enabled = false;

            InitializeComponent();

            retb_firstDouble.SetRegExp(new Regex(@"^[-\+]?\d+$"));
            retb_secondDouble.SetRegExp(new Regex(@"^\d{1,2}([,\.]\d+)?$"));

            retb_firstDouble.Value = "0";
            retb_secondDouble.Value = "0";

            this.Enabled = true;
        }

        private double StringToDouble(string s)
        {
            return Double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);
        }

        private void UpdateStation()
        {
            var stationValueHundredsString = retb_firstDouble.Value;
            var stationValueUnitsString = retb_secondDouble.Value;

            // skip for initialization
            if (stationValueHundredsString == null ||
                stationValueUnitsString == null)
            {
                return;
            }

            var stationValueHundreds = StringToDouble(stationValueHundredsString);
            var stationValueUnits = StringToDouble(stationValueUnitsString);

            _station = Math.Sign(stationValueHundreds) *
                (Math.Abs(stationValueHundreds) * 100 + stationValueUnits);
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(_station.ToString(CultureInfo.InvariantCulture));
        }

        public double Station
        {
            get
            {
                return _station;
            }
        }

        // update station from forms metods
        private void regExedTb_NumberOne_ValueChanged(object sender, EventArgs e)
        {
            UpdateStation();
        }
        private void regExedTb_NumberTwo_ValueChanged(object sender, EventArgs e)
        {
            UpdateStation();
        }
    }
}
