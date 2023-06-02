using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace UnitTests.Models
{
    public class FrameDataTest
    {
        #region This is the coordinates in global
        public Point StartPoint { get; set; } = new Point(0, 0, 0);
        public Point DirectionPoint { get; set; } = new Point(1000, 0, 0);
        #endregion

        #region This is the coordinates in Local
        //Left column 
        public Point StartPointLeftColumn { get; set; } = new Point(0, 0, 0);
        public Point EndPointLeftColumn { get; set; } = new Point(0, 3000, 0);
        //Right column
        public Point StartPointRightColumn { get { return GetStartPointRightColumn(); } }
        public Point EndPointRightColumn { get { return GetEndPointRightColumn(); } }
        #endregion

        public double BayOverall { get; set; } = 22100;

        public string PartNameColumns { get; set; } = "COLUMN11";
        public string ProfileColumns { get; set; } = "ПСУ400х100х20х3,0";
        public string MaterilColumns { get; set; } = "350";
        public string ClassColumns { get; set; } = "2";

        public double HeightColumns { get; set; } = 3000;


        private Point GetStartPointRightColumn()
        {
            return new Point(BayOverall, StartPointLeftColumn.Y, StartPointLeftColumn.Z);
        }
        private Point GetEndPointRightColumn()
        {
            return new Point(BayOverall, EndPointLeftColumn.Y, EndPointLeftColumn.Z);
        }
    }
}
