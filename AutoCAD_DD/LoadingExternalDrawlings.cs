using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.IO;

namespace SectionConverterPlugin
{
    class LoadingExternalDrawlings
    {
        /// <param name="targetDb">Целевая база данных</param>
        /// <param name="sourceFileName">Имя чертежа, в котором находятся нужные определения блоков</param>
        /// <param name="password">пароль для открытия файла, в котором хранятся нужные определения блоков</param>
        /// <param name="blockDefinitionNames">имена определений блоков, подлежащие импорту</param>
        /// <param name="behaviour">Как поступать, если в целевой базе данных уже имеется определение блока с таким именем</param>

        public void BlockDefinitionsImport(Database targetDb, string sourceFileName, string password,
              string blockDefinitionNames, DuplicateRecordCloning behaviour)
        {
            if (!File.Exists(sourceFileName))
                throw new FileNotFoundException(sourceFileName);
            using (var sourceDatabase = new Database(false, true))
            {

                try
                {
                    // Считываем содержимое чертежа (dwg-файла) в объект базы данных
                    sourceDatabase.ReadDwgFile(sourceFileName, FileShare.Read, true, password);

                    var blockIds = new ObjectIdCollection();

                    using (Transaction transaction = sourceDatabase.TransactionManager.StartTransaction())
                    {
                        // Открываем таблицу блоков
                        BlockTable bt = (BlockTable)transaction.GetObject(sourceDatabase.BlockTableId, OpenMode.ForRead, false);

                        // Извлекаем нужные определения блоков
                        foreach (var blockTableRecord in bt.Cast<ObjectId>().Select(n =>
                            (BlockTableRecord)transaction.GetObject(n, OpenMode.ForRead, false))
                          .Where(n => !n.IsAnonymous && !n.IsLayout && blockDefinitionNames.Contains(n.Name)))
                        {
                            blockIds.Add(blockTableRecord.ObjectId);
                            blockTableRecord.Dispose();
                        }
                        transaction.Commit();
                    }
                    // Копируем определения блоков в нужную нам базу данных
                    var mapping = new IdMapping();
                    sourceDatabase.WblockCloneObjects(blockIds, targetDb.BlockTableId, mapping, behaviour, false);
                }
                catch (Autodesk.AutoCAD.Runtime.Exception ex)
                {
                    throw new Exception(string.Format("В процессе импорта произошла ошибка: {0}" + ex.Message));
                }
            }
        }


        static public void InsertBlock()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            using (Database OpenDb = new Database(false, true))
            {
                OpenDb.ReadDwgFile("c:\\test.dwg",
                    FileShare.ReadWrite, true, "");

                ObjectIdCollection ids = new ObjectIdCollection();
                using (Transaction tr = OpenDb.TransactionManager.StartTransaction())
                {
                    // Для примера возьмем блок с именем "TEST"
                    BlockTable bt;
                    bt = (BlockTable)tr.GetObject(OpenDb.BlockTableId,OpenMode.ForRead);

                    if (bt.Has("636544821624451378"))
                    {
                        ids.Add(bt["636544821624451378"]);
                    }
                    tr.Commit();
                }

                // Если нашли – добавим блок
                if (ids.Count != 0)
                {
                    // Получаем текущую базу чертежа
                    Database destdb = doc.Database;
                    IdMapping iMap = new IdMapping();
                    destdb.WblockCloneObjects(ids, destdb.BlockTableId,iMap,DuplicateRecordCloning.Replace, false);
                }
            }
        }
    }
}
