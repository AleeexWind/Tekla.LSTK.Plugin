using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.BusinessRules.UseCases.Utils;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.TeklaAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators.FrameCalculators
{
    public class BottomChordTrussDataCalculator : IDataCalculator
    {
        private FrameBuildInputData _frameBuildInputData;
        private ElementData _leftBottomChord;
        private ElementData _rightBottomChord;
        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            _frameBuildInputData = inputData as FrameBuildInputData;
            FilterElements(elementsDatas);

            if (!CalcLeftBottomChord())
            {
                return false;
            }

            bool result = true;

            if (!_frameBuildInputData.IsHalfOption)
            {
                if (!CalcRightBottomChord())
                {
                    return false;
                }
                elementsDatas.Add(_rightBottomChord);
            }

            return result;
        }
        private void FilterElements(List<ElementData> elementsDatas)
        {
            _leftBottomChord = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(ElementGroupType.BottomChord) && x.ElementSideType.Equals(ElementSideType.Left));
        }
        private void CreateRightBottomChordsPrototypes()
        {
            _rightBottomChord = ElementDataCloner.CloneElementData(_leftBottomChord);
            _rightBottomChord.ElementSideType = ElementSideType.Right;
        }
        private bool CalcLeftBottomChord()
        {
            try
            {
                Point startPoint = _leftBottomChord.StartPoint;
                Point endPoint = _leftBottomChord.EndPoint;

                double profileHeight = TeklaPartAttributeGetter.GetProfileHeight(_leftBottomChord.Profile);
                (Point, Point) newCoord = (startPoint, endPoint);
                if (_frameBuildInputData.BottomChordLineOption.Equals("Below"))
                {
                    newCoord = CoordinateUtils.GetParallelLineCoordinate(startPoint, endPoint, profileHeight / 2);
                }
                else if (_frameBuildInputData.BottomChordLineOption.Equals("Above"))
                {
                    newCoord = CoordinateUtils.GetParallelLineCoordinate(startPoint, endPoint, -profileHeight / 2);
                }
                _leftBottomChord.StartPoint = newCoord.Item1;
                _leftBottomChord.EndPoint = newCoord.Item2;
                return true;
            }
            catch (Exception ex)
            {
                //TODO: Logging
                return false;
            }
        }
        private bool CalcRightBottomChord()
        {
            try
            {
                CreateRightBottomChordsPrototypes();
                Point startPoint = new Point()
                {
                    X = _frameBuildInputData.Bay * 2,
                    Y = _leftBottomChord.StartPoint.Y,
                    Z = 0.0
                };
                Point endPoint = new Point()
                {
                    X = _frameBuildInputData.Bay,
                    Y = _leftBottomChord.EndPoint.Y,
                    Z = 0.0
                };

                _rightBottomChord.StartPoint = startPoint;
                _rightBottomChord.EndPoint = endPoint;
                return true;
            }
            catch (Exception ex)
            {
                //TODO: Logging
                return false;
            }
        }
    }
}
