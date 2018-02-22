using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

//using ACadAppServices = Autodesk.AutoCAD.ApplicationServices;
//var editor = acad.DocumentManager.MdiActiveDocument.Editor;

namespace SectionConverterPlugin
{
    public partial class CreateFigures
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

            var dialogForm = new StationInputForm();
            var dialogResult = dialogForm.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return result;
            }

            // TODO:
            // Переместить в диалог
            station = Convert.ToDouble(dialogForm.Description);
            result = true;

            return result;
        }
        public bool GetHeightDialog(out double height)
        {
            bool result = false;
            height = Double.NaN;

            var dialogForm = new StationInputForm();
            var dialogResult = dialogForm.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return result;
            }

            // TODO:
            // Переместить в диалог
            height = Convert.ToDouble(dialogForm.Description);
            result = true;

            return result;
        }
        public bool GetBottomDialog(Document document, Database database, out Point3d point3D)
        {
            var editor = document.Editor;

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

        #endregion

        #region Acad Points Templates

        public BlockTableRecord GetAxisPointBlockTemplate(Database documentDatabase)
        {
            var entities = new List<Entity>();

            // создаем полилинию
            var poly = new Polyline();
            poly.SetDatabaseDefaults();
            poly.AddVertexAt(0, new Point2d(-50, -125), 0, 0, 0);
            poly.AddVertexAt(1, new Point2d(-50, 105), 0, 0, 0);
            poly.AddVertexAt(2, new Point2d(-20, 125), 0, 0, 0);
            poly.AddVertexAt(3, new Point2d(20, 125), 0, 0, 0);
            poly.AddVertexAt(4, new Point2d(50, 105), 0, 0, 0);
            poly.AddVertexAt(5, new Point2d(50, -125), 0, 0, 0);
            poly.AddVertexAt(6, new Point2d(-50, -125), 0, 0, 0);

            // добавляем полилинию в определение блока
            entities.Add(poly);

            // создаем окружность
            var circle = new Circle();
            circle.SetDatabaseDefaults();
            circle.Center = new Point3d(0, 90, 0);
            circle.Radius = 15;

            // добавляем окружность в определение блока
            entities.Add(circle);

            // создаем текст
            var text = new MText();
            text.Location = new Point3d(-25, -95, 0);
            text.Contents = "axisPoint_";

            // добавляем текст в определение блока
            entities.Add(text);

            var block = CreateNewBlock(documentDatabase);

            SetBlockEntities(block, entities);

            return block;
        }
        public BlockTableRecord GetAxisPointTeplate(Database database)
        {

            var entities = new List<Entity>();

            DBPoint dbPoint = new DBPoint(new Point3d(0, 0, 0));
            dbPoint.SetDatabaseDefaults();

            database.Pdmode = 35;
            database.Pdsize = 20;

            // создаем текст
            var text = new MText();
            text.Location = new Point3d(20, 0, 0);
            text.Contents = "#mark_axisPoint";

            entities.Add(text);
            entities.Add(dbPoint);

            var point = CreateNewBlock(database);

            SetBlockEntities(point, entities);

            return point;

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
                // открываем таблицу блоков на запись
                var blockTable = (BlockTable)transaction.GetObject(
                    documentDatabase.BlockTableId,
                    OpenMode.ForWrite);

                block = new BlockTableRecord()
                {
                    Name = _GetAnyIniqueBlockName(blockTable)
                };

                var blockTableID = blockTable.Add(block);
                transaction.AddNewlyCreatedDBObject(block, true);

                // открываем пространство модели на запись
                BlockTableRecord modelSpaceTableRecord = (BlockTableRecord)transaction.GetObject(
                    blockTable[BlockTableRecord.ModelSpace],
                    OpenMode.ForWrite);

                // создаем новое вхождение блока, используя ранее сохраненный ID определения блока
                BlockReference blockReference = new BlockReference(
                    new Point3d(0, 0, 0),
                    blockTableID);

                // добавляем созданное вхождение блока на пространство модели и в транзакцию
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
                // открываем таблицу блоков на запись
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
                // открываем таблицу блоков на запись
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
                // открываем таблицу блоков на запись
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
                // открываем таблицу блоков на запись
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
                // открываем таблицу блоков на запись
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

        //#region point methods
        //public BlockTableRecord CreateNewPoint(
        //    Database documentDatabase)
        //{
        //    BlockTableRecord point = null;

        //    using (var transaction =
        //            documentDatabase.TransactionManager.StartTransaction())
        //    {
        //        // открываем таблицу блоков на запись
        //        var blockTable = (BlockTable)transaction.GetObject(
        //            documentDatabase.BlockTableId,
        //            OpenMode.ForWrite);

        //        // открываем пространство модели на запись
        //        BlockTableRecord modelSpaceTableRecord = (BlockTableRecord)transaction.GetObject(
        //            blockTable[BlockTableRecord.ModelSpace],
        //            OpenMode.ForWrite);

        //        transaction.Commit();
        //    }

        //    return point;
        //}

        //public Point3d GetPointPosition(
        //    BlockTableRecord point)
        //{
        //    Database documentDatabase = point.Database;
        //    Point3d position = new Point3d(double.NaN, double.NaN, double.NaN);

        //    using (var transaction =
        //            documentDatabase.TransactionManager.StartTransaction())
        //    {
        //        // открываем таблицу блоков на запись
        //        var blockTable = (BlockTable)transaction.GetObject(
        //            documentDatabase.BlockTableId,
        //            OpenMode.ForRead);

        //        var blockReference = (BlockReference)transaction.GetObject(
        //            point.GetBlockReferenceIds(true, true)[0],
        //            OpenMode.ForRead);

        //        position = blockReference.Position;

        //        transaction.Commit();

        //        return position;
        //    }
        //}

        //public void SetPointPosition(
        //    BlockTableRecord point,
        //    Point3d position)
        //{
        //    Database documentDatabase = point.Database;

        //    using (var transaction =
        //            documentDatabase.TransactionManager.StartTransaction())
        //    {
        //        // открываем таблицу блоков на запись
        //        var blockTable = (BlockTable)transaction.GetObject(
        //            documentDatabase.BlockTableId,
        //            OpenMode.ForWrite);

        //        var blockTableRecord = (BlockTableRecord)transaction.GetObject(
        //            point.Id,
        //            OpenMode.ForWrite);

        //        transaction.Commit();
        //    }
        //}

        //public List<Entity> GetPointEntities(
        //    BlockTableRecord point)
        //{
        //    Database documentDatabase = point.Database;
        //    List<Entity> entities = new List<Entity>();

        //    using (var transaction =
        //            documentDatabase.TransactionManager.StartTransaction())
        //    {

        //        var blockTable = (BlockTable)transaction.GetObject(
        //            documentDatabase.BlockTableId,
        //            OpenMode.ForRead);

        //        foreach (var entityID in point)
        //        {
        //            var entity = (Entity)transaction.GetObject(
        //            entityID,
        //            OpenMode.ForRead);

        //            entities.Add(entity);
        //        }

        //        transaction.Commit();
        //    }

        //    return entities;
        //}

        //public void SetPointEntities(
        //    BlockTableRecord point,
        //    List<Entity> entities)
        //{
        //    Database documentDatabase = point.Database;

        //    using (var transaction =
        //            documentDatabase.TransactionManager.StartTransaction())
        //    {
        //        var blockTable = (BlockTable)transaction.GetObject(
        //            documentDatabase.BlockTableId,
        //            OpenMode.ForWrite);

        //        var blockTableRecord = (BlockTableRecord)transaction.GetObject(
        //          point.Id,
        //            OpenMode.ForWrite);

        //        foreach (var entity in entities)
        //        {
        //            blockTableRecord.AppendEntity(entity);

        //            try
        //            {
        //                var text = new DBText();
        //                text.Position = new Point3d(0, 0, 0);
        //                text.TextString = "#mark_axisPoint";

        //                transaction.AddNewlyCreatedDBObject(entity, true);
        //            }
        //            catch
        //            {
        //                // well, already created. ok.
        //            }
        //        }

        //        transaction.Commit();
        //    }
        //}

        //public bool CreateAxisPointTest(Document document)
        //{
        //    bool result = false;

        //    var database = document.Database;
        //    var editor = document.Editor;

        //    Point3d pointPosition = new Point3d(double.NaN, double.NaN, double.NaN);
        //    if (!GetPointAcadDialog(editor, out pointPosition)) return result;

        //    double station = double.NaN;
        //    if (!GetStationDialog(out station)) return result;

        //    var block = GetAxisPointTeplate(database);

        //    SetBlockPosition(block, pointPosition);

        //    result = true;

        //    return result;
        //}

        //#endregion

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
            return String.Format("ПК {0:00}+{1:00.00}", 
                station / 100,
                station % 100);
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

        private BlockTableRecord CreateBlockFromTemplate(
            BlockTableRecord blockTemplate, 
            string namePrefix, 
            Point3d position,
            Database database)
        {
            var block = GetAxisPointBlockTemplate(database);
            SetBlockName(block, namePrefix + GetBlockName(block));
            SetBlockPosition(block, position);

            return block;
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
            var block = CreateBlockFromTemplate(
                GetAxisPointBlockTemplate(database),
                blockNamePrefix,
                blockPosition,
                database);

            SetTextParams(
                block, 
                paramsTextPrefix, () => FormatStation(station));

            result = true;

            return result;
        }
    }
}

