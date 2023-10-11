using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.TeklaAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators.FrameCalculators
{
    public class TopChordTrussDataCalculator : IDataCalculator
    {
        private FrameInputData _frameInputData;
        private FrameData _frameData;
        private List<ElementDataPrototype> _topChordTrusstDataPrototypes;
        private readonly ElementGroupType _elementGroupType = ElementGroupType.TopChord;
        public void Calculate(FrameData frameData, InputData inputData)
        {
            _frameInputData = inputData as FrameInputData;
            _frameData = frameData;

            _topChordTrusstDataPrototypes = _frameInputData.ElementDataPrototypes.Where(x => x.ElementGroupType.Equals(_elementGroupType)).ToList();

            TrussData trussData = _topChordTrusstDataPrototypes != null
                ? new TrussData()
                {
                    LeftTopChord = new ElementData(),
                    RightTopChord = new ElementData()
                }
                : new TrussData()
                {
                    LeftTopChord = CalcLeftTopChord(),
                    RightTopChord = CalcRightTopChord()
                };
            frameData.TrussData = trussData;
        }
        private ElementData CalcLeftTopChord()
        {
            ElementDataPrototype topChordPrototype = _topChordTrusstDataPrototypes.FirstOrDefault(x => x.ElementSideType.Equals(ElementSideType.Left));
            if (topChordPrototype == null)
            {
                return new ElementData();
            }
            Point startPoint = topChordPrototype.StartPoint;
            Point endPoint = topChordPrototype.EndPoint;

            double profileHeight = TeklaPartAttributeGetter.GetProfileHeight(_frameInputData.ProfileTopChord);
            AttributeGroup attributeGroup = _frameInputData.AttributeGroups.FirstOrDefault(x => x.Id.Equals(topChordPrototype.AttributeGroupId));

            (Point, Point) newCoord = (startPoint, endPoint);
            if (_frameInputData.TopChordLineOption.Equals("Below") && _frameInputData.ColumnLineOption.Equals("Inside"))
            {
                newCoord = GetParallelLineCoordinate(startPoint, endPoint, profileHeight/2);
            }
            else if (_frameInputData.TopChordLineOption.Equals("Below") && _frameInputData.ColumnLineOption.Equals("Center"))
            {
                startPoint.X = -(_frameData.ColumnsData.LeftColumn.ProfileHeight)/2;
                newCoord = GetParallelLineCoordinate(startPoint, endPoint, profileHeight/2);
            }
            else if (_frameInputData.TopChordLineOption.Equals("Center") && _frameInputData.ColumnLineOption.Equals("Inside"))
            {
                //startPoint.X = _frameData.ColumnsData.LeftColumn.ProfileHeight/2;
                //newCoord = GetParallelLineCoordinate(startPoint, endPoint, profileHeight/2);
                newCoord.Item1.X = _frameData.ColumnsData.LeftColumn.ProfileHeight/2;
            }


            ElementData elementData = CalcCommonData(newCoord.Item1, newCoord.Item2, profileHeight, attributeGroup);
            return elementData;
        }
        private ElementData CalcRightTopChord()
        {
            ElementDataPrototype topChordPrototype = _topChordTrusstDataPrototypes.FirstOrDefault(x => x.ElementSideType.Equals(ElementSideType.Right));
            if (topChordPrototype == null)
            {
                return new ElementData();
            }
            Point startPoint = topChordPrototype.StartPoint;
            Point endPoint = topChordPrototype.EndPoint;

            double profileHeight = TeklaPartAttributeGetter.GetProfileHeight(_frameInputData.ProfileTopChord);
            AttributeGroup attributeGroup = _frameInputData.AttributeGroups.FirstOrDefault(x => x.Id.Equals(topChordPrototype.AttributeGroupId));

            (Point, Point) newCoord = (startPoint, endPoint);
            if (_frameInputData.TopChordLineOption.Equals("Below") && _frameInputData.ColumnLineOption.Equals("Inside"))
            {
                newCoord = GetParallelLineCoordinate(startPoint, endPoint, profileHeight/2);
            }
            else if (_frameInputData.TopChordLineOption.Equals("Below") && _frameInputData.ColumnLineOption.Equals("Center"))
            {
                endPoint.X = endPoint.X + _frameData.ColumnsData.LeftColumn.ProfileHeight/2;
                newCoord = GetParallelLineCoordinate(startPoint, endPoint, profileHeight/2);
            }
            else if (_frameInputData.TopChordLineOption.Equals("Center") && _frameInputData.ColumnLineOption.Equals("Inside"))
            {
                //endPoint.X = endPoint.X - _frameData.ColumnsData.LeftColumn.ProfileHeight/2;
                //newCoord = GetParallelLineCoordinate(startPoint, endPoint, profileHeight/2);
                newCoord.Item2.X = endPoint.X - _frameData.ColumnsData.LeftColumn.ProfileHeight/2;
            }

            ElementData elementData = CalcCommonData(newCoord.Item1, newCoord.Item2, profileHeight, attributeGroup);
            return elementData;
        }
        private ElementData CalcCommonData(Point startPoint, Point endPoint, double profileHeight, AttributeGroup attributeGroup)
        {
            ElementData elementData = new ElementData()
            {
                PartName = attributeGroup.PartName,
                Profile = attributeGroup.Profile,
                ProfileHeight = profileHeight,
                Material = attributeGroup.Material,
                Class = attributeGroup.Class,
                RotationPosition = attributeGroup.RotationPosition,
                PlanePosition = attributeGroup.PlanePosition,
                DepthPosition = attributeGroup.DepthPosition,
                StartPoint = startPoint,
                EndPoint = endPoint
            };

            return elementData;
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
