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

        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            bool result = false;
            _frameBuildInputData = inputData as FrameBuildInputData;
            FilterElements(elementsDatas);

            if(CalcRightTrussPosts())
            {
                elementsDatas.AddRange(_rightTrussPosts);
                result = true;
            }

            return result;
        }

        private void FilterElements(List<ElementData> elementsDatas)
        {
            _leftTrussPosts = elementsDatas.Where(x => x.ElementGroupType.Equals(ElementGroupType.TrussPost) && x.ElementSideType.Equals(ElementSideType.Left)).ToList();

            _rightTrussPosts = new List<ElementData>();

            foreach (var item in _leftTrussPosts)
            {
                ElementData el = ElementDataCloner.CloneElementData(item);
                el.ElementSideType = ElementSideType.Right;
                _rightTrussPosts.Add(el);
            }
        }
        private bool CalcRightTrussPosts()
        {
            try
            {
                foreach (var elem in _rightTrussPosts)
                {
                    Point startPoint = new Point()
                    {
                        X = _frameBuildInputData.Bay*2 - elem.StartPoint.X,
                        Y = elem.StartPoint.Y,
                        Z = 0.0
                    };
                    Point endPoint = new Point()
                    {
                        X = _frameBuildInputData.Bay*2 - elem.StartPoint.X,
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
        private Point GetTrussPostEndPointOnTheLine(Point startPointLine, Point endPointLine, Point startPointOfTrussPost)
        {
            Point point = new Point();

            return point;
        }
    }
}
