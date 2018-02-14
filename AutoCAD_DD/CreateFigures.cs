using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Interop.Common;
using Autodesk.AutoCAD.EditorInput;

using acad = Autodesk.AutoCAD.ApplicationServices.Application;

namespace AutoCAD_DD
{
    class CreateFigures
    {
        public void CreateCircle()
        {
            Point3d point;

            var ed = acad.DocumentManager.MdiActiveDocument.Editor;

            for (int i = 0; i <= 5; i++)
            {
                var ptRes = ed.GetPoint("\nPick a 5 point: ");
                point = ptRes.Value;

                // получаем текущий документ и его БД
                Document acDoc = acad.DocumentManager.MdiActiveDocument;
                Database acCurDb = acDoc.Database;

                // начинаем транзакцию
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {

                    // открываем таблицу блоков документа
                    BlockTable acBlkTbl;
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // открываем пространство модели (Model Space) - оно является одной из записей в таблице блоков документа
                    BlockTableRecord acBlkTblRec;
                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // создаем линию между точками с указанными координатами
                    Circle acCicle = new Circle();
                    acCicle.Center = new Point3d(point.X, point.Y, point.Z);
                    acCicle.Radius = 40.25;

                    // устанавливаем параметры созданного объекта равными параметрам по умолчанию
                    acCicle.SetDatabaseDefaults();

                    // добавляем созданный объект в пространство модели
                    acBlkTblRec.AppendEntity(acCicle);

                    // также добавляем созданный объект в транзакцию
                    acTrans.AddNewlyCreatedDBObject(acCicle, true);

                    // фиксируем изменения
                    acTrans.Commit();
                }
            }
        }

        public void CreatePolyline()
        {
            Point3d point;

            // получаем текущий документ и его БД
            Document acDoc = acad.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            var ed = acad.DocumentManager.MdiActiveDocument.Editor;

            for (int i = 0; i <= 1; i++)
            {
                var ptRes = ed.GetPoint("\nPick a point: ");
                point = ptRes.Value;

                // начинаем транзакцию
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    // открываем таблицу блоков документа
                    BlockTable acBlkTbl;
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // открываем пространство модели (Model Space) - оно является одной из записей в таблице блоков документа
                    BlockTableRecord acBlkTblRec;
                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // создаем полилинию
                    Polyline acPolyline = new Polyline();

                    // устанавливаем параметры созданного объекта равными параметрам по умолчанию
                    acPolyline.SetDatabaseDefaults();

                    // добавляем к полилинии вершины
                    acPolyline.AddVertexAt(0, new Point2d(-50, -125), 0, 0, 0);
                    acPolyline.AddVertexAt(1, new Point2d(-50, 105), 0, 0, 0);
                    acPolyline.AddVertexAt(2, new Point2d(-20, 125), 0, 0, 0);
                    acPolyline.AddVertexAt(3, new Point2d(20, 125), 0, 0, 0);
                    acPolyline.AddVertexAt(4, new Point2d(50, 105), 0, 0, 0);
                    acPolyline.AddVertexAt(5, new Point2d(50, -125), 0, 0, 0);
                    acPolyline.AddVertexAt(6, new Point2d(-50, -125), 0, 0, 0);

                    // добавляем созданный объект в пространство модели
                    acBlkTblRec.AppendEntity(acPolyline);

                    // также добавляем созданный объект в транзакцию
                    acTrans.AddNewlyCreatedDBObject(acPolyline, true);

                    // фиксируем изменения
                    acTrans.Commit();
                }
            }
        }

        public void CreateBlock(string name, string desc)
        {
            Point3d point;
            // получаем ссылки на документ и его БД
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            // поле документа "Editor" понадобится нам для вывода сообщений в окно консоли AutoCAD
            Editor ed = doc.Editor;

            var editor = acad.DocumentManager.MdiActiveDocument.Editor;

            // начинаем транзакцию
            using (var trans = db.TransactionManager.StartTransaction())
            {
                //***
                // ШАГ 1 - создаем новую запись в таблице блоков
                //***

                // открываем таблицу блоков на запись
                var bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForWrite);

                // имя создаваемого блока
                string blockName = name;

                // вначале проверяем, нет ли в таблице блока с таким именем
                // если есть - выводим сообщение об ошибке и заканчиваем выполнение команды
                if (bt.Has(blockName))
                {
                    ed.WriteMessage(@"A block with the name ""{0}"" already exists.", blockName);
                    return;
                }

                // создаем новое определение блока, задаем ему имя
                BlockTableRecord btr = new BlockTableRecord();
                btr.Name = blockName;

                // запрашиваем координаты вставки блока
                var ptRes = editor.GetPoint("\nPick a point: ");
                point = ptRes.Value;


                // добавляем созданное определение блока в таблицу блоков и в транзакцию,
                // запоминаем ID созданного определения блока (оно пригодится чуть позже)
                ObjectId btrId = bt.Add(btr);
                trans.AddNewlyCreatedDBObject(btr, true);

                //***
                // ШАГ 2 - добавляем к созданной записи необходимые геометрические примитивы
                //***

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
                btr.AppendEntity(poly);
                trans.AddNewlyCreatedDBObject(poly, true);

                // создаем окружность
                Circle cir = new Circle();
                cir.SetDatabaseDefaults();
                cir.Center = new Point3d(0, 90, 0);
                cir.Radius = 15;

                // добавляем окружность в определение блока и в транзакцию
                btr.AppendEntity(cir);
                trans.AddNewlyCreatedDBObject(cir, true);

                // создаем текст
                DBText text = new DBText();
                text.Position = new Point3d(-25, -95, 0);
                text.Height = 25;
                text.TextString = desc;

                // добавляем текст в определение блока и в транзакцию
                btr.AppendEntity(text);
                trans.AddNewlyCreatedDBObject(text, true);

                //***
                // ШАГ 3 - добавляем вхождение созданного блока на чертеж
                //***

                // открываем пространство модели на запись
                BlockTableRecord ms = (BlockTableRecord)trans.GetObject(
                    bt[BlockTableRecord.ModelSpace],
                    OpenMode.ForWrite);

                // создаем новое вхождение блока, используя ранее сохраненный ID определения блока
                BlockReference br = new BlockReference(new Point3d(point.X, point.Y, point.Z), btrId);

                // добавляем созданное вхождение блока на пространство модели и в транзакцию
                ms.AppendEntity(br);
                trans.AddNewlyCreatedDBObject(br, true);

                // фиксируем транзакцию
                trans.Commit();
            }
        }
    }
}
