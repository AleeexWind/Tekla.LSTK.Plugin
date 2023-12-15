using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.DataBoundaries
{
    public interface IRotateElements
    {
        bool RotateElements(List<int> elementIds);
    }
}
