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
using PointPlugin = LSTK.Frame.Interactors.DataStructures;

namespace LSTK.Frame.Controllers
{
    public class InterfaceDataController
    {
        //private MainWindowViewModel _mainWindowViewModel;
        private FrameData _frameData;
        private PluginData _pluginData;
        private InputData _inputData;
        private IBoundaryInput _inputBoundary;
        public InterfaceDataController(PluginData pluginData, FrameData frameData)
        {
            //_mainWindowViewModel = mainWindowViewModel;
            _pluginData = pluginData;
            _frameData = frameData;
        }
        public InterfaceDataController(PluginData pluginData, IBoundaryInput inputBoundary)
        {
            //_mainWindowViewModel = mainWindowViewModel;
            _pluginData = pluginData;
            _inputBoundary = inputBoundary;
            _inputData = new InputData();

        }
        public InputData GatherInput()
        {
            _inputData.ProfileColumns = _pluginData.ProfileColumns;
            _inputData.PartNameColumns = _pluginData.PartNameColumns;
            _inputData.MaterialColumns = _pluginData.MaterialColumns;
            _inputData.ClassColumns = _pluginData.ClassColumns;
            _inputData.HeightColumns = double.Parse(_pluginData.HeightColumns, System.Globalization.CultureInfo.InvariantCulture);

            _inputData.ProfileTopChord = _pluginData.ProfileTopChord;
            _inputData.PartNameTopChord = _pluginData.PartNameTopChord;
            _inputData.MaterialTopChord = _pluginData.MaterialTopChord;
            _inputData.ClassTopChord = _pluginData.ClassTopChord;
            _inputData.RoofRidgeHeight = double.Parse(_pluginData.RoofRidgeHeight, System.Globalization.CultureInfo.InvariantCulture);

            _inputData.ProfileBottomChord = _pluginData.ProfileBottomChord;
            _inputData.PartNameBottomChord = _pluginData.PartNameBottomChord;
            _inputData.MaterialBottomChord = _pluginData.MaterialBottomChord;
            _inputData.ClassBottomChord = _pluginData.ClassBottomChord;
            _inputData.RoofBottomHeight = double.Parse(_pluginData.RoofBottomHeight, System.Globalization.CultureInfo.InvariantCulture);

            _inputData.Bay = double.Parse(_pluginData.Bay, System.Globalization.CultureInfo.InvariantCulture);
            _inputData.FrameOption = _pluginData.FrameOption;
            _inputData.StartPoint = TransformatePoint(_pluginData.StartPoint);
            _inputData.DirectionPoint = TransformatePoint(_pluginData.DirectionPoint);
            SendInput();
            return _inputData;
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

        public FrameData GetColumnProfileInput()
        {
            _frameData.ProfileColumns = _pluginData.ProfileColumns;
            return _frameData;
        }
        public FrameData GetColumnNameInput()
        {
            _frameData.PartNameColumns = _pluginData.PartNameColumns;
            return _frameData;
        }
        public FrameData GetLeftColumnCoordinatesInput()
        {
            //double endPointY = double.Parse(_pluginData.HeightColumns, System.Globalization.CultureInfo.InvariantCulture) ;
            //_frameData.EndPointLeftColumn = new Point(_frameData.StartPointLeftColumn.X, endPointY, _frameData.StartPointLeftColumn.Z);
            return _frameData;
        }
        public FrameData GetRightColumnCoordinatesInput()
        {
            //double pointX = double.Parse(_pluginData.BayOverall, System.Globalization.CultureInfo.InvariantCulture);

            //_frameData.StartPointRightColumn = new Point(pointX, _frameData.StartPointLeftColumn.Y, _frameData.StartPointLeftColumn.Z);

            //_frameData.EndPointRightColumn = new Point(_frameData.StartPointRightColumn.X, _frameData.EndPointLeftColumn.Y, _frameData.StartPointLeftColumn.Z);
            return _frameData;
        }
        public FrameData GetStartPointInput()
        {
            _frameData.StartPoint = _pluginData.StartPoint;
            return _frameData;
        }
        public FrameData GetDirectionPointInput()
        {
            _frameData.DirectionPoint = _pluginData.DirectionPoint;
            return _frameData;
        }
    }
}
