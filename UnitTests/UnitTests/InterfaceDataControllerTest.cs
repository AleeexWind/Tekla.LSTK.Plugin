using LSTK.Frame;
using LSTK.Frame.Adapters.Controllers;
using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.UseCases;
using PointTekla = Tekla.Structures.Geometry3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Reflection;

namespace UnitTests.UnitTests
{
    public class InterfaceDataControllerTest
    {
        [Fact]
        public void GatherInputTest()
        {
            //Arrange
            PointTekla.Point point = new PointTekla.Point();
            PluginData pluginData = new PluginData()
            { 
                Bay = "5000",
                HeightColumns = "5000",
                RoofBottomHeight = "5000",
                RoofRidgeHeight = "5000",
                StartPoint = point,
                DirectionPoint = point
            };

            FrameCreatorManager frameCreatorManager = new FrameCreatorManager();
            InterfaceDataController interfaceDataController = new InterfaceDataController(frameCreatorManager);

            //Act
            interfaceDataController.GatherInput(pluginData);

            //Assert

            object t = typeof(InterfaceDataController).GetField("_inputData", BindingFlags.NonPublic |
                         BindingFlags.Instance).GetValue(interfaceDataController);
            Assert.NotNull(t);
        }
        [Fact]
        public void SendInputTest()
        {
            //Arrange
            PointTekla.Point point = new PointTekla.Point();
            PluginData pluginData = new PluginData()
            {
                Bay = "5000",
                HeightColumns = "5000",
                RoofBottomHeight = "5000",
                RoofRidgeHeight = "5000",
                StartPoint = point,
                DirectionPoint = point
            };

            FrameCreatorManager frameCreatorManager = new FrameCreatorManager();
            InterfaceDataController interfaceDataController = new InterfaceDataController(frameCreatorManager);
            interfaceDataController.GatherInput(pluginData);

            //Act
            interfaceDataController.SendInput();

            //Assert

            object t = typeof(FrameCreatorManager).GetField("_frameInputData", BindingFlags.NonPublic |
                         BindingFlags.Instance).GetValue(frameCreatorManager);
            Assert.NotNull(t);
        }
    }
}
