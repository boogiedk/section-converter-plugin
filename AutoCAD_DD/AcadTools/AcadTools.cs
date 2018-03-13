using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using SectionConverterPlugin.Forms;
using Autodesk.AutoCAD.Colors;

using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.Runtime;

namespace SectionConverterPlugin
{
    public class AcadTools
    {

        #region Temporal Methods

        private static string GetAnyIniqueBlockName(BlockTable blockTable)
        {
            Func<string> GetAnyIniqueBlockName = () => DateTime.Now.Ticks.ToString();

            var blockName = GetAnyIniqueBlockName();
            while (blockTable.Has(blockName))
            {
                blockName = GetAnyIniqueBlockName();
            }
            return blockName;
        }

        #endregion
        
        #region GUI Dialogs

        public static bool GetPointAcadDialog(Editor editor, out Point3d point3D)
        {
            bool result = false;
            point3D = new Point3d(
                Double.NaN,
                Double.NaN,
                Double.NaN);

            var getPointAcadDialogResult = editor.GetPoint("\nPick a point:");

            if (getPointAcadDialogResult.Status != PromptStatus.OK)
            {
                return result;
            }

            point3D = getPointAcadDialogResult.Value;
            result = true;

            return result;
        }
        public static bool GetStationDialog(out double station)
        {
            bool result = false;
            station = Double.NaN;

            var dialogForm = new InputStationDialog();
            var dialogResult = dialogForm.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return result;
            }

            station = dialogForm.Station;
            result = true;

            return result;
        }
        public static bool GetHeightDialog(out double height)
        {
            bool result = false;
            height = Double.NaN;

            var dialogForm = new InputHeightDialog();
            var dialogResult = dialogForm.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return result;
            }

            height = dialogForm.PointHeight;
            result = true;

