using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.UseCases
{
    public class SchemaCreateManager : IFirstSchemaInputBoundary
    {
        private readonly IFirstSchemaOutputBoundary _firstSchemaOutputBoundary;
        private readonly List<IDataCalculator> _calculators;
        private FrameData _frameData;

        public SchemaCreateManager(List<IDataCalculator> calculators, IFirstSchemaOutputBoundary firstSchemaOutputBoundary)
        {
            _calculators = calculators;
            _firstSchemaOutputBoundary = firstSchemaOutputBoundary;
        }
        public void CreateSchema(SchemaInputData schemaInputData)
        {
            _frameData = new FrameData();
            foreach (IDataCalculator calc in _calculators)
            {
                calc.Calculate(_frameData, schemaInputData);
            }
            _firstSchemaOutputBoundary.TransferSchema(GetAllElementsOfTruss(), schemaInputData.Bay/2, schemaInputData.HeightRoofBottom + schemaInputData.HeightRoofRidge);
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
            //result.AddRange(_frameData.TrussData.TrussPosts);

            return result;
        }
    }
}
