using LSTK.Frame.Interactors.Boundaries;
using LSTK.Frame.Interactors.DataStructures;
using LSTK.Frame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
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
                ProfileColumns = pluginData.ProfileColumns,
                PartNameColumns = pluginData.PartNameColumns,
                MaterialColumns = pluginData.MaterialColumns,
                ClassColumns = pluginData.ClassColumns,
                HeightColumns = double.Parse(pluginData.HeightColumns, System.Globalization.CultureInfo.InvariantCulture),

                ProfileTopChord = pluginData.ProfileTopChord,
                PartNameTopChord = pluginData.PartNameTopChord,
                MaterialTopChord = pluginData.MaterialTopChord,
                ClassTopChord = pluginData.ClassTopChord,
                RoofRidgeHeight = double.Parse(pluginData.RoofRidgeHeight, System.Globalization.CultureInfo.InvariantCulture),

                ProfileBottomChord = pluginData.ProfileBottomChord,
                PartNameBottomChord = pluginData.PartNameBottomChord,
                MaterialBottomChord = pluginData.MaterialBottomChord,
                ClassBottomChord = pluginData.ClassBottomChord,
                RoofBottomHeight = double.Parse(pluginData.RoofBottomHeight, System.Globalization.CultureInfo.InvariantCulture),

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
