using LSTK.Frame.Utils;
using PointTekla = Tekla.Structures.Geometry3d;
using PointPlugin = LSTK.Frame.Entities;
using Xunit;

namespace UnitTests.UnitTests.Utils
{
    public class TeklaPointConverterTest
    {
        [Fact]
        public void ConvertToTeklaPoint()
        {
            //Arrange
            PointPlugin.Point point = new PointPlugin.Point()
            { 
                X = 1,
                Y = 1,
                Z = 1
            };

            //Act
            PointTekla.Point teklaPoint = TeklaPointConverter.ConvertPoint(point);

            //Assert
            Assert.Equal(point.X, teklaPoint.X);
            Assert.Equal(point.Y, teklaPoint.Y);
            Assert.Equal(point.Z, teklaPoint.Z);
        }
        [Fact]
        public void ConvertToPluginPoint()
        {
            //Arrange
            PointTekla.Point point = new PointTekla.Point()
            {
                X = 1,
                Y = 1,
                Z = 1
            };

            //Act
            PointPlugin.Point pluginPoint = TeklaPointConverter.ConvertPoint(point);

            //Assert
            Assert.Equal(point.X, pluginPoint.X);
            Assert.Equal(point.Y, pluginPoint.Y);
            Assert.Equal(point.Z, pluginPoint.Z);
        }
    }
}
