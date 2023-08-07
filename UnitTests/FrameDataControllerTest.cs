using LSTK.Frame.Models;
using LSTK.Frame;
using LSTK.Frame.Controllers;
using UnitTests.Models;
using Xunit;
using LSTK.Frame.Interactors;
using Tekla.Structures.Geometry3d;
using LSTK.Frame.Interactors.DataStructures;
using LSTK.Frame.Interactors.Boundaries;

namespace UnitTests
{
    public class FrameDataControllerTest
    {
        private FrameDataTest _frameDataTest;
        private IBoundaryInput _inputBoundary;
        private InputData _inputDataTest;
        private PluginData _pluginData;
        public FrameDataControllerTest()
        {
            _frameDataTest = new FrameDataTest();
            _inputBoundary = new FrameDataHandler();
            _pluginData = new PluginData()
            {
                ProfileColumns = "ПСУ400х100х20х3,0",
                PartNameColumns = "COLUMN11",
                MaterialColumns = "350",
                ClassColumns = "2",
                HeightColumns = "5000",

                ProfileTopChord = "ПСУ400х100х20х2,0",
                PartNameTopChord = "TOPCHORD",
                MaterialTopChord = "350",
                ClassTopChord = "3",
                RoofRidgeHeight = "2500",

                ProfileBottomChord = "ПСУ400х100х20х2,0",
                PartNameBottomChord = "BOTTOMCHORD",
                MaterialBottomChord = "350",
                ClassBottomChord = "4",
                RoofBottomHeight = "2000",

                Bay = "22000",
                FrameOption = "Whole"
            };
            _inputDataTest = new InputData()
            {
                ProfileColumns = _pluginData.ProfileColumns,
                PartNameColumns = _pluginData.PartNameColumns,
                MaterialColumns = _pluginData.MaterialColumns,
                ClassColumns = _pluginData.ClassColumns,
                HeightColumns = 5000,

                ProfileTopChord = _pluginData.ProfileTopChord,
                PartNameTopChord = _pluginData.PartNameTopChord,
                MaterialTopChord = _pluginData.MaterialTopChord,
                ClassTopChord = _pluginData.ClassTopChord,
                RoofRidgeHeight = 2500,

                ProfileBottomChord = _pluginData.ProfileBottomChord,
                PartNameBottomChord = _pluginData.PartNameBottomChord,
                MaterialBottomChord = _pluginData.MaterialBottomChord,
                ClassBottomChord = _pluginData.ClassBottomChord,
                RoofBottomHeight = 2000,

                Bay = 22000,
                FrameOption = _pluginData.FrameOption
            };


        }
        [Fact]
        public void GatherInputTest()
        {
            //Arrange
            FrameData frameData = new FrameData();
            //InputData inputData = new InputData();
            //MainWindowViewModel mainWindowViewModel = new MainWindowViewModel
            //{
            //    ProfileColumns = _frameDataTest.ProfileColumns
            //};

            InterfaceDataController frameDataController = new InterfaceDataController(_pluginData, _inputBoundary);

            //Act
            InputData inputData = frameDataController.GatherInput();

            //Assert
            Assert.Equal(_inputDataTest.ProfileColumns, inputData.ProfileColumns);
        }

        //[Fact]
        //public void GetColumnProfileInputTest()
        //{
        //    //Arrange
        //    FrameData frameData = new FrameData();
        //    //MainWindowViewModel mainWindowViewModel = new MainWindowViewModel
        //    //{
        //    //    ProfileColumns = _frameDataTest.ProfileColumns
        //    //};
        //    PluginData pluginData = new PluginData()
        //    {
        //        ProfileColumns = _frameDataTest.ProfileColumns
        //    };

        //    FrameDataController frameDataController = new FrameDataController(pluginData, frameData);

        //    //Act
        //     FrameData frameInput = frameDataController.GetColumnProfileInput();

        //    //Assert
        //    Assert.Equal(_frameDataTest.ProfileColumns, frameInput.ProfileColumns);
        //}

