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
            FrameInputData inputData = GatherInput(pluginData);
            _frameBuilder.BuildFrame(inputData);
        }
        private FrameInputData GatherInput(PluginData pluginData)
        {
            FrameInputData inputData = new FrameInputData();
            //try
            //{
            //    inputData.AttributeGroups = JsonConvert.DeserializeObject<List<AttributeGroup>>(pluginData.AttributeGroups);
            //    inputData.ElementDataPrototypes = JsonConvert.DeserializeObject<List<ElementDataPrototype>>(pluginData.ElementDataPrototypes);
            //}
            //catch (System.Exception)
            //{
            //    throw;
            //}

            return inputData;
        }
    }
}
