using FrameCreator.Entities;
using System.Collections.Generic;

namespace FrameCreator.Frameworks.DataBase
{
    public class DataBase
    {
        public int CurrentElementDataId { get; set; }
        public List<ElementData> SchemaElements { get; set; } = new List<ElementData>();

        public int CurrentStateId { get; set; }
        public List<State> States { get; set; } = new List<State>();
    }
}
