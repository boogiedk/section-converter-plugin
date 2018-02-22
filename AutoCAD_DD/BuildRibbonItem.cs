using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using System.Windows.Media.Imaging;
using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;


namespace SectionConverterPlugin
{
    class BuildRibbonItem
    {
        string tabTitleName = "Selection Converter Plugin";
        string tabID = "RibbonPluginStart";
        string command = "RunPlugin";

        void ComponentManager_ItemInitialized(object sender, RibbonItemEventArgs e)
        {
            // Проверяем, что лента загружена
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
            if (e.Name.Equals("WSCURRENT")) BuildRibbonTab();
        }

        void CreateRibbonTab()
        {
            try
            {
                var ribbonControl = ComponentManager.Ribbon;

                // добавляем свою вкладку
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
                RibbonToolTip ribbonToolTip;

                var ribbonPanelSource = new RibbonPanelSource();
                ribbonPanelSource.Title = "Start Plugin";

                // теперь саму панель
                var ribbonPanel = new RibbonPanel();
                ribbonPanel.Source = ribbonPanelSource;
                ribbonTab.Panels.Add(ribbonPanel);

                #region create split button

                // создаем split button
                RibbonSplitButton ribbonSplitButton = new RibbonSplitButton();
                ribbonSplitButton.Text = "RibbonSplitButton";
                // Ориентация кнопки
                ribbonSplitButton.Orientation = System.Windows.Controls.Orientation.Vertical;
                // Размер кнопки
                ribbonSplitButton.Size = RibbonItemSize.Large;
                // Показывать текст
                ribbonSplitButton.ShowText = true;
                // Стиль кнопки
                ribbonSplitButton.ListButtonStyle = Autodesk.Private.Windows.RibbonListButtonStyle.SplitButton;
                ribbonSplitButton.ResizeStyle = RibbonItemResizeStyles.NoResize;
                ribbonSplitButton.ListStyle = RibbonSplitButtonListStyle.List;
                /* Далее создаем две кнопки и добавляем их
                 * не в панель, а в RibbonSplitButton
                 */
                #endregion

                #region Кнопка №1
                // Создаем новый экземпляр подсказки
                ribbonToolTip = new RibbonToolTip();
                // Отключаем вызов справки (в данном примере её нету)
                ribbonToolTip.IsHelpEnabled = false;
                // Создаем кнопку
                RibbonButton ribbonButton = new RibbonButton();
                ribbonButton.CommandParameter = ribbonToolTip.Command = command;
                // Имя кнопки
                ribbonButton.Name = "Run Plugin";
                // Создаем новый (собственный) обработчик команд (см.ниже)
                ribbonButton.CommandHandler = new RibbonCommandHandler();

                // Ориентация кнопки
                ribbonButton.Orientation = System.Windows.Controls.Orientation.Horizontal;
                // Размер кнопки
                ribbonButton.Size = RibbonItemSize.Large;
                /* Т.к. используем размер кнопки Large, то добавляем
                 * большое изображение с помощью специальной функции (см.ниже)
                 */
                ribbonButton.LargeImage = LoadImage("32x32");
                // Показывать картинку
                ribbonButton.ShowImage = true;
                // Показывать текст
                ribbonButton.ShowText = true;
                // Заполняем содержимое всплывающей подсказки
                ribbonToolTip.Content = "Press to button for start plugin";
                // Подключаем подсказку к кнопке
                ribbonButton.ToolTip = ribbonToolTip;
                // Добавляем кнопку в RibbonSplitButton
                ribbonSplitButton.Items.Add(ribbonButton);

                #endregion

                ribbonSplitButton.Current = ribbonButton;

                ribbonPanelSource.Items.Add(ribbonSplitButton);
                RibbonRowPanel ribRowPanel = new RibbonRowPanel();
                ribbonPanelSource.Items.Add(ribRowPanel);
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
                new Uri("C:\\Users\\boogie\\Documents\\Visual Studio 2017\\Projects\\AutoCAD_DD\\AutoCAD_DD\\Resource\\" + ImageName + ".png"));
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
                Document doc = acadApp.DocumentManager.MdiActiveDocument;

                if (parameter is RibbonButton)
                {
                    RibbonButton button = parameter as RibbonButton;
                    acadApp.DocumentManager.MdiActiveDocument.SendStringToExecute(
                        button.CommandParameter + " ", true, false, true);
                }
            }
        }
    }
}
