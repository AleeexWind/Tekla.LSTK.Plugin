using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators.SchemaCalculators
{
    public class TrussPostsSchemaCalculator : IDataCalculator
    {
        private SchemaInputData _schemaInputData;
        private FrameData _frameData;
        private string[] _distances;
        private List<double> _trussPostDistancesLeft;
        private double _previousCoord;
        private List<ElementData> _trussPostsElementsLeft;
        public void Calculate(FrameData frameData, InputData inputData)
        {
            _schemaInputData = inputData as SchemaInputData;
            _frameData = frameData;
            _distances = ParsePanelsString();
            _previousCoord = 0;

            List<ElementData> trussPostsElements = new List<ElementData>();
            _trussPostsElementsLeft = CalcLeftTrussPosts();
            trussPostsElements.AddRange(_trussPostsElementsLeft);
            trussPostsElements.AddRange(CalcRightThrussPosts());

            _frameData.TrussData.TrussPosts = trussPostsElements;
        }

        private string[] ParsePanelsString()
        {
            string[] p = _schemaInputData.Panels.Split(' ');
            return p;
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
            double startOffsetX = (_schemaInputData.Bay/2 - _previousCoord) * 2;
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
                        Y = _frameData.TrussData.RightBottomChord.StartPoint.Y,
                        Z = 0.0
                    };

                    double allLength = _frameData.TrussData.LeftTopChord.EndPoint.X - _frameData.TrussData.LeftTopChord.StartPoint.X;
                    double allHeigtht = _frameData.TrussData.LeftTopChord.EndPoint.Y - _frameData.TrussData.LeftTopChord.StartPoint.Y;

                    double lengthFromZero = _previousCoord -_frameData.TrussData.LeftTopChord.StartPoint.X;
                    double offsetY = _frameData.TrussData.RightBottomChord.StartPoint.Y + _schemaInputData.HeightRoofBottom;
                    double additionalOffset = _frameData.TrussData.LeftTopChord.StartPoint.Y - _schemaInputData.HeightColumns;

                    offsetY += additionalOffset;

                    Point endPoint = GetTrussPostEndPointOnTheLine(lengthFromZero, allLength, allHeigtht, offsetY, _previousCoord);

                    ElementData elementData = new ElementData()
                    {
                        StartPoint = startPoint,
                        EndPoint = endPoint
                    };
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
                        Y = _frameData.TrussData.RightBottomChord.StartPoint.Y,
                        Z = 0.0
                    };

                    Point endPoint = new Point()
                    {
                        X = startPoint.X,
                        Y = 0,
                        Z = 0
                    };

                    ElementData elementData = new ElementData()
                    {
                        StartPoint = startPoint,
                        EndPoint = endPoint
                    };
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
    }
}
