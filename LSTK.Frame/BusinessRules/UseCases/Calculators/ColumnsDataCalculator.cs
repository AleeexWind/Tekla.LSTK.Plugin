using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public class ColumnsDataCalculator
    {
        private FrameData _frameData;
        public ColumnsDataCalculator(FrameData frameData)
        {
            _frameData = frameData;
        }
        public void Calculate(FrameInputData frameInputData)
        {
            _frameData.ColumnsData = new ColumnsData();
        }
    }
}
