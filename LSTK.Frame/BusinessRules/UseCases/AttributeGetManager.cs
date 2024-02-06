using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
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
                result.PartName = CheckProperty(elementDatas, nameof(ElementData.PartName));
                result.Profile = CheckProperty(elementDatas, nameof(ElementData.Profile));
                result.Material = CheckProperty(elementDatas, nameof(ElementData.Material));
                result.Class = CheckProperty(elementDatas, nameof(ElementData.Class));
            }

            return result;
        }
        private AttributeGroup GenerateSpecificAttributeGroup(string option)
        {
            return new AttributeGroup()
            {
                PartName = option,
                Profile = option,
                Material = option,
                Class = option
            };
        }

        private string ApplyResultProperty(bool isEqual, bool isValid, string basicValue)
        {
            if (!isValid)
            {
                return "error";
            }
            else if (isEqual)
            {
                return basicValue;
            }
            else
            {
                return "varies";
            }
        }

        private string CheckProperty(List<ElementData> elements, string propName)
        {
            List<string> values = new List<string>();
            foreach (ElementData element in elements)
            {
                string elemPropValue = element.GetType().GetProperty(propName).GetValue(element).ToString();
                values.Add(elemPropValue);
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
