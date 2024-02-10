using FrameCreator.BusinessRules.DataBoundaries;
using FrameCreator.BusinessRules.Models;
using FrameCreator.BusinessRules.UseCases.Utils;
using FrameCreator.Entities;
using FrameCreator.Frameworks.TeklaAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameCreator.BusinessRules.UseCases.Calculators.FrameCalculators
{
    public class TopChordTrussDataCalculator : IDataCalculator
    {
        private FrameBuildInputData _frameBuildInputData;
        private readonly ElementGroupType _elementGroupType = ElementGroupType.TopChord;

        private ElementData _leftColumn;
        private ElementData _leftTopChord;
        private ElementData _rightTopChord;

        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            _frameBuildInputData = inputData as FrameBuildInputData;
            FilterElements(elementsDatas);

            if (!CalcLeftTopChord())
            {
                return false;
            }
            bool result = true;

            if (!_frameBuildInputData.IsHalfOption)
            {
                if (!CalcRightTopChord())
                {
                    return false;
                }
                elementsDatas.Add(_rightTopChord);
            }

            return result;
        }
        private void FilterElements(List<ElementData> elementsDatas)
        {
            _leftColumn = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(ElementGroupType.Column) && x.ElementSideType.Equals(ElementSideType.Left));
            _leftTopChord = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(_elementGroupType) && x.ElementSideType.Equals(ElementSideType.Left));
        }
        private void CreateRightTopChordsPrototypes()
        {
            _rightTopChord = ElementDataCloner.CloneElementData(_leftTopChord);
            _rightTopChord.ElementSideType = ElementSideType.Right;
        }
        private bool CalcLeftTopChord()
        {
            try
            {
                Point startPoint = _leftTopChord.StartPoint;
                Point endPoint = _leftTopChord.EndPoint;

                double profileHeight = TeklaPartAttributeGetter.GetProfileHeight(_leftTopChord.Profile);
                (Point, Point) newCoord = (startPoint, endPoint);
                if (_frameBuildInputData.TopChordLineOption.Equals("Below") && _frameBuildInputData.ColumnLineOption.Equals("Inside"))
                {
                    newCoord = CoordinateUtils.GetParallelLineCoordinate(startPoint, endPoint, profileHeight / 2);
                }
                else if (_frameBuildInputData.TopChordLineOption.Equals("Below") && _frameBuildInputData.ColumnLineOption.Equals("Center"))
                {
                    startPoint.X = -_leftColumn.ProfileHeight / 2;
                    newCoord = CoordinateUtils.GetParallelLineCoordinate(startPoint, endPoint, profileHeight / 2);
                }
                else if (_frameBuildInputData.TopChordLineOption.Equals("Center") && _frameBuildInputData.ColumnLineOption.Equals("Inside"))
                {
                    newCoord.Item1.X = _leftColumn.ProfileHeight / 2;
                }
                _leftTopChord.StartPoint = newCoord.Item1;
                _leftTopChord.EndPoint = newCoord.Item2;

                return true;
            }
            catch (Exception)
            {
                //TODO: Logging
                return false;
            }
        }
        private bool CalcRightTopChord()
        {
            try
            {
                CreateRightTopChordsPrototypes();
                double offset = _leftTopChord.EndPoint.X - _frameBuildInputData.Bay;

                Point startPoint = new Point()
                {
                    X = _frameBuildInputData.Bay * 2 - offset,
                    Y = _leftTopChord.StartPoint.Y,
                    Z = 0.0
                };
                Point endPoint = new Point()
                {
                    X = _frameBuildInputData.Bay - offset,
                    Y = _leftTopChord.EndPoint.Y,
                    Z = 0.0
                };

                _rightTopChord.StartPoint = startPoint;
                _rightTopChord.EndPoint = endPoint;

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
