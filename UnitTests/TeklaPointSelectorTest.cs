using System;
using System.Linq;
using Xunit;
using LSTK.Frame.Models;
using LSTK.Frame.Interactors;

namespace UnitTests
{
    public class TeklaPointSelectorTest
    {
        [Fact]
        public void AreReceivedPointsNotNull()
        {
            //Arrange
            FrameData frameData = new FrameData();
            TeklaPointSelector teklaPointSelector = new TeklaPointSelector();

            //Act
            teklaPointSelector.SelectPoints(frameData);

            //Assert
            Assert.NotNull(frameData.StartPoint);
            Assert.NotNull(frameData.EndPoint);
        }
    }
}
