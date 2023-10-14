using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.Models
{
    public class FrameData2
    {
        public Point StartPoint { get; set; } = new Point();
        public Point DirectionPoint { get; set; } = new Point();
        public List<ElementData> Elements { get; set; } = new List<ElementData>();
    }
}
