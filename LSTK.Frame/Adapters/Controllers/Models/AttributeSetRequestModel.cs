using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.Adapters.Controllers.Models
{
    public class AttributeSetRequestModel
    {
        public List<int> ElementIds { get; set; } = new List<int>();
        public string PartName { get; set; } = string.Empty;
        public string Profile { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public EventHandler OnSendingRequest;
    }
}
