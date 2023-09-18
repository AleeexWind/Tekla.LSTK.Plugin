using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.DataBoundaries
{
    public interface IFirstSchemaOutputBoundary
    {
        void TransferSchema(List<ElementData> elementDatas, double coordXmax, double coordYmax, double yOffset);
    }
}
