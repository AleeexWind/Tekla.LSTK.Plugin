using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using LSTK.Frame.BusinessRules.UseCases.Utils;
using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.UseCases
{
    public class FrameBuilder : IFrameBuilder
    {
        private readonly ITargetAppAccess _targetAppAccess;
        private readonly List<IDataCalculator> _calculators;
        private List<ElementData> _elementsDatas;
        private FrameBuildInputData _frameBuildInputData;

        public FrameBuilder(ITargetAppAccess targetAppAccess, List<IDataCalculator> calculators)
        {
            _targetAppAccess = targetAppAccess;
            _calculators = calculators;
        }
        public bool BuildFrame(FrameBuildInputData frameBuildInputData)
        {
            bool res;

            _elementsDatas = frameBuildInputData.FrameData.Elements;
            _frameBuildInputData = frameBuildInputData;

            res = _targetAppAccess.RecieveCurrentWorkPlane();
            if (!res) return res;

            res = _targetAppAccess.SetTemporaryLocalPlane(frameBuildInputData.FrameData.StartPoint, frameBuildInputData.FrameData.DirectionPoint);
            if (!res) return res;

            if(!frameBuildInputData.IsHalfOption)
            {
                CalculateRightElements();
            }

            List<ElementData> cl = new List<ElementData>();
            foreach (var elem in frameBuildInputData.FrameData.Elements)
            {
                if(_frameBuildInputData.DoubleProfileOption)
                {
                    ElementData ce = ElementDataCloner.CloneElementData(elem);
                    ce.IsMirrored = true;
                    cl.Add(ce);
                }

                elem.StartPoint.Z = -frameBuildInputData.FrameData.Gap/2;
                elem.EndPoint.Z = -frameBuildInputData.FrameData.Gap / 2;

                res = _targetAppAccess.CreatePart(elem);
                if (!res) return res;
            }
            foreach (var elem in cl)
            {
                elem.StartPoint.Z = frameBuildInputData.FrameData.Gap / 2;
                elem.EndPoint.Z = frameBuildInputData.FrameData.Gap / 2;

                res = _targetAppAccess.CreatePart(elem);
                if (!res) return res;
            }

            res = _targetAppAccess.SetCurrentWorkPlane();
            if (!res) return res;

            res = _targetAppAccess.CommitChanges();
            if (!res) return res;

            return res;
        }

        private void CalculateRightElements()
        {
            foreach (IDataCalculator calc in _calculators)
            {
                calc.Calculate(_elementsDatas, _frameBuildInputData);
            }
        }
    }
}
