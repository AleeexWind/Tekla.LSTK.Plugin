using LSTK.Frame.Controllers;
using LSTK.Frame.Models;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace LSTK.Frame.Interactors
{
    public class FrameCreatorManager
    {
        private FrameData _frameData;
        private FrameDataController _frameDataController;
        private TeklaPointSelector _teklaPointSelector;
        private LocalPlaneManager _localPlaneManager;
        private TransformationPlane _currentTransformationPlane;
        private Model _model;

        public FrameCreatorManager(FrameData frameData, MainWindowViewModel mainWindowViewModel, TeklaPointSelector teklaPointSelector, LocalPlaneManager localPlaneManager)
        {
            _model = new Model();
            _frameData = frameData;
            _frameDataController = new FrameDataController(mainWindowViewModel, frameData);
            _teklaPointSelector = teklaPointSelector;
            _localPlaneManager = localPlaneManager;
        }
        public void BuildFrameData()
        {
            _frameDataController.GetColumnProfileInput();
            _frameDataController.GetColumnNameInput();
            _frameDataController.GetLeftColumnCoordinatesInput();
            _frameDataController.GetRightColumnCoordinatesInput();
        }
        public void SetLocalWorkingPlane()
        {
            _currentTransformationPlane = _localPlaneManager.RecieveCurrentWorkPlane();

            _localPlaneManager.SetTemporaryWorkPlane();

            (Point, Point) selectedPoints = _teklaPointSelector.SelectPoints();
            _frameData.StartPoint = selectedPoints.Item1;
            _frameData.DirectionPoint = selectedPoints.Item2;


            (Vector, Vector) vectors = _localPlaneManager.SetVectors(selectedPoints.Item1, selectedPoints.Item2);
            CoordinateSystem localCoordinateSystem = _localPlaneManager.SetLocalCoordinateSystem(selectedPoints.Item1, vectors);
            _localPlaneManager.SetLocalWorkPlane(localCoordinateSystem);
        }

        public (Beam LeftColumn, Beam RightColumn) CreateColumns(TeklaPartCreator teklaPartCreator)
        {
            Beam leftColumn = teklaPartCreator.CreateLeftColumn();
            Beam rightColumn = teklaPartCreator.CreateRightColumn();
            leftColumn.Insert();
            rightColumn.Insert();
            return (leftColumn, rightColumn);
        }
        public bool Commit()
        {
            return _model.CommitChanges();
        }
    }
}
