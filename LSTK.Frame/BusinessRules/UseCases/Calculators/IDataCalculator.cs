using LSTK.Frame.BusinessRules.DataBoundaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public interface IDataCalculator
    {
        void Calculate(FrameInputData frameInputData);
    }
}
