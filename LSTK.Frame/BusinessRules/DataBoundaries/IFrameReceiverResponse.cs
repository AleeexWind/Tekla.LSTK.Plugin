using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.DataBoundaries
{
    public interface IFrameReceiverResponse
    {
        void ShowResult(List<ElementData> elementDatas);
    }
}
