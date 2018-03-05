using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;

using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;


namespace SectionConverterPlugin
{
    public class MainExtensionApplication : IExtensionApplication
    {
        /// <summary>
        /// Функция инициализации (выполняется при загрузке плагина)
        /// </summary>
        public void Initialize()
        {
            var document = Autodesk.AutoCAD.ApplicationServices
   .Application.DocumentManager.MdiActiveDocument;

            acadApp.Idle += StartPluginHandler;

            AcadTools.SetDefaultPdMode(document);

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