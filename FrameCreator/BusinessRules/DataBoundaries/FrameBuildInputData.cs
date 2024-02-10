using FrameCreator.BusinessRules.Models;

namespace FrameCreator.BusinessRules.DataBoundaries
{
    public class FrameBuildInputData : InputData
    {
        public bool IsHalfOption { get; set; }
        public double Bay { get; set; }
        public string ColumnLineOption { get; set; } = string.Empty;
        public string TopChordLineOption { get; set; } = string.Empty;
        public string BottomChordLineOption { get; set; } = string.Empty;

        public bool DoubleProfileOption { get; set; }
        public FrameData FrameData { get; set; }
    }
}
