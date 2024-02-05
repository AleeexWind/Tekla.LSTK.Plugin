using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators.SchemaCalculators
{
    public class TrussPostsSchemaCalculator : IDataCalculator
    {
        private SchemaInputData _schemaInputData;
        private string[] _distances;
        private List<double> _trussPostDistancesLeft;
        private double _previousCoord;
        private List<ElementData> _trussPostsElementsLeft;
        private readonly ElementGroupType _elementGroupType = ElementGroupType.TrussPost;

        private ElementData _leftTopChord;
        private ElementData _leftBottomChord;

        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            _schemaInputData = inputData as SchemaInputData;
            _distances = ParsePanelsString();
            _previousCoord = 0;
            FilterElements(elementsDatas);
            _trussPostsElementsLeft = CalcLeftTrussPosts();
            //List<ElementData> trussPostsElementsRight = CalcRightThrussPosts();


            if(_trussPostsElementsLeft != null)
            {
                elementsDatas.AddRange(_trussPostsElementsLeft);
                //elementsDatas.AddRange(trussPostsElementsRight);
                return true;
            }
            else
            {
                //TODO: Logging
                return false;
            }
        }

        private string[] ParsePanelsString()
        {
            string[] p = _schemaInputData.Panels.Split(' ');
            return p;
        }
        private void FilterElements(List<ElementData> elementsDatas)
        {
            _leftTopChord = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(ElementGroupType.TopChord) && x.ElementSideType.Equals(ElementSideType.Left));
            _leftBottomChord = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(ElementGroupType.BottomChord) && x.ElementSideType.Equals(ElementSideType.Left));
        }

        private List<double> GetLeftDistances(string[] distancesAsString)
        {
            List<double> distances = new List<double>();
            for (int i = 0; i < distancesAsString.Length; i++)
            {
                string[] p = distancesAsString[i].Split('*');

                double amount = 0;
                double dist = 0;

                if (p.Length > 1)
                {
                    amount = double.Parse(p[0], System.Globalization.CultureInfo.InvariantCulture);
                    dist = double.Parse(p[1], System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    amount = 1;
                    dist = double.Parse(p[0], System.Globalization.CultureInfo.InvariantCulture);
                }

                for (int r = 0; r < amount; r++)
                {
                    distances.Add(dist);
                }
            }
            return distances;
        }

        private List<double> GetRightDistances(List<double> leftDistances)
        {
            double startOffsetX = (_schemaInputData.Bay - _previousCoord) * 2;
            List<double> distances = leftDistances;
            distances.Add(startOffsetX);
            distances = distances.Reverse<double>().ToList();
            return distances;
        }

        private List<ElementData> CalcLeftTrussPosts()
        {
            List<ElementData> result = new List<ElementData>();
            _trussPostDistancesLeft = GetLeftDistances(_distances);
            if (_trussPostDistancesLeft.Any())
            {
                _trussPostDistancesLeft.RemoveAt(_trussPostDistancesLeft.Count -1);
                foreach (var dist in _trussPostDistancesLeft)
                {
                    _previousCoord += dist;
                    Point startPoint = new Point()
                    {
                        X = _previousCoord,
                        Y = _leftBottomChord.StartPoint.Y,
                        Z = 0.0
                    };

                    double allLength = _leftTopChord.EndPoint.X - _leftTopChord.StartPoint.X;
                    double allHeigtht = _leftTopChord.EndPoint.Y - _leftTopChord.StartPoint.Y;

                    double lengthFromZero = _previousCoord - _leftTopChord.StartPoint.X;
                    double offsetY = _leftBottomChord.StartPoint.Y + _schemaInputData.HeightRoofBottom;
                    double additionalOffset = _leftTopChord.StartPoint.Y - _schemaInputData.HeightColumns;

                    offsetY += additionalOffset;

                    Point endPoint = GetTrussPostEndPointOnTheLine(lengthFromZero, allLength, allHeigtht, offsetY, _previousCoord);

                    ElementData elementData = CreateElementData();
                    elementData.StartPoint = startPoint;
                    elementData.EndPoint = endPoint;
                    elementData.ElementSideType = ElementSideType.Left;

                    result.Add(elementData);
                }
            }

            return result;
        }

        private List<ElementData> CalcRightThrussPosts()
        {
            List<ElementData> result = new List<ElementData>();
            List<double> trussPostDistancesRight = GetRightDistances(_trussPostDistancesLeft);
            if (trussPostDistancesRight.Any())
            {
                trussPostDistancesRight.RemoveAt(trussPostDistancesRight.Count -1);
                foreach (var dist in trussPostDistancesRight)
                {
                    _previousCoord += dist;
                    Point startPoint = new Point()
                    {
                        X = _previousCoord,
                        Y = _leftBottomChord.StartPoint.Y,
                        Z = 0.0
                    };

                    Point endPoint = new Point()
                    {
                        X = startPoint.X,
                        Y = 0,
                        Z = 0
                    };

                    ElementData elementData = CreateElementData();
                    elementData.StartPoint = startPoint;
                    elementData.EndPoint = endPoint;
                    elementData.ElementSideType = ElementSideType.Right;


                    result.Add(elementData);
                }
                List<double> leftPostsYcoord = new List<double>();
                foreach (var leftTrussPost in _trussPostsElementsLeft)
                {
                    leftPostsYcoord.Add(leftTrussPost.EndPoint.Y);
                }
                leftPostsYcoord.Reverse();
                for (int i = 0; i < result.Count; i++)
                {
                    result[i].EndPoint.Y = leftPostsYcoord[i];
                }
            }

            return result;
        }
        private Point GetTrussPostEndPointOnTheLine(double lengthFromZero, double allLength, double allHeight, double offsetY, double coordX)
        {
            Point point = new Point();
            double height = lengthFromZero * allHeight / allLength;

            point.X = coordX;
            point.Y = offsetY + height;
            point.Z = 0.0;

            return point;
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
