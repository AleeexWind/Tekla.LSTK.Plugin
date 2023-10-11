using LSTK.Frame.BusinessRules.Models;

namespace LSTK.Frame.Entities
{
    public class ElementData
    {
        public int Id { get; set; }
        public ElementGroupType ElementGroupType { get; set; }
        public Point StartPoint { get; set; } = new Point();
        public Point EndPoint { get; set; } = new Point();

        public string Profile { get; set; } = string.Empty;
        public double ProfileHeight { get; set; }
        public string PartName { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;

        public string RotationPosition { get; set; } = string.Empty;
        public string PlanePosition { get; set; } = string.Empty;
        public string DepthPosition { get; set; } = string.Empty;

        public int AttributeGroupId { get; set; }
    }
}
