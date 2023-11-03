using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.Gateways
{
    public interface IDataAccess
    {
        ElementData GetElementData(int elementDataId);
        AttributeGroup GetAttributeGroup(int attributeGroupId);
        bool AddElementData(ElementData elementData);
        bool AddAttributeGroup(AttributeGroup attributeGroup);
        List<ElementData> GetElementDatas();
        bool UpdateElementData(ElementData elementData);
    }
}
