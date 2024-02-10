using System;
using System.Collections.Generic;

namespace FrameCreator.Adapters.Controllers.Models
{
    public class AttributeSetRequestModel
    {
        public List<int> ElementIds { get; set; } = new List<int>();
        public string PartName { get; set; } = string.Empty;
        public string Profile { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public EventHandler OnSendingRequest { get; set; }
    }
}
