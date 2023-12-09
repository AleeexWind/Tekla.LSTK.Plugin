using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.Adapters.Controllers.Models
{
    internal class NewSessionBuildSchemaRequestModel
    {
        public string ExistedSchema { get; set; }
        public bool FirstBuild { get; set; } = true;
        public EventHandler OnSendingRequest { get; set; }
    }
}
