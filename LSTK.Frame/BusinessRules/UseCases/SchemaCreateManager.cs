using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using LSTK.Frame.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.UseCases
{
    public class SchemaCreateManager : ISchemaBuilder
    {
        private readonly IBuildSchemaResponse _schemaResponse;
        private readonly List<IDataCalculator> _calculators;
        private readonly IDataAccess _dataAccess;

        private List<ElementData> _elementsDatas;

        public SchemaCreateManager(IDataAccess dataAccess, List<IDataCalculator> calculators, IBuildSchemaResponse firstSchemaOutputBoundary)
        {
            _dataAccess = dataAccess;
            _calculators = calculators;
            _schemaResponse = firstSchemaOutputBoundary;
        }
        public void BuildSchema(SchemaInputData schemaInputData)
        {
            if(!string.IsNullOrEmpty(schemaInputData.ExistedSchema))
            {
                _elementsDatas = JsonConvert.DeserializeObject<List<ElementData>>(schemaInputData.ExistedSchema);
            }
            else
            {
                _elementsDatas = new List<ElementData>();
                foreach (IDataCalculator calc in _calculators)
                {
                    calc.Calculate(_elementsDatas, schemaInputData);
                }
                SetIds(_elementsDatas);             
            }

            if (!AddElementsToDB(_elementsDatas))
            {
                //TODO: Logging
            }

            BuiltSchemaData builtSchemaData = new BuiltSchemaData()
            {
                ElementDatas = _elementsDatas,
                CoordXmax = schemaInputData.Bay,
                Yoffset = GetSchemaYoffset(schemaInputData),
                CoordYmax = schemaInputData.HeightColumns + schemaInputData.HeightRoofRidge,
            };

            _schemaResponse.TransferSchema(builtSchemaData);
        }
        //private List<ElementData> GetAllElementsOfTruss()
        //{
        //    List<ElementData> result = new List<ElementData>
        //    {
        //        _frameData.TrussData.LeftTopChord,
        //        _frameData.TrussData.RightTopChord,
        //        _frameData.TrussData.LeftBottomChord,
        //        _frameData.TrussData.RightBottomChord
        //    };
        //    result.AddRange(_frameData.TrussData.TrussPosts);

        //    if(!AddElementsToDB(result))
        //    {
        //        //TODO: Logging
        //    }

        //    return result;
        //}
        private double GetSchemaYoffset(SchemaInputData schemaInputData)
        {
            double columnHeight = schemaInputData.HeightColumns;
            double result = columnHeight - schemaInputData.HeightRoofBottom;
            result = 0;
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
        private void SetIds(List<ElementData> elementDatas)
        {
            int previousId = 1;
            foreach (ElementData el in elementDatas)
            {
                el.Id = previousId;
                previousId++;
            }
        }

        private void BuildExistedSchema()
        {

        }
    }
}
