using FrameCreator.BusinessRules.DataBoundaries;
using FrameCreator.Entities;
using System.Collections.Generic;

namespace FrameCreator.BusinessRules.UseCases.Calculators
{
    public interface IDataCalculator
    {
        bool Calculate(List<ElementData> elementsDatas, InputData inputData);
    }
}
