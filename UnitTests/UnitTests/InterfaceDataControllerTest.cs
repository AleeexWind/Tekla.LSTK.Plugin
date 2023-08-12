using LSTK.Frame;
using LSTK.Frame.Adapters.Controllers;
using LSTK.Frame.BusinessRules.UseCases;
using PointTekla = Tekla.Structures.Geometry3d;
using System.Collections.Generic;
using Xunit;
using System.Reflection;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.TeklaAPI;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using Tekla.Structures.Model;

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
                HeightRoofBottom = "5000",
                HeightRoofRidge = "5000",
                StartPoint = point,
                DirectionPoint = point
            };

            FrameCreatorManager frameCreatorManager = new FrameCreatorManager(new FrameData(), new TeklaPartCreator(new Model(), new TeklaPartAttributeSetter()), new List<IDataCalculator>(), new LocalPlaneManager(new Model()));
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
                HeightRoofBottom = "5000",
                HeightRoofRidge = "5000",
                StartPoint = point,
                DirectionPoint = point
            };

            FrameCreatorManager frameCreatorManager = new FrameCreatorManager(new FrameData(), new TeklaPartCreator(new Model(), new TeklaPartAttributeSetter()), new List<IDataCalculator>(), new LocalPlaneManager(new Model()));
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
