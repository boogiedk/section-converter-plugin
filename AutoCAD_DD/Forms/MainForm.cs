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

            //LoadingExternalDrawlings.InsertBlock();
        }

        private void PluginN1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var document = Autodesk.AutoCAD.ApplicationServices
              .Application.DocumentManager.MdiActiveDocument;

            var a = new CreateFigures();

            while (a.CreateAxisPointBlock(document)) { };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Get the current document and database

            var document = Autodesk.AutoCAD.ApplicationServices
               .Application.DocumentManager.MdiActiveDocument;
            var database = document.Database;

            // Start a transaction
            using (Transaction acTrans = document.TransactionManager.StartTransaction())
            {
                // Open the Block table for read
                BlockTable acBlkTbl;
                acBlkTbl = acTrans.GetObject(database.BlockTableId,
                                                OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                BlockTableRecord acBlkTblRec;
                acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                OpenMode.ForWrite) as BlockTableRecord;

                // Create a single-line text object
                using (DBText acText = new DBText())
                {
                    acText.Position = new Point3d(2, 2, 0);
                    acText.Height = 0.5;
                    acText.TextString = "Hello, World.";

                    acBlkTblRec.AppendEntity(acText);
                    acTrans.AddNewlyCreatedDBObject(acText, true);
                }

                // Save the changes and dispose of the transaction
                acTrans.Commit();
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
              // Перерисовка чертежа
              acad.UpdateScreen();
              acad.DocumentManager.MdiActiveDocument.Editor.UpdateScreen();
              // Регенерация чертежа
              acad.DocumentManager.MdiActiveDocument.Editor.Regen();
              this.Hide();
        }
    }
}
