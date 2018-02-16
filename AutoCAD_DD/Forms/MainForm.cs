using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;

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
    }
}

