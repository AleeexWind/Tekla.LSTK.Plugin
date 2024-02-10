using LSTK.Frame.Adapters.Controllers;
using LSTK.Frame.Adapters.Gateways;
using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.UseCases;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using LSTK.Frame.BusinessRules.UseCases.Calculators.FrameCalculators;
using LSTK.Frame.Frameworks.TeklaAPI;
using System;
using System.Collections.Generic;
using Tekla.Structures.Model;
using Tekla.Structures.Plugins;
using Point = Tekla.Structures.Geometry3d.Point;

namespace LSTK.Frame
{
    [Plugin("FrameCreator")]
    [PluginUserInterface("FrameCreator")]
    public class MainPlugin : PluginBase
    {
        private readonly Model _model;
        private readonly PluginData _data;

        public MainPlugin(PluginData data)
        {
            _model = new Model();
            _data = data;
        }

        public override List<InputDefinition> DefineInput()
        {
            TeklaPointSelector teklaPointSelector = new TeklaPointSelector();
            List<InputDefinition> PointList = new List<InputDefinition>();

            (Point, Point) selectedPoints = teklaPointSelector.SelectPoints();
            InputDefinition Input1 = new InputDefinition(selectedPoints.Item1);
            InputDefinition Input2 = new InputDefinition(selectedPoints.Item2);

            PointList.Add(Input1);
            PointList.Add(Input2);

            return PointList;
        }
        public override bool Run(List<InputDefinition> Input)
        {
            try
            {
                Point Point1 = (Point)Input[0].GetInput();
                Point Point2 = (Point)Input[1].GetInput();
                _data.StartPoint = Point1;
                _data.DirectionPoint = Point2;

                bool doubleProfileOption = false;
                if (_data.DoubleProfileOption.Equals("Yes"))
                {
                    doubleProfileOption = true;
                }




                TeklaPartAttributeSetter teklaPartAttributeSetter = new TeklaPartAttributeSetter();
                LocalPlaneManager localPlaneManager = new LocalPlaneManager(_model);

                List<IDataCalculator> calculators = new List<IDataCalculator>()
                {
                    new ColumnsDataCalculator(),
                    new TopChordTrussDataCalculator(),
                    new BottomChordTrussDataCalculator(),
                    new TrussPostsCalculator(),
                    new DiagonalRodsDataCalculator()
                };

                ITargetAppAccess targetAppAccess = new TeklaAccess(_model, localPlaneManager, teklaPartAttributeSetter, doubleProfileOption);
                IFrameBuilder frameBuilder = new FrameBuilder(targetAppAccess, calculators);
                FrameBuildController frameBuildController = new FrameBuildController(frameBuilder);
                frameBuildController.BuildFrame(_data);
            }
            catch (Exception Ex)
            {
                //TODO: Logging
                throw;
            }
            return true;
        }
    }
    public class PluginData
    {
        public Point StartPoint { get; set; }
        public Point DirectionPoint { get; set; }

        [StructuresField("elementPrototypes")]
        public string ElementPrototypes;

        [StructuresField("bay")]
        public string Bay;

        [StructuresField("frameOption")]
        public string FrameOption;

        [StructuresField("topChordLineOption")]
        public string TopChordLineOption;
        [StructuresField("bottomChordLineOpt")]
        public string BottomChordLineOption;
        [StructuresField("columnLineOption")]
        public string ColumnLineOption;
        [StructuresField("centerColumnLineOpt")]
        public string CentralColumnLineOption;

        [StructuresField("profileGap")]
        public string ProfileGap;

        [StructuresField("doubleProfileOption")]
        public string DoubleProfileOption;
    }
}
