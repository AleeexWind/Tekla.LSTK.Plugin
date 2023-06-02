using LSTK.Frame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;

namespace LSTK.Frame.Controllers
{
    public class FrameDataController
    {
        private MainWindowViewModel _mainWindowViewModel;
        private FrameData _frameData;
        public FrameDataController(MainWindowViewModel mainWindowViewModel, FrameData frameData)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _frameData = frameData;
        }
        public FrameData GetColumnProfileInput()
        {
            _frameData.ProfileColumns = _mainWindowViewModel.ProfileColumns;
            return _frameData;
        }
        public FrameData GetColumnNameInput()
        {
            _frameData.PartNameColumns = _mainWindowViewModel.PartNameColumns;
            return _frameData;
        }
        public FrameData GetLeftColumnCoordinatesInput()
        {
            double endPointY = double.Parse(_mainWindowViewModel.HeightColumns, System.Globalization.CultureInfo.InvariantCulture) ;
            _frameData.EndPointLeftColumn = new Point(_frameData.StartPointLeftColumn.X, endPointY, _frameData.StartPointLeftColumn.Z);
            return _frameData;
        }
        public FrameData GetRightColumnCoordinatesInput()
        {
            double pointX = double.Parse(_mainWindowViewModel.BayOverall, System.Globalization.CultureInfo.InvariantCulture);

            _frameData.StartPointRightColumn = new Point(pointX, _frameData.StartPointLeftColumn.Y, _frameData.StartPointLeftColumn.Z);

            _frameData.EndPointRightColumn = new Point(_frameData.StartPointRightColumn.X, _frameData.EndPointLeftColumn.Y, _frameData.StartPointLeftColumn.Z);
            return _frameData;
        }
    }
}
