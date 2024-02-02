using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.Gateways
{
    public interface IDataAccess
    {
        ElementData GetElementData(int elementDataId);
        bool AddElementDataCollection(List<ElementData> elements);
        List<ElementData> GetElementDatas();

    }
}
