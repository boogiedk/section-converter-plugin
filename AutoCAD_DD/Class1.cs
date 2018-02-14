using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;
using System.Reflection;
using AutoCAD_DD;
using acad = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.Interop.Common;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System.Linq;

namespace MyAutoCADDll
{
    public class Commands : IExtensionApplication
    {
        // функция инициализации (выполняется при загрузке плагина)
        public void Initialize()
        {
            MessageBox.Show("Hello!");
        }

        // функция, выполняемая при выгрузке плагина
        public void Terminate()
        {
            MessageBox.Show("Goodbye!");
        }

        // эта функция будет вызываться при выполнении в AutoCAD команды «TestCommand»

        [CommandMethod("TestCommand")]
        public void MyCommand()
        {
            MessageBox.Show("RunPlugin");

            using (var f = new PluginN1())
                f.ShowDialog();
        }
    }
}