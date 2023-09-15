using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators.SchemaCalculators
{
    public class BottomChordSchemaCalculator : IDataCalculator
    {
        private SchemaInputData _schemaInputData;
        public void Calculate(FrameData frameData, InputData inputData)
        {
            _schemaInputData = inputData as SchemaInputData;

            frameData.TrussData.LeftBottomChord = CalcLeftBottomChord();
            frameData.TrussData.RightBottomChord = CalcRightBottomChord();
        }
        private ElementData CalcLeftBottomChord()
        {
            Point startPoint = new Point()
            {
                X = 0.0,
                Y = _schemaInputData.HeightColumns - _schemaInputData.HeightRoofBottom,
                Z = 0.0
            };
            Point endPoint = new Point()
            {
                X = _schemaInputData.Bay/2,
                Y = _schemaInputData.HeightColumns - _schemaInputData.HeightRoofBottom,
                Z = 0.0
            };
            ElementData elementData = new ElementData()
            {
                StartPoint = startPoint,
                EndPoint = endPoint
            };
            return elementData;
        }
        private ElementData CalcRightBottomChord()
        {
            Point startPoint = new Point()
            {
                X = _schemaInputData.Bay/2,
                Y = _schemaInputData.HeightColumns - _schemaInputData.HeightRoofBottom,
                Z = 0.0
            };
            Point endPoint = new Point()
            {
                X = _schemaInputData.Bay,
                Y = _schemaInputData.HeightColumns - _schemaInputData.HeightRoofBottom,
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
