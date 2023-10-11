using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.DataBoundaries
{
    public interface IAttributeSetter
    {
        bool SetAttributesToElements(List<int> elementIds, AttributeGroup attributeGroup);
    }
}
