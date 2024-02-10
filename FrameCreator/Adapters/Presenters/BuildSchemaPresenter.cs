using FrameCreator.BusinessRules.DataBoundaries;
using FrameCreator.BusinessRules.Models;
using FrameCreator.Entities;
using Newtonsoft.Json;
using System;

namespace FrameCreator.Adapters.Presenters
{
    class BuildSchemaPresenter : IBuildSchemaResponse
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        public BuildSchemaPresenter(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }
        public void DrawSchema(BuiltSchemaData builtSchemaData)
        {
            _mainWindowViewModel.SchemaElements.Clear();
            _mainWindowViewModel.FrameWidthForSchema = builtSchemaData.CoordXmax;
            _mainWindowViewModel.FrameHeightForSchema = builtSchemaData.CoordYmax;
            _mainWindowViewModel.YoffsetSchema = builtSchemaData.Yoffset;

            foreach (var elemData in builtSchemaData.ElementDatas)
            {
                SchemaElement schemaElement = new SchemaElement()
                {
                    StartPoint = new Point() { X = elemData.StartPoint.X, Y = elemData.StartPoint.Y, Z = elemData.StartPoint.Z },
                    EndPoint = new Point() { X = elemData.EndPoint.X, Y = elemData.EndPoint.Y, Z = elemData.EndPoint.Z },
                    Id = elemData.Id,
                    ToBeDrawn = !elemData.IsDeleted

                };

                _mainWindowViewModel.SchemaElements.Add(schemaElement);
            }

            string prototypes = JsonConvert.SerializeObject(builtSchemaData.ElementDatas);

            _mainWindowViewModel.TempElementPrototypes = prototypes;
            _mainWindowViewModel.OnDrawSchema?.Invoke(this, new EventArgs());
        }
    }
}
