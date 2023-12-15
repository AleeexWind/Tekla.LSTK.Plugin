using System;
using System.Collections.Generic;

namespace LSTK.Frame.Adapters.Controllers.Models
{
    public class RotateRequestModel
    {
        public List<int> ElementIds { get; set; } = new List<int>();
        public EventHandler OnSendingRequest { get; set; }
    }
}
