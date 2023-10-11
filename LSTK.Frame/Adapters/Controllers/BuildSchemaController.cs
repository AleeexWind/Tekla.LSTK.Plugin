using LSTK.Frame.Adapters.Controllers.Models;
using LSTK.Frame.BusinessRules.DataBoundaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.Adapters.Controllers
{
    public class BuildSchemaController
    {
        private readonly ISchemaBuilder _schemaBuilder;
        private readonly BuildSchemaRequestModel _buildSchemaRequestModel;
        private SchemaInputData _schemaInputData;

        public BuildSchemaController(ISchemaBuilder schemaBuilder, BuildSchemaRequestModel buildSchemaRequestModel)
        {
            _schemaBuilder = schemaBuilder;
            _buildSchemaRequestModel = buildSchemaRequestModel;
            _buildSchemaRequestModel.OnSendingRequest += BuildSchema;
        }
 
        void BuildSchema(object sender, EventArgs e)
        {
            GatherInput();
            _schemaBuilder.BuildSchema(_schemaInputData);
        }

        private void GatherInput()
        {
            SchemaInputData schemaInputData = new SchemaInputData()
            {
                Bay = double.Parse(_buildSchemaRequestModel.Bay, System.Globalization.CultureInfo.InvariantCulture),
                HeightRoofRidge = double.Parse(_buildSchemaRequestModel.HeightRoofRidge, System.Globalization.CultureInfo.InvariantCulture),
                HeightRoofBottom = double.Parse(_buildSchemaRequestModel.HeightRoofBottom, System.Globalization.CultureInfo.InvariantCulture),
                Panels = _buildSchemaRequestModel.Panels,
                HeightColumns = double.Parse(_buildSchemaRequestModel.HeightColumns, System.Globalization.CultureInfo.InvariantCulture),
            };
            _schemaInputData = schemaInputData;
        }
    }
}
