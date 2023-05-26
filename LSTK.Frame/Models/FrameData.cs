using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace LSTK.Frame.Models
{
    public class FrameData
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }


        public double ColumnHeight { get; set; }
        public string ColumnProfile { get; set; }
        public string ColumnMaterial { get; set; }
        public Position.RotationEnum RotationEnum { get; set; }
        public Position.PlaneEnum PlaneEnum { get; set; }
        public Position.DepthEnum DepthEnum { get; set; }

    }
}
