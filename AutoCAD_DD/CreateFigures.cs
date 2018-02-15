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
    class CreateFigures
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
                string description;

              //  #region Request point data from user

                // запрашиваем координаты вставки блока
                //TODO:
                // вынести в отдельный метод запрос точки для стандартизации этого запроса
                var getPointAcadDialogResult = documentEditor.GetPoint("Pick a point:");

                if (getPointAcadDialogResult.Status != PromptStatus.OK)
                {
                    exitRequested = true;
                    break;
                }
                coords = getPointAcadDialogResult.Value;

                // Запрашиваем описание к точке
                var axisPointInfoInputForm = new AxisPointInfoInputForm();
                var inputFormDialogResult = axisPointInfoInputForm.ShowDialog();

                description = axisPointInfoInputForm.Description;

                if (description == null)
                {
                    exitRequested = true;
                    break;
                }

              //  #endregion

                AddAxisPointMarkBlock(
                    documentDatabase,
                    documentEditor,
                    GetAnyIniqueBlockName,
                    coords,
                    description);
            }
        }

        public void AddAxisPointMarkBlock(
            Database documentDatabase,
            Editor documentEditor,
            Func<string> GetAnyIniqueBlockName,
            Point3d coords,
            string description)
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

                // получение уникального имени
                // TODO: 
                // можно ввести ограничение на число попыток
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

                #region Create block's primitives

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
                block.AppendEntity(poly);
                transaction.AddNewlyCreatedDBObject(poly, true);

                // создаем окружность
                Circle circle = new Circle();
                circle.SetDatabaseDefaults();
                circle.Center = new Point3d(0, 90, 0);
                circle.Radius = 15;

                // добавляем окружность в определение блока и в транзакцию
                block.AppendEntity(circle);
                transaction.AddNewlyCreatedDBObject(circle, true);

                // создаем текст
                DBText text = new DBText();
                text.Position = new Point3d(-25, -95, 0);
                text.Height = 25;
                text.TextString = description;

                // добавляем текст в определение блока и в транзакцию
                block.AppendEntity(text);
                transaction.AddNewlyCreatedDBObject(text, true);

                #endregion

                transaction.Commit();
            }
        }
    }
}
