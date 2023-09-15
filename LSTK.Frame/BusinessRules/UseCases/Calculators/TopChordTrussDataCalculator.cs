using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.TeklaAPI;
using System;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public class TopChordTrussDataCalculator : IDataCalculator
    {
        private FrameInputData _frameInputData;
        private FrameData _frameData;
        public void Calculate(FrameData frameData, InputData inputData)
        {
            _frameInputData = inputData as FrameInputData;
            _frameData = frameData;
            TrussData trussData = new TrussData()
            {
                LeftTopChord = CalcLeftTopChord(),
                RightTopChord = CalcRightTopChord()
            };
            frameData.TrussData = trussData;
        }
        private ElementData CalcLeftTopChord()
        {
            Point startPoint = new Point()
            {
                X = 0.0,
                Y = _frameInputData.HeightColumns,
                Z = 0.0
            };
            Point endPoint = new Point()
            {
                X = _frameInputData.Bay/2,
                Y = _frameInputData.HeightColumns + _frameInputData.HeightRoofRidge,
                Z = 0.0
            };

            double profileHeight = TeklaPartAttributeGetter.GetProfileHeight(_frameInputData.ProfileTopChord);

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
            else if(_frameInputData.TopChordLineOption.Equals("Center") && _frameInputData.ColumnLineOption.Equals("Inside"))
            {
                //startPoint.X = _frameData.ColumnsData.LeftColumn.ProfileHeight/2;
                //newCoord = GetParallelLineCoordinate(startPoint, endPoint, profileHeight/2);
                newCoord.Item1.X = _frameData.ColumnsData.LeftColumn.ProfileHeight/2;
            }


            ElementData elementData = CalcCommonData(newCoord.Item1, newCoord.Item2, profileHeight);
            return elementData;
        }
        private ElementData CalcRightTopChord()
        {
            Point startPoint = new Point()
            {
                X = _frameInputData.Bay/2,
                Y = _frameInputData.HeightColumns + _frameInputData.HeightRoofRidge,
                Z = 0.0
            };
            Point endPoint = new Point()
            {
                X = _frameInputData.Bay,
                Y = _frameInputData.HeightColumns,
                Z = 0.0
            };
            double profileHeight = TeklaPartAttributeGetter.GetProfileHeight(_frameInputData.ProfileTopChord);

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

            ElementData elementData = CalcCommonData(newCoord.Item1, newCoord.Item2, profileHeight);
            return elementData;
        }
        private ElementData CalcCommonData(Point startPoint, Point endPoint, double profileHeight)
        {
            ElementData elementData = new ElementData()
            {
                PartName =_frameInputData.PartNameTopChord,
                Profile = _frameInputData.ProfileTopChord,
                ProfileHeight = profileHeight,
                Material = _frameInputData.MaterialTopChord,
                Class = _frameInputData.ClassTopChord,
                RotationPosition = _frameInputData.RotationPositionTopChord,
                PlanePosition = _frameInputData.PlanePositionTopChord,
                DepthPosition = _frameInputData.DepthPositionTopChord,
                StartPoint = startPoint,
                EndPoint = endPoint
            };

            //TeklaPartAttributeGetter.GetProfileHeight(elementData, elementData.Profile);
            //(Point, Point) newCoord = (startPoint, endPoint);
            //if (_frameInputData.TopChordLineOption.Equals("Below") && _frameInputData.ColumnLineOption.Equals("Inside"))
            //{
            //    newCoord = GetParallelLineCoordinate(startPoint, endPoint, elementData.ProfileHeight/2);
            //}
            //else if (_frameInputData.TopChordLineOption.Equals("Below") && _frameInputData.ColumnLineOption.Equals("Center"))
            //{
            //    startPoint.X = -(_frameData.ColumnsData.LeftColumn.ProfileHeight)/2;
            //    newCoord = GetParallelLineCoordinate(startPoint, endPoint, elementData.ProfileHeight/2);

            //}
            //else
            //{
            //    newCoord = GetParallelLineCoordinate(startPoint, endPoint, -elementData.ProfileHeight/2);
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