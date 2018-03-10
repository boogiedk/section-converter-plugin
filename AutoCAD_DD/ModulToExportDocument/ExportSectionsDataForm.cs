using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;
using System.Linq;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

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
                        RedPoints = topSectionPoints.ToList(),
                        BlackPoints = bottomSectionPoints.ToList()
                    };
                })
                .Where(sectionData => sectionData != null)
                .ToArray();
        }

        //TODO: fix refular expr ^
        public static double GetStationValue(string mtext)
        {
            var _stationRegex =
     new Regex(@"^(((?<hundreds>\d+)\+(?<units>\d{1,2}))|(?<all_units>\d+))([,\.](?<fractional>\d+))?$");

            MatchCollection mc = _stationRegex.Matches(mtext.Substring(3, mtext.Length - 3));

            var match = mc[0];

            double station = .0;

            double unit = .0;
            double hungred = .0;

            if (match.Groups["hundreds"].Length > 0)
            {
                hungred = 100 * StringToDouble(match.Groups["hundreds"].Value);
                unit = StringToDouble(match.Groups["units"].Value);
            }
            else
            {
                unit += StringToDouble(match.Groups["all_units"].Value);
            }
            if (match.Groups["fractional"].Length > 0)
            {
                var fractional = StringToDouble(match.Groups["fractional"].Value);

                while (fractional > 1.0 && fractional != .0)
                {
                    fractional /= 10.0;
                }

                unit += fractional;
            }

            var sign = Math.Sign(unit);
            var abs = Math.Abs(hungred);

            station = sign * (abs / 100) + (abs % 100);

            return station;
        }

        private static double StringToDouble(string s)
        {
            return Double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);
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

                foreach (var entity in blockTableRecord)
                {
                    var currentEntity = transaction.GetObject(entity, OpenMode.ForRead) as MText;

                    if (currentEntity == null) continue;

                    var mText = transaction.GetObject(entity, OpenMode.ForRead) as MText;
                    _axisPoint = GetStationValue(mText.Contents);

                    break;
                }
                transaction.Commit();
            }
            return _axisPoint;
        }

        public static double GetHeightFromPointBlock(BlockTableRecord heightPointBlock)
        {
            var document = Autodesk.AutoCAD.ApplicationServices
                          .Application.DocumentManager.MdiActiveDocument;

            var database = document.Database;

            double _heightPoint = Double.NaN;

            using (var transaction = database.TransactionManager.StartTransaction())
            {
                var blockTableRecord = heightPointBlock;

                foreach (var entity in blockTableRecord)
                {
                    var currentEntity = transaction.GetObject(entity, OpenMode.ForRead) as MText;

                    if (currentEntity == null) continue;

                    var mText = transaction.GetObject(entity, OpenMode.ForRead) as MText;

                    string _heightPointString = mText.Contents.Substring(0, mText.Contents.IndexOf('м') - 1);

                    _heightPoint = double.Parse(_heightPointString, CultureInfo.InvariantCulture);

                    break;

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

                foreach (var entity in blockTableRecord)
                {
                    var currentEntity = transaction.GetObject(entity, OpenMode.ForRead) as MText;

                    if (currentEntity == null) continue;

                    if (currentEntity.GetType() == typeof(MText))
                    {
                        var mText = transaction.GetObject(entity, OpenMode.ForRead) as MText;

                        _pointNumber = Int32.Parse(mText.Contents);
                        break;
                    }
                }
                transaction.Commit();
            }
            return _pointNumber;
        }
    }
}
