using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.BusinessRules.UseCases
{
    public class AttributeGetManager : IAttributeGetter
    {
        private readonly IDataAccess _dataAccess;

        public AttributeGetManager(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public AttributeGroup GetAttributes(List<int> elementIds)
        {
            AttributeGroup attributeGroup = null;
            int attributeId;
            List<ElementData> elementDatas = new List<ElementData>();
            foreach (int id in elementIds)
            {
                ElementData elementData = _dataAccess.GetElementData(id);
                if (elementData != null)
                {
                    elementDatas.Add(elementData);
                }
            }
            if(elementDatas.Count == 1)
            {
                attributeId = elementDatas.First().AttributeGroupId;
                attributeGroup = _dataAccess.GetAttributeGroup(attributeId);
            }
            else if(elementDatas.Count == 0)
            {
                attributeGroup = GenerateSpecificAttributeGroup("error");
            }
            else
            {
                attributeGroup = GenerateSpecificAttributeGroup("varies");
            }

            return attributeGroup;
        }
        private AttributeGroup GenerateSpecificAttributeGroup(string option)
        {
            return new AttributeGroup()
            {
                Id = -1,
                PartName = option,
                Profile = option,
                Material = option,
                Class = option
            };
        }
    }
}
