using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.TeklaAPI;
using Tekla.Structures.Model;
using Xunit;

namespace UnitTests.UnitTests
{
    public class TeklaPartCreatorTest
    {
        [Fact]
        public void ColumnsCreationTest()
        {
            ////Arrange
            //TeklaPartAttributeSetter teklaPartAttributeSetter = new TeklaPartAttributeSetter();
            //FrameData frameData = new FrameData();
            //FrameInputData frameInputData = new FrameInputData()
            //{
            //    ClassColumns = "5",
            //    MaterialColumns = "350",
            //    ProfileColumns = "ПСУ400х100х20х3,0",
            //    PartNameColumns = "COLUMN11",
            //    HeightColumns = 3000
            //};

            //ColumnsDataCalculator columnsDataCalculator = new ColumnsDataCalculator();
            //columnsDataCalculator.Calculate(frameData, frameInputData);
            

            //ITeklaAccess teklaPartCreator = new TeklaPartCreator(new Model(), teklaPartAttributeSetter);

            ////Act
            //bool leftColumn = teklaPartCreator.CreateLeftColumn(frameData);
            //bool rightColumn = teklaPartCreator.CreateRightColumn(frameData);

            ////Assert
            //Assert.True(leftColumn);
            //Assert.True(rightColumn);
        }
    }
}
