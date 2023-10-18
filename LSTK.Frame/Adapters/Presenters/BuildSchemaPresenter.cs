using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.Adapters.Presenters
{
    class BuildSchemaPresenter : IBuildSchemaResponse
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        public BuildSchemaPresenter(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public void TransferSchema(List<ElementData> elementDatas, double coordXmax, double coordYmax, double yOffset)
        {
            _mainWindowViewModel.SchemaPoints.Clear();
            _mainWindowViewModel.FrameWidthForSchema = coordXmax;
            _mainWindowViewModel.FrameHeightForSchema = coordYmax;
            _mainWindowViewModel.YoffsetSchema = yOffset;

            foreach (var elemData in elementDatas.Where(x => !x.ElementGroupType.Equals(ElementGroupType.Column)))
            {
                var coord = (new Point() { X = elemData.StartPoint.X, Y = elemData.StartPoint.Y, Z = elemData.StartPoint.Z },
                    new Point() { X = elemData.EndPoint.X, Y = elemData.EndPoint.Y, Z = elemData.EndPoint.Z });
                _mainWindowViewModel.SchemaPoints.Add(coord);
            }

            string prototypes = JsonConvert.SerializeObject(elementDatas);

            _mainWindowViewModel.ElementPrototypes = prototypes;
            _mainWindowViewModel.OnDrawSchema?.Invoke(this, new EventArgs());
        }
    }
}
