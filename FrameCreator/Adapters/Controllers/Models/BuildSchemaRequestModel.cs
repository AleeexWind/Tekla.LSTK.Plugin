using System;

namespace FrameCreator.Adapters.Controllers.Models
{
    public class BuildSchemaRequestModel
    {
        public string Bay { get; set; }
        public string HeightRoofRidge { get; set; }
        public string HeightRoofBottom { get; set; }
        public string Panels { get; set; }
        public string HeightColumns { get; set; }
        public string ExistedSchema { get; set; }
        public bool FirstBuild { get; set; } = true;
        public EventHandler OnSendingRequest { get; set; }
    }
}
