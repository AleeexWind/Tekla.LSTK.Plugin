using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.TeklaAPI;
using Xunit;

namespace UnitTests.UnitTests
{
    public class TeklaPartCreatorTest
    {
        [Fact]
        public void ColumnsCreationTest()
        {
            //Arrange
            TeklaPartAttributeSetter teklaPartAttributeSetter = new TeklaPartAttributeSetter();
            FrameData frameData = new FrameData();
            FrameInputData frameInputData = new FrameInputData();
            ColumnsDataCalculator columnsDataCalculator = new ColumnsDataCalculator(frameData);
            columnsDataCalculator.Calculate(frameInputData);
            

            ITeklaAccess teklaPartCreator = new TeklaPartCreator(frameData, teklaPartAttributeSetter);

            //Act
            bool leftColumn = teklaPartCreator.CreateLeftColumn();
            bool rightColumn = teklaPartCreator.CreateRightColumn();

            //Assert
            Assert.True(leftColumn);
            Assert.True(rightColumn);
        }
    }
}