        //[Fact]
        //public void GetColumnNameInputTest()
        //{
        //    //Arrange
        //    FrameData frameData = new FrameData();
        //    //MainWindowViewModel mainWindowViewModel = new MainWindowViewModel
        //    //{
        //    //    PartNameColumns = _frameDataTest.PartNameColumns,
        //    //};
        //    PluginData pluginData = new PluginData()
        //    {
        //        PartNameColumns = _frameDataTest.PartNameColumns,
        //    };

        //    FrameDataController frameDataController = new FrameDataController(pluginData, frameData);

        //    //Act
        //    FrameData frameInput = frameDataController.GetColumnNameInput();

        //    //Assert
        //    Assert.Equal(_frameDataTest.PartNameColumns, frameInput.PartNameColumns);
        //}
        //[Fact]
        //public void GetLeftColumnCoordinatesInputTest()
        //{
        //    //Arrange
        //    FrameData frameData = new FrameData();
        //    //MainWindowViewModel mainWindowViewModel = new MainWindowViewModel
        //    //{
        //    //    HeightColumns = _frameDataTest.EndPointLeftColumn.Y.ToString(),
        //    //};
        //    PluginData pluginData = new PluginData()
        //    {
        //        HeightColumns = _frameDataTest.EndPointLeftColumn.Y.ToString(),
        //    };

        //    FrameDataController frameDataController = new FrameDataController(pluginData, frameData);

        //    //Act
        //    FrameData frameInput = frameDataController.GetLeftColumnCoordinatesInput();

        //    //Assert
        //    Assert.Equal(_frameDataTest.StartPointLeftColumn, frameInput.StartPointLeftColumn);
        //    Assert.Equal(_frameDataTest.EndPointLeftColumn, frameInput.EndPointLeftColumn);
        //}

        //[Fact]
        //public void GetRightColumnCoordinatesInputTest()
        //{
        //    //Arrange
        //    FrameData frameData = new FrameData();
        //    //MainWindowViewModel mainWindowViewModel = new MainWindowViewModel
        //    //{
        //    //    HeightColumns = _frameDataTest.EndPointLeftColumn.Y.ToString(),
        //    //    BayOverall = _frameDataTest.BayOverall.ToString()
        //    //};
        //    PluginData pluginData = new PluginData()
        //    {
        //        HeightColumns = _frameDataTest.EndPointLeftColumn.Y.ToString(),
        //        BayOverall = _frameDataTest.BayOverall.ToString()
        //    };

        //    FrameDataController frameDataController = new FrameDataController(pluginData, frameData);
        //    frameDataController.GetLeftColumnCoordinatesInput();

        //    //Act
        //    FrameData frameInput = frameDataController.GetRightColumnCoordinatesInput();

        //    //Assert
        //    Assert.Equal(_frameDataTest.StartPointRightColumn, frameInput.StartPointRightColumn);
        //    Assert.Equal(_frameDataTest.EndPointRightColumn, frameInput.EndPointRightColumn);
        //}
        //[Fact]
        //public void GetStartPointInputTest()
        //{
        //    //Arrange
        //    FrameData frameData = new FrameData();
        //    //MainWindowViewModel mainWindowViewModel = new MainWindowViewModel
        //    //{
        //    //    PartNameColumns = _frameDataTest.PartNameColumns,
        //    //};
        //    PluginData pluginData = new PluginData()
        //    {
        //        StartPoint = _frameDataTest.StartPoint,
        //    };

        //    FrameDataController frameDataController = new FrameDataController(pluginData, frameData);

        //    //Act
        //    FrameData frameInput = frameDataController.GetStartPointInput();

        //    //Assert
        //    Assert.Equal(_frameDataTest.StartPoint, frameInput.StartPoint);
        //}
        //[Fact]
        //public void GetDirectionPointInputTest()
        //{
        //    //Arrange
        //    FrameData frameData = new FrameData();

        //    PluginData pluginData = new PluginData()
        //    {
        //        DirectionPoint = _frameDataTest.DirectionPoint,
        //    };

        //    FrameDataController frameDataController = new FrameDataController(pluginData, frameData);

        //    //Act
        //    FrameData frameInput = frameDataController.GetDirectionPointInput();

        //    //Assert
        //    Assert.Equal(_frameDataTest.DirectionPoint, frameInput.DirectionPoint);
        //}
    }
}
