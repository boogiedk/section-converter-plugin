using Autodesk.AutoCAD.Runtime;
using System;

using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;

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

            new AcadTools().ChangeCurrentLayers();
            new AcadTools().DefaultPdMode(document);

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

            new AcadTools().ChangeCurrentLayers();
            new AcadTools().DefaultPdMode(document);

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

            new AcadTools().ChangeCurrentLayers();
            new AcadTools().DefaultPdMode(document);

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

            new AcadTools().ChangeCurrentLayers();
            new AcadTools().DefaultPdMode(document);

            var a = new AcadTools();
            while (a.CreateTopPointBlock(document)) { };

            new AcadTools().ChangeCurrentLayers();
        }

        [CommandMethod("axisdata")]
        public void axisdata()
        {
            Forms.InputAxisDataForm test = new Forms.InputAxisDataForm();
            test.ShowDialog();
        }
    }
}
