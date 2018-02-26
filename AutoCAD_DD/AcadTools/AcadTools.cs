using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Collections.Generic;
using System.Linq;


using Color=Autodesk.AutoCAD.Colors.Color;
using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using SectionConverterPlugin.Forms;

namespace SectionConverterPlugin
{
    public class AcadTools
    {
        #region Temporal Methods

        private string _GetAnyIniqueBlockName(BlockTable blockTable)
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

        public bool GetPointAcadDialog(Editor editor, out Point3d point3D)
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
        public bool GetStationDialog(out double station)
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
        public bool GetHeightDialog(out double height)
        {
            bool result = false;
            height = Double.NaN;

            var dialogForm = new InputHeightDialog();
            var dialogResult = dialogForm.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return result;
            }

            height = dialogForm.Height;
            result = true;

            return result;
        }

        #endregion

        #region Acad Points Templates

        public BlockTableRecord GetAxisPointBlockTemplate(Database documentDatabase)
        {
            var entities = new List<Entity>();

            DBPoint dbPoint = new DBPoint(new Point3d(0, 0, 0));
            //dbPoint.SetDatabaseDefaults();

            dbPoint.Color = Color.FromRgb(255, 66, 41);
            entities.Add(dbPoint);
         
            var text = new MText();
            text.Location = new Point3d(300, 0, 0);
            text.Attachment = AttachmentPoint.MiddleLeft;
            text.TextHeight = 200;
            text.Contents = "axisPoint_";
            entities.Add(text);

            var block = CreateNewBlock(documentDatabase);
            SetBlockEntities(block, entities);

            return block;
        }

        public BlockTableRecord GetHeightPointTemplate(Database documentDatabase)
        {
            var entities = new List<Entity>();

            var dbPoint = new DBPoint(new Point3d(0, 0, 0));
            //dbPoint.SetDatabaseDefaults();

            dbPoint.Color = Color.FromRgb(31, 207, 75);
            entities.Add(dbPoint);


            var text = new MText();
            text.Location = new Point3d(300, 0, 0);
            text.Attachment = AttachmentPoint.MiddleLeft;
            text.TextHeight = 200;
            text.Contents = "heightPoint_";
            entities.Add(text);

            var block = CreateNewBlock(documentDatabase);
            SetBlockEntities(block, entities);

            return block;
        }

        public BlockTableRecord GetBottomPointTemplate(Database documentDatabase)
        {
            var entities = new List<Entity>();

            DBPoint dbPoint = new DBPoint(new Point3d(0, 0, 0));
            //dbPoint.SetDatabaseDefaults();

            dbPoint.Color = Color.FromRgb(207, 31, 207);
            entities.Add(dbPoint);

            var block = CreateNewBlock(documentDatabase);
            SetBlockEntities(block, entities);

            return block;
        }

        public BlockTableRecord GetTopPointTemplate(Database documentDatabase)
        {
            var entities = new List<Entity>();

            DBPoint dbPoint = new DBPoint(new Point3d(0, 0, 0));
            //dbPoint.SetDatabaseDefaults();

            dbPoint.Color = Color.FromRgb(0, 0, 0);
            entities.Add(dbPoint);

            var block = CreateNewBlock(documentDatabase);
            SetBlockEntities(block, entities);

            return block;
        }

        #endregion

        #region Block methods

