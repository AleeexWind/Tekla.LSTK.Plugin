using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators.SchemaCalculators
{
    public class DiagonalRodsSchemaCalculator : IDataCalculator
    {
        private List<ElementData> _diagonalRodsElementsLeft;
        private List<ElementData> _leftTrussPosts;
        private ElementData _leftTopChord;
        private ElementData _leftBottomChord;
        private readonly ElementGroupType _elementGroupType = ElementGroupType.DiagonalRod;

        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            FilterElements(elementsDatas);
            OrderElements();
            _diagonalRodsElementsLeft = CalcLeftDiagonalRods();

            elementsDatas.AddRange(_diagonalRodsElementsLeft);

            List<ElementData> diaRods = _diagonalRodsElementsLeft.Where(x => x.ElementGroupType.Equals(ElementGroupType.DiagonalRod)).ToList();
            foreach (var dr in diaRods)
            {
                DiagonalRodElement diagonalRodElement = dr as DiagonalRodElement;
            }

            return true;
        }
        private List<ElementData> CalcLeftDiagonalRods()
        {
            List<ElementData> result = new List<ElementData>();

            ElementData previousTrussPost = null;
            foreach (var trussPost in _leftTrussPosts)
            {
                DiagonalRodElement elementData = CreateElementData();
                Point startPoint = new Point();
                Point endPoint = new Point();

                Point startPointAlt = new Point();
                Point endPointAlt = new Point();

                if (previousTrussPost == null)
                {
                    startPoint.X = _leftTopChord.StartPoint.X;
                    startPoint.Y = _leftTopChord.StartPoint.Y;
                    startPoint.Z = 0.0;

                    endPoint.X = trussPost.StartPoint.X;
                    endPoint.Y = trussPost.StartPoint.Y;
                    endPoint.Z = 0.0;

                    startPointAlt.X = _leftBottomChord.StartPoint.X;
                    startPointAlt.Y = _leftBottomChord.StartPoint.Y;
                    startPointAlt.Z = 0.0;

                    endPointAlt.X = trussPost.EndPoint.X;
                    endPointAlt.Y = trussPost.EndPoint.Y;
                    endPointAlt.Z = 0.0;
                }
                else
                {
                    startPoint.X = previousTrussPost.StartPoint.X;
                    startPoint.Y = previousTrussPost.StartPoint.Y;
                    startPoint.Z = 0.0;

                    endPoint.X = trussPost.EndPoint.X;
                    endPoint.Y = trussPost.EndPoint.Y;
                    endPoint.Z = 0.0;

                    startPointAlt.X = previousTrussPost.EndPoint.X;
                    startPointAlt.Y = previousTrussPost.EndPoint.Y;
                    startPointAlt.Z = 0.0;

                    endPointAlt.X = trussPost.StartPoint.X;
                    endPointAlt.Y = trussPost.StartPoint.Y;
                    endPointAlt.Z = 0.0;
                }

                elementData.StartPoint = startPoint;
                elementData.EndPoint = endPoint;

                elementData.AlternativeStartPoint = startPointAlt;
                elementData.AlternativeEndPoint = endPointAlt;

                result.Add(elementData);
                previousTrussPost = trussPost;
            }

            Point startPointLast = new Point();
            Point endPointLast = new Point();

            Point startPointAltLast = new Point();
            Point endPointAltLast = new Point();

            startPointLast.X = previousTrussPost.StartPoint.X;
            startPointLast.Y = previousTrussPost.StartPoint.Y;
            startPointLast.Z = 0.0;

            endPointLast.X = _leftTopChord.EndPoint.X;
            endPointLast.Y = _leftTopChord.EndPoint.Y;
            endPointLast.Z = 0.0;

            startPointAltLast.X = previousTrussPost.EndPoint.X;
            startPointAltLast.Y = previousTrussPost.EndPoint.Y;
            startPointAltLast.Z = 0.0;

            endPointAltLast.X = _leftBottomChord.EndPoint.X;
            endPointAltLast.Y = _leftBottomChord.EndPoint.Y;
            endPointAltLast.Z = 0.0;

            DiagonalRodElement elementDataLast = CreateElementData();
            elementDataLast.StartPoint = startPointLast;
            elementDataLast.EndPoint = endPointLast;

            elementDataLast.AlternativeStartPoint = startPointAltLast;
            elementDataLast.AlternativeEndPoint = endPointAltLast;

            result.Add(elementDataLast);

            return result;
        }
        private void FilterElements(List<ElementData> elementsDatas)
        {
            _leftTrussPosts = elementsDatas.Where(x => x.ElementGroupType.Equals(ElementGroupType.TrussPost) && x.ElementSideType.Equals(ElementSideType.Left)).ToList();
            _leftTopChord = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(ElementGroupType.TopChord) && x.ElementSideType.Equals(ElementSideType.Left));
            _leftBottomChord = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(ElementGroupType.BottomChord) && x.ElementSideType.Equals(ElementSideType.Left));
        }
        private void OrderElements()
        {
            _leftTrussPosts = _leftTrussPosts.OrderBy(x => x.StartPoint.X).ToList();
        }
        private DiagonalRodElement CreateElementData()
        {
            return new DiagonalRodElement()
            {
                ElementGroupType = _elementGroupType
            };
        }
    }
}
