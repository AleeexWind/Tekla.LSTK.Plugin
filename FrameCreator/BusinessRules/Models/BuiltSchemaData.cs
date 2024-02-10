using FrameCreator.Entities;
using System.Collections.Generic;

namespace FrameCreator.BusinessRules.Models
{
    public class BuiltSchemaData
    {
        public List<ElementData> ElementDatas { get; set; }
        public double CoordXmax { get; set; }
        public double CoordYmax { get; set; }
        public double Yoffset { get; set; }
    }
}
