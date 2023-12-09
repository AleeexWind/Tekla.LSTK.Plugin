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
        public void ShowResult(List<ElementData> elementDatas, List<AttributeGroup> attributeGroups)
        {
            string prototypes = string.Empty;
            string elementAttributes = string.Empty;

            _mainWindowViewModel.ToBeBuilt = false;

            if (elementDatas != null && elementDatas.Count > 0)
            {
                _mainWindowViewModel.ToBeBuilt = true;
                prototypes = JsonConvert.SerializeObject(elementDatas);
            }
            if(attributeGroups != null && attributeGroups.Count > 0)
            {
                elementAttributes = JsonConvert.SerializeObject(attributeGroups);
            }

            _mainWindowViewModel.ElementPrototypes = prototypes;
            _mainWindowViewModel.ElementAttributes = elementAttributes;
            _mainWindowViewModel.OnBuildSchema?.Invoke(this, new EventArgs());
        }
    }
}
