using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


using SectionConverterPlugin.HandlerEntity;

namespace SectionConverterPlugin.Forms
{
    // Экспортировать данные о сечениях
    public partial class ExportSectionsDataForm : Form
    {
        double _actuallyValue;

        bool _factPositionsValuesEnabled;

        private bool _dataReverted;

        public ExportSectionsDataForm()
        {
            this.Enabled = false;

            InitializeComponent();

            retb_ActuallyValue.SetRegExp(new Regex(@"^[-\+]?\d+([,\.]\d+)?$"));

            retb_ActuallyValue.Value = "0";
            _dataReverted = false;

            this.Enabled = true;
        }

        private double StringToDouble(string s)
        {
            return Double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);
        }

        private void UpdateActuallyValue()
        {
            _dataReverted = retb_ActuallyValue.Reverted;

            var actuallyValueeString = retb_ActuallyValue.Value;

            // skip for initialization
            if (actuallyValueeString == null)
            {
                return;
            }

            _actuallyValue = StringToDouble(actuallyValueeString);
        }

        public double ActuallyValue
        {
            get
            {
                return _actuallyValue;
            }
        }

        public bool GetTheValueOfCheckBox()
        {
            return _factPositionsValuesEnabled=cb_ActualValueMistake.Checked;
        }

        private void retb_ActuallyValue_ValueChanged(object sender, EventArgs e)
        {
            UpdateActuallyValue();
        }

        private void btn_Run_Click(object sender, EventArgs e)
        {
         var pluginSettings = PluginSettings.GetInstance();
         var sectionMaxSize = pluginSettings.SectionMaxSize;
        
         var sectionsData = RoadSectionParametersExtractor
             .ExtractSectionsData(sectionMaxSize);
        
         var pathToCurentDocument = AcadTools.GetAbsolutePath();

            var extractedSectionDataDoc = new XDocument(
               new XElement(
            "road_sections",
                  new XAttribute(
                "fact_enabled",
                GetTheValueOfCheckBox()),
                new XElement(
                    "road_section",
                    new XAttribute(
                        "staEq", sectionsData
                .Select(sd =>
                        RoadSectionParametersExtractor
             .GetStationFromPointBlock(sd.AxisPoint)))))); // выводит ересь

            extractedSectionDataDoc.Save("test.xml");

            this.ActiveControl = btn_Run;

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
