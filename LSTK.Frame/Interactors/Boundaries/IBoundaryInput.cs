using LSTK.Frame.Interactors.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.Interactors.Boundaries
{
    public interface IBoundaryInput
    {
        void TransferInputData(InputData inputData);
    }
}
