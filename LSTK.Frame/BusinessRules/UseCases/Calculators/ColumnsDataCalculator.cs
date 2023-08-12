using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public class ColumnsDataCalculator : IDataCalculator
    {
        private FrameData _frameData;
        private FrameInputData _frameInputData;
        public ColumnsDataCalculator(FrameData frameData)
        {
            _frameData = frameData;
        }
        public void Calculate(FrameInputData frameInputData)
        {
            _frameInputData = frameInputData;
            ColumnsData columnsData = new ColumnsData()
            {
                LeftColumn = CalcLeftColumn(),
                RightColumn = CalcRightColumn()
            };

            _frameData.ColumnsData = columnsData;
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
                StartPoint = startPoint,
                EndPoint = endPoint,
                Profile = _frameInputData.ProfileColumns,
                PartName =_frameInputData.PartNameColumns,
                Material = _frameInputData.MaterialColumns,
                Class = _frameInputData.MaterialColumns,
                RotationPosition = _frameInputData.RotationPositionColumns,
                PlanePosition = _frameInputData.PlanePositionColumns,
                DepthPosition = _frameInputData.DepthPositionColumns

            };
            return elementData;
        }
    }
}
