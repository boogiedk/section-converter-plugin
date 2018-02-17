using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Collections.Generic;

//using ACadAppServices = Autodesk.AutoCAD.ApplicationServices;
//var editor = acad.DocumentManager.MdiActiveDocument.Editor;

namespace SectionConverterPlugin
{
    public partial class CreateFigures
    {
        public bool GetPointAcadDialog(Editor editor, out Point3d point3D)
        {
            bool result = false;
            point3D = new Point3d(
                Double.NaN,
                Double.NaN,
                Double.NaN);

            var getPointAcadDialogResult = editor.GetPoint(@"\nPick a point:");

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
            
            // TODO:s
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

        public BlockTableRecord CreateNewBlock(
            Database documentDatabase, 
            List<Entity> entities)
        {
            BlockTableRecord block;

            using (var transaction =
                    documentDatabase.TransactionManager.StartTransaction())
            {
                // открываем таблицу блоков на запись
                var blockTable = (BlockTable)transaction.GetObject(
                    documentDatabase.BlockTableId,
                    OpenMode.ForWrite);

                block = new BlockTableRecord();

                var blockTableID = blockTable.Add(block);
                transaction.AddNewlyCreatedDBObject(block, true);

                // открываем пространство модели на запись
                BlockTableRecord blockTableRecord = (BlockTableRecord)transaction.GetObject(
                    blockTable[BlockTableRecord.ModelSpace],
                    OpenMode.ForWrite);

                // создаем новое вхождение блока, используя ранее сохраненный ID определения блока
                BlockReference blockReference = new BlockReference(
                    new Point3d(0, 0, 0), 
                    blockTableID);

                // добавляем созданное вхождение блока на пространство модели и в транзакцию
                blockTableRecord.AppendEntity(blockReference);
                transaction.AddNewlyCreatedDBObject(blockReference, true);

                //foreach (var entity in entities)
                //{
                //    block.AppendEntity(entity);
                //    transaction.AddNewlyCreatedDBObject(entity, true);
                //}

                transaction.Commit();
            }

            return block;
        }

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
            var text = new DBText();
                text.Position = new Point3d(-25, -95, 0);
                text.Height = 25;
                text.TextString = "#mark_axisPoint";

            // добавляем текст в определение блока
            entities.Add(text);

            return CreateNewBlock(documentDatabase, entities);
        }

        public bool CreateAxisPointBlock(
            Document document,
            Func<string> GetAnyIniqueBlockName)
        {
            bool result = false;

            var database = document.Database;
            var editor = document.Editor;

            Point3d coords;
            if (!GetPointAcadDialog(editor, out coords)) return result;

            double station;
            if (!GetStationDialog(out station)) return result;

            var block = GetAxisPointBlockTemplate(database);

            // НЕЛЬЗЯ РАБОТАТЬ С БЛОКОМ, НЕ ОТКРЫВ ЕГО НА ЗАПИСЬ
            // block.Name = GetAnyIniqueBlockName();
            // установить позицию блоку
            // установить координнаты блоку

            result = true;

            return result;
        }


        //public void CreateAxisPointMarkBlocks(s
        //    Document document,
        //    Func<string> GetAnyIniqueBlockName)
        //{
        //    var documentDatabase = document.Database;
        //    var documentEditor = document.Editor;
        //
        //    bool exitRequested = false;
        //    while (!exitRequested)
        //    {
        //        Point3d coords;
        //
        //        #region Request point data from user
        //
        //        // запрашиваем координаты вставки блока
        //        //TODO:
        //        // вынести в отдельный метод запрос точки для стандартизации этого запроса
        //        var getPointAcadDialogResult = documentEditor.GetPoint("Pick a point:");
        //
        //        if (getPointAcadDialogResult.Status != PromptStatus.OK)
        //        {
        //            exitRequested = true;
        //            break;
        //        }
        //       // coords = getPointAcadDialogResult.Value;
        //
        //        coords = GetAxisPoint(document);
        //
        //        //Description.Description = SetDescriptionForObject();
        //
        //        //if (Description.Description == null)
        //        //{
        //        //    exitRequested = true;
        //        //    break;
        //        //}
        //
        //        #endregion
        //
        //        AddAxisPointMarkBlock(
        //            documentDatabase,
        //            documentEditor,
        //            GetAnyIniqueBlockName,
        //            coords);
        //    }
        //}



