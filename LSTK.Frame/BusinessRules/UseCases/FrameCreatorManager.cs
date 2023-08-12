using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.Entities;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using System.Collections.Generic;
using LSTK.Frame.Frameworks.TeklaAPI;

namespace LSTK.Frame.BusinessRules.UseCases
{
    public class FrameCreatorManager : IFrameDataInputBoundary
    {
        private FrameInputData _frameInputData;

        private readonly ITeklaAccess _teklaAccess;
        private readonly List<IDataCalculator> _calculators;
        private readonly LocalPlaneManager _localPlaneManager;


        public FrameCreatorManager(ITeklaAccess teklaAccess, List<IDataCalculator> calculators, LocalPlaneManager localPlaneManager)
        {
            _teklaAccess = teklaAccess;
            _calculators = calculators;
            _localPlaneManager = localPlaneManager;
        }

        public bool CreateFrame()
        {
            bool res;

            foreach (var calc in _calculators)
            {
                calc.Calculate(_frameInputData);
            }

            _localPlaneManager.RecieveCurrentWorkPlane();
            _localPlaneManager.SetTemporaryLocalPlane(_frameInputData.StartPoint, _frameInputData.DirectionPoint);
            res = _teklaAccess.CreateLeftColumn();
            if (!res) return res;
            res = _teklaAccess.CreateRightColumn();
            if (!res) return res;

            res =_localPlaneManager.SetCurrentWorkPlane();
            if (!res) return res;
            res = _teklaAccess.CommitChanges();
            if (!res) return res;

            return res;
        }

        public void TransferInputData(FrameInputData inputData)
        {
            _frameInputData = inputData;
        }


        //public void BuildFrameData()
        //{
        //    _frameDataController.GetColumnProfileInput();
        //    _frameDataController.GetColumnNameInput();
        //    _frameDataController.GetLeftColumnCoordinatesInput();
        //    _frameDataController.GetRightColumnCoordinatesInput();
        //}
        //public void SetLocalWorkingPlane()
        //{
        //    _currentTransformationPlane = _localPlaneManager.RecieveCurrentWorkPlane();

        //    //_localPlaneManager.SetTemporaryWorkPlane();


        //    _frameDataController.GetStartPointInput();
        //    _frameDataController.GetDirectionPointInput();

        //    (Vector, Vector) vectors = _localPlaneManager.SetVectors(_frameData.StartPoint, _frameData.DirectionPoint);
        //    CoordinateSystem localCoordinateSystem = _localPlaneManager.SetLocalCoordinateSystem(_frameData.StartPoint, vectors);
        //    _localPlaneManager.SetLocalWorkPlane(localCoordinateSystem);
        //}

        //public (Beam LeftColumn, Beam RightColumn) CreateColumns(TeklaPartCreator teklaPartCreator)
        //{
        //    Beam leftColumn = teklaPartCreator.CreateLeftColumn();
        //    Beam rightColumn = teklaPartCreator.CreateRightColumn();
        //    leftColumn.Insert();
        //    rightColumn.Insert();
        //    return (leftColumn, rightColumn);
        //}
        //public bool SetCurrentPlane()
        //{
        //    return _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(_currentTransformationPlane);
        //}
        //public bool Commit()
        //{
        //    return _model.CommitChanges();
        //}

        //public void TransferInputData(InputData inputData)
        //{
        //    _inputData = inputData;
        //}


    }
}
