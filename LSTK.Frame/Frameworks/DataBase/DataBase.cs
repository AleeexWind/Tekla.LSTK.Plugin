using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.Frameworks.DataBase
{
    public static class DataBase
    {
        public static List<ElementData> SchemaElements { get; set; } = new List<ElementData>();
        public static List<AttributeGroup> AttributeGroups { get; set; } = new List<AttributeGroup>();
    }
}
