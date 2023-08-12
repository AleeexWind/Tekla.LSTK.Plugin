using LSTK.Frame.Entities;

namespace LSTK.Frame.BusinessRules.DataBoundaries
{
    public class FrameInputData
    {
        //Columns section
        public string ProfileColumns { get; set; } = string.Empty;
        public string PartNameColumns { get; set; } = string.Empty;
        public string MaterialColumns { get; set; } = string.Empty;
        public string ClassColumns { get; set; } = string.Empty;
        public double HeightColumns { get; set; }
        public string RotationPositionColumns { get; set; } = "FRONT";
        public string PlanePositionColumns { get; set; } = "MIDDLE";
        public string DepthPositionColumns { get; set; } = "MIDDLE";


        //TopChord section
        public string ProfileTopChord { get; set; } = string.Empty;
        public string PartNameTopChord { get; set; } = string.Empty;
        public string MaterialTopChord { get; set; } = string.Empty;
        public string ClassTopChord { get; set; } = string.Empty;
        public double RoofRidgeHeight { get; set; }
        public string RotationPositionTopChord { get; set; } = "FRONT";
        public string PlanePositionTopChord { get; set; } = "MIDDLE";
        public string DepthPositionTopChords { get; set; } = "MIDDLE";


        //BottomChord section
        public string ProfileBottomChord { get; set; } = string.Empty;
        public string PartNameBottomChord { get; set; } = string.Empty;
        public string MaterialBottomChord { get; set; } = string.Empty;
        public string ClassBottomChord { get; set; } = string.Empty;
        public double RoofBottomHeight { get; set; }
        public string RotationPositionBottomChord { get; set; } = "FRONT";
        public string PlanePositionBottomChord { get; set; } = "MIDDLE";
        public string DepthPositionBottomChord { get; set; } = "MIDDLE";


        //Common
        public double Bay { get; set; }
        public string FrameOption { get; set; } = string.Empty;

        public Point StartPoint { get; set; } = new Point();
        public Point DirectionPoint { get; set; } = new Point();
    }
}
