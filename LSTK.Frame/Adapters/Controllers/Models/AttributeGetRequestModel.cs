using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.Adapters.Controllers.Models
{
    public class AttributeGetRequestModel
    {
        public List<int> ElementIds { get; set; } = new List<int>();
        public EventHandler OnSendingRequest;
    }
}
