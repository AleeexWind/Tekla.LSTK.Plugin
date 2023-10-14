using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.DataBase;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.Adapters.Gateways
{
    public class DataHandler : IDataAccess
    {
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
            return DataBase.SchemaElements.FirstOrDefault(x => x.Id.Equals(elementDataId));
        }

        public List<ElementData> GetElementDatas()
        {
            return DataBase.SchemaElements;
        }
    }
}
