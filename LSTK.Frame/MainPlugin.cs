using LSTK.Frame.Adapters.Controllers;
using LSTK.Frame.Adapters.Gateways;
using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.UseCases;
using LSTK.Frame.Frameworks.TeklaAPI;
using System;
using System.Collections.Generic;
using Tekla.Structures.Model;
using Tekla.Structures.Plugins;
using Point = Tekla.Structures.Geometry3d.Point;

namespace LSTK.Frame
{
    [Plugin("LSTK_Frame")]
    [PluginUserInterface("LSTK.Frame.MainWindow")]
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

                TeklaPartAttributeSetter teklaPartAttributeSetter = new TeklaPartAttributeSetter();
                LocalPlaneManager localPlaneManager = new LocalPlaneManager(_model);

                ITargetAppAccess targetAppAccess = new TeklaAccess(_model, localPlaneManager, teklaPartAttributeSetter);
                IFrameBuilder frameBuilder = new FrameBuilder2(targetAppAccess);
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
        //public override bool Run(List<InputDefinition> Input)
        //{
        //    try
        //    {
        //        GetValuesFromDialog();

        //        Point Point1 = (Point)Input[0].GetInput();
        //        Point Point2 = (Point)Input[1].GetInput();
        //        _data.StartPoint = Point1;
        //        _data.DirectionPoint = Point2;

        //        //FrameData frameData = new FrameData();
        //        //TeklaPartAttributeSetter teklaPartAttributeSetter = new TeklaPartAttributeSetter();
        //        //ITeklaAccess teklaAccess = new TeklaPartCreator(_model, teklaPartAttributeSetter);

        //        //List<IDataCalculator> calculators = new List<IDataCalculator>()
        //        //{
        //        //    new ColumnsDataCalculator(),
        //        //    new TopChordTrussDataCalculator(),
        //        //    new BottomChordTrussDataCalculator(),
        //        //    new TrussPostsCalculator()
        //        //};

        //        //LocalPlaneManager localPlaneManager = new LocalPlaneManager(_model);

        //        //FrameCreatorManager frameCreatorManager = new FrameCreatorManager(frameData, teklaAccess, calculators, localPlaneManager);

        //        //InterfaceDataController interfaceDataController = new InterfaceDataController(frameCreatorManager);

        //        //interfaceDataController.GatherInput(_data);
        //        //interfaceDataController.SendInput();

        //        //_frameCreatorManager.CreateFrame();
        //        List<ElementData> el = DataBase.SchemaElements;

        //        TeklaPartAttributeSetter teklaPartAttributeSetter = new TeklaPartAttributeSetter();
        //        LocalPlaneManager localPlaneManager = new LocalPlaneManager(_model);

        //        List<IDataCalculator> calculators = new List<IDataCalculator>()
        //        {
        //            new ColumnsDataCalculator(),
        //            new TopChordTrussDataCalculator(),
        //            new BottomChordTrussDataCalculator(),
        //            new TrussPostsCalculator()
        //        };

        //        ITargetAppAccess targetAppAccess = new TeklaAccess(_model, localPlaneManager, teklaPartAttributeSetter);
        //        IFrameBuilder frameBuilder = new FrameBuilder(targetAppAccess, calculators);
        //        FrameBuildController frameBuildController = new FrameBuildController(frameBuilder);
        //        frameBuildController.BuildFrame(_data);
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw;
        //    }
        //    return true;
        //}
        //public override bool Run(List<InputDefinition> Input)
        //{
        //    try
        //    {
        //        GetValuesFromDialog();

        //        Point Point1 = (Point)Input[0].GetInput();
        //        Point Point2 = (Point)Input[1].GetInput();
        //        _data.StartPoint = Point1;
        //        _data.DirectionPoint = Point2;

        //        FrameData frameData = new FrameData();
        //        TeklaPartAttributeSetter teklaPartAttributeSetter = new TeklaPartAttributeSetter();
        //        ITeklaAccess teklaAccess = new TeklaPartCreator(_model, teklaPartAttributeSetter);

