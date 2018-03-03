using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Collections.Generic;
using System.Linq;
using SectionConverterPlugin;

using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.Runtime;
using System.Data;

namespace SectionConverterPlugin.HandlerEntity
{
    public class RoadSectionParametersExtractor
    {
        [CommandMethod("LISTATT")]
        public void ListAttributes()
        {
            var document = Autodesk.AutoCAD.ApplicationServices
 .Application.DocumentManager.MdiActiveDocument;

            Database database = document.Database;

            Editor editor = document.Editor;

            Database db = HostApplicationServices.WorkingDatabase;

            Transaction tr = db.TransactionManager.StartTransaction();

            // Start the transaction
            try
            {
                // Build a filter list so that only

                // block references are selected

                TypedValue[] filterList = new TypedValue[1] {
                    new TypedValue((int)DxfCode.Start, "INSERT")};
                // new TypedValue((int)DxfCode.Start,"axis"),
                // new TypedValue((int)DxfCode.Start,"height")};

                SelectionFilter filter = new SelectionFilter(filterList);
                PromptSelectionOptions opts = new PromptSelectionOptions();

                opts.MessageForAdding = "Select block references: ";

                PromptSelectionResult res = editor.GetSelection(opts, filter);


                if (res.Status != PromptStatus.OK)

                    return;

                SelectionSet selSet = res.Value;

                ObjectId[] idArray = selSet.GetObjectIds();

                foreach (ObjectId blkId in idArray)
                {
                    BlockReference blkRef = (BlockReference)tr.GetObject(blkId, OpenMode.ForRead);
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(blkRef.BlockTableRecord, OpenMode.ForRead);

                    editor.WriteMessage(
                      "\nBlock: " + btr.Name);

                    btr.Dispose();


                    AttributeCollection attCol = blkRef.AttributeCollection;

                    foreach (ObjectId attId in attCol)
                    {
                        AttributeReference attRef = (AttributeReference)tr.GetObject(attId, OpenMode.ForRead);
                        string str = ("\n  Attribute Tag: " + attRef.Tag + "\n    Attribute String: " + attRef.TextString);

                        editor.WriteMessage(str);

                    }
                }
                tr.Commit();
            }

            catch (Autodesk.AutoCAD.Runtime.Exception ex)

            {

                editor.WriteMessage(("Exception: " + ex.Message));

            }

            finally
            {
                tr.Dispose();
            }
        } // получить имя и атрибуты блока по клику

        [CommandMethod("getpoint")]
        public void GetCoordsBlock()
        {
            var document = Autodesk.AutoCAD.ApplicationServices
.Application.DocumentManager.MdiActiveDocument;

            var database = document.Database;

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                BlockTable blockTable;
                blockTable = transaction.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;

                BlockTableRecord blockTableRecord;
                blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace],
                                                         OpenMode.ForRead) as BlockTableRecord;

                if (blockTable[BlockTableRecord.ModelSpace.Substring(0, 9)].ToString() == "axisPoint_")
                    MessageBox.Show(BlockTableRecord.ModelSpace.Substring(0, 9));
                else
                    MessageBox.Show("try again");
                    Point3d coords = AcadTools.GetBlockPosition(blockTableRecord);

                    transaction.Commit();
            }
        }
    }
}
