using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.Gateways
{
    public interface IDataAccess
    {
        ElementData GetElementData(int elementDataId);
        AttributeGroup GetAttributeGroup(int attributeGroupId);
        //int AddElementData(ElementData elementData);
        bool AddElementDataCollection(List<ElementData> elements);
        bool AddAttributeGroup(AttributeGroup attributeGroup);
        List<ElementData> GetElementDatas();
        List<AttributeGroup> GetAttributeGroups();
        bool UpdateElementData(ElementData elementData);

        bool RestoreAttributeGroup(AttributeGroup attributeGroup);

    }
}
