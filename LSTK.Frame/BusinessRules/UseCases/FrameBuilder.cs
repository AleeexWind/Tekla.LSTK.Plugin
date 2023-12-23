using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.UseCases
{
    public class FrameBuilder : IFrameBuilder
    {
        private readonly ITargetAppAccess _targetAppAccess;
        public FrameBuilder(ITargetAppAccess targetAppAccess)
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
