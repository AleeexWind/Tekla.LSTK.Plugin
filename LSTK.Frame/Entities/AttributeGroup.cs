namespace LSTK.Frame.Entities
{
    public class AttributeGroup
    {
        public int Id { get; set; }
        public string PartName { get; set; } = string.Empty;
        public string Profile { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string RotationPosition { get; set; } = "FRONT";
        public string PlanePosition { get; set; } = "MIDDLE";
        public string DepthPosition { get; set; } = "MIDDLE";
    }
}
