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
        private List<double> _trussPostDistances;
        public void Calculate(FrameData frameData, InputData inputData)
        {
            _trussPostDistances = new List<double>();
            _schemaInputData = inputData as SchemaInputData;
            _frameData = frameData;
            ParsePanelsString();
            _frameData.TrussData.TrussPosts = CalcTrussPosts();
        }
        private void ParsePanelsString()
        {
            string[] p = _schemaInputData.Panels.Split(' ');
            for (int i = 0; i < p.Length; i++)
            {
                _trussPostDistances.AddRange(GetDistances(p[i]));
            }
        }
        private List<double> GetDistances(string value)
        {
            List<double> distances = new List<double>();
            string[] p = value.Split('*');

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

            for (int i = 0; i < amount; i++)
            {
                distances.Add(dist);
            }
            return distances;
        }
        private List<ElementData> CalcTrussPosts()
        {
            List<ElementData> result = new List<ElementData>();
            if (_trussPostDistances.Any())
            {
                _trussPostDistances.RemoveAt(_trussPostDistances.Count -1);
                double previousCoord = 0;
                foreach (var dist in _trussPostDistances)
                {
                    previousCoord += dist;
                    Point startPoint = new Point()
                    {
                        X = previousCoord,
                        Y = _frameData.TrussData.RightBottomChord.StartPoint.Y,
                        Z = 0.0
                    };

                    double allLength = _frameData.TrussData.LeftTopChord.EndPoint.X - _frameData.TrussData.LeftTopChord.StartPoint.X;
                    double allHeigtht = _frameData.TrussData.LeftTopChord.EndPoint.Y - _frameData.TrussData.LeftTopChord.StartPoint.Y;

                    double lengthFromZero = previousCoord -_frameData.TrussData.LeftTopChord.StartPoint.X;
                    double offsetY = _frameData.TrussData.RightBottomChord.StartPoint.Y + _schemaInputData.HeightRoofBottom;
                    double additionalOffset = _frameData.TrussData.LeftTopChord.StartPoint.Y - _schemaInputData.HeightColumns;

                    offsetY += additionalOffset;

                    Point endPoint = GetTrussPostEndPointOnTheLine(lengthFromZero, allLength, allHeigtht, offsetY, previousCoord);

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
