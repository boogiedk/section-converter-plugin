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
        Point3d point;

        public PluginN1()
        {
            InitializeComponent();
        }

        [CommandMethod("Test")]
        public void Test()
        {
            DialogResult result = acad.ShowModalDialog(this);
            if (result == DialogResult.OK)
                acad.ShowAlertDialog("{point.X}\n{point.Y}\n{point.Z}");
        }

        private void button2_Click(object sender, EventArgs e) // Circle 
        {
            MessageBox.Show("Not Implemented");
            //CreateFigures f = new CreateFigures();
            //f.CreateCircle();
        }

        private void button1_Click(object sender, MouseEventArgs e) // Get point 
        {
            var editor = acad.DocumentManager.MdiActiveDocument.Editor;
            var ptRes = editor.GetPoint("\nPick a point: ");
            if (ptRes.Status == PromptStatus.OK)
            {
                point = ptRes.Value;
                txtX.Text = point.X.ToString();
                txtY.Text = point.Y.ToString();
                txtZ.Text = point.Z.ToString();
            }
        }

        private void button2_Click_1(object sender, EventArgs e) // create block 
        {
            MessageBox.Show("Not Implemented");
            //CreateFigures f = new CreateFigures();
            //f.CreateAxisPointMarkBlock(textBoxName.Text, textBoxDesc.Text);
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

