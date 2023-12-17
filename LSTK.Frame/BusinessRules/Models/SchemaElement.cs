using LSTK.Frame.Entities;

namespace LSTK.Frame.BusinessRules.Models
{
    public class SchemaElement
    {
        public int Id;
        public Point StartPoint;
        public Point EndPoint;
        public bool ToBeDrawn = true;
    }
}
