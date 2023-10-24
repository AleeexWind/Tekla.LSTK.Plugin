using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.UseCases
{
    public class AttributeSetManager : IAttributeSetter
    {
        private readonly IDataAccess _dataAccess;
        List<ElementData> elementDatas1;
        public AttributeSetManager(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public bool SetAttributesToElements(List<int> elementIds, AttributeGroup attributeGroup)
        {
            try
            {
                elementDatas1 = _dataAccess.GetElementDatas();
                List<ElementData> elementDatas = new List<ElementData>();
                foreach (int id in elementIds)
                {
                    ElementData elementData = _dataAccess.GetElementData(id);

                    if(elementData == null || !SetAttributes(elementData, attributeGroup) || !_dataAccess.UpdateElementData(elementData))
                    {
                        return false;
                    }
                }
                elementDatas1 = _dataAccess.GetElementDatas();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //public bool SetAttributesToElements(List<int> elementIds, AttributeGroup attributeGroup)
        //{
        //    try
        //    {
        //        List<ElementData> elementDatas = new List<ElementData>();
        //        foreach (int id in elementIds)
        //        {
        //            ElementData elementData = _dataAccess.GetElementData(id);
        //            if (elementData == null || !SetAttributes(elementData, attributeGroup))
        //            {
        //                return false;
        //            }
        //        }

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        private bool SetAttributes(ElementData elementData, AttributeGroup attributeGroup)
        {
            try
            {
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
        private ElementData CreateElement(AttributeGroup attributeGroup)
        {
            ElementData result = null;
            try
            {
                result = new ElementData()
                {
                    PartName = attributeGroup.PartName,
                    Profile = attributeGroup.Profile,
                    Material = attributeGroup.Material,
                    Class = attributeGroup.Class,
                    RotationPosition = attributeGroup.RotationPosition,
                    PlanePosition = attributeGroup.PlanePosition,
                    DepthPosition = attributeGroup.DepthPosition
                };

            }
            catch
            {
                //TODO: Logging
            }
            return result;
        }
    }
}
