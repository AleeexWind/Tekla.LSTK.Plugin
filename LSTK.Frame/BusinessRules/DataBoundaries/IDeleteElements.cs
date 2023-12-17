using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.DataBoundaries
{
    public interface IDeleteElements
    {
        bool DeleteElements(List<int> elementsIds);
    }
}
