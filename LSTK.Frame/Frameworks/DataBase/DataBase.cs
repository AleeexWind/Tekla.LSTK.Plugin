using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.Frameworks.DataBase
{
    public class DataBase
    {
        public List<ElementData> SchemaElements { get; set; } = new List<ElementData>();
        public List<AttributeGroup> AttributeGroups { get; set; } = new List<AttributeGroup>();
    }
}
