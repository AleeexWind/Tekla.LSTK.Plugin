using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.DataBoundaries
{
    public interface IAttributeGetter
    {
        AttributeGroup GetAttributes(List<int> elementIds);
    }
}
