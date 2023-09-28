using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;

namespace LSTK.Frame.Adapters.Presenters
{
    class FirstSchemaPresenter : IFirstSchemaOutputBoundary
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        public FirstSchemaPresenter(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public void TransferSchema(List<ElementData> elementDatas, double coordXmax, double coordYmax, double yOffset)
        {
            _mainWindowViewModel.SchemaPoints.Clear();
            _mainWindowViewModel.FrameWidthForSchema = coordXmax;
            _mainWindowViewModel.FrameHeightForSchema = coordYmax;
            _mainWindowViewModel.YoffsetSchema = yOffset;

            foreach (var elemData in elementDatas)
            {
                var coord = (new Point() { X = elemData.StartPoint.X, Y = elemData.StartPoint.Y, Z = elemData.StartPoint.Z },
                    new Point() { X = elemData.EndPoint.X, Y = elemData.EndPoint.Y, Z = elemData.EndPoint.Z });
                _mainWindowViewModel.SchemaPoints.Add(coord);
            }
            _mainWindowViewModel.OnViewUpdate?.Invoke(this, new EventArgs());
        }
    }
}
