using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.Frameworks.DataBase
{
    //public static class DataBase
    //{
    //    public static int CurrentElementDataId;
    //    public static int CurrentAttributeGroupId;
    //    public static List<ElementData> SchemaElements { get; set; } = new List<ElementData>();
    //    public static List<AttributeGroup> AttributeGroups { get; set; } = new List<AttributeGroup>();
    //}
    public class DataBase
    {
        public int CurrentElementDataId { get; set; }
        public int CurrentAttributeGroupId { get; set; }
        public List<ElementData> SchemaElements { get; set; } = new List<ElementData>();
        public List<AttributeGroup> AttributeGroups { get; set; } = new List<AttributeGroup>();


        public int CurrentStateId { get; set; }
        public List<State> States { get; set; } = new List<State>();
    }
}
