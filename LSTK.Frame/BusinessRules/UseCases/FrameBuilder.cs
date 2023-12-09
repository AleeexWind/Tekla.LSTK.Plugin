using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.UseCases
{
    //public class FrameBuilder : IFrameBuilder
    //{
    //    private readonly ITargetAppAccess _targetAppAccess;
    //    private readonly List<IDataCalculator> _calculators;
    //    public FrameBuilder(ITargetAppAccess targetAppAccess, List<IDataCalculator> calculators)
    //    {
    //        _targetAppAccess = targetAppAccess;
    //        _calculators = calculators;
    //    }
    //    public bool BuildFrame(FrameInputData inputData)
    //    {
    //        bool res;
    //        FrameData frameData = new FrameData();
    //        foreach (IDataCalculator calc in _calculators)
    //        {
    //            calc.Calculate(frameData, inputData);
    //        }

    //        res = _targetAppAccess.RecieveCurrentWorkPlane();
    //        if (!res) return res;

    //        res = _targetAppAccess.SetTemporaryLocalPlane(inputData.StartPoint, inputData.DirectionPoint);
    //        if (!res) return res;

    //        List<ElementData> elementDatas = ExtractAllElementDatas(frameData);

    //        foreach (var elem in elementDatas)
    //        {
    //            res = _targetAppAccess.CreatePart(elem);
    //            if (!res) return res;
    //        }

    //        res = _targetAppAccess.SetCurrentWorkPlane();
    //        if (!res) return res;

    //        res = _targetAppAccess.CommitChanges();
    //        if (!res) return res;

    //        return res;
    //    }

    //    private List<ElementData> ExtractAllElementDatas(FrameData frameData)
    //    {
    //        List<ElementData> elementDatas = new List<ElementData>();

    //        elementDatas.Add(frameData.ColumnsData.LeftColumn);
    //        elementDatas.Add(frameData.ColumnsData.RightColumn);
    //        elementDatas.Add(frameData.TrussData.LeftTopChord);
    //        elementDatas.Add(frameData.TrussData.RightTopChord);
    //        elementDatas.Add(frameData.TrussData.LeftBottomChord);
    //        elementDatas.Add(frameData.TrussData.RightBottomChord);
    //        elementDatas.AddRange(frameData.TrussData.TrussPosts);

    //        return elementDatas;
    //    }
    //}
    public class FrameBuilder2 : IFrameBuilder
    {
        private readonly ITargetAppAccess _targetAppAccess;
        public FrameBuilder2(ITargetAppAccess targetAppAccess)
        {
            _targetAppAccess = targetAppAccess;
        }
        public bool BuildFrame(FrameData frameData)
        {
            bool res;
            res = _targetAppAccess.RecieveCurrentWorkPlane();
            if (!res) return res;

            res = _targetAppAccess.SetTemporaryLocalPlane(frameData.StartPoint, frameData.DirectionPoint);
            if (!res) return res;

            List<ElementData> cl = new List<ElementData>();
            foreach (var elem in frameData.Elements)
            {
                //SetAttributesTemp(elem);

                ElementData ce = CloneElementData(elem);
                cl.Add(ce);
                elem.StartPoint.Z = - frameData.Gap/2;
                elem.EndPoint.Z = - frameData.Gap / 2;

                res = _targetAppAccess.CreatePart(elem);
                if (!res) return res;
            }
            foreach (var elem in cl)
            {
                elem.StartPoint.Z = frameData.Gap / 2;
                elem.EndPoint.Z = frameData.Gap / 2;

                res = _targetAppAccess.CreatePart(elem);
                if (!res) return res;
            }

            res = _targetAppAccess.SetCurrentWorkPlane();
            if (!res) return res;

            res = _targetAppAccess.CommitChanges();
            if (!res) return res;

            return res;
        }
        private void SetDoubleProfile(List<ElementData> Elements)
        {
            foreach (var element in Elements)
            {
                ElementData data = element as ElementData;

            }
        }
        private ElementData CloneElementData(ElementData elementData)
        {
            ElementData clonedElementData = new ElementData
            {
                Id = elementData.Id,
                ElementGroupType = elementData.ElementGroupType,
                ElementSideType = elementData.ElementSideType,
                StartPoint = elementData.StartPoint,
                EndPoint = elementData.EndPoint,
                Profile = elementData.Profile,
                ProfileHeight = elementData.ProfileHeight,
                PartName = elementData.PartName,
                Material = elementData.Material,
                Class = elementData.Class,
                RotationPosition = elementData.RotationPosition,
                PlanePosition = elementData.PlanePosition,
                DepthPosition = elementData.DepthPosition,
                AttributeGroupId = elementData.AttributeGroupId,
                IsMirrored = true
            };

            return clonedElementData;
        }
    }
}
