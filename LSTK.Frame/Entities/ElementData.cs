using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.Entities
{
    public class ElementData
    {
        public Point StartPoint { get; set; } = new Point();
        public Point EndPoint { get; set; } = new Point();

        public string Profile { get; set; } = string.Empty;
        public string PartName { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        //public string Position { get; set; } = string.Empty;

        public string RotationPosition { get; set; } = string.Empty;

        public string PlanePosition { get; set; } = string.Empty;

        public string DepthPosition { get; set; } = string.Empty;
    }
}
