using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public class ColumnsDataCalculator
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
            ElementData elementData = new ElementData()
            {
                StartPoint = startPoint,
                EndPoint = endPoint
            };
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
            ElementData elementData = new ElementData()
            {
                StartPoint = startPoint,
                EndPoint = endPoint
            };
            return elementData;
        }
    }
}
