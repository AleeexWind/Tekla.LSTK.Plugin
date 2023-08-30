using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.Entities
{
    class ProfileGroup
    {
        public string Profile { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public List<ElementData> Elements { get; set; } = new List<ElementData>();
    }
}
