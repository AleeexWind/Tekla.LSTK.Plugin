using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.TeklaAPI;
using System;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public class ColumnsScemaCalculator : IDataCalculator
    {
        private SchemaInputData _schemaInputData;
        private readonly ElementGroupType _elementGroupType = ElementGroupType.Column;

        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            _schemaInputData = inputData as SchemaInputData;

            ElementData leftColumn = CalcLeftColumn();
            ElementData rightColumn = CalcRightColumn();

            if (leftColumn != null && rightColumn != null)
            {
                elementsDatas.Add(leftColumn);
                elementsDatas.Add(rightColumn);
                return true;
            }
            else
            {
                //TODO: Logging
                return false;
            }
        }
        private ElementData CalcLeftColumn()
        {
            ElementSideType elementSideType = ElementSideType.Left;
            ElementData elementData;
            try
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
                    Y = _schemaInputData.HeightColumns,
                    Z = 0.0
                };

                elementData = CreateElementData(elementSideType);
                elementData.StartPoint = startPoint;
                elementData.EndPoint = endPoint;
            }
            catch (Exception ex)
            {
                //TODO: Logging
                throw;
            }

            return elementData;
        }
        private ElementData CalcRightColumn()
        {
            ElementSideType elementSideType = ElementSideType.Left;
            ElementData elementData;
            try
            {
                Point startPoint = new Point()
                {
                    X = _schemaInputData.Bay,
                    Y = 0.0,
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
            catch (Exception)
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
