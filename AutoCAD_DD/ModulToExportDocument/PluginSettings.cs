using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectionConverterPlugin
{
    public class PluginSettings
    {
        private static PluginSettings _instance;

        private Size _sectionMaxSize;

        private PluginSettings() { }

        public static PluginSettings GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PluginSettings();
            }
            return _instance;
        }

        public Size SectionMaxSize
        {
            get => _sectionMaxSize;
            set => _sectionMaxSize = value;
        }
    }
}
