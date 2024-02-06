using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.BusinessRules.UseCases.Utils;
using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators.FrameCalculators
{
    public class TrussPostsCalculator : IDataCalculator
    {
        private FrameBuildInputData _frameBuildInputData;

        private List<ElementData> _leftTrussPosts;
        private List<ElementData> _rightTrussPosts;
        private ElementData _leftTopChord;
        private ElementData _leftBottomChord;

        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            bool result = false;
            _frameBuildInputData = inputData as FrameBuildInputData;
            FilterElements(elementsDatas);

            if (!CalcLeftTrussPosts())
            {
                result = false;
            }
            foreach (var item in _leftTrussPosts)
            {
                ElementData el = ElementDataCloner.CloneElementData(item);
                el.ElementSideType = ElementSideType.Right;
                _rightTrussPosts.Add(el);
            }

            if (CalcRightTrussPosts())
            {
                elementsDatas.AddRange(_rightTrussPosts);
                result = true;
            }

            return result;
        }

        private void FilterElements(List<ElementData> elementsDatas)
        {
            _leftTrussPosts = elementsDatas.Where(x => x.ElementGroupType.Equals(ElementGroupType.TrussPost) && x.ElementSideType.Equals(ElementSideType.Left)).ToList();
            _leftTopChord = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(ElementGroupType.TopChord) && x.ElementSideType.Equals(ElementSideType.Left));
            _leftBottomChord = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(ElementGroupType.BottomChord) && x.ElementSideType.Equals(ElementSideType.Left));

            _rightTrussPosts = new List<ElementData>();
        }
        private bool CalcLeftTrussPosts()
        {
            try
            {
                foreach (var elem in _leftTrussPosts)
                {
                    elem.StartPoint.Y = _leftBottomChord.StartPoint.Y;
                    elem.EndPoint = GetEndPointOnTheLineFromLeft(_leftTopChord.StartPoint, _leftTopChord.EndPoint, elem.StartPoint.X);
                }
                return true;
            }
            catch (Exception)
            {
                //TODO: Logging
                return false;
            }
        }
        public static Point GetEndPointOnTheLineFromLeft(Point leftTopChordStartPoint, Point leftTopChordEndPoint, double XcoordOfTargetPoint)
        {
            double allLength = leftTopChordEndPoint.X - leftTopChordStartPoint.X;
            double allHeight = leftTopChordEndPoint.Y - leftTopChordStartPoint.Y;
            double lengthFromZero = XcoordOfTargetPoint - leftTopChordStartPoint.X;

            Point point = new Point();
            double height = lengthFromZero * allHeight / allLength;

            point.X = XcoordOfTargetPoint;
            point.Y = leftTopChordStartPoint.Y + height;
            point.Z = 0.0;

            return point;
        }
        private bool CalcRightTrussPosts()
        {
            try
            {
                foreach (var elem in _rightTrussPosts)
                {
                    Point startPoint = new Point()
                    {
                        X = _frameBuildInputData.Bay * 2 - elem.StartPoint.X,
                        Y = elem.StartPoint.Y,
                        Z = 0.0
                    };
                    Point endPoint = new Point()
                    {
                        X = _frameBuildInputData.Bay * 2 - elem.StartPoint.X,
                        Y = elem.EndPoint.Y,
                        Z = 0.0
                    };

                    elem.StartPoint = startPoint;
                    elem.EndPoint = endPoint;
                }
                return true;
            }
            catch (Exception)
            {
                //TODO: Logging
                return false;
            }
        }
    }
}