            return result;
        }
        public static bool GetPointNumberDialog(out int pointNumber)
        {
            bool result = false;
            pointNumber = 0;

            var dialogForm = new InputPointNumberDialog();
            var dialogResult = dialogForm.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return result;
            }

            pointNumber = dialogForm.PointNumber;
            result = true;

            return result;
        }

        #endregion

        #region Acad Points Templates

        public static BlockTableRecord GetAxisPointBlockTemplate(Database documentDatabase)
        {
            var entities = new List<Entity>();

            DBPoint dbPoint = new DBPoint(new Point3d(0, 0, 0));
            //dbPoint.SetDatabaseDefaults();

            dbPoint.Color = Color.FromRgb(243, 5, 255);
            entities.Add(dbPoint);

            var text = new MText();
            text.Location = new Point3d(0.05, 0, 0);
            text.Attachment = AttachmentPoint.MiddleLeft;
            text.TextHeight = 0.05;
            text.Color = dbPoint.Color;
            text.Contents = "axisPoint_";
            entities.Add(text);

            var block = CreateNewBlock(documentDatabase);
            SetBlockEntities(block, entities);

            return block;
        }
        public static BlockTableRecord GetHeightPointTemplate(Database documentDatabase)
        {
            var entities = new List<Entity>();

            var dbPoint = new DBPoint(new Point3d(0, 0, 0));
            //dbPoint.SetDatabaseDefaults();

            dbPoint.Color = Color.FromRgb(76, 255, 5);
            entities.Add(dbPoint);


            var text = new MText();
            text.Location = new Point3d(0.05, 0, 0);
            text.Attachment = AttachmentPoint.MiddleLeft;
            text.TextHeight = 0.05;
            text.Color = dbPoint.Color;
            text.Contents = "heightPoint_";
            entities.Add(text);

            var block = CreateNewBlock(documentDatabase);
            SetBlockEntities(block, entities);

            return block;
        }
        public static BlockTableRecord GetBlackPointTemplate(Database documentDatabase)
        {
            var entities = new List<Entity>();

            DBPoint dbPoint = new DBPoint(new Point3d(0, 0, 0));
            //dbPoint.SetDatabaseDefaults();

            dbPoint.Color = Color.FromColorIndex(ColorMethod.ByLayer, 256);
            entities.Add(dbPoint);

            var text = new MText();
            text.Location = new Point3d(0.05, 0, 0);
            text.Attachment = AttachmentPoint.MiddleLeft;
            text.TextHeight = 0.05;
            text.Color = dbPoint.Color;
            text.Contents = "blackPoint_";
            entities.Add(text);


            var block = CreateNewBlock(documentDatabase);
            SetBlockEntities(block, entities);

            return block;
        }
        public static BlockTableRecord GetRedPointTemplate(Database documentDatabase)
        {
            var entities = new List<Entity>();

            DBPoint dbPoint = new DBPoint(new Point3d(0, 0, 0));
            //dbPoint.SetDatabaseDefaults();

            dbPoint.Color = Color.FromRgb(255, 0, 0);
            entities.Add(dbPoint);

            var text = new MText();
            text.Location = new Point3d(0.05, 0, 0);
            text.Attachment = AttachmentPoint.MiddleLeft;
            text.TextHeight = 0.05;
            text.Color = dbPoint.Color;
            text.Contents = "redPoint_";
            entities.Add(text);

            var block = CreateNewBlock(documentDatabase);
            SetBlockEntities(block, entities);

            return block;
        }

        #endregion

        #region Block methods

        public static BlockTableRecord CreateNewBlock(
            Database documentDatabase)
        {

            BlockTableRecord block = null;

            using (var transaction =
                    documentDatabase.TransactionManager.StartTransaction())
            {
                var blockTable = (BlockTable)transaction.GetObject(
                    documentDatabase.BlockTableId,
                    OpenMode.ForWrite);

                block = new BlockTableRecord()
                {
                    Name = GetAnyIniqueBlockName(blockTable)
                };

                var blockTableID = blockTable.Add(block);
                transaction.AddNewlyCreatedDBObject(block, true);

                BlockTableRecord modelSpaceTableRecord = (BlockTableRecord)transaction.GetObject(
                    blockTable[BlockTableRecord.ModelSpace],
                    OpenMode.ForWrite);

                BlockReference blockReference = new BlockReference(
                    new Point3d(0, 0, 0),
                    blockTableID);

                modelSpaceTableRecord.AppendEntity(blockReference);
                transaction.AddNewlyCreatedDBObject(blockReference, true);

                transaction.Commit();
            }

            return block;
        }

        public static Point3d GetBlockPosition(
            BlockTableRecord block)
        {
            Database documentDatabase = block.Database;
            Point3d position = new Point3d(double.NaN, double.NaN, double.NaN);

            using (var transaction =
                    documentDatabase.TransactionManager.StartTransaction())
            {
                var blockTable = (BlockTable)transaction.GetObject(
                    documentDatabase.BlockTableId,
                    OpenMode.ForRead);

                var blockReference = (BlockReference)transaction.GetObject(
                    block.GetBlockReferenceIds(true, true)[0],
                    OpenMode.ForRead);

                position = blockReference.Position;

                transaction.Commit();
            }

            return position;
        }
        public static void SetBlockPosition(
            BlockTableRecord block,
            Point3d position)
        {
            Database documentDatabase = block.Database;

            using (var transaction =
                    documentDatabase.TransactionManager.StartTransaction())
            {
                var blockTable = (BlockTable)transaction.GetObject(
                    documentDatabase.BlockTableId,
                    OpenMode.ForWrite);

                var blockReference = (BlockReference)transaction.GetObject(
                    block.GetBlockReferenceIds(true, true)[0],
                    OpenMode.ForWrite);

                blockReference.Position = position;

                transaction.Commit();
            }
        }

        public static string GetBlockName(
            BlockTableRecord block)
        {
            return block.Name;
        }
        public static void SetBlockName(
            BlockTableRecord block,
            string name)
        {
            Database documentDatabase = block.Database;

            using (var transaction =
                    documentDatabase.TransactionManager.StartTransaction())
            {
                var blockTable = (BlockTable)transaction.GetObject(
                    documentDatabase.BlockTableId,
                    OpenMode.ForWrite);

                var blockTableRecord = (BlockTableRecord)transaction.GetObject(
                    block.Id,
                    OpenMode.ForWrite);

                blockTableRecord.Name = name;

                transaction.Commit();
            }
        }

        public static List<Entity> GetBlockEntities(
            BlockTableRecord block)
        {
            Database documentDatabase = block.Database;
            List<Entity> entities = new List<Entity>();

            using (var transaction =
                    documentDatabase.TransactionManager.StartTransaction())
            {
                var blockTable = (BlockTable)transaction.GetObject(
                    documentDatabase.BlockTableId,
                    OpenMode.ForRead);

                foreach (var entityID in block)
                {
                    var entity = (Entity)transaction.GetObject(
                    entityID,
                    OpenMode.ForRead);

                    entities.Add(entity);
                }

                transaction.Commit();
            }

            return entities;
        }

        public static void SetBlockEntities(
            BlockTableRecord block,
            List<Entity> entities)
        {
            Database documentDatabase = block.Database;

            using (var transaction =
                    documentDatabase.TransactionManager.StartTransaction())
            {
                var blockTable = (BlockTable)transaction.GetObject(
                    documentDatabase.BlockTableId,
                    OpenMode.ForWrite);

                var blockTableRecord = (BlockTableRecord)transaction.GetObject(
                    block.Id,
                    OpenMode.ForWrite);

                foreach (var entity in entities)
                {
                    blockTableRecord.AppendEntity(entity);

                    try
                    {
                        transaction.AddNewlyCreatedDBObject(entity, true);
                    }
                    catch
                    {
                        // well, already created. ok.
                    }
                }

                transaction.Commit();
            }
        }

        public static void RefreshBlockGraphics(
            BlockTableRecord block)
        {
            Database documentDatabase = block.Database;

            using (var transaction =
                    documentDatabase.TransactionManager.StartTransaction())
            {
                var blockReference = (BlockReference)transaction.GetObject(
                    block.GetBlockReferenceIds(true, true)[0],
                    OpenMode.ForWrite);

                // REQUED FOR UPDATE!!!!
                blockReference.Position = blockReference.Position;

                transaction.Commit();
            }
        }

        #endregion

        public static MText GetAnyMText(List<Entity> entities, string startWith = "")
        {
            var mtexts = entities
                .Select(e => e as MText)
                .Where(e => e != null);

            return startWith == "" ?
                mtexts.First() :
                mtexts.First(mt => mt.Text.StartsWith(startWith));
        }
        public static void ApplyFunction(Entity entity, Action<Entity> Function)
        {
            var documentDatabase = entity.Database;

            using (var transaction =
                    documentDatabase.TransactionManager.StartTransaction())
            {
                var entityOpened = (Entity)transaction.GetObject(
                    entity.Id,
                    OpenMode.ForWrite);

                Function(entityOpened);

                transaction.Commit();
            }
        }

        public static string FormatStation(double station)
        {
            var roundedStation = Math.Round(station, 3);

            var sign = Math.Sign(roundedStation);
            var abs = Math.Abs(roundedStation);

            return String.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "ПК {0}+{1:00.000}",
                sign * (int)abs / 100,
                abs % 100);
        }
        public static string FormatHeight(double height)
        {
            return String.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "{0:0.000}м", height);
        }
        public static string FormatPointNumber(int pointNumber)
        {
            return String.Format("{0}", pointNumber);
        }

        public static string DoubleToFormattedString(double num)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:0.000}", num);
        }

        private static void SetTextParams(
            BlockTableRecord block,
            string paramsTextPrefix,
            Func<string> GetParamsText)
        {
            ApplyFunction(
                GetAnyMText(AcadTools.GetBlockEntities(block), paramsTextPrefix),
                e =>
                {
                    var text = (MText)e;
                    text.Contents = GetParamsText();
                });

            RefreshBlockGraphics(block);
        }

        public static bool CreateAxisPointBlock(Document document)
        {
            bool result = false;

            CreateLayersForPluginTool(document);

            var blockNamePrefix = "axisPoint_";
            var paramsTextPrefix = blockNamePrefix;

            var database = document.Database;
            var editor = document.Editor;

            // Get data dialogs
            var blockPosition = new Point3d(double.NaN, double.NaN, double.NaN);
            if (!GetPointAcadDialog(editor, out blockPosition)) return result;

            double station = double.NaN;
            if (!GetStationDialog(out station)) return result;

            // block creation
            var block = GetAxisPointBlockTemplate(database);
            SetBlockName(block, blockNamePrefix + GetBlockName(block));
            SetBlockPosition(block, blockPosition);

            SetTextParams(
                block,
                paramsTextPrefix, () => FormatStation(station));

            result = true;

            return result;
        }
        public static bool CreateHeightPointBlock(Document document)
        {
            bool result = false;

            CreateLayersForPluginTool(document);

            var blockNamePrefix = "heightPoint_";
            var paramsTextPrefix = blockNamePrefix;

            var database = document.Database;
            var editor = document.Editor;

            // Get data dialogs
            var blockPosition = new Point3d(double.NaN, double.NaN, double.NaN);
            if (!GetPointAcadDialog(editor, out blockPosition)) return result;

            double height = double.NaN;
            if (!GetHeightDialog(out height)) return result;

            // block creation
            var block = GetHeightPointTemplate(database);
            SetBlockName(block, blockNamePrefix + GetBlockName(block));
            SetBlockPosition(block, blockPosition);

            SetTextParams(
                block,
                paramsTextPrefix, () => FormatHeight(height));

            //ManageColorsForEntity(document);
            result = true;

            return result;
        }
        public static bool CreateBlackPointBlock(Document document, int pointNumber)
        {
            bool result = false;

            CreateLayersForPluginTool(document);

            var blockNamePrefix = "blackPoint_";
            var paramsTextPrefix = blockNamePrefix;

            var database = document.Database;
            var editor = document.Editor;

            // Get data dialogs
            var blockPosition = new Point3d(double.NaN, double.NaN, double.NaN);
            if (!GetPointAcadDialog(editor, out blockPosition)) return result;

            // block creation
            var block = GetBlackPointTemplate(database);
            SetBlockName(block, blockNamePrefix + GetBlockName(block));
            SetBlockPosition(block, blockPosition);

            SetTextParams(
               block,
               paramsTextPrefix, () => FormatPointNumber(pointNumber));

            result = true;

            //ManageColorsForEntity(document);
            return result;
        }
        public static bool CreateRedPointBlock(Document document, int pointNumber)
        {
            bool result = false;

            CreateLayersForPluginTool(document);

            var blockNamePrefix = "redPoint_";
            var paramsTextPrefix = blockNamePrefix;

            var database = document.Database;
            var editor = document.Editor;

            // Get data dialogs
            var blockPosition = new Point3d(double.NaN, double.NaN, double.NaN);
            if (!GetPointAcadDialog(editor, out blockPosition)) return result;

            // block creation
            var block = GetRedPointTemplate(database);
            SetBlockName(block, blockNamePrefix + GetBlockName(block));
            SetBlockPosition(block, blockPosition);

            SetTextParams(
              block,
              paramsTextPrefix, () => FormatPointNumber(pointNumber));

            result = true;
            return result;
        }

        #region layer

        public static void CreateLayersForPluginTool(Document document)
        {
            var database = document.Database;

            if (!CheckAvailabilityLayers(document))
            {
                using (var documentlock = document.LockDocument())
                {
                    using (var transaction = database.TransactionManager.StartTransaction())
                    {
                        ObjectId plugin_layer;

                        var layerTable = transaction.GetObject(database.LayerTableId, OpenMode.ForWrite) as LayerTable;

                        var layerTableRecord = new LayerTableRecord();

                        layerTableRecord.Name = "selection_converter";

                        plugin_layer = layerTable.Add(layerTableRecord);

                        transaction.AddNewlyCreatedDBObject(layerTableRecord, true);

                        transaction.Commit();
                    }
                }
            }
        }
        public static bool CheckAvailabilityLayers(Document document)
        {
            var database = document.Database;
            {
                using (Transaction transaction = database.TransactionManager.StartTransaction())
                {
                    LayerTable layerTable = (LayerTable)transaction.GetObject(database.LayerTableId, OpenMode.ForRead, false);

                    foreach (ObjectId entity in layerTable)
                    {
                        LayerTableRecord LayerTableRecord = (LayerTableRecord)transaction.GetObject(entity, OpenMode.ForRead);
                        if (LayerTableRecord.Name == "selection_converter")
                            return true;
                    }
                    transaction.Commit();
                    return false;
                }
            }
        }
        public static void ChangeCurrentLayers()
        {
            var document = Autodesk.AutoCAD.ApplicationServices
   .Application.DocumentManager.MdiActiveDocument;
            var database = document.Database;

            using (var transaction = database.TransactionManager.StartTransaction())
            {
                var layerTable = transaction.GetObject(database.LayerTableId, OpenMode.ForRead) as LayerTable;

                if (database.Clayer == layerTable["0"])
                    database.Clayer = layerTable["selection_converter"];
                else
                    database.Clayer = layerTable["0"];

                transaction.Commit();
            }
        }

        #endregion

        public static void SetDefaultPdMode(Document document)
        {
            var database = document.Database;
            database.Pdmode = 35;
            database.Pdsize = 0.05;
        }

        public static string GetAbsolutePathWithName()
        {
            var document = Autodesk.AutoCAD.ApplicationServices
                .Application.DocumentManager.MdiActiveDocument;

            var database = document.Database;

            string pathDrawing = database.OriginalFileName;

            return pathDrawing;
        }

        public static string GetAbsolutePath()
        {
            string filePath = "";

            return filePath = acadApp.GetSystemVariable("DWGPREFIX").ToString();
        }

        public static  string GetAcadCurVerKey()
        {
            RegistryKey registryKey = Registry.CurrentUser;

            string path = @"Software\Autodesk\AutoCAD\";

            using (RegistryKey registryKeyCurrent = registryKey.OpenSubKey(path))
            {
                path += registryKeyCurrent.GetValue("CurVer");
                using (RegistryKey rk2 = registryKey.OpenSubKey(path))
                {
                    return path + "\\" + rk2.GetValue("CurVer");
                }
            }
        }

        public static string GetAcadLocation()
        {
            var document = Autodesk.AutoCAD.ApplicationServices
     .Application.DocumentManager.MdiActiveDocument;

            var database = document.Database;

            var ed = document.Editor;

            string s;

            RegistryKey registryKey = Registry.LocalMachine;
            string path = GetAcadCurVerKey();
            using (RegistryKey registryKeyCurrent = registryKey.OpenSubKey(path))
            {
                s = (string)registryKeyCurrent.GetValue("AcadLocation");

                return s;
            }
        }
    }
}
