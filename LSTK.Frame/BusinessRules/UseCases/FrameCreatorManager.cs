using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Controllers;
using LSTK.Frame.Interactors;
using LSTK.Frame.Interactors.Boundaries;
using LSTK.Frame.Interactors.DataStructures;
using LSTK.Frame.Models;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace LSTK.Frame.BusinessRules.UseCases
{
    public class FrameCreatorManager : IFrameDataInputBoundary
    {
        private FrameData _frameData;
        private InterfaceDataController _frameDataController;
        //private TeklaPointSelector _teklaPointSelector;
        private LocalPlaneManager _localPlaneManager;
        private TransformationPlane _currentTransformationPlane;
        private PluginData _pluginData;
        private Model _model;

        private FrameInputData _frameInputData;

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

            //_localPlaneManager.SetTemporaryWorkPlane();


            _frameDataController.GetStartPointInput();
            _frameDataController.GetDirectionPointInput();

            (Vector, Vector) vectors = _localPlaneManager.SetVectors(_frameData.StartPoint, _frameData.DirectionPoint);
            CoordinateSystem localCoordinateSystem = _localPlaneManager.SetLocalCoordinateSystem(_frameData.StartPoint, vectors);
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
        public bool SetCurrentPlane()
        {
            return _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(_currentTransformationPlane);
        }
        public bool Commit()
        {
            return _model.CommitChanges();
        }

        //public void TransferInputData(InputData inputData)
        //{
        //    _inputData = inputData;
        //}

        public void TransferInputData(FrameInputData inputData)
        {
            _frameInputData = inputData;
        }
    }
}
