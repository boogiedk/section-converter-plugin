using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Collections.Generic;
using System.Linq;
using SectionConverterPlugin.Forms;

using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.Runtime;


namespace SectionConverterPlugin
{
    public class ConsoleCommand
    {
        [CommandMethod("buildaxis")]
        public void Buildaxis()
        {
            Func<string> GetAnyIniqueBlockName = () => DateTime.Now.Ticks.ToString();

            var document = Autodesk.AutoCAD.ApplicationServices
               .Application.DocumentManager.MdiActiveDocument;

            new AcadTools().CreateLayersForPluginTool(document);
            new AcadTools().ChangeCurrentLayers();
            new AcadTools().SetDefaultPdMode(document);

            var a = new AcadTools();
            while (a.CreateAxisPointBlock(document)) { };
            new AcadTools().ChangeCurrentLayers();
        }
        [CommandMethod("buildheight")]
        public void Buildheight()
        {
            Func<string> GetAnyIniqueBlockName = () => DateTime.Now.Ticks.ToString();

            var document = Autodesk.AutoCAD.ApplicationServices
               .Application.DocumentManager.MdiActiveDocument;

            new AcadTools().CreateLayersForPluginTool(document);
            new AcadTools().ChangeCurrentLayers();
            new AcadTools().SetDefaultPdMode(document);

            var a = new AcadTools();
            while (a.CreateHeightPointBlock(document)) { };
            new AcadTools().ChangeCurrentLayers();
        }
        [CommandMethod("buildbottom")]
        public void Buildbottom()
        {
            Func<string> GetAnyIniqueBlockName = () => DateTime.Now.Ticks.ToString();

            var document = Autodesk.AutoCAD.ApplicationServices
               .Application.DocumentManager.MdiActiveDocument;

            new AcadTools().CreateLayersForPluginTool(document);
            new AcadTools().ChangeCurrentLayers();
            new AcadTools().SetDefaultPdMode(document);

            var a = new AcadTools();
            while (a.CreateBottomPointBlock(document)) { };

            new AcadTools().ChangeCurrentLayers();
        }
        [CommandMethod("buildtop")]
        public void Buildtop()
        {
            Func<string> GetAnyIniqueBlockName = () => DateTime.Now.Ticks.ToString();

            var document = Autodesk.AutoCAD.ApplicationServices
               .Application.DocumentManager.MdiActiveDocument;

            new AcadTools().CreateLayersForPluginTool(document);
            new AcadTools().ChangeCurrentLayers();
            new AcadTools().SetDefaultPdMode(document);

            var a = new AcadTools();
            while (a.CreateTopPointBlock(document)) { };

            new AcadTools().ChangeCurrentLayers();
        }
    }
}

