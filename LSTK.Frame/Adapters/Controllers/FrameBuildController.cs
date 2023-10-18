using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LSTK.Frame.Adapters.Controllers
{
    public class FrameBuildController
    {
        private readonly IFrameBuilder _frameBuilder;
        public FrameBuildController(IFrameBuilder frameBuilder)
        {
            _frameBuilder = frameBuilder;
        }
        public void BuildFrame(PluginData pluginData)
        {
            FrameData inputData = GatherInput(pluginData);
            _frameBuilder.BuildFrame(inputData);
        }
        private FrameData GatherInput(PluginData pluginData)
        {
            FrameData inputData = new FrameData();
            try
            {
                List<ElementData> elementDatas = JsonConvert.DeserializeObject<List<ElementData>>(pluginData.ElementPrototypes);
                inputData.Elements = elementDatas;
            }
            catch (System.Exception)
            {
                throw;
            }

            return inputData;
        }
    }
}
