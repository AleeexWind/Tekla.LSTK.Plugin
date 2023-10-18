using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public interface IDataCalculator
    {
        bool Calculate(List<ElementData> elementsDatas, InputData inputData);
    }
}
