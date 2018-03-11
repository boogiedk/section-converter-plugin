using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;

using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;


namespace SectionConverterPlugin
{
    public class MainExtensionApplication : IExtensionApplication
    {
        private readonly Size _initialSectionMaxSize;

        PluginSettings _settings;

        public MainExtensionApplication()
        {
            #region ReadOnly Initialization

            _initialSectionMaxSize = new Size()
            {
                Width = 50.0,
                Height = 50.0
            };

            #endregion

            _settings = PluginSettings.GetInstance();
        }

        /// <summary>
        /// Функция инициализации (выполняется при загрузке плагина)
        /// </summary>
        public void Initialize()
        {
            _settings.SectionMaxSize = _initialSectionMaxSize;

            var document = Autodesk.AutoCAD.ApplicationServices
                .Application.DocumentManager.MdiActiveDocument;

            acadApp.Idle += StartPluginHandler;

            AcadTools.SetDefaultPdMode(document);

            BuildRibbonItem buildRibbonItem = new BuildRibbonItem();

            if (Autodesk.Windows.ComponentManager.Ribbon == null)
            {
                acadApp.SystemVariableChanged += new SystemVariableChangedEventHandler(Application_SystemVariableChanged);

            }
            else
            {
                acadApp.SystemVariableChanged +=new SystemVariableChangedEventHandler(Application_SystemVariableChanged);
            }

            void Application_SystemVariableChanged(object sender, SystemVariableChangedEventArgs e)
            {

                if (e.Name.ToLower() == "wscurrent")
                {
                    AcadTools.CreateLayersForPluginTool(document);
                    new BuildRibbonItem().CreateRibbonTab();
                }
            }
        }


         private void StartPluginHandler(object sender, EventArgs e)
        {
            var document = Autodesk.AutoCAD.ApplicationServices
               .Application.DocumentManager.MdiActiveDocument;

            acadApp.Idle -= StartPluginHandler;

            AcadTools.CreateLayersForPluginTool(document);
            new BuildRibbonItem().CreateRibbonTab();
        }

        /// <summary>
        /// Функция, выполняемая при выгрузке плагина
        /// </summary>
        public void Terminate()
        {
        }
    }
}