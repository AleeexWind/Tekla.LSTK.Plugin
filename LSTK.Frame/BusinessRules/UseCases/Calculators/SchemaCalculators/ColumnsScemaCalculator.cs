using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.TeklaAPI;
using System;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public class ColumnsScemaCalculator : IDataCalculator
    {
        private FrameInputData _frameInputData;

        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            _frameInputData = inputData as FrameInputData;

            ElementData leftColumn = CalcLeftColumn();
            ElementData rightColumn = CalcRightColumn();

            if (leftColumn != null && rightColumn != null)
            {
                elementsDatas.Add(leftColumn);
                elementsDatas.Add(rightColumn);
                return true;
            }
            else
            {
                //TODO: Logging
                return false;
            }
        }
        private ElementData CalcLeftColumn()
        {
            Point startPoint = new Point()
            {
                X = 0.0,
                Y = 0.0,
                Z = 0.0
            };
            Point endPoint = new Point()
            {
                X = 0.0,
                Y = _frameInputData.HeightColumns,
                Z = 0.0
            };
            double profileHeight = TeklaPartAttributeGetter.GetProfileHeight(_frameInputData.ProfileColumns);
            (Point, Point) newCoord = (startPoint, endPoint);
            if (_frameInputData.ColumnLineOption.Equals("Inside"))
            {
                newCoord = GetParallelLineCoordinate(startPoint, endPoint, profileHeight/2);
            }

            ElementData elementData = CalcCommonDataForColumn(newCoord.Item1, newCoord.Item2, profileHeight);
            return elementData;
        }
        private ElementData CalcRightColumn()
        {
            Point startPoint = new Point()
            {
                X = _frameInputData.Bay,
                Y = 0.0,
                Z = 0.0
            };
            Point endPoint = new Point()
            {
                X = _frameInputData.Bay,
                Y = _frameInputData.HeightColumns,
                Z = 0.0
            };

            double profileHeight = TeklaPartAttributeGetter.GetProfileHeight(_frameInputData.ProfileColumns);
            (Point, Point) newCoord = (startPoint, endPoint);
            if (_frameInputData.ColumnLineOption.Equals("Inside"))
            {
                newCoord = GetParallelLineCoordinate(startPoint, endPoint, -profileHeight/2);
            }

            ElementData elementData = CalcCommonDataForColumn(newCoord.Item1, newCoord.Item2, profileHeight);
            return elementData;
        }
        private ElementData CalcCommonDataForColumn(Point startPoint, Point endPoint, double profileHeight)
        {
            ElementData elementData = new ElementData()
            {
                PartName =_frameInputData.PartNameColumns,
                Profile = _frameInputData.ProfileColumns,
                ProfileHeight = profileHeight,
                Material = _frameInputData.MaterialColumns,
                Class = _frameInputData.ClassColumns,
                RotationPosition = _frameInputData.RotationPositionColumns,
                PlanePosition = _frameInputData.PlanePositionColumns,
                DepthPosition = _frameInputData.DepthPositionColumns,
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
