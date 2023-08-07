using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.UnitTests
{
    public class ColumnsDataCalculatorTest
    {
        [Fact]
        public void Calculate()
        {
            //Arrange
            FrameData frameData = new FrameData();
            ColumnsDataCalculator columnsDataCalculator = new ColumnsDataCalculator(frameData);
            FrameInputData frameInputData = new FrameInputData();

            //Act
            columnsDataCalculator.Calculate(frameInputData);

            //Assert

            FrameData frameDataOutput = typeof(ColumnsDataCalculator).GetField("_frameData", BindingFlags.NonPublic |
                         BindingFlags.Instance).GetValue(columnsDataCalculator) as FrameData;
            Assert.NotNull(frameDataOutput.ColumnsData);
        }
    }
}
