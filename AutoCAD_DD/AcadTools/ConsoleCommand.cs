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

            AcadTools.CreateLayersForPluginTool(document);
            AcadTools.ChangeCurrentLayers();

            while (AcadTools.CreateAxisPointBlock(document)) { };

            AcadTools.ChangeCurrentLayers();
        }
        [CommandMethod("buildheight")]
        public void Buildheight()
        {
            Func<string> GetAnyIniqueBlockName = () => DateTime.Now.Ticks.ToString();

            var document = Autodesk.AutoCAD.ApplicationServices
               .Application.DocumentManager.MdiActiveDocument;

            AcadTools.CreateLayersForPluginTool(document);
            AcadTools.ChangeCurrentLayers();

            while (AcadTools.CreateHeightPointBlock(document)) { };

            AcadTools.ChangeCurrentLayers();
        }
        [CommandMethod("buildbottom")]
        public void BuildBottom()
        {
            BuildRoadPoint(AcadTools.CreateBottomPointBlock);
        }
        [CommandMethod("buildtop")]
        public void BuildTop()
        {
            BuildRoadPoint(AcadTools.CreateTopPointBlock);
        }

        private void BuildRoadPoint(Func<Document, int, bool> CreatePointBlockDialog)
        {
            var document = Autodesk.AutoCAD.ApplicationServices
               .Application.DocumentManager.MdiActiveDocument;

            int pointNumber = 0;
            if (!AcadTools.GetPointNumberDialog(out pointNumber)) return;

            AcadTools.CreateLayersForPluginTool(document);
            AcadTools.ChangeCurrentLayers();

            while (CreatePointBlockDialog(document, pointNumber))
            {
                pointNumber++;
            };

            AcadTools.ChangeCurrentLayers();
        }
    }
}

