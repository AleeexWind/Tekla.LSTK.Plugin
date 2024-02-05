using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.BusinessRules.UseCases.Utils;
using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators.FrameCalculators
{
    public class DiagonalRodsDataCalculator : IDataCalculator
    {
        private FrameBuildInputData _frameBuildInputData;
        private List<ElementData> _leftDiagonalRods;
        private List<ElementData> _rightDiagonalRods;
        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            bool result = false;
            _frameBuildInputData = inputData as FrameBuildInputData;
            FilterElements(elementsDatas);

            if (CalcRightDiagonalRods())
            {
                elementsDatas.AddRange(_rightDiagonalRods);
                result = true;
            }

            return result;
        }
        private void FilterElements(List<ElementData> elementsDatas)
        {
            _leftDiagonalRods = elementsDatas.Where(x => x.ElementGroupType.Equals(ElementGroupType.DiagonalRod) && x.ElementSideType.Equals(ElementSideType.Left)).ToList();
            _rightDiagonalRods = new List<ElementData>();

            foreach (var item in _leftDiagonalRods)
            {
                ElementData el = ElementDataCloner.CloneElementData(item);
                el.ElementSideType = ElementSideType.Right;
                _rightDiagonalRods.Add(el);
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
                        X = _frameBuildInputData.Bay*2 - elem.StartPoint.X,
                        Y = elem.StartPoint.Y,
                        Z = 0.0
                    };
                    Point endPoint = new Point()
                    {
                        X = _frameBuildInputData.Bay*2 - elem.EndPoint.X,
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
