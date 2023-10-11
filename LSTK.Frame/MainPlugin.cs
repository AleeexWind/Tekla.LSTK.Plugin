using LSTK.Frame.Adapters.Controllers;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.UseCases;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.TeklaAPI;
using LSTK.Frame.Utils;
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
        private Model _model;
        private PluginData _data;
        private FrameCreatorManager _frameCreatorManager;

        //private Model Model
        //{
        //    get { return this._model; }
        //    set { this._model = value; }
        //}

        //private PluginData Data
        //{
        //    get { return this._data; }
        //    set { this._data = value; }
        //}

        public MainPlugin(PluginData data)
        {
            _model = new Model();
            _data = data;
            try
            {
                ServiceProvider.GetService<MainWindowViewModel>();
            }
            catch (Exception)
            {
                _data = data;
            }
           // ServiceProvider.AddService<MainWindowViewModel>();
            
            //FrameData frameData = new FrameData();
            //TeklaPartAttributeSetter teklaPartAttributeSetter = new TeklaPartAttributeSetter();
            //ITeklaAccess teklaAccess = new TeklaPartCreator(_model, teklaPartAttributeSetter);

            //List<IDataCalculator> calculators = new List<IDataCalculator>()
            //    {
            //        new ColumnsDataCalculator(),
            //        new TopChordTrussDataCalculator(),
            //        new BottomChordTrussDataCalculator(),
            //        new TrussPostsCalculator()
            //    };

            //LocalPlaneManager localPlaneManager = new LocalPlaneManager(_model);

            //_frameCreatorManager = new FrameCreatorManager(frameData, teklaAccess, calculators, localPlaneManager);

            //InterfaceDataController interfaceDataController = new InterfaceDataController(_frameCreatorManager);




            //BuildSchemaController buildSchemaController = new BuildSchemaController(_data, dataModel, interfaceDataController);
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
                GetValuesFromDialog();

                Point Point1 = (Point)Input[0].GetInput();
                Point Point2 = (Point)Input[1].GetInput();
                _data.StartPoint = Point1;
                _data.DirectionPoint = Point2;

                FrameData frameData = new FrameData();
                TeklaPartAttributeSetter teklaPartAttributeSetter = new TeklaPartAttributeSetter();
                ITeklaAccess teklaAccess = new TeklaPartCreator(_model, teklaPartAttributeSetter);

                List<IDataCalculator> calculators = new List<IDataCalculator>()
                {
                    new ColumnsDataCalculator(),
                    new TopChordTrussDataCalculator(),
                    new BottomChordTrussDataCalculator(),
                    new TrussPostsCalculator()
                };

                LocalPlaneManager localPlaneManager = new LocalPlaneManager(_model);

                FrameCreatorManager frameCreatorManager = new FrameCreatorManager(frameData, teklaAccess, calculators, localPlaneManager);

                InterfaceDataController interfaceDataController = new InterfaceDataController(frameCreatorManager);

                interfaceDataController.GatherInput(_data);
                interfaceDataController.SendInput();

                _frameCreatorManager.CreateFrame();
            }
            catch (Exception Ex)
            {
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

        //Columns section
        [StructuresField("partNameColumns")]
        public string PartNameColumns;
        [StructuresField("profileColumns")]
        public string ProfileColumns;
        [StructuresField("materialColumns")]
        public string MaterialColumns;
        [StructuresField("classColumns")]
        public string ClassColumns;
        [StructuresField("heightColumns")]
        public string HeightColumns;


        //TopChord section
        [StructuresField("partNameTopChord")]
        public string PartNameTopChord;
        [StructuresField("profileTopChord")]
        public string ProfileTopChord;
        [StructuresField("materialTopChord")]
        public string MaterialTopChord;
        [StructuresField("classTopChord")]
        public string ClassTopChord;
        [StructuresField("heightRoofRidge")]
        public string HeightRoofRidge;


        //BottomChord section
        [StructuresField("partNameBottomChord")]
        public string PartNameBottomChord;
        [StructuresField("profileBottomChord")]
        public string ProfileBottomChord;
        [StructuresField("materialBottomChord")]
        public string MaterialBottomChord;
        [StructuresField("classBottomChord")]
        public string ClassBottomChord;
        [StructuresField("heightRoofBottom")]
        public string HeightRoofBottom;

        //BottomChord section
        [StructuresField("partNameGroup")]
        public string PartNameGroup;
        [StructuresField("profileGroup")]
        public string ProfileGroup;
        [StructuresField("materialGroup")]
        public string MaterialGroup;
        [StructuresField("classGroup")]
        public string ClassGroup;


        //Common
        [StructuresField("bay")]
        public string Bay;
        [StructuresField("frameOption")]
        public string FrameOption;
        [StructuresField("topChordLineOption")]
        public string TopChordLineOption;
        [StructuresField("columnLineOption")]
        public string ColumnLineOption;
        [StructuresField("panels")]
        public string Panels;
        #endregion

        public Point StartPoint { get; set; }
        public Point DirectionPoint { get; set; }

        [StructuresField("attributeGroups")]
        public string AttributeGroups;

        [StructuresField("elementPrototypes")]
        public string ElementDataPrototypes;
    }
}
