using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators.SchemaCalculators
{
    public class DiagonalRodsSchemaCalculator : IDataCalculator
    {
        private List<ElementData> _diagonalRodsElementsLeft;
        private List<ElementData> _leftTrussPosts;
        private ElementData _leftTopChord;
        private readonly ElementGroupType _elementGroupType = ElementGroupType.DiagonalRod;

        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            FilterElements(elementsDatas);
            OrderElements();
            _diagonalRodsElementsLeft = CalcLeftDiagonalRods();

            elementsDatas.AddRange(_diagonalRodsElementsLeft);

            return true;
        }
        private List<ElementData> CalcLeftDiagonalRods()
        {
            List<ElementData> result = new List<ElementData>();

            ElementData previousTrussPost = null;
            foreach (var trussPost in _leftTrussPosts)
            {
                ElementData elementData = CreateElementData();
                Point startPoint = new Point();
                Point endPoint = new Point();

                if (previousTrussPost == null)
                {
                    startPoint.X = _leftTopChord.StartPoint.X;
                    startPoint.Y = _leftTopChord.StartPoint.Y;
                    startPoint.Z = 0.0;

                    endPoint.X = trussPost.StartPoint.X;
                    endPoint.Y = trussPost.StartPoint.Y;
                    endPoint.Z = 0.0;
                }
                else
                {
                    startPoint.X = previousTrussPost.StartPoint.X;
                    startPoint.Y = previousTrussPost.StartPoint.Y;
                    startPoint.Z = 0.0;

                    endPoint.X = trussPost.EndPoint.X;
                    endPoint.Y = trussPost.EndPoint.Y;
                    endPoint.Z = 0.0;
                }

                elementData.StartPoint = startPoint;
                elementData.EndPoint = endPoint;

                result.Add(elementData);
                previousTrussPost = trussPost;
            }

            Point startPointLast = new Point();
            Point endPointLast = new Point();
            startPointLast.X = previousTrussPost.StartPoint.X;
            startPointLast.Y = previousTrussPost.StartPoint.Y;
            startPointLast.Z = 0.0;

            endPointLast.X = _leftTopChord.EndPoint.X;
            endPointLast.Y = _leftTopChord.EndPoint.Y;
            endPointLast.Z = 0.0;

            ElementData elementDataLast = CreateElementData();
            elementDataLast.StartPoint = startPointLast;
            elementDataLast.EndPoint = endPointLast;

            result.Add(elementDataLast);

            return result;
        }
        private void FilterElements(List<ElementData> elementsDatas)
        {
            _leftTrussPosts = elementsDatas.Where(x => x.ElementGroupType.Equals(ElementGroupType.TrussPost) && x.ElementSideType.Equals(ElementSideType.Left)).ToList();
            _leftTopChord = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(ElementGroupType.TopChord) && x.ElementSideType.Equals(ElementSideType.Left));
        }
        private void OrderElements()
        {
            _leftTrussPosts = _leftTrussPosts.OrderBy(x => x.StartPoint.X).ToList();
        }
        private ElementData CreateElementData()
        {
            return new ElementData()
            {
                ElementGroupType = _elementGroupType
            };
        }
    }
}
