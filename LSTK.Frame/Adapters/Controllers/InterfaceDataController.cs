using PointTekla = Tekla.Structures.Geometry3d;
using PointPlugin = LSTK.Frame.Entities;
using LSTK.Frame.BusinessRules.DataBoundaries;

namespace LSTK.Frame.Adapters.Controllers
{
    public class InterfaceDataController
    {
        private readonly IFrameDataInputBoundary _inputBoundary;
        private FrameInputData _inputData;

        public InterfaceDataController(IFrameDataInputBoundary inputBoundary)
        {
            _inputBoundary = inputBoundary;
        }
        public void GatherInput(PluginData pluginData)
        {
            FrameInputData inputData = new FrameInputData
            {
                PartNameColumns = pluginData.PartNameColumns,
                ProfileColumns = pluginData.ProfileColumns,
                MaterialColumns = pluginData.MaterialColumns,
                ClassColumns = pluginData.ClassColumns,
                HeightColumns = double.Parse(pluginData.HeightColumns, System.Globalization.CultureInfo.InvariantCulture),

                PartNameTopChord = pluginData.PartNameTopChord,
                ProfileTopChord = pluginData.ProfileTopChord,
                MaterialTopChord = pluginData.MaterialTopChord,
                ClassTopChord = pluginData.ClassTopChord,
                HeightRoofRidge = double.Parse(pluginData.HeightRoofRidge, System.Globalization.CultureInfo.InvariantCulture),

                PartNameBottomChord = pluginData.PartNameBottomChord,
                ProfileBottomChord = pluginData.ProfileBottomChord,
                MaterialBottomChord = pluginData.MaterialBottomChord,
                ClassBottomChord = pluginData.ClassBottomChord,
                HeightRoofBottom = double.Parse(pluginData.HeightRoofBottom, System.Globalization.CultureInfo.InvariantCulture),

                Bay = double.Parse(pluginData.Bay, System.Globalization.CultureInfo.InvariantCulture),
                FrameOption = pluginData.FrameOption,
                StartPoint = TransformatePoint(pluginData.StartPoint),
                DirectionPoint = TransformatePoint(pluginData.DirectionPoint)
            };
            _inputData = inputData;
        }
        public void SendInput()
        {
            _inputBoundary.TransferInputData(_inputData);
        }
        private PointPlugin.Point TransformatePoint(PointTekla.Point point)
        {
            PointPlugin.Point targetPoint = new PointPlugin.Point()
            {
                X = point.X,
                Y = point.Y,
                Z = point.Z
            };
            return targetPoint;
        }
    }
}
