using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using LSTK.Frame.Entities;
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
            ColumnsDataCalculator columnsDataCalculator = new ColumnsDataCalculator();
            FrameInputData frameInputData = new FrameInputData();

            //Act
            columnsDataCalculator.Calculate(frameData, frameInputData);

            //Assert
            Assert.NotNull(frameData.ColumnsData);
        }
    }
}
