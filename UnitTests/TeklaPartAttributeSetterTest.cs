using Xunit;
using LSTK.Frame.Interactors;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using LSTK.Frame.Models;
using UnitTests.Models;

namespace UnitTests
{
    public class TeklaPartAttributeSetterTest
    {
        [Fact]
        public void StartColumnsCoordinatesToBe0_0_0With2000Height()
        {
            //Arrange
            FrameDataTest frameDataTest = new FrameDataTest();
            Beam beam = new Beam();

            TeklaPartAttributeSetter teklaPartCreator = new TeklaPartAttributeSetter();

            //Act
            teklaPartCreator.SetCoordinatesToStartColumn(beam, frameDataTest.StartPointLeftColumn, frameDataTest.EndPointLeftColumn);

            //Assert
            Assert.Equal(frameDataTest.StartPointLeftColumn, beam.StartPoint);
            Assert.Equal(frameDataTest.EndPointLeftColumn, beam.EndPoint);
        }

        [Fact]
        public void StartColumnProfileShouldBePSU400x100x3()
        {
            //Arrange
            Beam beam = new Beam();
            string profile = "ПСУ400х100х20х3,0";

            TeklaPartAttributeSetter teklaPartCreator = new TeklaPartAttributeSetter();

            //Act
            teklaPartCreator.SetProfileToStartColumn(beam, profile);

            //Assert
            Assert.Equal(profile, beam.Profile.ProfileString);
        }

        [Fact]
        public void StartColumnPositionDepthToBeMIDDLE()
        {
            //Arrange
            Beam beam = new Beam();
            Position.DepthEnum depthEnum = Position.DepthEnum.MIDDLE;

            TeklaPartAttributeSetter teklaPartCreator = new TeklaPartAttributeSetter();

            //Act
            teklaPartCreator.SetPositionDepthToStartColumn(beam, depthEnum);

            //Assert
            Assert.Equal(depthEnum, beam.Position.Depth);
        }

        [Fact]
        public void StartColumnPositionPlaneToBeMIDDLE()
        {
            //Arrange
            Beam beam = new Beam();
            Position.PlaneEnum planeEnum = Position.PlaneEnum.MIDDLE;

            TeklaPartAttributeSetter teklaPartCreator = new TeklaPartAttributeSetter();

            //Act
            teklaPartCreator.SetPositionPlaneToStartColumn(beam, planeEnum);

            //Assert
            Assert.Equal(planeEnum, beam.Position.Plane);
        }

        [Fact]
        public void StartColumnPositionRotationToBeFRONT()
        {
            //Arrange
            Beam beam = new Beam();
            Position.RotationEnum rotationEnum = Position.RotationEnum.FRONT;

            TeklaPartAttributeSetter teklaPartCreator = new TeklaPartAttributeSetter();

            //Act
            teklaPartCreator.SetPositionRotationToStartColumn(beam, rotationEnum);

            //Assert
            Assert.Equal(rotationEnum, beam.Position.Rotation);
        }

        [Fact]
        public void StartColumnMaterialToBe350()
        {
            //Arrange
            Beam beam = new Beam();
            string material = "350";

            TeklaPartAttributeSetter teklaPartCreator = new TeklaPartAttributeSetter();

            //Act
            teklaPartCreator.SetMaterialToStartColumn(beam, material);

            //Assert
            Assert.Equal(material, beam.Material.MaterialString);
        }
    }
}
