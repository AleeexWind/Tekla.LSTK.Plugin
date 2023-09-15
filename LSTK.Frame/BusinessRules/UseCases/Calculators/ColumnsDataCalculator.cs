using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.TeklaAPI;
using System;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public class ColumnsDataCalculator : IDataCalculator
    {
        private FrameInputData _frameInputData;

        public void Calculate(FrameData frameData, InputData inputData)
        {

            _frameInputData = inputData as FrameInputData;
            ColumnsData columnsData = new ColumnsData()
            {
                LeftColumn = CalcLeftColumn(),
                RightColumn = CalcRightColumn()
            };

            frameData.ColumnsData = columnsData;
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

            //TeklaPartAttributeGetter.GetProfileHeight(elementData, elementData.Profile);

            //(Point, Point) newCoord = (startPoint, endPoint);
            //if (_frameInputData.ColumnLineOption.Equals("Inside"))
            //{
            //    newCoord = GetParallelLineCoordinate(startPoint, endPoint, elementData.ProfileHeight);
            //}
            //startPoint = newCoord.Item1;
            //endPoint = newCoord.Item2;

            //elementData.StartPoint = startPoint;
            //elementData.EndPoint = endPoint;

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
