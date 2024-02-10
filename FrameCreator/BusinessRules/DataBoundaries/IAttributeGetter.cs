using FrameCreator.Entities;
using System.Collections.Generic;

namespace FrameCreator.BusinessRules.DataBoundaries
{
    public interface IAttributeGetter
    {
        AttributeGroup GetAttributes(List<int> elementIds);
    }
}
