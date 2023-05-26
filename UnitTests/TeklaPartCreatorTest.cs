using UnitTests.Models;
using Tekla.Structures.Model;
using LSTK.Frame.Interactors;
using Xunit;
using LSTK.Frame.Models;

namespace UnitTests
{
    public class TeklaPartCreatorTest
    {
        [Fact]
        public void PartCreationTest()
        {
            //Arrange
            StartColumnDataTest startColumnDataTest = new StartColumnDataTest();

            FrameData frameData = new FrameData()
            {
                StartPoint = startColumnDataTest.Beam.StartPoint,
                ColumnHeight = startColumnDataTest.Beam.EndPoint.Z,
                ColumnProfile = startColumnDataTest.Beam.Profile.ProfileString,
                ColumnMaterial = startColumnDataTest.Beam.Material.MaterialString,
                RotationEnum = startColumnDataTest.Beam.Position.Rotation,
                PlaneEnum = startColumnDataTest.Beam.Position.Plane,
                DepthEnum = startColumnDataTest.Beam.Position.Depth
            };

            TeklaPartCreator teklaPartCreator = new TeklaPartCreator(frameData);


            //Act
            Beam beam = teklaPartCreator.CreateStartColumn();

            //Assert
            Assert.NotNull(beam);
            Assert.Equal(startColumnDataTest.Beam.Profile.ProfileString, beam.Profile.ProfileString);
            Assert.Equal(startColumnDataTest.Beam.Material.MaterialString, beam.Material.MaterialString);
            Assert.Equal(startColumnDataTest.Beam.EndPoint.Z, beam.EndPoint.Z);
            Assert.Equal(startColumnDataTest.Beam.StartPoint.X, beam.StartPoint.X);
            Assert.Equal(startColumnDataTest.Beam.Position.Rotation, beam.Position.Rotation);
            Assert.Equal(startColumnDataTest.Beam.Position.Depth, beam.Position.Depth);
            Assert.Equal(startColumnDataTest.Beam.Position.Plane, beam.Position.Plane);
        }
    }
}
