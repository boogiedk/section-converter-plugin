using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Input;


using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace SectionConverterPlugin
{
    class BuildRibbonItem : RibbonTextBox
    {
        string tabTitleName = "Конвертер сечений";
        string tabID = "RibbonPluginStart";

        #region command

       const string buildAxis = "BuildAxis";
       const string buildHeight = "BuildHeight";
       const string buildRed = "BuildRed";
       const string buildBlack = "BuildBlack";   
       const string exportSelectionsDataForm = "ExportSelectionsDataForm";       
       const string showPointStyleDialog = "_ptype";     
       const string showSizeWindowDialog = "ShowSizeWindowDialog";

        #endregion

        void ComponentManager_ItemInitialized(object sender, RibbonItemEventArgs e)
        {
            if (ComponentManager.Ribbon != null)
            {
                BuildRibbonTab();
                ComponentManager.ItemInitialized -=
                    new EventHandler<RibbonItemEventArgs>(ComponentManager_ItemInitialized);
            }
        }

        public void BuildRibbonTab()
        {
            if (!IsLoaded())
            {
                CreateRibbonTab();

                acadApp.SystemVariableChanged += new SystemVariableChangedEventHandler(acadApp_SystemVariableChanged);
            }
        }

        bool IsLoaded()
        {
            bool _loaded = false;
            RibbonControl ribbonControl = ComponentManager.Ribbon;

            foreach (RibbonTab tab in ribbonControl.Tabs)
            {
                if (tab.Id.Equals(tabID) & tab.Title.Equals(tabTitleName))
                {
                    _loaded = true;
                    break;
                }
                else _loaded = false;
            }
            return _loaded;
        }

        void RemoveRibbonTab()
        {
            try
            {
                RibbonControl ribbonControl = ComponentManager.Ribbon;
                foreach (RibbonTab ribbontab in ribbonControl.Tabs)
                {
                    if (ribbontab.Id.Equals(tabID) & ribbontab.Title.Equals(tabTitleName))
                    {
                        ribbonControl.Tabs.Remove(ribbontab);
                        acadApp.SystemVariableChanged -= new SystemVariableChangedEventHandler(acadApp_SystemVariableChanged);
                        break;
                    }
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.
                  DocumentManager.MdiActiveDocument.Editor.WriteMessage(ex.Message);
            }
        }

        void acadApp_SystemVariableChanged(object sender, SystemVariableChangedEventArgs e)
        {
            if (e.Name.Equals("WSCURRENT"))
            {
                var document = Autodesk.AutoCAD.ApplicationServices
                    .Application.DocumentManager.MdiActiveDocument;

                AcadTools.CreateLayersForPluginTool(document);
                new BuildRibbonItem().CreateRibbonTab();
            }
        }

        public void CreateRibbonTab()
        {
            try
            {
                var ribbonControl = ComponentManager.Ribbon;

                var ribbonTab = new RibbonTab();
                ribbonTab.Title = tabTitleName;
                ribbonTab.Id = tabID;
                ribbonControl.Tabs.Add(ribbonTab);

                AddPluginContent(ribbonTab);

                ribbonControl.UpdateLayout();
            }
            catch (System.Exception ex)
            {
                acadApp.DocumentManager.MdiActiveDocument.Editor.WriteMessage(ex.Message);
            }
        }

        public void AddPluginContent(RibbonTab ribbonTab)
        {
            try
            {          
                RibbonControl ribbonControl = ComponentManager.Ribbon;

                Autodesk.Windows.RibbonPanelSource ribbonPanelSourceMain = new RibbonPanelSource();
                ribbonPanelSourceMain.Title = "Создание точек";
                RibbonPanel mainPanel = new RibbonPanel();
                mainPanel.Source = ribbonPanelSourceMain;
                ribbonTab.Panels.Add(mainPanel);

                Autodesk.Windows.RibbonPanelSource ribbonPanelSourceSettings = new RibbonPanelSource();
                ribbonPanelSourceSettings.Title = "Настройки";
                RibbonPanel ribbonPanelSetting = new RibbonPanel();
                ribbonPanelSetting.Source = ribbonPanelSourceSettings;
                ribbonTab.Panels.Add(ribbonPanelSetting);

                Autodesk.Windows.RibbonPanelSource ribbonPanelSourceControl = new RibbonPanelSource();
                ribbonPanelSourceControl.Title = "Управление";
                RibbonPanel ribbonPanelControl = new RibbonPanel();
                ribbonPanelControl.Source = ribbonPanelSourceControl;
                ribbonTab.Panels.Add(ribbonPanelControl);

                #region axis
                RibbonButton ribbonButtonAxis = new RibbonButton();
                ribbonButtonAxis.Text = "Ось";
                ribbonButtonAxis.ShowText = true;
                ribbonButtonAxis.ShowImage = true;

                Bitmap imgAxis = Properties.Resources.PurplePoint_32x32;
                ribbonButtonAxis.LargeImage = GetBitmap(imgAxis);
                ribbonButtonAxis.Orientation = System.Windows.Controls.Orientation.Vertical;
                ribbonButtonAxis.Size = RibbonItemSize.Large;
                ribbonButtonAxis.CommandHandler = new RibbonCommandHandler();
                ribbonButtonAxis.CommandParameter = buildAxis;
                #endregion

                #region height
                RibbonButton ribbonButtonHeight = new RibbonButton();
                ribbonButtonHeight.Text = "Отметка";
                ribbonButtonHeight.ShowText = true;
                ribbonButtonHeight.ShowImage = true;


                Bitmap imgHeight = Properties.Resources.GreenPoint_32x32;

                ribbonButtonHeight.LargeImage = GetBitmap(imgHeight);
                ribbonButtonHeight.Orientation = System.Windows.Controls.Orientation.Vertical;
                ribbonButtonHeight.Size = RibbonItemSize.Large;
                ribbonButtonHeight.CommandHandler = new RibbonCommandHandler();
                ribbonButtonHeight.CommandParameter = buildHeight;
                #endregion

                #region black
                RibbonButton ribbonButtonBlack = new RibbonButton();
                ribbonButtonBlack.Text = "Черная";
                ribbonButtonBlack.ShowText = true;
                ribbonButtonBlack.ShowImage = true;

                Bitmap imgButtom = Properties.Resources.BlackPoint_32x32;
                ribbonButtonBlack.LargeImage = GetBitmap(imgButtom);
                ribbonButtonBlack.Orientation = System.Windows.Controls.Orientation.Vertical;
                ribbonButtonBlack.Size = RibbonItemSize.Large;
                ribbonButtonBlack.CommandHandler = new RibbonCommandHandler();
                ribbonButtonBlack.CommandParameter = buildBlack;
                #endregion

                #region red
                RibbonButton ribbonButtonRed = new RibbonButton();
                ribbonButtonRed.Text = "Красная";
                ribbonButtonRed.ShowText = true;
                ribbonButtonRed.ShowImage = true;

                Bitmap imgTop = Properties.Resources.RedPoint_32x32;
                ribbonButtonRed.LargeImage = GetBitmap(imgTop);
                ribbonButtonRed.Orientation = System.Windows.Controls.Orientation.Vertical;
                ribbonButtonRed.Size = RibbonItemSize.Large;
                ribbonButtonRed.CommandHandler = new RibbonCommandHandler();
                ribbonButtonRed.CommandParameter = buildRed;
                #endregion

                #region createBlocksofList
                RibbonButton ribbonButtonGroup = new RibbonButton();
                ribbonButtonGroup.Text = "Экспорт сечений";
                ribbonButtonGroup.ShowText = true;
                ribbonButtonGroup.ShowImage = true;

                Bitmap imgGroup = Properties.Resources.ExportArrow;
                ribbonButtonGroup.LargeImage = GetBitmap(imgGroup);
                ribbonButtonGroup.Orientation = System.Windows.Controls.Orientation.Vertical;
                ribbonButtonGroup.Size = RibbonItemSize.Large;
                ribbonButtonGroup.CommandHandler = new RibbonCommandHandler();
                ribbonButtonGroup.CommandParameter = exportSelectionsDataForm;
                #endregion

                #region point style dialog
                RibbonButton ribbonButtonPointStyle = new RibbonButton();
                ribbonButtonPointStyle.Text = "Настройка точек";
                ribbonButtonPointStyle.ShowText = true;
                ribbonButtonPointStyle.ShowImage = true;

                Bitmap imgPointStyle = Properties.Resources.settings;
                ribbonButtonPointStyle.LargeImage = GetBitmap(imgPointStyle);
                ribbonButtonPointStyle.Orientation = System.Windows.Controls.Orientation.Vertical;
                ribbonButtonPointStyle.Size = RibbonItemSize.Large;
                ribbonButtonPointStyle.CommandHandler = new RibbonCommandHandler();
                ribbonButtonPointStyle.CommandParameter = showPointStyleDialog;
                #endregion

                #region size window
                RibbonButton ribbonButtonWindowSize = new RibbonButton();
                ribbonButtonWindowSize.Text = "Размер окна";
                ribbonButtonWindowSize.ShowText = true;
                ribbonButtonWindowSize.ShowImage = true;

                Bitmap imgWindowSize = Properties.Resources.windowSize;
                ribbonButtonWindowSize.LargeImage = GetBitmap(imgWindowSize);
                ribbonButtonWindowSize.Orientation = System.Windows.Controls.Orientation.Vertical;
                ribbonButtonWindowSize.Size = RibbonItemSize.Large;
                ribbonButtonWindowSize.CommandHandler = new RibbonCommandHandler();
                ribbonButtonWindowSize.CommandParameter = showSizeWindowDialog;
                #endregion

                ribbonPanelSourceMain.Items.Add(ribbonButtonAxis);
                ribbonPanelSourceMain.Items.Add(ribbonButtonHeight);
                ribbonPanelSourceMain.Items.Add(ribbonButtonBlack);
                ribbonPanelSourceMain.Items.Add(ribbonButtonRed);

                ribbonPanelSourceSettings.Items.Add(ribbonButtonWindowSize);
                ribbonPanelSourceSettings.Items.Add(ribbonButtonPointStyle);

                ribbonPanelSourceControl.Items.Add(ribbonButtonGroup);

                ribbonTab.IsActive = true;
            }
            catch (System.Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.
                  DocumentManager.MdiActiveDocument.Editor.WriteMessage(ex.Message);
            }
        }

        BitmapImage LoadImage(string imageName)
        {
            return new BitmapImage(
                new Uri("pack://application:,,,/AutoCAD_DD;component/" + imageName + ".png"));
        }

        public BitmapImage GetBitmap(Bitmap image)
        {
            var stream = new MemoryStream();

            image.Save(stream, ImageFormat.Png);
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = stream;
            bmp.EndInit();
            return bmp;

        }

        public class RibbonCommandHandler : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                Document doc = acadApp.DocumentManager.MdiActiveDocument;

                if (parameter is RibbonButton)
                {
                    TurnOffInputPoint();
                    var button = (RibbonButton)parameter;
                    acadApp.DocumentManager.MdiActiveDocument.SendStringToExecute(
                        button.CommandParameter + " ", true, false, true);
                }
            }
            public void TurnOffInputPoint()
            {
                Document doc = acadApp.DocumentManager.MdiActiveDocument;
                string esc = "\x03";

                string cmds = (string)acadApp.GetSystemVariable("CMDNAMES");

                if (cmds.Length > 1)

                {
                    int cmdNum = cmds.Split(new char[] { '\'' }).Length;


                    for (int i = 0; i < cmdNum; i++)

                        esc += '\x03';
                    doc.SendStringToExecute(esc + "", true, false, true);
                }
            }
        }
    }
}



