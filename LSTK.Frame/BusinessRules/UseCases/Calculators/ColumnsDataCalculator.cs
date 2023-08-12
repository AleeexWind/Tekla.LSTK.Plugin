using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public class ColumnsDataCalculator : IDataCalculator
    {
        private FrameInputData _frameInputData;

        public void Calculate(FrameData frameData, FrameInputData frameInputData)
        {
            _frameInputData = frameInputData;
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
            ElementData elementData = CalcCommonDataForColumn(startPoint, endPoint);
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
            ElementData elementData = CalcCommonDataForColumn(startPoint, endPoint);
            return elementData;
        }
        private ElementData CalcCommonDataForColumn(Point startPoint, Point endPoint)
        {
            ElementData elementData = new ElementData()
            {
                PartName =_frameInputData.PartNameColumns,
                Profile = _frameInputData.ProfileColumns,
                Material = _frameInputData.MaterialColumns,
                Class = _frameInputData.ClassColumns,
                RotationPosition = _frameInputData.RotationPositionColumns,
                PlanePosition = _frameInputData.PlanePositionColumns,
                DepthPosition = _frameInputData.DepthPositionColumns,
                StartPoint = startPoint,
                EndPoint = endPoint,
            };
            return elementData;
        }
    }
}
