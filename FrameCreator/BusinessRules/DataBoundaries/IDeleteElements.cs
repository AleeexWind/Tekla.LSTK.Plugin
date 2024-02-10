using System.Collections.Generic;

namespace FrameCreator.BusinessRules.DataBoundaries
{
    public interface IDeleteElements
    {
        bool DeleteElements(List<int> elementsIds);
    }
}
