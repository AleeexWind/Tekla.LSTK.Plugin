using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.TeklaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public class TrussPostsCalculator : IDataCalculator
    {
        private FrameInputData _frameInputData;

        private ElementData _leftTopChord;
        private ElementData _leftBottomChord;

        private List<ElementData> _leftTrussPosts;
        private List<ElementData> _rightTrussPosts;

        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            _frameInputData = inputData as FrameInputData;

            return CalcLeftTrussPosts() && CalcRightTrussPosts();


            //_frameData = frameData;
            //ParsePanelsString();
            //_frameData.TrussData.TrussPosts = CalcTrussPosts();
        }
        private bool CalcLeftTrussPosts()
        {
            try
            {
                //foreach (var elem in _leftTrussPosts)
                //{
                //    elem.StartPoint.Y = _leftBottomChord.StartPoint.Y;


                //    double allLength = _leftTopChord.EndPoint.X - _leftTopChord.StartPoint.X;
                //    double allHeigtht = _leftTopChord.EndPoint.Y - _leftTopChord.StartPoint.Y;
                //    double lengthFromZero = elem.EndPoint.X - _leftTopChord.StartPoint.X;

                //    Point endPoint = GetTrussPostEndPointOnTheLine(lengthFromZero, allLength, allHeigtht, offsetY, _previousCoord);


                //}

                return true;
            }
            catch (Exception)
            {
                //TODO: Logging
                return false;
            }
        }
        private bool CalcRightTrussPosts()
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                //TODO: Logging
                return false;
            }
        }
        private Point GetTrussPostEndPointOnTheLine(double lengthFromZero, double allLength, double allHeight, double offsetY, double coordX)
        {
            Point point = new Point();
            double height = lengthFromZero * allHeight / allLength;

            point.X = coordX;
            point.Y = offsetY + height;
            point.Z = 0.0;

            return point;
        }
    }
}
