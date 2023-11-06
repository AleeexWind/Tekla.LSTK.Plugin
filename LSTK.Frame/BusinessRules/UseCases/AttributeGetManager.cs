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

        //public AttributeGroup GetAttributes(List<int> elementIds)
        //{
        //    AttributeGroup attributeGroup = null;
        //    int attributeId;
        //    List<ElementData> elementDatas = new List<ElementData>();
        //    foreach (int id in elementIds)
        //    {
        //        ElementData elementData = _dataAccess.GetElementData(id);
        //        if (elementData != null)
        //        {
        //            elementDatas.Add(elementData);
        //        }
        //    }
        //    if(elementDatas.Count == 1)
        //    {
        //        attributeId = elementDatas.First().AttributeGroupId;
        //        attributeGroup = _dataAccess.GetAttributeGroup(attributeId);
        //    }
        //    else if(elementDatas.Count == 0)
        //    {
        //        attributeGroup = GenerateSpecificAttributeGroup("error");
        //    }
        //    else
        //    {
        //        attributeGroup = GenerateSpecificAttributeGroup("varies");
        //    }

        //    return attributeGroup;
        //}
        public AttributeGroup GetAttributes(List<int> elementIds)
        {
            AttributeGroup result = GenerateSpecificAttributeGroup("error");

            List<ElementData> elementDatas = new List<ElementData>();       

            foreach (int id in elementIds)
            {
                ElementData elementData = _dataAccess.GetElementData(id);
                if (elementData != null)
                {
                    elementDatas.Add(elementData);
                }
            }

            if (elementDatas.Count == 0)
            {
                return result;
            }
            else
            {
                List<AttributeGroup> attributeGroups = GetAttributeGroupsFromElements(elementDatas);
                if(attributeGroups.Count == 0)
                {
                    return result;
                }

                result.PartName = CheckProperty(attributeGroups, nameof(AttributeGroup.PartName));
                result.Profile = CheckProperty(attributeGroups, nameof(AttributeGroup.Profile));
                result.Material = CheckProperty(attributeGroups, nameof(AttributeGroup.Material));
                result.Class = CheckProperty(attributeGroups, nameof(AttributeGroup.Class));
            }

            return result;
        }
        private List<AttributeGroup> GetAttributeGroupsFromElements(List<ElementData> elementDatas)
        {
            List<AttributeGroup> result = new List<AttributeGroup>();

            List<int> attrIds = elementDatas.Select(x => x.AttributeGroupId).Distinct().ToList();
            foreach (var id in attrIds)
            {
                AttributeGroup attributeGroup = _dataAccess.GetAttributeGroup(id);
                if(attributeGroup != null)
                {
                    result.Add(attributeGroup);
                }
            }
            return result;
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

        private string ApplyResultProperty(bool isEqual, bool isValid, string basicValue)
        {
            if(!isValid)
            {
                return "error";
            }
            else if(isEqual)
            {
                return basicValue;
            }
            else
            {
                return "varies";
            }
        }

        //private void CheckPartNames(List<AttributeGroup> attributeGroups)
        //{
        //    string basicValue = attributeGroups.First().PartName;
        //    if (AreValuesEqual(attributeGroups.Select(a => a.PartName).ToList()))
        //    {
        //        ApplyResultProperty(true, true, basicValue);
        //    }
        //    else
        //    {
        //        ApplyResultProperty(false, true, basicValue);
        //    }
        //}
        private string CheckProperty(List<AttributeGroup> attributeGroups, string propName)
        {
            List<string> values = new List<string>();
            foreach (AttributeGroup attribute in attributeGroups)
            {
                string attrPropValue = attribute.GetType().GetProperty(propName).GetValue(attribute).ToString();
                values.Add(attrPropValue);
            }
            string basicValue = values.First();
            if (AreValuesEqual(values))
            {
                return ApplyResultProperty(true, true, basicValue);
            }
            else
            {
                return ApplyResultProperty(false, true, basicValue);
            }
        }

        private bool AreValuesEqual(List<string> values)
        {
            return values.All(x => x.Equals(values.First()));
        }
    }
}
