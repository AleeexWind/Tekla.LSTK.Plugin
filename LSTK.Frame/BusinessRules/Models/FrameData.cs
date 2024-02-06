using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.Models
{
    public class FrameData
    {
        public Point StartPoint { get; set; } = new Point();
        public Point DirectionPoint { get; set; } = new Point();
        public List<ElementData> Elements { get; set; } = new List<ElementData>();

        public double Gap { get; set; }
    }
}