        public BlockTableRecord CreateNewBlock(
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
                    Name = _GetAnyIniqueBlockName(blockTable)
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

        public Point3d GetBlockPosition(
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
        public void SetBlockPosition(
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

        public string GetBlockName(
            BlockTableRecord block)
        {
            return block.Name;
        }
        public void SetBlockName(
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

        public List<Entity> GetBlockEntities(
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

        public void SetBlockEntities(
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
        #endregion
   

        public MText GetAnyMText(List<Entity> entities, string startWith = "")
        {
            var mtexts = entities
                .Select(e => e as MText)
                .Where(e => e != null);

            return startWith == "" ?
                mtexts.First() :
                mtexts.First(mt => mt.Text.StartsWith(startWith));
        }
        public void ApplyFunction(Entity entity, Action<Entity> Function)
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

        private string FormatStation(double station)
        {
            var sign = Math.Sign(station);
            var abs = Math.Abs(station);

            return String.Format("ПК {0:00}+{1:00.000}",
                sign * (abs / 100),
                abs % 100);
        }

        private string FormatHeight(double height)
        {
            var sign = Math.Sign(height);

            return String.Format("{0000,000}"+"м", height);
        }

        private void SetTextParams(
            BlockTableRecord block,
            string paramsTextPrefix,
            Func<string> GetParamsText)
        {
            ApplyFunction(
                GetAnyMText(GetBlockEntities(block), paramsTextPrefix),
                e =>
                {
                    var text = (MText)e;
                    text.Contents = GetParamsText();
                });
        }

        public bool CreateAxisPointBlock(Document document)
        {
            bool result = false;

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
            UpdateCurrentScreen();

            return result;
        }
        public bool CreateHeightPointBlock(Document document)
        {
            bool result = false;

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

            result = true;

            UpdateCurrentScreen();
            return result;
        }

        public bool CreateBottomPointBlock(Document document)
        {
            bool result = false;

            var blockNamePrefix = "bottomPoint_";

            var database = document.Database;
            var editor = document.Editor;

            // Get data dialogs
            var blockPosition = new Point3d(double.NaN, double.NaN, double.NaN);
            if (!GetPointAcadDialog(editor, out blockPosition)) return result;       

            // block creation
            var block = GetBottomPointTemplate(database);
            SetBlockName(block, blockNamePrefix + GetBlockName(block));
            SetBlockPosition(block, blockPosition);

            result = true;

            UpdateCurrentScreen();
            return result;
        }
        public bool CreateTopPointBlock(Document document)
        {
            bool result = false;

            var blockNamePrefix = "topPoint_";

            var database = document.Database;
            var editor = document.Editor;

            // Get data dialogs
            var blockPosition = new Point3d(double.NaN, double.NaN, double.NaN);
            if (!GetPointAcadDialog(editor, out blockPosition)) return result;        

            // block creation
            var block = GetTopPointTemplate(database);
            SetBlockName(block, blockNamePrefix + GetBlockName(block));
            SetBlockPosition(block, blockPosition);

            result = true;

            UpdateCurrentScreen();
            return result;
        }

        public void UpdateCurrentScreen()
        {
            //   redrawing the drawing
            //   acadApp.UpdateScreen();
            //   acadApp.DocumentManager.MdiActiveDocument.Editor.UpdateScreen();

            //   regeneration of the drawing
            acadApp.DocumentManager.MdiActiveDocument.Editor.Regen();
        }

        public void CreateLayersForPluginTool(Document document)
        {
            var database = document.Database;

            using (var documentlock = document.LockDocument())
            {
                using (var transaction = database.TransactionManager.StartTransaction())
                {
                    var layerTable = transaction.GetObject(database.LayerTableId, OpenMode.ForWrite) as LayerTable;

                    var layerTableRecord = new LayerTableRecord();
                    layerTableRecord.Name = "plugin_layer";
   
                    ObjectId plugin_layer = layerTable.Add(layerTableRecord);
                    
                    transaction.AddNewlyCreatedDBObject(layerTableRecord, true);

                    transaction.Commit();
                }
            }
        }

        public void ChangeCurrentLayers()
        {
            var document = Autodesk.AutoCAD.ApplicationServices
   .Application.DocumentManager.MdiActiveDocument;
            var database = document.Database;

            using (var transaction = database.TransactionManager.StartTransaction())
            {
                var layerTable = transaction.GetObject(database.LayerTableId, OpenMode.ForRead) as LayerTable;

                if (database.Clayer == layerTable["0"])
                    database.Clayer = layerTable["plugin_layer"];
                else
                    database.Clayer = layerTable["0"];

                transaction.Commit();
            }
        }

        public void DefaultPdMode(Document document)
        {
            var database = document.Database;
            database.Pdmode = 35;
            database.Pdsize = 200;
        }
    }
}

