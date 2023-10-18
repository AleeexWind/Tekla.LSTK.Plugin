using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.TeklaAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators.FrameCalculators
{
    public class TopChordTrussDataCalculator : IDataCalculator
    {
        private FrameInputData _frameInputData;
        private readonly ElementGroupType _elementGroupType = ElementGroupType.TopChord;

        private ElementData _leftColumn;
        private ElementData _leftTopChord;
        private ElementData _rightTopChord;

        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            _frameInputData = inputData as FrameInputData;
            FilterElements(elementsDatas);

            return CalcLeftTopChord() && CalcRightTopChord();
        }
        private void FilterElements(List<ElementData> elementsDatas)
        {
            _leftColumn = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(ElementGroupType.Column) && x.ElementSideType.Equals(ElementSideType.Left));
            _leftTopChord = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(_elementGroupType) && x.ElementSideType.Equals(ElementSideType.Left));
            _rightTopChord = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(_elementGroupType) && x.ElementSideType.Equals(ElementSideType.Right));
        }
        private bool CalcLeftTopChord()
        {
            try
            {
                Point startPoint = _leftTopChord.StartPoint;
                Point endPoint = _leftTopChord.EndPoint;

                double profileHeight = TeklaPartAttributeGetter.GetProfileHeight(_frameInputData.ProfileTopChord);
                (Point, Point) newCoord = (startPoint, endPoint);
                if (_frameInputData.TopChordLineOption.Equals("Below") && _frameInputData.ColumnLineOption.Equals("Inside"))
                {
                    newCoord = GetParallelLineCoordinate(startPoint, endPoint, profileHeight/2);
                }
                else if (_frameInputData.TopChordLineOption.Equals("Below") && _frameInputData.ColumnLineOption.Equals("Center"))
                {
                    startPoint.X = -(_leftColumn.ProfileHeight)/2;
                    newCoord = GetParallelLineCoordinate(startPoint, endPoint, profileHeight/2);
                }
                else if (_frameInputData.TopChordLineOption.Equals("Center") && _frameInputData.ColumnLineOption.Equals("Inside"))
                {
                    newCoord.Item1.X = _leftColumn.ProfileHeight/2;
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
                Point startPoint = _rightTopChord.StartPoint;
                Point endPoint = _rightTopChord.EndPoint;

                double profileHeight = TeklaPartAttributeGetter.GetProfileHeight(_frameInputData.ProfileTopChord);

                (Point, Point) newCoord = (startPoint, endPoint);

                if (_frameInputData.TopChordLineOption.Equals("Below") && _frameInputData.ColumnLineOption.Equals("Inside"))
                {
                    newCoord = GetParallelLineCoordinate(startPoint, endPoint, profileHeight/2);
                }
                else if (_frameInputData.TopChordLineOption.Equals("Below") && _frameInputData.ColumnLineOption.Equals("Center"))
                {
                    endPoint.X = endPoint.X + _leftColumn.ProfileHeight/2;
                    newCoord = GetParallelLineCoordinate(startPoint, endPoint, profileHeight/2);
                }
                else if (_frameInputData.TopChordLineOption.Equals("Center") && _frameInputData.ColumnLineOption.Equals("Inside"))
                {
                    newCoord.Item2.X = endPoint.X - _leftColumn.ProfileHeight/2;
                }
                _rightTopChord.StartPoint = newCoord.Item1;
                _rightTopChord.EndPoint = newCoord.Item2;

                return true;
            }
            catch (Exception)
            {
                //TODO: Logging
                return false;
            }
        }

        private (Point start, Point end) GetParallelLineCoordinate(Point start, Point end, double dist)
        {
            double xV = start.X - end.X;
            double yV = start.Y - end.Y;

            double len = Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));

            double udx = xV / len;
            double udy = yV / len;

            double fX = start.X - udy * dist;
            double fY = start.Y + udx * dist;

            double sX = fX - xV;
            double sY = fY - yV;

            return (new Point() { X = fX, Y = fY }, new Point() { X = sX, Y = sY });
        }
    }
}
