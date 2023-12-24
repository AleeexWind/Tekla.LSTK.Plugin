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
    public class ColumnsDataCalculator : IDataCalculator
    {
        private FrameBuildInputData _frameBuildInputData;
        private readonly ElementGroupType _elementGroupType = ElementGroupType.Column;
        private ElementData _leftColumn;
        private ElementData _rightColumn;

        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            bool result = false;
            _frameBuildInputData = inputData as FrameBuildInputData;
            FilterElements(elementsDatas);

            if(CalcLeftColumn() && CalcRightColumn())
            {
                elementsDatas.Add(_rightColumn);
                result = true;
            }

            return result;
        }
        private void FilterElements(List<ElementData> elementsDatas)
        {
            _leftColumn = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(_elementGroupType) && x.ElementSideType.Equals(ElementSideType.Left));
            _rightColumn = ElementDataCloner.CloneElementData(_leftColumn);
            _rightColumn.ElementSideType = ElementSideType.Right;
        }
        private bool CalcLeftColumn()
        {
            try
            {
                Point startPoint = _leftColumn.StartPoint;
                Point endPoint = _leftColumn.EndPoint;

                double profileHeight = TeklaPartAttributeGetter.GetProfileHeight(_leftColumn.Profile);

                (Point, Point) newCoord = (startPoint, endPoint);

                if (_frameBuildInputData.ColumnLineOption.Equals("Inside"))
                {
                    newCoord = CoordinateUtils.GetParallelLineCoordinate(startPoint, endPoint, profileHeight/2);
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
                Point startPoint = new Point()
                {
                    X = _frameBuildInputData.Bay,
                    Y = 0.0,
                    Z = 0.0
                };
                Point endPoint = new Point()
                {
                    X = _frameBuildInputData.Bay,
                    Y = _rightColumn.EndPoint.Y,
                    Z = 0.0
                };

                double profileHeight = TeklaPartAttributeGetter.GetProfileHeight(_rightColumn.Profile);

                (Point, Point) newCoord = (startPoint, endPoint);

                if (_frameBuildInputData.ColumnLineOption.Equals("Inside"))
                {
                    newCoord = CoordinateUtils.GetParallelLineCoordinate(startPoint, endPoint, -profileHeight/2);
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
    }
}