        //public void AddAxisPointMarkBlock(
        //    Database documentDatabase,
        //    Editor documentEditor,
        //    Func<string> GetAnyIniqueBlockName,
        //    Point3d coords)
        //{
        //    using (var transaction =
        //            documentDatabase.TransactionManager.StartTransaction())
        //    {
        //        BlockTableRecord block;
        //
        //        #region New Block Registration
        //
        //        // открываем таблицу блоков на запись
        //        var blockTable = (BlockTable)transaction.GetObject(
        //            documentDatabase.BlockTableId,
        //            OpenMode.ForWrite);
        //
        //
        //        string blockName;
        //
        //        do
        //        {
        //            blockName = GetAnyIniqueBlockName();
        //        }
        //        while (blockTable.Has(blockName));
        //
        //        block = new BlockTableRecord()
        //        {
        //            Name = blockName
        //        };
        //
        //        var blockTableID = blockTable.Add(block);
        //        transaction.AddNewlyCreatedDBObject(block, true);
        //
        //        // открываем пространство модели на запись
        //        BlockTableRecord blockTableRecord = (BlockTableRecord)transaction.GetObject(
        //            blockTable[BlockTableRecord.ModelSpace],
        //            OpenMode.ForWrite);
        //
        //        // создаем новое вхождение блока, используя ранее сохраненный ID определения блока
        //        BlockReference blockReference = new BlockReference(coords, blockTableID);
        //
        //        // добавляем созданное вхождение блока на пространство модели и в транзакцию
        //        blockTableRecord.AppendEntity(blockReference);
        //        transaction.AddNewlyCreatedDBObject(blockReference, true);
        //
        //        #endregion
        //
        //        AddBlock(
        //            documentDatabase,
        //            documentEditor,
        //            transaction,
        //            blockTable,
        //            block);
        //
        //        transaction.Commit();
        //    }
        //}
        //
        //public void AddBlock(
        //     Database documentDatabase,
        //     Editor documentEditor,
        //     Transaction transaction,
        //     BlockTable blockTable,
        //     BlockTableRecord block)
        //{
        //    //TODO: лямда выражения - изучить
        //    Action<Entity> AppendEntity = e =>
        //    {
        //        block.AppendEntity(e);
        //        transaction.AddNewlyCreatedDBObject(e, true);
        //    };
        //
        //    // создаем полилинию
        //    Polyline poly = new Polyline();
        //    poly.SetDatabaseDefaults();
        //    poly.AddVertexAt(0, new Point2d(-50, -125), 0, 0, 0);
        //    poly.AddVertexAt(1, new Point2d(-50, 105), 0, 0, 0);
        //    poly.AddVertexAt(2, new Point2d(-20, 125), 0, 0, 0);
        //    poly.AddVertexAt(3, new Point2d(20, 125), 0, 0, 0);
        //    poly.AddVertexAt(4, new Point2d(50, 105), 0, 0, 0);
        //    poly.AddVertexAt(5, new Point2d(50, -125), 0, 0, 0);
        //    poly.AddVertexAt(6, new Point2d(-50, -125), 0, 0, 0);
        //
        //    // добавляем полилинию в определение блока и в транзакцию
        //    AppendEntity(poly);
        //
        //    // создаем окружность
        //    Circle circle = new Circle();
        //    circle.SetDatabaseDefaults();
        //    circle.Center = new Point3d(0, 90, 0);
        //    circle.Radius = 15;
        //
        //    // добавляем окружность в определение блока и в транзакцию
        //    AppendEntity(circle);
        //
        //    // создаем текст
        //    DBText text = new DBText();
        //    text.Position = new Point3d(-25, -95, 0);
        //    text.Height = 25;
        ////     text.TextString = Description.Description;
        //
        //    // добавляем текст в определение блока и в транзакцию
        //    AppendEntity(text);
        //}
        //
        ////TODO: 
        //// вставить проверку на корректный ввод точки
        //public Point3d GetAxisPoint(Document document)
        //{
        //    Point3d coords;
        //    var documentEditor = document.Editor;
        //
        //    var getPointAcadDialogResult = documentEditor.GetPoint("\nPick a point:");
        //
        //    coords = getPointAcadDialogResult.Value;
        //
        //    return coords;
        //}
        //
        //public string SetDescriptionForObject()
        //{
        //    var axisPointInfoInputForm = new AxisPointInfoInputForm();
        //    var inputFormDialogResult = axisPointInfoInputForm.ShowDialog();
        //    return axisPointInfoInputForm.Description;
        //}
        //
        //public void CreateAxisPoint(Document document)
        //{
        //    // Получение текущего документа и базы данных
        //    Database database = document.Database;
        //
        //    // Старт транзакции
        //    using (Transaction transaction = database.TransactionManager.StartTransaction())
        //    {
        //        // Открытие таблицы Блоков для чтения
        //        BlockTable blocktable;
        //        blocktable = transaction.GetObject(database.BlockTableId,
        //                                       OpenMode.ForRead) as BlockTable;
        //        // Открытие записи таблицы Блоков для записи
        //        BlockTableRecord blockTableRecord;
        //        blockTableRecord = transaction.GetObject(blocktable[BlockTableRecord.ModelSpace],
        //                                    OpenMode.ForWrite) as BlockTableRecord;
        //
        //        // Создание точки с в пространстве Модели
        //        Point3d coords = GetAxisPoint(document);
        //        var acPoint = new DBPoint(new Point3d(coords.X, coords.Y, coords.Z));
        //        acPoint.SetDatabaseDefaults();
        //
        //        // Установка стиля для всех объектов точек в чертеже
        //        database.Pdmode = 35;
        //        database.Pdsize = 20;
        //
        //        // Добавление нового объекта в запись таблицы блоков и в транзакцию
        //        blockTableRecord.AppendEntity(acPoint);
        //        transaction.AddNewlyCreatedDBObject(acPoint, true);
        //
        //        // Сохранение нового объекта в базе данных
        //        transaction.Commit();
        //
        //    }
        //}
    }
}
