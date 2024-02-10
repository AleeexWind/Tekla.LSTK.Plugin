using FrameCreator.Entities;
using System.Collections.Generic;

namespace FrameCreator.BusinessRules.Gateways
{
    public interface IDataAccess
    {
        ElementData GetElementData(int elementDataId);
        bool AddElementDataCollection(List<ElementData> elements);
        List<ElementData> GetElementDatas();
    }
}
