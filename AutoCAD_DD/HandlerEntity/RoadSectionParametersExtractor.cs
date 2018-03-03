using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Collections.Generic;
using System.Linq;
using SectionConverterPlugin;
using System.IO;


using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.Runtime;
using System.Data;
using System.Xml.Serialization;

namespace SectionConverterPlugin.HandlerEntity
{
    public class RoadSectionParametersExtractor
    {

        public List<BlockTableRecord> GetListBlocksByPrefix(string prefix)
        {
            List<BlockTableRecord> blocks = new List<BlockTableRecord>();

            var document = Autodesk.AutoCAD.ApplicationServices
                .Application.DocumentManager.MdiActiveDocument;

            var database = document.Database;

            using (var transaction = database.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);

                foreach (ObjectId id in blockTable)
                {
                    BlockTableRecord blockTableRecord = (BlockTableRecord)transaction.GetObject(id, OpenMode.ForRead);

                    if (CheckPrefixNameOfBlock(prefix, blockTableRecord.Name))
                        blocks.Add(blockTableRecord);
                }

                transaction.Commit();
            }
            return blocks;
        }

        public bool CheckPrefixNameOfBlock(string prefix, string blockName)
        {
            if (blockName.Substring(0, blockName.IndexOf('_') + 1) == prefix)
                return true;

            return false;
        }

        [CommandMethod("GroupBlocks")]
        public void GroupBlocksToList()
        {
            List<BlockTableRecord> axisBlocks = new List<BlockTableRecord>(GetListBlocksByPrefix("axisPoint_"));
            List<BlockTableRecord> heightBlocks = new List<BlockTableRecord>(GetListBlocksByPrefix("heightPoint_"));
            List<BlockTableRecord> topBlocks = new List<BlockTableRecord>(GetListBlocksByPrefix("topPoint_"));
            List<BlockTableRecord> bottomBlocks = new List<BlockTableRecord>(GetListBlocksByPrefix("bottomPoint_"));
        }   
    }
}

