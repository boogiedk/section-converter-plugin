using System;
using Autodesk.AutoCAD.ApplicationServices;
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
        [CommandMethod("buildblack")]
        public void BuildBottom()
        {
            BuildRoadPoint(AcadTools.CreateBlackPointBlock);
        }
        [CommandMethod("buildred")]
        public void BuildTop()
        {
            BuildRoadPoint(AcadTools.CreateRedPointBlock);
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

