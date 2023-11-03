using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.DataBase;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.Adapters.Gateways
{
    public class DataHandler : IDataAccess
    {
        private int _currentAttributeId;

        public bool AddAttributeGroup(AttributeGroup attributeGroup)
        {
            try
            {
                attributeGroup.Id = _currentAttributeId;
                DataBase.AttributeGroups.Add(attributeGroup);
                _currentAttributeId++;
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool AddElementData(ElementData elementData)
        {
            try
            {
                DataBase.SchemaElements.Add(elementData);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public AttributeGroup GetAttributeGroup(int attributeGroupId)
        {
            return DataBase.AttributeGroups.FirstOrDefault(x => x.Id.Equals(attributeGroupId));
        }

        public ElementData GetElementData(int elementDataId)
        {
            ElementData result = null;
            ElementData foundElement = DataBase.SchemaElements.FirstOrDefault(x => x.Id.Equals(elementDataId));
            if(foundElement != null)
            {
                result = CloneElementData(foundElement);
            }
            return result;
        }

        public List<ElementData> GetElementDatas()
        {
            return DataBase.SchemaElements;
        }

        public bool UpdateElementData(ElementData elementData)
        {
            ElementData foundElement = DataBase.SchemaElements.FirstOrDefault(x => x.Id.Equals(elementData.Id));
            if(foundElement != null)
            {
                foundElement.Id = elementData.Id;
                foundElement.ElementGroupType = elementData.ElementGroupType;
                foundElement.ElementSideType = elementData.ElementSideType;
                foundElement.StartPoint = elementData.StartPoint;
                foundElement.EndPoint = elementData.EndPoint;
                foundElement.Profile = elementData.Profile;
                foundElement.ProfileHeight = elementData.ProfileHeight;
                foundElement.PartName = elementData.PartName;
                foundElement.Material = elementData.Material;
                foundElement.Class = elementData.Class;
                foundElement.RotationPosition = elementData.RotationPosition;
                foundElement.PlanePosition = elementData.PlanePosition;
                foundElement.DepthPosition = elementData.DepthPosition;
                foundElement.AttributeGroupId = elementData.AttributeGroupId;
                return true;
            }
            else
            {
                return false;
            }
        }
        private ElementData CloneElementData(ElementData elementData)
        {
            ElementData clonedElement = new ElementData()
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
                AttributeGroupId = elementData.AttributeGroupId
            };
            return clonedElement;
        }
    }
}
