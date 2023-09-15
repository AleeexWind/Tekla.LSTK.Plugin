using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public class BottomChordTrussDataCalculator : IDataCalculator
    {
        private FrameInputData _frameInputData;
        public void Calculate(FrameData frameData, InputData inputData)
        {
            _frameInputData = inputData as FrameInputData;
            frameData.TrussData.LeftBottomChord = CalcLeftBottomChord();
            frameData.TrussData.RightBottomChord = CalcRightBottomChord();
        }
        private ElementData CalcLeftBottomChord()
        {
            Point startPoint = new Point()
            {
                X = 0.0,
                Y = _frameInputData.HeightColumns - _frameInputData.HeightRoofBottom,
                Z = 0.0
            };
            Point endPoint = new Point()
            {
                X = _frameInputData.Bay/2,
                Y = _frameInputData.HeightColumns - _frameInputData.HeightRoofBottom,
                Z = 0.0
            };
            ElementData elementData = CalcCommonData(startPoint, endPoint);
            return elementData;
        }
        private ElementData CalcRightBottomChord()
        {
            Point startPoint = new Point()
            {
                X = _frameInputData.Bay/2,
                Y = _frameInputData.HeightColumns - _frameInputData.HeightRoofBottom,
                Z = 0.0
            };
            Point endPoint = new Point()
            {
                X = _frameInputData.Bay,
                Y = _frameInputData.HeightColumns - _frameInputData.HeightRoofBottom,
                Z = 0.0
            };
            ElementData elementData = CalcCommonData(startPoint, endPoint);
            return elementData;
        }
        private ElementData CalcCommonData(Point startPoint, Point endPoint)
        {
            ElementData elementData = new ElementData()
            {
                PartName =_frameInputData.PartNameBottomChord,
                Profile = _frameInputData.ProfileBottomChord,
                Material = _frameInputData.MaterialBottomChord,
                Class = _frameInputData.ClassBottomChord,
                RotationPosition = _frameInputData.RotationPositionBottomChord,
                PlanePosition = _frameInputData.PlanePositionBottomChord,
                DepthPosition = _frameInputData.DepthPositionBottomChord,
                StartPoint = startPoint,
                EndPoint = endPoint,
            };
            return elementData;
        }
    }
}
