using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators.SchemaCalculators
{
    public class TopChordSchemaCalculator : IDataCalculator
    {
        private SchemaInputData _schemaInputData;
        private readonly ElementGroupType _elementGroupType = ElementGroupType.TopChord;
        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            _schemaInputData = inputData as SchemaInputData;
            ElementData leftTopChord = CalcLeftTopChord();
            ElementData rightTopChord = CalcRightTopChord();

            if(leftTopChord != null && rightTopChord != null)
            {
                elementsDatas.Add(leftTopChord);
                elementsDatas.Add(rightTopChord);
                return true;
            }
            else
            {
                //TODO: Logging
                return false;
            }
        }
        private ElementData CalcLeftTopChord()
        {
            ElementSideType elementSideType = ElementSideType.Left;
            ElementData elementData;
            try
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

                elementData = CreateElementData(elementSideType);
                elementData.StartPoint = startPoint;
                elementData.EndPoint = endPoint;
            }
            catch (System.Exception)
            {
                //TODO: Logging
                throw;
            }

            return elementData;
        }
        private ElementData CalcRightTopChord()
        {
            ElementSideType elementSideType = ElementSideType.Right;
            ElementData elementData;
            try
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

                elementData = CreateElementData(elementSideType);
                elementData.StartPoint = startPoint;
                elementData.EndPoint = endPoint;
            }
            catch (System.Exception)
            {
                //TODO: Logging
                throw;
            }

            return elementData;
        }
        private ElementData CreateElementData(ElementSideType elementSideType)
        {
            return new ElementData()
            {
                ElementGroupType = _elementGroupType,
                ElementSideType = elementSideType
            };
        }
    }
}
