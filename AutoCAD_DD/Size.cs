using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectionConverterPlugin
{
    public struct Size
    {
        public double _width;
        public double _height;

        public double Width
            {
            get { return _width; }
            set { _width = value; }
            }

        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }
    }

}
