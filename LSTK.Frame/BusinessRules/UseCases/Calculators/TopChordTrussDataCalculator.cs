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
            ElementData elementData = new ElementData()
            {
                StartPoint = startPoint,
                EndPoint = endPoint,
                Profile = _frameInputData.ProfileTopChord,
                PartName =_frameInputData.PartNameTopChord,
                Material = _frameInputData.MaterialTopChord,
                Class = _frameInputData.ClassTopChord,
                RotationPosition = _frameInputData.RotationPositionTopChord,
                PlanePosition = _frameInputData.PlanePositionTopChord,
                DepthPosition = _frameInputData.DepthPositionTopChords

            };
            return elementData;
        }
    }
}
