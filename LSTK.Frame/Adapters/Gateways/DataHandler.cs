using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.DataBase;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.Adapters.Gateways
{
    public class DataHandler : IDataAccess
    {
        private readonly DataBase _dataBase;

        public DataHandler(DataBase dataBase)
        {
            _dataBase = dataBase;
        }

        public bool AddElementData(ElementData elementData)
        {
            try
            {
                _dataBase.SchemaElements.Add(elementData);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public AttributeGroup GetAttributeGroup(int attributeGroupId)
        {
            return _dataBase.AttributeGroups.FirstOrDefault(x => x.Id.Equals(attributeGroupId));
        }

        public ElementData GetElementData(int elementDataId)
        {
            return _dataBase.SchemaElements.FirstOrDefault(x => x.Id.Equals(elementDataId));
        }

        public List<ElementData> GetElementDatas()
        {
            return _dataBase.SchemaElements;
        }
    }
}
