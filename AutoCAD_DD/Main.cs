using Autodesk.AutoCAD.Runtime;
using System.Windows.Forms;

namespace SectionConverterPlugin
{
    public class MainExtensionApplication : IExtensionApplication
    {
        /// <summary>
        /// Функция инициализации (выполняется при загрузке плагина)
        /// </summary>
        public void Initialize()
        {
            // TODO:
            // ПРОЧИТАТЬ ПРО ОТЛИЧИЕ ССЫЛОЧНЫХ ТИПОВ ДАННЫХ ОТ ВЕЩЕСТВЕННЫХ (class vs struct)
            // Заменить на нормальное приветствие
            // Почитать про NLog
            // Почитать про XML комментарии/документацию
            // Поискать список шорткатов на хабре
            MessageBox.Show("Hello!");
        }

        /// <summary>
        /// Функция, выполняемая при выгрузке плагина
        /// </summary>
        public void Terminate()
        {
            MessageBox.Show("Goodbye!");
        }
    }
}