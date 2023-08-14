using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using System;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public class TopChordTrussDataCalculator : IDataCalculator
    {
        private FrameInputData _frameInputData;
        public void Calculate(FrameData frameData, FrameInputData frameInputData)
        {
            _frameInputData = frameInputData;
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

            ElementData elementData = CalcCommonData(startPoint, endPoint);
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


            ElementData elementData = CalcCommonData(startPoint, endPoint);
            return elementData;
        }
        private ElementData CalcCommonData(Point startPoint, Point endPoint)
        {
            if (_frameInputData.TopChordLineOption.Equals("Below"))
            {
                (Point, Point) newCoord = GetParallelLineCoordinate(startPoint, endPoint, 150);
                startPoint = newCoord.Item1;
                endPoint = newCoord.Item2;
            }

            ElementData elementData = new ElementData()
            {
                PartName =_frameInputData.PartNameTopChord,
                Profile = _frameInputData.ProfileTopChord,
                Material = _frameInputData.MaterialTopChord,
                Class = _frameInputData.ClassTopChord,
                RotationPosition = _frameInputData.RotationPositionTopChord,
                PlanePosition = _frameInputData.PlanePositionTopChord,
                DepthPosition = _frameInputData.DepthPositionTopChord,
                StartPoint = startPoint,
                EndPoint = endPoint,
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