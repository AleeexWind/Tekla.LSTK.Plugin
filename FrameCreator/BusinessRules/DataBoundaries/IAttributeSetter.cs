using FrameCreator.Entities;
using System.Collections.Generic;

namespace FrameCreator.BusinessRules.DataBoundaries
{
    public interface IAttributeSetter
    {
        bool SetAttributesToElements(List<int> elementIds, AttributeGroup attributeGroup);
    }
}
