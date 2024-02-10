using FrameCreator.BusinessRules.DataBoundaries;
using FrameCreator.BusinessRules.Models;
using FrameCreator.BusinessRules.UseCases.Utils;
using FrameCreator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameCreator.BusinessRules.UseCases.Calculators.FrameCalculators
{
    public class DiagonalRodsDataCalculator : IDataCalculator
    {
        private FrameBuildInputData _frameBuildInputData;
        private List<ElementData> _leftDiagonalRods;
        private List<ElementData> _rightDiagonalRods;
        private ElementData _leftTopChord;
        private ElementData _leftBottomChord;
        private ElementData _leftColumn;
        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {

            _frameBuildInputData = inputData as FrameBuildInputData;
            FilterElements(elementsDatas);

            if (!CalcLeftDiagonalRods())
            {
                return false;
            }

            bool result = true;

            if (!_frameBuildInputData.IsHalfOption)
            {
                if (!CalcRightDiagonalRods())
                {
                    return false;
                }
                elementsDatas.AddRange(_rightDiagonalRods);
            }

            return result;
        }
        private void FilterElements(List<ElementData> elementsDatas)
        {
            _leftDiagonalRods = elementsDatas.Where(x => x.ElementGroupType.Equals(ElementGroupType.DiagonalRod) && x.ElementSideType.Equals(ElementSideType.Left)).ToList();
            _leftTopChord = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(ElementGroupType.TopChord) && x.ElementSideType.Equals(ElementSideType.Left));
            _leftBottomChord = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(ElementGroupType.BottomChord) && x.ElementSideType.Equals(ElementSideType.Left));
            _leftColumn = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(ElementGroupType.Column) && x.ElementSideType.Equals(ElementSideType.Left));
        }
        private void CreateRightDiagonalRodsPrototypes()
        {
            _rightDiagonalRods = new List<ElementData>();
            foreach (var item in _leftDiagonalRods)
            {
                ElementData el = ElementDataCloner.CloneElementData(item);
                el.ElementSideType = ElementSideType.Right;
                _rightDiagonalRods.Add(el);
            }
        }
        private bool CalcLeftDiagonalRods()
        {
            try
            {
                CreateRightDiagonalRodsPrototypes();
                foreach (var elem in _leftDiagonalRods)
                {
                    Point higherPoint = elem.EndPoint;
                    Point lowerPoint = elem.StartPoint;
                    if (elem.StartPoint.Y > elem.EndPoint.Y)
                    {
                        higherPoint = elem.StartPoint;
                        lowerPoint = elem.EndPoint;
                    }

                    if (elem.StartPoint.X == 0)
                    {
                        if (higherPoint.Y == elem.StartPoint.Y)
                        {
                            Point newStartPoint = TrussPostsCalculator.GetEndPointOnTheLineFromLeft(_leftTopChord.StartPoint, _leftTopChord.EndPoint, _leftColumn.StartPoint.X);
                            elem.StartPoint.X = newStartPoint.X;
                            elem.StartPoint.Y = newStartPoint.Y;

                            elem.EndPoint.Y = _leftBottomChord.StartPoint.Y;
                        }
                        else
                        {
                            Point newEndPoint = TrussPostsCalculator.GetEndPointOnTheLineFromLeft(_leftTopChord.StartPoint, _leftTopChord.EndPoint, higherPoint.X);

                            elem.StartPoint.X = _leftColumn.StartPoint.X;
                            elem.StartPoint.Y = _leftBottomChord.StartPoint.Y;

                            elem.EndPoint.Y = newEndPoint.Y;
                        }
                    }
                    else
                    {
                        lowerPoint.Y = _leftBottomChord.StartPoint.Y;

                        Point newHigherPoint = TrussPostsCalculator.GetEndPointOnTheLineFromLeft(_leftTopChord.StartPoint, _leftTopChord.EndPoint, higherPoint.X);
                        higherPoint.Y = newHigherPoint.Y;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                //TODO: Logging
                return false;
            }
        }
        private bool CalcRightDiagonalRods()
        {
            try
            {
                foreach (var elem in _rightDiagonalRods)
                {
                    Point startPoint = new Point()
                    {
                        X = _frameBuildInputData.Bay * 2 - elem.StartPoint.X,
                        Y = elem.StartPoint.Y,
                        Z = 0.0
                    };
                    Point endPoint = new Point()
                    {
                        X = _frameBuildInputData.Bay * 2 - elem.EndPoint.X,
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
