using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Autodesk.AutoCAD.Geometry;

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

            Func<double, double, string> CoordsToString = (x,y) =>
                String.Format("{0} {1}",
                    AcadTools.DoubleToFormattedString(x),
                    AcadTools.DoubleToFormattedString(y));

            Func<Point3d, Point3d, int, XElement> GetPointElement =
                (projPos, factPos, number) =>
                    new XElement(
                        "point",
                            new XElement(
                                "proj_position",
                                CoordsToString(projPos.X, projPos.Y)),
                            new XElement(
                                "fact_position",
                                CoordsToString(factPos.X, factPos.Y)),
                            new XElement("number", number));

            var extractedSectionDataDoc = new XDocument(
                new XElement(
                    "road_sections",
                    new XAttribute(
                        "fact_enabled",
                        _factPositionsValuesEnabled),
                    sectionsData
                        .Select(sd =>
                            new XElement(
                                "road_section",
                                new XElement(
                                    "staEq",
                                    AcadTools.DoubleToFormattedString(
                                        RoadSectionParametersExtractor.GetStationFromPointBlock(sd.AxisPoint))),
                                new XElement(
                                    "origin",
                                    CoordsToString(
                                        AcadTools.GetBlockPosition(sd.AxisPoint).X,
                                        AcadTools.GetBlockPosition(sd.HeightPoint).Y)),
                                    new XElement("origin_height",
                                    AcadTools.DoubleToFormattedString(
                                        RoadSectionParametersExtractor.GetHeightFromPointBlock(sd.HeightPoint))),

                                    new XElement(
                                        "red_points",
                                        sd.RedPoints
                                        .Select(redPoint =>
                                            GetPointElement(
                                                AcadTools.GetBlockPosition(redPoint),
                                                AcadTools.GetBlockPosition(redPoint),
                                                RoadSectionParametersExtractor.GetPointNumberFromPointBlock(redPoint)))),

                                    new XElement(
                                        "black_points",
                                    sd.BlackPoints
                                        .Select(blackPoint =>
                                            GetPointElement(
                                                AcadTools.GetBlockPosition(blackPoint),
                                                AcadTools.GetBlockPosition(blackPoint),
                                                RoadSectionParametersExtractor.GetPointNumberFromPointBlock(blackPoint))))
                                    ))));

            extractedSectionDataDoc.Save("tester.xml");

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

        private void cb_ActualValueMistake_CheckedChanged(object sender, EventArgs e)
        {
            _factPositionsValuesEnabled = cb_ActualValueMistake.Checked;
        }
    }
}
