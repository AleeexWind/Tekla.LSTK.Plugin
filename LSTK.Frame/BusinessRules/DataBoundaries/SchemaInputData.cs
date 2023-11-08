using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.DataBoundaries
{
    public class SchemaInputData : InputData
    {
        public double Bay { get; set; }
        public double HeightRoofRidge { get; set; }
        public double HeightRoofBottom { get; set; }
        public string Panels { get; set; }
        public double HeightColumns { get; set; }
        public string ExistedSchema { get; set; }
    }
}
