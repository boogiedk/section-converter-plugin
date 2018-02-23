using System;
using System.IO;
using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

using acad = Autodesk.AutoCAD.ApplicationServices.Application;

namespace SectionConverterPlugin
{
    public partial class PluginN1 : Form
    {
        public PluginN1()
        {
            InitializeComponent();
        }

        [CommandMethod("RunPlugin")]
        public void RunPlugin()
        {
            DialogResult result = acad.ShowModalDialog(this);
            if (result == DialogResult.OK)
                acad.ShowAlertDialog("");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Func<string> GetAnyIniqueBlockName = () => DateTime.Now.Ticks.ToString();
            var document = Autodesk.AutoCAD.ApplicationServices
                .Application.DocumentManager.MdiActiveDocument;

            new CreateFigures().CreateAxisPointMarkBlocks(
                document,
                GetAnyIniqueBlockName);
        }

        private void btn_createpoint_Click(object sender, EventArgs e)
        {
            var document = Autodesk.AutoCAD.ApplicationServices
                .Application.DocumentManager.MdiActiveDocument;
            new CreateFigures().CreateAxisPoint(document);
        }

        private void btn_execute_Click(object sender, EventArgs e)
        {
            Func<string> GetAnyIniqueBlockName = () => DateTime.Now.Ticks.ToString();

            var document = Autodesk.AutoCAD.ApplicationServices
               .Application.DocumentManager.MdiActiveDocument;

            var a = new CreateFigures();
            while (a.CreateAxisPointBlock(document)) { };

        }

        private void PluginN1_Load(object sender, EventArgs e)
        {

        }

        private void btn_Height_Click(object sender, EventArgs e)
        {
            Func<string> GetAnyIniqueBlockName = () => DateTime.Now.Ticks.ToString();

            var document = Autodesk.AutoCAD.ApplicationServices
               .Application.DocumentManager.MdiActiveDocument;

            var a = new CreateFigures();
          //  while (a.CreateAxisPointMark(document)) { };
        }

        private void btn_Bottom_Click(object sender, EventArgs e)
        {

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            new CreateFigures().UpdateCurrentScreen();
              this.Hide();
        }

        [CommandMethod("buildaxis")]
        public void buildaxis()
        {
            Func<string> GetAnyIniqueBlockName = () => DateTime.Now.Ticks.ToString();

            var document = Autodesk.AutoCAD.ApplicationServices
               .Application.DocumentManager.MdiActiveDocument;

            var a = new CreateFigures();
            while (a.CreateAxisPointBlock(document)) { };
        }
        [CommandMethod("buildheight")]
        public void buildheight()
        {
            Func<string> GetAnyIniqueBlockName = () => DateTime.Now.Ticks.ToString();

            var document = Autodesk.AutoCAD.ApplicationServices
               .Application.DocumentManager.MdiActiveDocument;

            var a = new CreateFigures();
            while (a.CreateHeightPointBlock(document)) { };
        }
        [CommandMethod("buildbottom")]
        public void buildbottom()
        {
            Func<string> GetAnyIniqueBlockName = () => DateTime.Now.Ticks.ToString();

            var document = Autodesk.AutoCAD.ApplicationServices
               .Application.DocumentManager.MdiActiveDocument;

            var a = new CreateFigures();
            while (a.CreateBottomPointBlock(document)) { };
        }
        [CommandMethod("buildtop")]
        public void buildtop()
        {
            Func<string> GetAnyIniqueBlockName = () => DateTime.Now.Ticks.ToString();

            var document = Autodesk.AutoCAD.ApplicationServices
               .Application.DocumentManager.MdiActiveDocument;

            var a = new CreateFigures();
            while (a.CreateTopPointBlock(document)) { };
        }
        [CommandMethod("axisdata")]
        public void axisdata()
        {
            Forms.InPutAxisData test = new Forms.InPutAxisData();
            test.ShowDialog();
        }
    }
}
