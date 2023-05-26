using Xunit;
using LSTK.Frame.Interactors;
using Tekla.Structures.Model;
using UnitTests.Models;

namespace UnitTests
{
    public class TeklaElementInsertHandlerTest
    {
        [Fact]
        public void IsInsertionSucceed()
        {
            //Arrange
            StartColumnDataTest startColumnDataTest = new StartColumnDataTest();

            //CoordinateData coordinateData = new CoordinateData();
            TeklaElementInsertHandler teklaElementArrangeHandler = new TeklaElementInsertHandler();

            //Act
            bool success = teklaElementArrangeHandler.InsertElement(startColumnDataTest.Beam);

            //Assert
            Assert.True(success);
            //model.CommitChanges();
        }
    }
}
