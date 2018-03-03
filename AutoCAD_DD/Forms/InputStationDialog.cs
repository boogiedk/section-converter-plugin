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
        public Regex _stationRegex;

        double _station;

        private bool _dataReverted;

        public InputStationDialog()
        {
            this.Enabled = false;

            InitializeComponent();

            _stationRegex = 
                new Regex(@"^(((?<hundreds>\d+)\+(?<units>\d{1,2}))|(?<all_units>\d+))([,\.](?<fractional>\d+))?$");

            retb_Station.SetRegExp(_stationRegex);

            retb_Station.Value = "0";

            _dataReverted = false;

            this.Enabled = true;

            this.ActiveControl = retb_Station;
        }

        private double StringToDouble(string s)
        {
            return Double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);
        }

        private void UpdateStation()
        {
            _dataReverted = retb_Station.Reverted;

            var stationString = retb_Station.Value;

            // skip for initialization
            if (stationString == null)
            {
                return;
            }

            _station = ParseStationString(stationString);
        }

        private double ParseStationString(string stationString)
        {
            MatchCollection mc = _stationRegex.Matches(stationString);

            var match = mc[0];

            double station = .0;

            if (match.Groups["hundreds"].Length > 0)
            {
                station += 100 * StringToDouble(match.Groups["hundreds"].Value) +
                    StringToDouble(match.Groups["units"].Value);
            }
            else
            {
                station += StringToDouble(match.Groups["all_units"].Value);
            }
            if (match.Groups["fractional"].Length > 0)
            {
                var fractional = StringToDouble(match.Groups["fractional"].Value);
                
                while (fractional > 1.0 && fractional != .0)
                {
                    fractional /= 10.0;
                }

                station += fractional;
            }

            return station;
        }

        private bool TrySetActiveAnyRevertedInputControl()
        {
            bool result = false;

            if (retb_Station.Reverted)
            {
                this.ActiveControl = retb_Station;
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

        public double Station
        {
            get
            {
                return _station;
            }
        }
        
        // update station from forms metods
        private void retb_StationValue_ValueChanged(object sender, EventArgs e)
        {
            UpdateStation();
        }
    }
}
