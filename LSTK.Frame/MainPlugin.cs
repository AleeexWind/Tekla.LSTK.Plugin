using LSTK.Frame.Interactors;
using LSTK.Frame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Plugins;

namespace LSTK.Frame
{
    [Plugin("LSTK_Frame")]
    [PluginUserInterface("LSTK.Frame.MainWindow")]
    public class MainPlugin : PluginBase
    {
        private Model _model;
        private PluginData _data;

        private string _partNameColumns = string.Empty;
        private string _profileColumns = string.Empty;
        private string _heightColumns = string.Empty;
        private string _profileTopChord = string.Empty;
        private string _bayOverall = string.Empty;

        private Model Model
        {
            get { return this._model; }
            set { this._model = value; }
        }

        private PluginData Data
        {
            get { return this._data; }
            set { this._data = value; }
        }

        public MainPlugin(PluginData data)
        {
            Model = new Model();
            Data = data;
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

                Point Point1 = (Point)(Input[0]).GetInput();
                Point Point2 = (Point)(Input[1]).GetInput();
                Data.StartPoint = Point1;
                Data.DirectionPoint = Point2;


                FrameData frameData = new FrameData();
                LocalPlaneManager localPlaneManager = new LocalPlaneManager(Model);
                TeklaPartAttributeSetter teklaPartAttributeSetter = new TeklaPartAttributeSetter();
                TeklaPartCreator teklaPartCreator = new TeklaPartCreator(frameData, teklaPartAttributeSetter);
                FrameCreatorManager frameCreatorManager = new FrameCreatorManager(frameData, Data, localPlaneManager);

                frameCreatorManager.SetLocalWorkingPlane();
                frameCreatorManager.BuildFrameData();
                frameCreatorManager.CreateColumns(teklaPartCreator);
                frameCreatorManager.SetCurrentPlane();
                frameCreatorManager.Commit();


            }
            catch (Exception Ex)
            {              
                throw;
            }
            return true;
        }

        private void GetValuesFromDialog()
        {
            _partNameColumns = Data.PartNameColumns;
            _profileColumns = Data.ProfileColumns;
            _heightColumns = Data.HeightColumns;
            _profileTopChord = Data.ProfileTopChord;
            _bayOverall = Data.BayOverall;

            if (IsDefaultValue(_partNameColumns))
                _partNameColumns = "TEST";
            if (IsDefaultValue(_profileColumns))
                _profileColumns = "ПСУ400х100х20х3,0";
            if (IsDefaultValue(_heightColumns))
                _heightColumns = "5000";
            if (IsDefaultValue(_profileTopChord))
                _profileTopChord = "ПСУ300х100х20х2,0";
            if (IsDefaultValue(_bayOverall))
                _bayOverall = "20000";
        }
    }
    public class PluginData
    {
        #region Fields
        //
        // Define the fields specified on the Form.
        //
        [StructuresField("partNameColumns")]
        public string PartNameColumns;
         
        [StructuresField("profileColumns")]
        public string ProfileColumns;
        [StructuresField("profileTopChord")]
        public string ProfileTopChord;

        [StructuresField("heightColumns")]
        public string HeightColumns;

        [StructuresField("bayOverall")]
        public string BayOverall;

        #endregion

        public Point StartPoint { get; set; }
        public Point DirectionPoint { get; set; }
    }
}
