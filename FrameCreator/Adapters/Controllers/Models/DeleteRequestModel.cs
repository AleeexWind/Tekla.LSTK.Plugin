using System;
using System.Collections.Generic;

namespace FrameCreator.Adapters.Controllers.Models
{
    public class DeleteRequestModel
    {
        public List<int> ElementIds { get; set; } = new List<int>();
        public EventHandler OnSendingRequest { get; set; }
    }
}
