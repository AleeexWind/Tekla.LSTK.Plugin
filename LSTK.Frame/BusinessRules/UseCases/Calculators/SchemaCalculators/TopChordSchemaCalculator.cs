using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators.SchemaCalculators
{
    public class TopChordSchemaCalculator : IDataCalculator
    {
        private SchemaInputData _schemaInputData;
        public void Calculate(FrameData frameData, InputData inputData)
        {
            _schemaInputData = inputData as SchemaInputData;
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
                Y = _schemaInputData.HeightColumns,
                Z = 0.0
            };
            Point endPoint = new Point()
            {
                X = _schemaInputData.Bay/2,
                Y = _schemaInputData.HeightColumns + _schemaInputData.HeightRoofRidge,
                Z = 0.0
            };


            ElementData elementData = new ElementData()
            {
                StartPoint = startPoint,
                EndPoint = endPoint
            };
            return elementData;
        }
        private ElementData CalcRightTopChord()
        {
            Point startPoint = new Point()
            {
                X = _schemaInputData.Bay/2,
                Y = _schemaInputData.HeightColumns + _schemaInputData.HeightRoofRidge,
                Z = 0.0
            };
            Point endPoint = new Point()
            {
                X = _schemaInputData.Bay,
                Y = _schemaInputData.HeightColumns,
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
