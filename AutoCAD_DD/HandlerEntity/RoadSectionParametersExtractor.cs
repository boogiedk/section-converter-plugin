using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using System.Linq;

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
                var blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);

                foreach (ObjectId id in blockTable)
                {
                    var blockTableRecord = (BlockTableRecord)transaction.GetObject(id, OpenMode.ForRead);

                    if (CheckPrefixNameOfBlock(prefix, blockTableRecord.Name))
                        blocks.Add(blockTableRecord);
                }

                transaction.Commit();
            }
            return blocks;
        }

        private bool CheckPrefixNameOfBlock(string prefix, string blockName)
        {
            if (!(blockName == null))
                if (blockName.Substring(0, blockName.IndexOf('_') + 1) == prefix)
                    return true;

            return false;
        }

        public bool CheckBlockInWindow(BlockTableRecord block, Point3d origin, Point3d windowSize)
        {
            var blockLocalPos = AcadTools.GetBlockPosition(block) - origin;

            var windowWidth = windowSize.X;
            var windowHeight = windowSize.Y;

            return -windowWidth / 2.0 <= blockLocalPos.X &&
                blockLocalPos.X < windowWidth / 2.0 &&
                -windowHeight / 2.0 <= blockLocalPos.Y &&
                blockLocalPos.Y < windowHeight / 2.0;
        }

        [CommandMethod("CreateListsOfBlocks")]
        public void CreateListsOfBlocks()
        {
            var axisPoints = new List<BlockTableRecord>(GetListBlocksByPrefix("axisPoint_"));
            var heightPoints = new List<BlockTableRecord>(GetListBlocksByPrefix("heightPoint_"));
            var topPoints = new List<BlockTableRecord>(GetListBlocksByPrefix("topPoint_"));
            var bottomPoints = new List<BlockTableRecord>(GetListBlocksByPrefix("bottomPoint_"));

            // TODO:
            // to config or settings
            Point3d windowSize = new Point3d(50, 50, 0);

            var data = axisPoints.
                Select(axisPoint =>
                {
                    var origin = AcadTools.GetBlockPosition(axisPoint);

                    return new SectionData()
                    {
                        AxisPoint = axisPoint,
                        HeightPoint = heightPoints
                            .First(point => CheckBlockInWindow(point, origin, windowSize)),
                        TopPoints = topPoints
                            .Where(point => CheckBlockInWindow(point, origin, windowSize))
                            .ToList(),
                        BottomPoints = bottomPoints
                            .Where(point => CheckBlockInWindow(point, origin, windowSize))
                            .ToList()
                    };
                });
        }
    }
}
