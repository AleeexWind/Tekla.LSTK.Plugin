using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace LSTK.Frame.Models
{
    public class FrameData
    {
        #region This is the coordinates in global
        public Point StartPoint { get; set; }
        public Point DirectionPoint { get; set; }
        #endregion


        #region This is the coordinates in Local
        public Point StartPointLeftColumn { get; set; } = new Point(0, 0, 0);
        public Point EndPointLeftColumn { get; set; }
        public Point StartPointRightColumn { get; set; }
        public Point EndPointRightColumn { get; set; }


        public Point StartPointLeftTopChord { get; set; }
        public Point EndPointLeftTopChord { get; set; }
        public Point StartPointRightTopChord { get; set; }
        public Point EndPointRightTopChord { get; set; }
        #endregion


        public string PartNameColumns { get; set; }
        public double HeightColumns { get; set; }
        public string ProfileColumns { get; set; }
        public string MaterialColumns { get; set; }
        public Position.RotationEnum RotationEnum { get; set; }
        public Position.PlaneEnum PlaneEnum { get; set; }
        public Position.DepthEnum DepthEnum { get; set; }
    }
}