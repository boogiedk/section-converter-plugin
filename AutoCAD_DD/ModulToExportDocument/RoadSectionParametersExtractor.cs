using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using System.Linq;
using System.Windows.Forms;
using System;
using System.Globalization;

using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.EditorInput;

namespace SectionConverterPlugin.HandlerEntity
{
    public static class RoadSectionParametersExtractor
    {
        public static List<BlockTableRecord> GetListBlocksByPrefix(string prefix)
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

        private static bool CheckPrefixNameOfBlock(string prefix, string blockName)
        {
            if (!(blockName == null && blockName == ""))
                if (blockName.Substring(0, blockName.IndexOf('_') + 1) == prefix)
                    return true;

            return false;
        }

        public static bool CheckBlockInWindow(BlockTableRecord block, Point3d origin, Size windowSize)
        {
            var blockLocalPos = AcadTools.GetBlockPosition(block) - origin;

            var windowWidth = windowSize.Width;
            var windowHeight = windowSize.Height;

            return -windowWidth / 2.0 <= blockLocalPos.X &&
                blockLocalPos.X < windowWidth / 2.0 &&
                -windowHeight / 2.0 <= blockLocalPos.Y &&
                blockLocalPos.Y < windowHeight / 2.0;
        }

        public static SectionData[] ExtractSectionsData(Size searchWindowSize)
        {
            var axisPoints = GetListBlocksByPrefix("axisPoint_");
            var heightPoints = GetListBlocksByPrefix("heightPoint_");
            var topPoints = GetListBlocksByPrefix("redPoint_");
            var bottomPoints = GetListBlocksByPrefix("blackPoint_");

            var data = new SectionData[0];

            if (axisPoints.Count == 0)
            {
                return data;
            }

            return axisPoints.
                Select(axisPoint =>
                {
                    var sectionOrigin = AcadTools.GetBlockPosition(axisPoint);

                    var heightSectionPoints = heightPoints
                        .Where(point => CheckBlockInWindow(point, sectionOrigin, searchWindowSize));
                    if (heightSectionPoints.Count() != 1) return null;
                    var heightSectionPoint = heightSectionPoints.First();

                    var topSectionPoints = topPoints
                        .Where(point => CheckBlockInWindow(point, sectionOrigin, searchWindowSize));
                    if (topSectionPoints.Count() < 2) return null;

                    var bottomSectionPoints = bottomPoints
                        .Where(point => CheckBlockInWindow(point, sectionOrigin, searchWindowSize));

                    return new SectionData()
                    {
                        AxisPoint = axisPoint,
                        HeightPoint = heightSectionPoint,
                        TopPoints = topSectionPoints.ToList(),
                        BottomPoints = bottomSectionPoints.ToList()
                    };

                })
                .Where(sectionData => sectionData != null)
                .ToArray();
        }

        public static double GetStationFromPointBlock(BlockTableRecord axisPointBlock)
        {
            var document = Autodesk.AutoCAD.ApplicationServices
                          .Application.DocumentManager.MdiActiveDocument;

            var database = document.Database;

            double _axisPoint = Double.NaN;

            using (var transaction = database.TransactionManager.StartTransaction())
            {
                var blockTableRecord = axisPointBlock;

                foreach (ObjectId entity in blockTableRecord)
                {
                    var mText = (MText)transaction.GetObject(entity, OpenMode.ForRead);

                    string _axisPointStringUnit = mText.Contents.Substring(2, mText.Contents.IndexOf('+') - 1);
                    string _axisPointStringHungred = mText.Contents.Substring(mText.Contents.IndexOf('+') + 1, mText.Contents.Length - mText.Contents.IndexOf('+'));

                    var sign = Math.Sign(double.Parse(_axisPointStringUnit));
                    var abs = Math.Abs(double.Parse(_axisPointStringHungred));

                    _axisPoint = sign * (abs / 100) + (abs % 100);
                }
                transaction.Commit();
            }
            return _axisPoint;
        }

        // TODO: пикета в точке с высотой нет, там только высота
        // пофиксить точка/запятая
        public static double GetHeightFromPointBlock(BlockTableRecord heightPointBlock)
        {
            var document = Autodesk.AutoCAD.ApplicationServices
                          .Application.DocumentManager.MdiActiveDocument;

            var database = document.Database;

            double _heightPoint = Double.NaN;

            using (var transaction = database.TransactionManager.StartTransaction())
            {
                var blockTableRecord = heightPointBlock;

                foreach (ObjectId entity in blockTableRecord)
                {
                    var mText = (MText)transaction.GetObject(entity, OpenMode.ForRead);

                    string _heightPointString = mText.Contents.Substring(0, mText.Contents.IndexOf('м') - 1);

                    _heightPoint = double.Parse(_heightPointString, CultureInfo.InvariantCulture);
                }
                transaction.Commit();
            }
            return _heightPoint;
        }

        public static int GetPointNumberFromPointBlock(BlockTableRecord pointNumberBlock)
        {
            var document = Autodesk.AutoCAD.ApplicationServices
                          .Application.DocumentManager.MdiActiveDocument;

            var database = document.Database;

            var _pointNumber = -1;

            using (var transaction = database.TransactionManager.StartTransaction())
            {
                var blockTableRecord = pointNumberBlock;

                foreach (ObjectId entity in blockTableRecord)
                {
                    var mText = (MText)transaction.GetObject(entity, OpenMode.ForRead);

                    _pointNumber = Int32.Parse(mText.Contents);
                }
                transaction.Commit();
            }
            return _pointNumber;
        }
    }
}
