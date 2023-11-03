using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.UseCases
{
    public class AttributeSetManager : IAttributeSetter
    {
        private readonly IDataAccess _dataAccess;
        public AttributeSetManager(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public bool SetAttributesToElements(List<int> elementIds, AttributeGroup attributeGroup)
        {
            try
            {
                List<ElementData> elementDatas = new List<ElementData>();
                foreach (int id in elementIds)
                {
                    ElementData elementData = _dataAccess.GetElementData(id);

                    if(elementData == null ||!_dataAccess.AddAttributeGroup(attributeGroup) || !SetAttributes(elementData, attributeGroup) || 
                        !_dataAccess.UpdateElementData(elementData))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool SetAttributes(ElementData elementData, AttributeGroup attributeGroup)
        {
            try
            {
                elementData.AttributeGroupId = attributeGroup.Id;
                elementData.PartName = attributeGroup.PartName;
                elementData.Profile = attributeGroup.Profile;
                elementData.Material = attributeGroup.Material;
                elementData.Class = attributeGroup.Class;
                elementData.RotationPosition = attributeGroup.RotationPosition;
                elementData.PlanePosition = attributeGroup.PlanePosition;
                elementData.DepthPosition = attributeGroup.DepthPosition;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
