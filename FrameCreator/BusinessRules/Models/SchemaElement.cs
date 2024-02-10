using FrameCreator.Entities;

namespace FrameCreator.BusinessRules.Models
{
    public class SchemaElement
    {
        public int Id;
        public Point StartPoint;
        public Point EndPoint;
        public bool ToBeDrawn = true;
    }
}
