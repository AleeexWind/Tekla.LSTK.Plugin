using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.TeklaAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public class ColumnsDataCalculator : IDataCalculator
    {
        private FrameInputData _frameInputData;
        private readonly ElementGroupType _elementGroupType = ElementGroupType.Column;
        private ElementData _leftColumn;
        private ElementData _rightColumn;

        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            _frameInputData = inputData as FrameInputData;
            FilterElements(elementsDatas);

            return CalcLeftColumn() && CalcRightColumn();
        }
        private void FilterElements(List<ElementData> elementsDatas)
        {
            _leftColumn = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(_elementGroupType) && x.ElementSideType.Equals(ElementSideType.Left));
            _rightColumn = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(_elementGroupType) && x.ElementSideType.Equals(ElementSideType.Right));
        }
        private bool CalcLeftColumn()
        {
            try
            {
                Point startPoint = _leftColumn.StartPoint;
                Point endPoint = _leftColumn.EndPoint;

                double profileHeight = TeklaPartAttributeGetter.GetProfileHeight(_frameInputData.ProfileColumns);

                (Point, Point) newCoord = (startPoint, endPoint);

                if (_frameInputData.ColumnLineOption.Equals("Inside"))
                {
                    newCoord = GetParallelLineCoordinate(startPoint, endPoint, profileHeight/2);
                }

                _leftColumn.StartPoint = newCoord.Item1;
                _leftColumn.EndPoint = newCoord.Item2;

                return true;
            }
            catch (Exception)
            {
                //TODO: Logging
                return false;
            }
        }
        private bool CalcRightColumn()
        {
            try
            {
                Point startPoint = _rightColumn.StartPoint;
                Point endPoint = _rightColumn.EndPoint;

                double profileHeight = TeklaPartAttributeGetter.GetProfileHeight(_frameInputData.ProfileColumns);

                (Point, Point) newCoord = (startPoint, endPoint);

                if (_frameInputData.ColumnLineOption.Equals("Inside"))
                {
                    newCoord = GetParallelLineCoordinate(startPoint, endPoint, -profileHeight/2);
                }
                _rightColumn.StartPoint = newCoord.Item1;
                _rightColumn.EndPoint = newCoord.Item2;

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
