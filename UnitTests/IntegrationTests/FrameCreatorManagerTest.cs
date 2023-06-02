using Xunit;
using System;
using LSTK.Frame;
using UnitTests.Models;
using LSTK.Frame.Models;
using LSTK.Frame.Controllers;
using LSTK.Frame.Interactors;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace UnitTests.IntegrationTests
{
    public class FrameCreatorManagerTest
    {
        [Fact]
        public void ColumnFrameDataShouldBeFilled()
        {
            //Arrange
            FrameDataTest frameDataTest = new FrameDataTest();

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel()
            {
                PartNameColumns = frameDataTest.PartNameColumns,
                ProfileColumns = frameDataTest.ProfileColumns,
                HeightColumns = frameDataTest.HeightColumns.ToString(),
                BayOverall = frameDataTest.BayOverall.ToString()

            };


            FrameData frameData = new FrameData();
            TeklaPointSelector teklaPointSelector = new TeklaPointSelector();
            LocalPlaneManager localPlaneManager = new LocalPlaneManager();
            FrameCreatorManager frameCreatorManager = new FrameCreatorManager(frameData, mainWindowViewModel, teklaPointSelector, localPlaneManager);

            //Act
            frameCreatorManager.SetLocalWorkingPlane();
            frameCreatorManager.BuildFrameData();

            //Assert
            Assert.Equal(frameDataTest.StartPointLeftColumn, frameData.StartPointLeftColumn);
            Assert.Equal(frameDataTest.EndPointLeftColumn, frameData.EndPointLeftColumn);
            Assert.Equal(frameDataTest.ProfileColumns, frameData.ProfileColumns);
            Assert.Equal(frameDataTest.PartNameColumns, frameData.PartNameColumns);
        }
        [Fact]
        public void NewLocalWorkPlaneIsSetToStartPoint()
        {
            //Arrange
            FrameDataTest frameDataTest = new FrameDataTest();
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();


            FrameData frameData = new FrameData();
            TeklaPointSelector teklaPointSelector = new TeklaPointSelector();
            LocalPlaneManager localPlaneManager = new LocalPlaneManager();
            FrameCreatorManager frameCreatorManager = new FrameCreatorManager(frameData, mainWindowViewModel, teklaPointSelector, localPlaneManager);

            //Act
            frameCreatorManager.SetLocalWorkingPlane();
            (Point, Point) selectedPointsTest = teklaPointSelector.SelectPoints();

            //Assert
            Assert.Equal(frameDataTest.StartPoint, selectedPointsTest.Item1);
        }
        [Fact]
        public void ColumnCreationTest()
        {
            //Arrange
            FrameDataTest frameDataTest = new FrameDataTest();
            Beam leftbeamForChecking = new Beam();
            leftbeamForChecking.StartPoint.X = frameDataTest.StartPointLeftColumn.X;
            leftbeamForChecking.StartPoint.Y = frameDataTest.StartPointLeftColumn.Y;
            leftbeamForChecking.StartPoint.Z = frameDataTest.StartPointLeftColumn.Z;
            leftbeamForChecking.EndPoint.X = frameDataTest.EndPointLeftColumn.X;
            leftbeamForChecking.EndPoint.Y = frameDataTest.EndPointLeftColumn.Y;
            leftbeamForChecking.EndPoint.Z = frameDataTest.EndPointLeftColumn.Z;

            leftbeamForChecking.Profile.ProfileString = frameDataTest.ProfileColumns;

            Beam rightbeamForChecking = new Beam();
            rightbeamForChecking.StartPoint.X = frameDataTest.StartPointRightColumn.X;
            rightbeamForChecking.StartPoint.Y = frameDataTest.StartPointRightColumn.Y;
            rightbeamForChecking.StartPoint.Z = frameDataTest.StartPointRightColumn.Z;
            rightbeamForChecking.EndPoint.X = frameDataTest.EndPointRightColumn.X;
            rightbeamForChecking.EndPoint.Y = frameDataTest.EndPointRightColumn.Y;
            rightbeamForChecking.EndPoint.Z = frameDataTest.EndPointRightColumn.Z;

            rightbeamForChecking.Profile.ProfileString = frameDataTest.ProfileColumns;


            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel()
            {
                PartNameColumns = frameDataTest.PartNameColumns,
                ProfileColumns = frameDataTest.ProfileColumns,
                HeightColumns = frameDataTest.HeightColumns.ToString(),
                BayOverall = frameDataTest.BayOverall.ToString()

            };

            FrameData frameData = new FrameData();
            TeklaPointSelector teklaPointSelector = new TeklaPointSelector();
            LocalPlaneManager localPlaneManager = new LocalPlaneManager();
            TeklaPartAttributeSetter teklaPartAttributeSetter = new TeklaPartAttributeSetter();
            TeklaPartCreator teklaPartCreator = new TeklaPartCreator(frameData, teklaPartAttributeSetter);

            FrameCreatorManager frameCreatorManager = new FrameCreatorManager(frameData, mainWindowViewModel, teklaPointSelector, localPlaneManager);

            //Act
            frameCreatorManager.SetLocalWorkingPlane();
            frameCreatorManager.BuildFrameData();
            (Beam, Beam) columns = frameCreatorManager.CreateColumns(teklaPartCreator);


            //Assert
            Assert.Equal(leftbeamForChecking.StartPoint, columns.Item1.StartPoint);
            Assert.Equal(rightbeamForChecking.StartPoint, columns.Item2.StartPoint);
        }
        [Fact]
        public void IsCommitSucceed()
        {
            //Arrange
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();

            FrameData frameData = new FrameData();
            TeklaPointSelector teklaPointSelector = new TeklaPointSelector();
            LocalPlaneManager localPlaneManager = new LocalPlaneManager();

            FrameCreatorManager frameCreatorManager = new FrameCreatorManager(frameData, mainWindowViewModel, teklaPointSelector, localPlaneManager);

            //Act
            bool result = frameCreatorManager.Commit();

            //Assert
            Assert.True(result);
        }
    }
}
