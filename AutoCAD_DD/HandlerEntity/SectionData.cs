using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace SectionConverterPlugin
{
    public class SectionData
    {
        private BlockTableRecord _axisPoint;
        private BlockTableRecord _heightPoint;
        private List<BlockTableRecord> _topPoints = new List<BlockTableRecord>();
        private List<BlockTableRecord> _bottomPoints = new List<BlockTableRecord>();

        public BlockTableRecord AxisPoint { get => _axisPoint; set => _axisPoint = value; }
        public BlockTableRecord HeightPoint { get => _heightPoint; set => _heightPoint = value; }
        public List<BlockTableRecord> TopPoints { get => _topPoints; set => _topPoints = value; }
        public List<BlockTableRecord> BottomPoints { get => _bottomPoints; set => _bottomPoints = value; }
    }
}
