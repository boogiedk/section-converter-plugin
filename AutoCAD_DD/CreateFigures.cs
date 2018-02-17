using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

//using ACadAppServices = Autodesk.AutoCAD.ApplicationServices;
//var editor = acad.DocumentManager.MdiActiveDocument.Editor;

namespace SectionConverterPlugin
{
    public partial class CreateFigures
    {
        public void CreateAxisPointMarkBlocks(
            Document document,
            Func<string> GetAnyIniqueBlockName)
        {
            var documentDatabase = document.Database;
            var documentEditor = document.Editor;

            bool exitRequested = false;
            while (!exitRequested)
            {
                Point3d coords;

                #region Request point data from user

                // запрашиваем координаты вставки блока
                //TODO:
                // вынести в отдельный метод запрос точки для стандартизации этого запроса
                var getPointAcadDialogResult = documentEditor.GetPoint("Pick a point:");

                if (getPointAcadDialogResult.Status != PromptStatus.OK)
                {
                    exitRequested = true;
                    break;
                }
               // coords = getPointAcadDialogResult.Value;

                coords = GetAxisPoint(document);
       
                //Description.Description = SetDescriptionForObject();

                //if (Description.Description == null)
                //{
                //    exitRequested = true;
                //    break;
                //}

                #endregion

                AddAxisPointMarkBlock(
                    documentDatabase,
                    documentEditor,
                    GetAnyIniqueBlockName,
                    coords);
            }
        }

        public void AddAxisPointMarkBlock(
            Database documentDatabase,
            Editor documentEditor,
            Func<string> GetAnyIniqueBlockName,
            Point3d coords)
        {
            using (var transaction =
                    documentDatabase.TransactionManager.StartTransaction())
            {
                BlockTableRecord block;

                #region New Block Registration

                // открываем таблицу блоков на запись
                var blockTable = (BlockTable)transaction.GetObject(
                    documentDatabase.BlockTableId,
                    OpenMode.ForWrite);


                string blockName;

                do
                {
                    blockName = GetAnyIniqueBlockName();
                }
                while (blockTable.Has(blockName));

                block = new BlockTableRecord()
                {
                    Name = blockName
                };

                var blockTableID = blockTable.Add(block);
                transaction.AddNewlyCreatedDBObject(block, true);

                // открываем пространство модели на запись
                BlockTableRecord blockTableRecord = (BlockTableRecord)transaction.GetObject(
                    blockTable[BlockTableRecord.ModelSpace],
                    OpenMode.ForWrite);

                // создаем новое вхождение блока, используя ранее сохраненный ID определения блока
                BlockReference blockReference = new BlockReference(coords, blockTableID);

                // добавляем созданное вхождение блока на пространство модели и в транзакцию
                blockTableRecord.AppendEntity(blockReference);
                transaction.AddNewlyCreatedDBObject(blockReference, true);

                #endregion

                AddBlock(
                    documentDatabase,
                    documentEditor,
                    transaction,
                    blockTable,
                    block);

                transaction.Commit();
            }
        }

        public void AddBlock(
             Database documentDatabase,
             Editor documentEditor,
             Transaction transaction,
             BlockTable blockTable,
             BlockTableRecord block)
        {
            //TODO: лямда выражения - изучить
            Action<Entity> AppendEntity = e =>
            {
                block.AppendEntity(e);
                transaction.AddNewlyCreatedDBObject(e, true);
            };

            // создаем полилинию
            Polyline poly = new Polyline();
            poly.SetDatabaseDefaults();
            poly.AddVertexAt(0, new Point2d(-50, -125), 0, 0, 0);
            poly.AddVertexAt(1, new Point2d(-50, 105), 0, 0, 0);
            poly.AddVertexAt(2, new Point2d(-20, 125), 0, 0, 0);
            poly.AddVertexAt(3, new Point2d(20, 125), 0, 0, 0);
            poly.AddVertexAt(4, new Point2d(50, 105), 0, 0, 0);
            poly.AddVertexAt(5, new Point2d(50, -125), 0, 0, 0);
            poly.AddVertexAt(6, new Point2d(-50, -125), 0, 0, 0);

            // добавляем полилинию в определение блока и в транзакцию
            AppendEntity(poly);

            // создаем окружность
            Circle circle = new Circle();
            circle.SetDatabaseDefaults();
            circle.Center = new Point3d(0, 90, 0);
            circle.Radius = 15;

            // добавляем окружность в определение блока и в транзакцию
            AppendEntity(circle);

            // создаем текст
            DBText text = new DBText();
            text.Position = new Point3d(-25, -95, 0);
            text.Height = 25;
       //     text.TextString = Description.Description;

            // добавляем текст в определение блока и в транзакцию
            AppendEntity(text);
        }

        //TODO: 
        // вставить проверку на корректный ввод точки
        public Point3d GetAxisPoint(Document document)
        {
            Point3d coords;
            var documentEditor = document.Editor;

            var getPointAcadDialogResult = documentEditor.GetPoint("\nPick a point:");

            coords = getPointAcadDialogResult.Value;

            return coords;
        }

        public string SetDescriptionForObject()
        {
            var axisPointInfoInputForm = new StationInputForm();
            var inputFormDialogResult = axisPointInfoInputForm.ShowDialog();
            return axisPointInfoInputForm.Description;
        }

        public void CreateAxisPoint(Document document)
        {
            // Получение текущего документа и базы данных
            Database database = document.Database;

            // Старт транзакции
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                // Открытие таблицы Блоков для чтения
                BlockTable blocktable;
                blocktable = transaction.GetObject(database.BlockTableId,
                                               OpenMode.ForRead) as BlockTable;
                // Открытие записи таблицы Блоков для записи
                BlockTableRecord blockTableRecord;
                blockTableRecord = transaction.GetObject(blocktable[BlockTableRecord.ModelSpace],
                                            OpenMode.ForWrite) as BlockTableRecord;

                // Создание точки с в пространстве Модели
                Point3d coords = GetAxisPoint(document);
                var acPoint = new DBPoint(new Point3d(coords.X, coords.Y, coords.Z));
                acPoint.SetDatabaseDefaults();

                // Установка стиля для всех объектов точек в чертеже
                database.Pdmode = 35;
                database.Pdsize = 20;

                // Добавление нового объекта в запись таблицы блоков и в транзакцию
                blockTableRecord.AppendEntity(acPoint);
                transaction.AddNewlyCreatedDBObject(acPoint, true);

                // Сохранение нового объекта в базе данных
                transaction.Commit();

            }
        }
    }
}
