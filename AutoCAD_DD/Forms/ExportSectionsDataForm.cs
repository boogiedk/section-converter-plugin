using System;
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

                MessageBox.Show(@"Invalid input");

                return;
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = AcadTools.GetAbsolutePath();
                dialog.Title = @"Экспортировать в ";

                dialog.FileName=GenerateNameForFolder();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string saveName = Path.GetFileName(dialog.FileName);
                    string savePath = dialog.FileName.Replace(GenerateNameForFolder(), "");

                    CreateNewFolder(savePath);
                    GenerateNewXmlFile(savePath+ "\\data.xml");

                    string savePathNameSettings = savePath + "\\settings.xml";

                    GenerateASettingXmlFile(
                        savePathNameSettings,
                         savePath + "\\data.xml",
                        savePath+"\\"+ GenerateNameForListTsvFile(saveName) + ".tsv",
                        Path.Combine(AcadTools.GetAcadLocation(), "SectionConverterPlugin\\SectionListGenerator\\BlueprintTemplate" + ".dxf")
                        ,savePath+"\\"+GenerateNameForBlueprintTsvFile(saveName)+".tsv"
                        );

                    StartProcessForCreateTsvFile(savePathNameSettings);

                    StartProcessForCreateDxfFile(savePathNameSettings);
                }

                this.ActiveControl = btn_ExportPoints;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }  //button

        public void GenerateNewXmlFile(string fileName) 
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

        public void GenerateASettingXmlFile(
            string fileName,
            string valueXml,
            string valueTsv,
            string valueDxf,
            string valueBluePrint)
        {
            var extractedSectionDataDoc = new XDocument(
                      new XElement(
                          "settings",
                          new XElement(
                              "add",
                          new XAttribute("key", "roadSectionsDataPath"),
                           new XAttribute("value", valueXml)),
                          new XElement("add",
                              new XAttribute("key", "roadSectionsListPath"),
                              new XAttribute("value",valueTsv)),
                          new XElement("add",
                              new XAttribute("key", "blueprintTemplatePath"),
                              new XAttribute("value", valueDxf)),
                         new XElement("add",
                              new XAttribute("key", "roadSectionsBlueprintPath"),
                              new XAttribute("value", valueBluePrint))
                                           ));

            extractedSectionDataDoc.Save(fileName);
        }

        public bool CreateNewFolder(string pathFolder)
        {
                bool result = false;

                var folderName = GenerateNameForFolder();

                if ((folderName == "default"))
                {
                    result = true;
                    return result;
                }
                else
                {
                    if (Directory.Exists(pathFolder+folderName))
                    {
                        MessageBox.Show(@"Такая папка уже существует!");
                        return false;
                    }

                    result = true;
                    var directoryInfo = Directory.CreateDirectory(pathFolder);
                    return result;
                }
            
        }    

        public string GenerateNameForFolder()
        {
            var drawingName = Path.GetFileNameWithoutExtension(AcadTools.GetAbsolutePathWithName());

            const string folderName = "default";

            if (string.IsNullOrEmpty(drawingName))
            {
                return folderName;
            }
            else
                return drawingName + "_" + DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss");
        }

        public string GenerateNameForDxfFile(string fileName)
        {
            string dfxName = Path.GetFileNameWithoutExtension(AcadTools.GetAbsolutePathWithName());

            return dfxName + "_" + "blueprint_" + fileName;
        }

        public string GenerateNameForListTsvFile(string fileName)
        {
            string tsvName = Path.GetFileNameWithoutExtension(AcadTools.GetAbsolutePathWithName());

            return tsvName + "_list_" + fileName;
        }

        public string GenerateNameForBlueprintTsvFile(string fileName)
        {
            string tsvName = Path.GetFileNameWithoutExtension(AcadTools.GetAbsolutePathWithName());

            return tsvName + "_blueprint_" + fileName;
        }

        private void StartProcessForCreateTsvFile(string pathSettingsXml)
        {
            try
            {
                var process = new Process();

                var processStartInfo = new ProcessStartInfo()
                {
                    FileName = Path.Combine(AcadTools.GetAcadLocation(), @"SectionConverterPlugin\SectionListGenerator\SectionListGenerator.exe"),
                    Arguments = "\"" + pathSettingsXml + "\""
                };

                process = Process.Start(processStartInfo);

                process.WaitForExit();
            }
            catch
            {
                MessageBox.Show("Failed to execute");
            }
        }

        private void StartProcessForCreateDxfFile(string pathSettingsXml)
        {
            try
            {       
                var process = new Process();

                var processStartInfo = new ProcessStartInfo()
                {
                    FileName = Path.Combine(AcadTools.GetAcadLocation(), @"SectionConverterPlugin\SectionListGenerator\SectionBlueprintGenerator.exe"),
                    Arguments = "\"" + pathSettingsXml + "\""
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
