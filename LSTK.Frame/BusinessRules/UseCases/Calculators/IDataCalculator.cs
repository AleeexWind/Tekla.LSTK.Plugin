using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public interface IDataCalculator
    {
        void Calculate(FrameData frameData, FrameInputData frameInputData);
    }
}
