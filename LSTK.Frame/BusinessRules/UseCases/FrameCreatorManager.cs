using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using System.Collections.Generic;
using LSTK.Frame.Frameworks.TeklaAPI;
using LSTK.Frame.Entities;

namespace LSTK.Frame.BusinessRules.UseCases
{
    public class FrameCreatorManager : IFrameDataInputBoundary
    {
        private FrameInputData _frameInputData;

        private readonly ITeklaAccess _teklaAccess;
        private readonly FrameData _frameData;
        private readonly List<IDataCalculator> _calculators;
        private readonly LocalPlaneManager _localPlaneManager;
        private readonly IFirstSchemaOutputBoundary _firstSchemaBoundary;


        public FrameCreatorManager(FrameData frameData, ITeklaAccess teklaAccess, List<IDataCalculator> calculators, LocalPlaneManager localPlaneManager)
        {
            _frameData = frameData;
            _teklaAccess = teklaAccess;
            _calculators = calculators;
            _localPlaneManager = localPlaneManager;
        }

        public bool CreateFrame()
        {
            bool res;

            foreach (IDataCalculator calc in _calculators)
            {
                calc.Calculate(_frameData, _frameInputData);
            }

            _localPlaneManager.RecieveCurrentWorkPlane();
            _localPlaneManager.SetTemporaryLocalPlane(_frameInputData.StartPoint, _frameInputData.DirectionPoint);


            res = _teklaAccess.CreateLeftColumn(_frameData);
            if (!res) return res;
            res = _teklaAccess.CreateRightColumn(_frameData);
            if (!res) return res;

            res = _teklaAccess.CreateLeftTopChord(_frameData);
            if (!res) return res;
            res = _teklaAccess.CreateRightTopChord(_frameData);
            if (!res) return res;

            res = _teklaAccess.CreateLeftBottomChord(_frameData);
            if (!res) return res;
            res = _teklaAccess.CreateRightBottomChord(_frameData);
            if (!res) return res;
            res = _teklaAccess.CreateTrussPosts(_frameData);
            if (!res) return res;



            res =_localPlaneManager.SetCurrentWorkPlane();
            if (!res) return res;
            res = _teklaAccess.CommitChanges();
            if (!res) return res;

            return res;
        }
        public void CreateSchema()
        {
            foreach (IDataCalculator calc in _calculators)
            {
                calc.Calculate(_frameData, _frameInputData);
            }
            //_firstSchemaBoundary.BuildSchema(GetAllElementsOfTruss(), _frameInputData.Bay/2, _frameInputData.HeightRoofBottom + _frameInputData.HeightRoofRidge);
        }

        public void TransferInputData(FrameInputData inputData)
        {
            _frameInputData = inputData;
        }
        private List<ElementData> GetAllElementsOfTruss()
        {
            List<ElementData> result = new List<ElementData>
            {
                _frameData.TrussData.LeftTopChord,
                _frameData.TrussData.RightTopChord,
                _frameData.TrussData.LeftBottomChord,
                _frameData.TrussData.RightBottomChord
            };
            result.AddRange(_frameData.TrussData.TrussPosts);

            return result;
        }
    }
}
