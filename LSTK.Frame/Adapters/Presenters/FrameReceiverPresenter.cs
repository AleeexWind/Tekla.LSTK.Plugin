using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LSTK.Frame.Adapters.Presenters
{
    public class FrameReceiverPresenter : IFrameReceiverResponse
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        public FrameReceiverPresenter(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }
        public void ShowResult(List<ElementData> elementDatas)
        {
            _mainWindowViewModel.ToBeBuilt = false;

            if (elementDatas != null && elementDatas.Count > 0)
            {
                _mainWindowViewModel.ToBeBuilt = true;
            }

            _mainWindowViewModel.OnBuildSchema?.Invoke(this, new EventArgs());
        }
    }
}
