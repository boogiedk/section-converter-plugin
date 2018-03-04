using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using System.Windows.Media.Imaging;
using Autodesk.AutoCAD.Windows.Data;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;


namespace SectionConverterPlugin
{
    class BuildRibbonItem
    {
        string tabTitleName = "Конвертер сечений";
        string tabID = "RibbonPluginStart";

        #region command

        string buildheight = "buildheight";
        string buildbottom = "buildbottom";
        string buildtop = "buildtop";
        string buildaxis = "buildaxis";

        string createListOfBlocks = "CreateListsOfBlocks"; 

        string showPointStyleDialog = "_ptype";

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
            if (!isLoaded())
            {
                CreateRibbonTab();

                acadApp.SystemVariableChanged += new SystemVariableChangedEventHandler(acadApp_SystemVariableChanged);
            }
        }

        bool isLoaded()
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

                addPluginContent(ribbonTab);

                ribbonControl.UpdateLayout();
            }
            catch (System.Exception ex)
            {
                acadApp.DocumentManager.MdiActiveDocument.Editor.WriteMessage(ex.Message);
            }
        }

        void addPluginContent(RibbonTab ribbonTab)
        {
            try
            {
                RibbonControl ribbonControl = ComponentManager.Ribbon;

                Autodesk.Windows.RibbonPanelSource ribbonPanelSourceMain = new RibbonPanelSource();
                    ribbonPanelSourceMain.Title = "Главная панель";
                    RibbonPanel mainPanel = new RibbonPanel();
                    mainPanel.Source = ribbonPanelSourceMain;
                    ribbonTab.Panels.Add(mainPanel);

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
                ribbonButtonAxis.CommandParameter = buildaxis;
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
                ribbonButtonHeight.CommandParameter = buildheight;
                #endregion

                #region bottom
                RibbonButton ribbonButtonButtom = new RibbonButton();
                    ribbonButtonButtom.Text = "Низ полотна";
                    ribbonButtonButtom.ShowText = true;
                    ribbonButtonButtom.ShowImage = true;

                Bitmap imgButtom = Properties.Resources.BlackPoint_32x32;
                ribbonButtonButtom.LargeImage = GetBitmap(imgButtom);
                ribbonButtonButtom.Orientation = System.Windows.Controls.Orientation.Vertical;
                ribbonButtonButtom.Size = RibbonItemSize.Large;
                ribbonButtonButtom.CommandHandler = new RibbonCommandHandler();
                ribbonButtonButtom.CommandParameter = buildbottom;
                #endregion

                #region top
                RibbonButton ribbonButtonTop = new RibbonButton();
                ribbonButtonTop.Text = "Вверх полотна";
                ribbonButtonTop.ShowText = true;
                ribbonButtonTop.ShowImage = true;

                Bitmap imgTop = Properties.Resources.RedPoint_32x32;
                ribbonButtonTop.LargeImage = GetBitmap(imgTop);
                ribbonButtonTop.Orientation = System.Windows.Controls.Orientation.Vertical;
                ribbonButtonTop.Size = RibbonItemSize.Large;
                ribbonButtonTop.CommandHandler = new RibbonCommandHandler();
                ribbonButtonTop.CommandParameter = buildtop;
                #endregion

                #region createBlocksofList
                RibbonButton ribbonButtonGroup = new RibbonButton();
                ribbonButtonGroup.Text = "Создать списки блоков";
                ribbonButtonGroup.ShowText = true;
                ribbonButtonGroup.ShowImage = true;

                Bitmap imgGroup = Properties.Resources.GroupBlocks;
                ribbonButtonGroup.LargeImage = GetBitmap(imgGroup);
                ribbonButtonGroup.Orientation = System.Windows.Controls.Orientation.Vertical;
                ribbonButtonGroup.Size = RibbonItemSize.Large;
                ribbonButtonGroup.CommandHandler = new RibbonCommandHandler();
                ribbonButtonGroup.CommandParameter = createListOfBlocks;
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

                ribbonPanelSourceMain.Items.Add(ribbonButtonAxis);
                ribbonPanelSourceMain.Items.Add(ribbonButtonHeight);
                ribbonPanelSourceMain.Items.Add(ribbonButtonButtom);
                ribbonPanelSourceMain.Items.Add(ribbonButtonTop);
                ribbonPanelSourceMain.Items.Add(ribbonButtonGroup);

                ribbonPanelSourceMain.Items.Add(ribbonButtonPointStyle);

                ribbonTab.IsActive = true;
                }
            catch (System.Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.
                  DocumentManager.MdiActiveDocument.Editor.WriteMessage(ex.Message);
            }

        }
    

    BitmapImage LoadImage(string ImageName)
        {
            return new BitmapImage(
                new Uri("pack://application:,,,/AutoCAD_DD;component/" + ImageName + ".png"));
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

        class RibbonCommandHandler : System.Windows.Input.ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                Document document = acadApp.DocumentManager.MdiActiveDocument;

                if (parameter is RibbonButton)
                {
                    TurnOffInputPoint();
                    RibbonButton button = parameter as RibbonButton;
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
