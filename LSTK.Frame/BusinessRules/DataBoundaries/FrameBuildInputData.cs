using LSTK.Frame.BusinessRules.Models;

namespace LSTK.Frame.BusinessRules.DataBoundaries
{
    public class FrameBuildInputData : InputData
    {
        public bool IsHalfOption { get; set; }
        public double Bay { get; set; }
        public string ColumnLineOption { get; set; } = string.Empty;
        public string TopChordLineOption { get; set; } = string.Empty;
        public FrameData FrameData { get; set; }
    }
}
