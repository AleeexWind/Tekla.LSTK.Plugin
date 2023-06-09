using LSTK.Frame.Models;
using LSTK.Frame;
using LSTK.Frame.Controllers;
using UnitTests.Models;
using Xunit;
using LSTK.Frame.Interactors;
using Tekla.Structures.Geometry3d;

namespace UnitTests
{
    public class FrameDataControllerTest
    {
        private FrameDataTest _frameDataTest;
        public FrameDataControllerTest()
        {
            _frameDataTest = new FrameDataTest();
        }
        [Fact]
        public void GetColumnProfileInputTest()
        {
            //Arrange
            FrameData frameData = new FrameData();
            //MainWindowViewModel mainWindowViewModel = new MainWindowViewModel
            //{
            //    ProfileColumns = _frameDataTest.ProfileColumns
            //};
            PluginData pluginData = new PluginData()
            {
                ProfileColumns = _frameDataTest.ProfileColumns
            };

            FrameDataController frameDataController = new FrameDataController(pluginData, frameData);

            //Act
             FrameData frameInput = frameDataController.GetColumnProfileInput();

            //Assert
            Assert.Equal(_frameDataTest.ProfileColumns, frameInput.ProfileColumns);
        }

        [Fact]
        public void GetColumnNameInputTest()
        {
            //Arrange
            FrameData frameData = new FrameData();
            //MainWindowViewModel mainWindowViewModel = new MainWindowViewModel
            //{
            //    PartNameColumns = _frameDataTest.PartNameColumns,
            //};
            PluginData pluginData = new PluginData()
            {
                PartNameColumns = _frameDataTest.PartNameColumns,
            };

            FrameDataController frameDataController = new FrameDataController(pluginData, frameData);

            //Act
            FrameData frameInput = frameDataController.GetColumnNameInput();

            //Assert
            Assert.Equal(_frameDataTest.PartNameColumns, frameInput.PartNameColumns);
        }
        [Fact]
        public void GetLeftColumnCoordinatesInputTest()
        {
            //Arrange
            FrameData frameData = new FrameData();
            //MainWindowViewModel mainWindowViewModel = new MainWindowViewModel
            //{
            //    HeightColumns = _frameDataTest.EndPointLeftColumn.Y.ToString(),
            //};
            PluginData pluginData = new PluginData()
            {
                HeightColumns = _frameDataTest.EndPointLeftColumn.Y.ToString(),
            };

            FrameDataController frameDataController = new FrameDataController(pluginData, frameData);
  
            //Act
            FrameData frameInput = frameDataController.GetLeftColumnCoordinatesInput();
         
            //Assert
            Assert.Equal(_frameDataTest.StartPointLeftColumn, frameInput.StartPointLeftColumn);
            Assert.Equal(_frameDataTest.EndPointLeftColumn, frameInput.EndPointLeftColumn);
        }

        [Fact]
        public void GetRightColumnCoordinatesInputTest()
        {
            //Arrange
            FrameData frameData = new FrameData();
            //MainWindowViewModel mainWindowViewModel = new MainWindowViewModel
            //{
            //    HeightColumns = _frameDataTest.EndPointLeftColumn.Y.ToString(),
            //    BayOverall = _frameDataTest.BayOverall.ToString()
            //};
            PluginData pluginData = new PluginData()
            {
                HeightColumns = _frameDataTest.EndPointLeftColumn.Y.ToString(),
                BayOverall = _frameDataTest.BayOverall.ToString()
            };

            FrameDataController frameDataController = new FrameDataController(pluginData, frameData);
            frameDataController.GetLeftColumnCoordinatesInput();

            //Act
            FrameData frameInput = frameDataController.GetRightColumnCoordinatesInput();

            //Assert
            Assert.Equal(_frameDataTest.StartPointRightColumn, frameInput.StartPointRightColumn);
            Assert.Equal(_frameDataTest.EndPointRightColumn, frameInput.EndPointRightColumn);
        }
        [Fact]
        public void GetStartPointInputTest()
        {
            //Arrange
            FrameData frameData = new FrameData();
            //MainWindowViewModel mainWindowViewModel = new MainWindowViewModel
            //{
            //    PartNameColumns = _frameDataTest.PartNameColumns,
            //};
            PluginData pluginData = new PluginData()
            {
                StartPoint = _frameDataTest.StartPoint,
            };

            FrameDataController frameDataController = new FrameDataController(pluginData, frameData);

            //Act
            FrameData frameInput = frameDataController.GetStartPointInput();

            //Assert
            Assert.Equal(_frameDataTest.StartPoint, frameInput.StartPoint);
        }
        [Fact]
        public void GetDirectionPointInputTest()
        {
            //Arrange
            FrameData frameData = new FrameData();

            PluginData pluginData = new PluginData()
            {
                DirectionPoint = _frameDataTest.DirectionPoint,
            };

            FrameDataController frameDataController = new FrameDataController(pluginData, frameData);

            //Act
            FrameData frameInput = frameDataController.GetDirectionPointInput();

            //Assert
            Assert.Equal(_frameDataTest.DirectionPoint, frameInput.DirectionPoint);
        }
    }
}