        //        List<IDataCalculator> calculators = new List<IDataCalculator>()
        //        {
        //            new ColumnsDataCalculator(),
        //            new TopChordTrussDataCalculator(),
        //            new BottomChordTrussDataCalculator(),
        //            new TrussPostsCalculator()
        //        };

        //        LocalPlaneManager localPlaneManager = new LocalPlaneManager(_model);

        //        FrameCreatorManager frameCreatorManager = new FrameCreatorManager(frameData, teklaAccess, calculators, localPlaneManager);

        //        InterfaceDataController interfaceDataController = new InterfaceDataController(frameCreatorManager);

        //        interfaceDataController.GatherInput(_data);
        //        interfaceDataController.SendInput();

        //        _frameCreatorManager.CreateFrame();
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw;
        //    }
        //    return true;
        //}

        //public override bool Run(List<InputDefinition> Input)
        //{
        //    try
        //    {
        //        GetValuesFromDialog();

        //        Point Point1 = (Point)Input[0].GetInput();
        //        Point Point2 = (Point)Input[1].GetInput();
        //        _data.StartPoint = Point1;
        //        _data.DirectionPoint = Point2;

        //        FrameData frameData = new FrameData();
        //        TeklaPartAttributeSetter teklaPartAttributeSetter = new TeklaPartAttributeSetter();
        //        ITeklaAccess teklaAccess = new TeklaPartCreator(_model, teklaPartAttributeSetter);

        //        List<IDataCalculator> calculators = new List<IDataCalculator>()
        //        {
        //            new ColumnsDataCalculator(),
        //            new TopChordTrussDataCalculator(),
        //            new BottomChordTrussDataCalculator(),
        //            new TrussPostsCalculator()
        //        };

        //        LocalPlaneManager localPlaneManager = new LocalPlaneManager(_model);

        //        FrameCreatorManager frameCreatorManager = new FrameCreatorManager(frameData, teklaAccess, calculators, localPlaneManager);

        //        InterfaceDataController interfaceDataController = new InterfaceDataController(frameCreatorManager);

        //        interfaceDataController.GatherInput(_data);
        //        interfaceDataController.SendInput();

        //        _frameCreatorManager.CreateFrame();
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw;
        //    }
        //    return true;
        //}

        private void GetValuesFromDialog()
        {
            //_partNameColumns = Data.PartNameColumns;
            //_profileColumns = Data.ProfileColumns;
            //_heightColumns = Data.HeightColumns;
            //_profileTopChord = Data.ProfileTopChord;
            //_bay = Data.Bay;
            //_roofRidgeHeight = Data.HeightRoofRidge;
            //_roofBottomHeight = Data.HeightRoofBottom;
            //_frameOption = Data.FrameOption;

            //if (IsDefaultValue(_partNameColumns))
            //    _partNameColumns = "TEST";
            //if (IsDefaultValue(_profileColumns))
            //    _profileColumns = "ПСУ400х100х20х3,0";
            //if (IsDefaultValue(_heightColumns))
            //    _heightColumns = "5000";
            //if (IsDefaultValue(_profileTopChord))
            //    _profileTopChord = "ПСУ300х100х20х2,0";
            //if (IsDefaultValue(_bay))
            //    _bay = "20000";
        }
    }
    public class PluginData
    {
        #region Fields
        //
        // Define the fields specified on the Form.
        //
        #endregion

        public Point StartPoint { get; set; }
        public Point DirectionPoint { get; set; }

        [StructuresField("elementPrototypes")]
        public string ElementPrototypes;

        [StructuresField("topChordLineOption")]
        public string TopChordLineOption;
        [StructuresField("bottomChordLineOption")]
        public string BottomChordLineOption;
        [StructuresField("columnLineOption")]
        public string ColumnLineOption;
        [StructuresField("centralColumnLineOption")]
        public string CentralColumnLineOption;
    }
}
