using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Autodesk.AutoCAD.Geometry;
using SectionConverterPlugin.HandlerEntity;
using System.IO;
using System.Diagnostics;

namespace SectionConverterPlugin.Forms
{
    // Экспортировать данные о сечениях
    public partial class ExportSectionsDataForm : Form
    {
       private bool _factPosEnable;

        private double _factPosNoizeUpperBound;
        private double _factPosNoizeLowerBound;

        private bool _dataReverted;

        public ExportSectionsDataForm()
        {
            this.Enabled = false;

            InitializeComponent();

            retb__FactPosNoizeUpperBound.SetRegExp(new Regex(@"^[-\+]?\d+([,\.]\d+)?$"));
            retb__FactPosNoizeLowerBound.SetRegExp(new Regex(@"^[-\+]?\d+([,\.]\d+)?$"));

            retb__FactPosNoizeLowerBound.Value = "0";
            retb__FactPosNoizeUpperBound.Value = "0";

            _dataReverted = false;

            this.Enabled = true;
        }

        private static double StringToDouble(string s)
        {
            return Double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);
        }


        private void UpdateFactPosNoizeUpperBound()
        {
            _dataReverted = retb__FactPosNoizeUpperBound.Reverted;

            var factValueeString = retb__FactPosNoizeUpperBound.Value;

            // skip for initialization
            if (factValueeString == null)
            {
                return;
            }

            _factPosNoizeUpperBound = StringToDouble(factValueeString);
        }

        private void UpdateFactPosNoizeLowerBound()
        {
            _dataReverted = retb__FactPosNoizeLowerBound.Reverted;

            var factValueeString = retb__FactPosNoizeLowerBound.Value;

            // skip for initialization
            if (factValueeString == null)
            {
                return;
            }

            _factPosNoizeLowerBound = StringToDouble(factValueeString);
        }


        public double FactPosNoizeUpperBound
        {
            get
            {
                return _factPosNoizeUpperBound;
            }
        }

        public double FactPosNoizeLowerBound
        {
            get
            {
                return _factPosNoizeLowerBound;
            }
        }


        private void retb__FactPosNoizeUpperBound_ValueChanged(object sender, EventArgs e)
        {
            UpdateFactPosNoizeUpperBound();
        }

        private void retb__FactPosNoizeLowerBound_ValueChanged(object sender, EventArgs e)
        {
            UpdateFactPosNoizeLowerBound();
        }

        private void cb_FactValueMistake_CheckedChanged(object sender, EventArgs e)
        {
            _factPosEnable = cb_ActualValueMistake.Checked;
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

            #region Bad Blocks Cleanup
            string[] prefixes = { "axisPoint_", "heightPoint_", "redPoint_", "blackPoint_" };
            int recordsErased;
            int referencesErased;

            AcadTools.ClenUpDatabaseFromBadBlocks(
                prefixes,
                out recordsErased,
                out referencesErased);

            if (referencesErased != 0)
            {
                MessageBox.Show("Копий блоков удалено: " + referencesErased);
            }
            #endregion 

            using (var dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = AcadTools.GetAbsolutePath();
                dialog.Title = @"Экспортировать в...";

                string saveTime = GenerateNameForFolder();

                dialog.FileName = Path.GetFileNameWithoutExtension(AcadTools.GetAbsolutePathWithName()) +
                                  "_" + saveTime;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string savePath = dialog.FileName.Replace(GenerateNameForFolder(), "");

                    CreateNewFolder(savePath);

                    Func<Vector3d> GetFactPossNoizeAddition = null;
                    if (_factPosEnable)
                    {
                        var randomGen = new Random();
                        var factPossNoizeAmplitude =
                            _factPosNoizeUpperBound - _factPosNoizeLowerBound / 2.0;
                        Func<double> GetNoize = () =>
                        {
                            return _factPosNoizeLowerBound +
                                   factPossNoizeAmplitude * randomGen.NextDouble();
                        };

                        GetFactPossNoizeAddition = () => new Vector3d(GetNoize(), GetNoize(), .0);
                    }

                    GenerateDataXmlFile(savePath + "\\data.xml", GetFactPossNoizeAddition);

                    string savePathNameSettings = savePath + "\\settings.xml";

                    GenerateSettingXmlFile(
                        savePathNameSettings,
                         savePath + "\\data.xml",
                        savePath+"\\"+ GenerateNameForListTsvFile(saveTime) + ".tsv",
                        Path.Combine(AcadTools.GetAcadLocation(), "SectionConverterPlugin\\SectionsBlueprintGenerator\\BlueprintTemplate" + ".dxf")
                        ,savePath+"\\"+ GenerateNameForDxfFile(saveTime) +".dxf"
                        );

                    StartProcessForCreateTsvFile(savePathNameSettings);

                    StartProcessForCreateDxfFile(savePathNameSettings);
                }

                this.ActiveControl = btn_ExportPoints;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }  //button

        public void GenerateDataXmlFile(
            string fileName,
            Func<Vector3d> getFactPositionNoizeAddition = null)
        {
            bool factPossEnabled = true;
            if (getFactPositionNoizeAddition == null)
            {
                getFactPositionNoizeAddition = () => new Vector3d();
                factPossEnabled = false;
            }

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
                    new XAttribute("fact_enabled", factPossEnabled),
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
                                                AcadTools.GetBlockPosition(redPoint) + getFactPositionNoizeAddition(),
                                                RoadSectionParametersExtractor.GetPointNumberFromPointBlock(redPoint)))),

                                    new XElement(
                                        "black_points",
                                    sd.BlackPoints
                                        .Select(blackPoint =>
                                            GetPointElement(
                                                AcadTools.GetBlockPosition(blackPoint),
                                                AcadTools.GetBlockPosition(blackPoint) + getFactPositionNoizeAddition(),
                                                RoadSectionParametersExtractor.GetPointNumberFromPointBlock(blackPoint))))
                                    ))));

            extractedSectionDataDoc.Save(fileName);

        }

        public void GenerateSettingXmlFile(
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

                 var drawingName = Path.GetFileNameWithoutExtension(AcadTools.GetAbsolutePathWithName());

                  var folderName = drawingName + "_" + GenerateNameForFolder();

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
                return DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss");
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
                    FileName = Path.Combine(AcadTools.GetAcadLocation(),
                        @"SectionConverterPlugin\SectionsListGenerator\SectionListGenerator.exe"),
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
                    FileName = Path.Combine(AcadTools.GetAcadLocation(),
                        @"SectionConverterPlugin\SectionsBlueprintGenerator\SectionsBlueprintGenerator.exe"),
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
