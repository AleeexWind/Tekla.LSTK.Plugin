using FrameCreator.BusinessRules.DataBoundaries;
using FrameCreator.BusinessRules.Models;
using FrameCreator.Entities;
using FrameCreator.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FrameCreator.Adapters.Controllers
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
            FrameBuildInputData frameBuildInputData = GatherInput(pluginData);
            _frameBuilder.BuildFrame(frameBuildInputData);
        }
        private FrameBuildInputData GatherInput(PluginData pluginData)
        {
            FrameBuildInputData frameBuildInputData = new FrameBuildInputData();
            FrameData inputData = new FrameData();
            try
            {
                List<ElementData> elementDatas = JsonConvert.DeserializeObject<List<ElementData>>(pluginData.ElementPrototypes);
                elementDatas = elementDatas.Where(x => !x.IsDeleted).ToList();
                inputData.Elements = elementDatas;
                inputData.StartPoint = TeklaPointConverter.ConvertPoint(pluginData.StartPoint);
                inputData.DirectionPoint = TeklaPointConverter.ConvertPoint(pluginData.DirectionPoint);
                inputData.Gap = double.Parse(pluginData.ProfileGap, System.Globalization.CultureInfo.InvariantCulture);

                frameBuildInputData.FrameData = inputData;
                frameBuildInputData.IsHalfOption = false;
                frameBuildInputData.DoubleProfileOption = false;
                frameBuildInputData.Bay = double.Parse(pluginData.Bay, System.Globalization.CultureInfo.InvariantCulture);
                frameBuildInputData.ColumnLineOption = pluginData.ColumnLineOption;
                frameBuildInputData.TopChordLineOption = pluginData.TopChordLineOption;
                frameBuildInputData.BottomChordLineOption = pluginData.BottomChordLineOption;
                if (pluginData.FrameOption.Equals("Half"))
                {
                    frameBuildInputData.IsHalfOption = true;
                }
                if (pluginData.DoubleProfileOption.Equals("Yes"))
                {
                    frameBuildInputData.DoubleProfileOption = true;
                }
            }
            catch (System.Exception)
            {
                throw;
            }

            return frameBuildInputData;
        }
    }
}
