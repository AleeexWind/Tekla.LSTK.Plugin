using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators.SchemaCalculators
{
    public class BottomChordSchemaCalculator : IDataCalculator
    {
        private SchemaInputData _schemaInputData;
        private readonly ElementGroupType _elementGroupType = ElementGroupType.BottomChord;
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

            ElementData elementData = CreateElementData();
            elementData.StartPoint = startPoint;
            elementData.EndPoint = endPoint;

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

            ElementData elementData = CreateElementData();
            elementData.StartPoint = startPoint;
            elementData.EndPoint = endPoint;

            return elementData;
        }
        private ElementData CreateElementData()
        {
            return new ElementData()
            {
                ElementGroupType = _elementGroupType
            };
        }
    }
}
