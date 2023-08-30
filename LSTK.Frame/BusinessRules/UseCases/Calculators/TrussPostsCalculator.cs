using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.TeklaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public class TrussPostsCalculator : IDataCalculator
    {
        private FrameInputData _frameInputData;
        private FrameData _frameData;
        private readonly List<double> _trussPostDistances = new List<double>();
        public void Calculate(FrameData frameData, FrameInputData frameInputData)
        {
            _frameInputData = frameInputData;
            _frameData = frameData;
            ParsePanelsString();
            _frameData.TrussData.TrussPosts = CalcTrussPosts();
        }
        private void ParsePanelsString()
        {
            string[] p = _frameInputData.Panels.Split(' ');
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
            if(_trussPostDistances.Any())
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
                    Point endPoint = new Point()
                    {
                        X = previousCoord,
                        Y = _frameData.TrussData.RightBottomChord.StartPoint.Y + 1000,
                        Z = 0.0
                    };

                    double profileHeight = TeklaPartAttributeGetter.GetProfileHeight(_frameInputData.ProfileGroup);
                    ElementData elementData = CalcCommonDataForTrussPost(startPoint, endPoint, profileHeight);
                    result.Add(elementData);
                }
            }

            return result;
        }
        private ElementData CalcCommonDataForTrussPost(Point startPoint, Point endPoint, double profileHeight)
        {
            ElementData elementData = new ElementData()
            {
                PartName =_frameInputData.PartNameGroup,
                Profile = _frameInputData.ProfileGroup,
                ProfileHeight = profileHeight,
                Material = _frameInputData.MaterialGroup,
                Class = _frameInputData.ClassGroup,
                RotationPosition = _frameInputData.RotationPositionGroup,
                PlanePosition = _frameInputData.PlanePositionGroup,
                DepthPosition = _frameInputData.DepthPositionGroup,
                StartPoint = startPoint,
                EndPoint = endPoint
            };

            return elementData;
        }
    }
}
