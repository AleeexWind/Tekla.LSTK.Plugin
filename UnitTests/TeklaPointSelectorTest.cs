using Xunit;
using LSTK.Frame.Models;
using LSTK.Frame.Interactors;
using Tekla.Structures.Geometry3d;

namespace UnitTests
{
    public class TeklaPointSelectorTest
    {
        [Fact]
        public void AreReceivedPointsNotNull()
        {
            //Arrange
            TeklaPointSelector teklaPointSelector = new TeklaPointSelector();

            //Act
            (Point, Point) coordinates = teklaPointSelector.SelectPoints();

            //Assert
            Assert.NotNull(coordinates.Item1);
            Assert.NotNull(coordinates.Item2);
        }
    }
}
