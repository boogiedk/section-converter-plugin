using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Interop.Common;
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
    }
}

