using LSTK.Frame.Entities;

namespace LSTK.Frame.BusinessRules.UseCases.Utils
{
    public static class ElementDataCloner
    {
        public static ElementData CloneElementData(ElementData elementData)
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
                IsMirrored = elementData.IsMirrored,
                IsDeleted = elementData.IsDeleted
            };

            return clonedElementData;
        }
    }
}
