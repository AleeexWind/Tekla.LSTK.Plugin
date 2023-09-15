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
        private readonly IFirstSchemaInputBoundary _firstSchemaInputBoundary;
        private MainWindowViewModel _mainWindowViewModel;
        private SchemaInputData _schemaInputData;

        public BuildSchemaController(IFirstSchemaInputBoundary firstSchemaInputBoundary, MainWindowViewModel mainWindowViewModel)
        {
            _firstSchemaInputBoundary = firstSchemaInputBoundary;
            _mainWindowViewModel = mainWindowViewModel;
            _mainWindowViewModel.OnBuildSchema += BuildSchema;
        }
        void BuildSchema(object sender, EventArgs e)
        {
            GatherInput();
            _firstSchemaInputBoundary.CreateSchema(_schemaInputData);
        }
        private void GatherInput()
        {
            SchemaInputData schemaInputData = new SchemaInputData()
            {
                Bay = double.Parse(_mainWindowViewModel.Bay, System.Globalization.CultureInfo.InvariantCulture),
                HeightRoofRidge = double.Parse(_mainWindowViewModel.HeightRoofRidge, System.Globalization.CultureInfo.InvariantCulture),
                HeightRoofBottom = double.Parse(_mainWindowViewModel.HeightRoofBottom, System.Globalization.CultureInfo.InvariantCulture),
                Panels = _mainWindowViewModel.Panels,
                HeightColumns = double.Parse(_mainWindowViewModel.HeightColumns, System.Globalization.CultureInfo.InvariantCulture),
            };
            _schemaInputData = schemaInputData;
        }
    }
}
