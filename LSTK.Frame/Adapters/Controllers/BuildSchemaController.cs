using LSTK.Frame.Adapters.Controllers.Models;
using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using Newtonsoft.Json;
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
            //if (IsValid())
            //{
            //    GatherInput();
            //    _schemaBuilder.BuildSchema(_schemaInputData);
            //}
            //else
            //{
            //    //TODO: show error in status bar
            //}

            if(_buildSchemaRequestModel.FirstBuild)
            {
                if (IsValid())
                {
                    _buildSchemaRequestModel.FirstBuild = false;
                    GatherInput();
                    _schemaBuilder.BuildSchema(_schemaInputData);
                }
                else
                {
                    //TODO: show error in status bar
                }
            }



            //TODO: Temporary unavailable
            if (string.IsNullOrEmpty(_buildSchemaRequestModel.ExistedSchema))
            {
                if (IsValid())
                {
                    GatherInput();
                    _schemaBuilder.BuildSchema(_schemaInputData);
                }
                else
                {
                    //TODO: show error in status bar
                }
            }
            else
            {
                List<ElementData> elementDatas = null;
                if (true)
                {
                    elementDatas = JsonConvert.DeserializeObject<List<ElementData>>(_buildSchemaRequestModel.ExistedSchema);
                }



                _schemaBuilder.RebuildSchema(elementDatas);
            }
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
                ExistedSchema = _buildSchemaRequestModel.ExistedSchema
            };
            _schemaInputData = schemaInputData;
        }
        private bool IsValid()
        {
            bool result = true;
            if(string.IsNullOrEmpty(_buildSchemaRequestModel.Bay) || string.IsNullOrEmpty(_buildSchemaRequestModel.HeightRoofRidge) ||
                string.IsNullOrEmpty(_buildSchemaRequestModel.HeightRoofBottom) || string.IsNullOrEmpty(_buildSchemaRequestModel.Panels) ||
                string.IsNullOrEmpty(_buildSchemaRequestModel.HeightColumns))
            {
                result = false;
            }
            return result;
        }
    }
}
