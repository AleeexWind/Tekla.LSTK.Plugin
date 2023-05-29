using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Xunit;
using LSTK.Frame.Interactors;
using UnitTests.Models;

namespace UnitTests
{
    public class LocalPlaneManagerTest
    {
        private LocalPlaneManager _localPlaneManager;
        public LocalPlaneManagerTest()
        {
            _localPlaneManager = new LocalPlaneManager();
        }

        [Fact]
        public void IsPresavedCurrentWorkPlaneNotNull()
        {
            //Arrange

            //Act
            TransformationPlane currentPlane = _localPlaneManager.RecieveCurrentWorkPlane();

            //Assert
            Assert.NotNull(currentPlane);
        }

        [Fact]
        public void AreNotVectorsNull()
        {
            //Arrange
            FrameDataTest frameDataTest = new FrameDataTest();
            Point startPoint = frameDataTest.StartPoint;
            Point endPoint = frameDataTest.DirectionPoint;

            //Act
            (Vector X, Vector Y) vectors = _localPlaneManager.SetVectors(startPoint, endPoint);

            //Assert
            Assert.NotNull(vectors.X);
            Assert.NotNull(vectors.Y);
        }

        [Fact]
        public void IsSetCoordinateSystem()
        {
            //Arrange
            FrameDataTest frameDataTest = new FrameDataTest();
            Point startPoint = frameDataTest.StartPoint;
            Point endPoint = frameDataTest.DirectionPoint;
            (Vector X, Vector Y) vectors = _localPlaneManager.SetVectors(startPoint, endPoint);

            //Act
            CoordinateSystem localCoordinateSystem = _localPlaneManager.SetLocalCoordinateSystem(startPoint, vectors);

            //Assert
            Assert.Equal(vectors.X, localCoordinateSystem.AxisX);
            Assert.Equal(vectors.Y, localCoordinateSystem.AxisY);
        }

        [Fact]
        public void IsSetLocalWorkPlaneSucceed()
        {
            //Arrange
            FrameDataTest frameDataTest = new FrameDataTest();
            Point startPoint = frameDataTest.StartPoint;
            Point endPoint = new Point(1000, 0, 0);
            (Vector X, Vector Y) vectors = _localPlaneManager.SetVectors(startPoint, endPoint);
            CoordinateSystem localCoordinateSystem = _localPlaneManager.SetLocalCoordinateSystem(startPoint, vectors);

            //Act
            bool result = _localPlaneManager.SetLocalWorkPlane(localCoordinateSystem);

            //Assert
            Assert.True(result);
        }
        [Fact]
        public void IsSetCurrentWorkPlaneSucceed()
        {
            //Arrange
            TransformationPlane currentPlane = _localPlaneManager.RecieveCurrentWorkPlane();

            //Act
            bool result = _localPlaneManager.SetCurrentWorkPlane(currentPlane);

            //Assert
            Assert.True(result);
        }

    }
}
