using LSTK.Frame.Interactors.Boundaries;
using LSTK.Frame.Interactors.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.Interactors
{
    public class FrameDataHandler : IBoundaryInput
    {
        private InputData _inputData;
        public void TransferInputData(InputData inputData)
        {
            _inputData = inputData;
        }
    }
}
