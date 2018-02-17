using System;
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

        [CommandMethod("test")]
        public void Test()
        {
            DialogResult result = acad.ShowModalDialog(this);
            if (result == DialogResult.OK)
                acad.ShowAlertDialog("{point.X}\n{point.Y}\n{point.Z}");
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

            while (a.CreateAxisPointBlock(document, GetAnyIniqueBlockName)) { };

            //LoadingExternalDrawlings.InsertBlock();
        }

        private void PluginN1_Load(object sender, EventArgs e)
        {

        }
    }
}

