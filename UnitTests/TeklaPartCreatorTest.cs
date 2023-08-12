using UnitTests.Models;
using Tekla.Structures.Model;
using Xunit;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.TeklaAPI;
using LSTK.Frame.BusinessRules.Gateways;

namespace UnitTests
{
    public class TeklaPartCreatorTest
    {
        [Fact]
        public void PartCreationTest()
        {
            //Arrange
            TeklaPartAttributeSetter teklaPartAttributeSetter = new TeklaPartAttributeSetter();
            FrameData frameData = new FrameData();

            ITeklaAccess teklaPartCreator = new TeklaPartCreator(new Model(), frameData, teklaPartAttributeSetter);

            //Act
            bool leftColumn = teklaPartCreator.CreateLeftColumn();
            bool rightColumn = teklaPartCreator.CreateRightColumn();

            //Assert
            Assert.True(leftColumn);
            Assert.True(rightColumn);

            //Arrange
            //FrameDataTest frameDataTest = new FrameDataTest();
            //StartColumnDataTest startColumnDataTest = new StartColumnDataTest(frameDataTest);
            //TeklaPartAttributeSetter teklaPartAttributeSetter = new TeklaPartAttributeSetter();
            //FrameData frameData = new FrameData()
            //{
            //    StartPointLeftColumn = startColumnDataTest.Beam.StartPoint,
            //    EndPointLeftColumn = startColumnDataTest.Beam.EndPoint,
            //    ProfileColumns = startColumnDataTest.Beam.Profile.ProfileString,
            //    MaterialColumns = startColumnDataTest.Beam.Material.MaterialString,
            //    RotationEnum = startColumnDataTest.Beam.Position.Rotation,
            //    PlaneEnum = startColumnDataTest.Beam.Position.Plane,
            //    DepthEnum = startColumnDataTest.Beam.Position.Depth
            //};

            //TeklaPartCreator teklaPartCreator = new TeklaPartCreator(frameData, teklaPartAttributeSetter);


            ////Act
            //Beam beam = teklaPartCreator.CreateLeftColumn();

            ////Assert
            //Assert.NotNull(beam);
            //Assert.Equal(startColumnDataTest.Beam.Profile.ProfileString, beam.Profile.ProfileString);
            //Assert.Equal(startColumnDataTest.Beam.Material.MaterialString, beam.Material.MaterialString);
            //Assert.Equal(startColumnDataTest.Beam.EndPoint.Z, beam.EndPoint.Z);
            //Assert.Equal(startColumnDataTest.Beam.StartPoint.X, beam.StartPoint.X);
            //Assert.Equal(startColumnDataTest.Beam.Position.Rotation, beam.Position.Rotation);
            //Assert.Equal(startColumnDataTest.Beam.Position.Depth, beam.Position.Depth);
            //Assert.Equal(startColumnDataTest.Beam.Position.Plane, beam.Position.Plane);
        }
    }
}
