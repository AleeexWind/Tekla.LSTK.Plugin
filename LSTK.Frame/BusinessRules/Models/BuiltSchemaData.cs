using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.Models
{
    public class BuiltSchemaData
    {
        public List<ElementData> ElementDatas { get; set; }
        public double CoordXmax { get; set; }
        public double CoordYmax { get; set; }
        public double Yoffset { get; set; }
    }
}
