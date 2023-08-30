using System.Collections.Generic;

namespace LSTK.Frame.Entities
{
    public class TrussData
    {
        public ElementData LeftTopChord { get; set; }
        public ElementData RightTopChord { get; set; }
        public ElementData LeftBottomChord { get; set; }
        public ElementData RightBottomChord { get; set; }
        public List<ElementData> TrussPosts { get; set; }
    }
}
