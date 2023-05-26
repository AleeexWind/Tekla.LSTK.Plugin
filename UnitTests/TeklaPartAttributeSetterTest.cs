using Xunit;
using LSTK.Frame.Interactors;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;

namespace UnitTests
{
    public class TeklaPartAttributeSetterTest
    {
        [Fact]
        public void StartColumnsCoordinatesToBe0_0_0With2000Height()
        {
            //Arrange
            Beam beam = new Beam();
            Point startPoint = new Point(0, 0, 0);
            double height = 2000;

            TeklaPartAttributeSetter teklaPartCreator = new TeklaPartAttributeSetter();

            //Act
            teklaPartCreator.SetCoordinatesToStartColumn(beam, startPoint, height);

            //Assert
            Assert.Equal(startPoint.X, beam.StartPoint.X);
            Assert.Equal(startPoint.Y, beam.StartPoint.Y);
            Assert.Equal(startPoint.Z, beam.StartPoint.Z);

            Assert.Equal(startPoint.X, beam.EndPoint.X);
            Assert.Equal(startPoint.Y, beam.EndPoint.Y);
            Assert.Equal(height, beam.EndPoint.Z);
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
