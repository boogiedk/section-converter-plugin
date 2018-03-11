using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Autodesk.AutoCAD.Geometry;

using SectionConverterPlugin.HandlerEntity;
using System.IO;
using System.Xml;
using System.Text;
using System.Diagnostics;

namespace SectionConverterPlugin.Forms
{
    // Экспортировать данные о сечениях
    public partial class ExportSectionsDataForm : Form
    {
        double _fuctPassDeviantionAmplitude;
        bool _factPassEnable;

        private bool _dataReverted;

        public ExportSectionsDataForm()
        {
            this.Enabled = false;

            InitializeComponent();

            retb_FactValue.SetRegExp(new Regex(@"^\d+([,\.]\d+)?$"));

            retb_FactValue.Value = "0";
            _dataReverted = false;

            this.Enabled = true;
            this.ActiveControl = retb_FactValue;
        }

        private double StringToDouble(string s)
        {
            return Double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);
        }

        private void UpdateFactValue()
        {
            _dataReverted = retb_FactValue.Reverted;

            var factValueeString = retb_FactValue.Value;

            // skip for initialization
            if (factValueeString == null)
            {
                return;
            }

            _fuctPassDeviantionAmplitude = StringToDouble(factValueeString);
        }

        public double FactValue
        {
            get
            {
                return _fuctPassDeviantionAmplitude;
            }
        }

        private void retb_FactValue_ValueChanged(object sender, EventArgs e)
        {
            UpdateFactValue();
        }

        private void cb_FactValueMistake_CheckedChanged(object sender, EventArgs e)
        {
            _factPassEnable = cb_ActualValueMistake.Checked;
        }

        private void btn_ExportPoints_Click(object sender, EventArgs e)
        {
            ActiveControl = btn_ExportPoints;

            if (_dataReverted == true)
            {
                _dataReverted = false;

                MessageBox.Show("Invalid input");

                return;
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = AcadTools.GetAbsolutePath();
                dialog.Title = "Экспортировать в ";

                dialog.FileName=GenerateNameForFolder();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string saveName = Path.GetFileName(dialog.FileName);
                    string savePath = dialog.FileName.Replace(GenerateNameForFolder(), "");

                    CreateANeWFolder(savePath);  // path

                    GenerateANewXmlFile(savePath+ $"\\{saveName}.xml");   //path+name

                    string savePathNameSettings = savePath + "\\setting.xml";

                    GenerateASettingXmlFile(
                        savePathNameSettings,
                        "roadSectionsDataPath", savePath + $"\\{saveName}.xml",
                        "roadSectionsListPath",
                        savePath+"\\"+saveName + ".tsv"
                        );

                    StartProcess(savePathNameSettings);
                }

                this.ActiveControl = btn_ExportPoints;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public void GenerateANewXmlFile(string fileName) 
        {
            var pluginSettings = PluginSettings.GetInstance();
            var sectionMaxSize = pluginSettings.SectionMaxSize;

            var sectionsData = RoadSectionParametersExtractor
                .ExtractSectionsData(sectionMaxSize);          

            Func<double, double, string> CoordsToString = (x, y) =>
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
                        _factPassEnable),
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

            extractedSectionDataDoc.Save(fileName);

        }

        public void GenerateASettingXmlFile(string fileName,
            string keyXml,
            string valueXml,
            string keyTsv,
            string valueTsv)
        {
            var extractedSectionDataDoc = new XDocument(
                      new XElement(
                          "settings",
                          new XElement(
                              "add",
                          new XAttribute(
                              "key",keyXml),
                           new XAttribute(
                              "value", valueXml)),
                          new XElement("add",
                              new XAttribute("key",keyTsv),
                              new XAttribute("value",valueTsv)
                                           )));

            extractedSectionDataDoc.Save(fileName);
        }

        public bool CreateANeWFolder(string pathFolder)
        {
                bool result = false;

                 string folderName = GenerateNameForFolder();

                if ((folderName == "default"))
                {
                    result = true;
                    return result;
                }
                else
                {
                    if (Directory.Exists(pathFolder+folderName))
                    {
                        MessageBox.Show("Такая папка уже существует!");
                        return false;
                    }

                    result = true;
                    var directoryInfo = Directory.CreateDirectory(pathFolder);
                    return result;
                }
            
        }    

        public string GenerateNameForFolder()
        {
            string drawingName = Path.GetFileNameWithoutExtension(AcadTools.GetAbsolutePathWithName());

            string folderName = "default";

            if (drawingName == "" || drawingName == null)
            {
                return folderName;
            }
            else
                return drawingName + "_" + ReplaceSymbolToDate(DateTime.Now.ToString());
        }

        private string ReplaceSymbolToDate(string s)
        {
            return s.Replace(':', '.');
        }

        private void StartProcess(string pathSettingsXml)
        {
            try
            {
                Process process = new Process();

                var processStartInfo = new ProcessStartInfo()
                {
                    FileName = Path.Combine(AcadTools.GetAcadLocation(), @"SectionConverterPlugin\SectionListGenerator\SectionListGenerator.exe"),
                    Arguments = pathSettingsXml
                };

                process = Process.Start(processStartInfo);


                process.WaitForExit();
            }
            catch
            {
                MessageBox.Show("Failed to execute");
            }
        }     
    }
}
