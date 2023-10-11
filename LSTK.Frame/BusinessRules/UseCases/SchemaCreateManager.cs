using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.UseCases
{
    public class SchemaCreateManager : ISchemaBuilder
    {
        private readonly IBuildSchemaResponse _firstSchemaOutputBoundary;
        private readonly List<IDataCalculator> _calculators;
        private readonly IDataAccess _dataAccess;
        private FrameData _frameData;

        public SchemaCreateManager(IDataAccess dataAccess, List<IDataCalculator> calculators, IBuildSchemaResponse firstSchemaOutputBoundary)
        {
            _dataAccess = dataAccess;
            _calculators = calculators;
            _firstSchemaOutputBoundary = firstSchemaOutputBoundary;
        }
        public void BuildSchema(SchemaInputData schemaInputData)
        {
            _frameData = new FrameData();
            foreach (IDataCalculator calc in _calculators)
            {
                calc.Calculate(_frameData, schemaInputData);
            }

            _firstSchemaOutputBoundary.TransferSchema(GetAllElementsOfTruss(), schemaInputData.Bay, schemaInputData.HeightRoofBottom + schemaInputData.HeightRoofRidge, GetSchemaYoffset(schemaInputData));
        }
        private List<ElementData> GetAllElementsOfTruss()
        {
            List<ElementData> result = new List<ElementData>
            {
                _frameData.TrussData.LeftTopChord,
                _frameData.TrussData.RightTopChord,
                _frameData.TrussData.LeftBottomChord,
                _frameData.TrussData.RightBottomChord
            };
            result.AddRange(_frameData.TrussData.TrussPosts);

            if(!AddElementsToDB(result))
            {
                //TODO: Logging
            }

            return result;
        }
        private double GetSchemaYoffset(SchemaInputData schemaInputData)
        {
            double columnHeight = schemaInputData.HeightColumns;
            double result = columnHeight - schemaInputData.HeightRoofBottom;

            return result;
        }
        private bool AddElementsToDB(List<ElementData> elements)
        {
            foreach (var element in elements)
            {
                if(!_dataAccess.AddElementData(element))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
