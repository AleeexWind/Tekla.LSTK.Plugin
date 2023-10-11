using LSTK.Frame.Adapters.Controllers;
using LSTK.Frame.Adapters.Controllers.Models;
using System;

namespace LSTK.Frame.Adapters.Presenters
{
    public class AttributePresenter : IAttributeGetResponse
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        public AttributePresenter(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public void ShowAttributes(ElementAttributes elementAttributes)
        {
            try
            {
                _mainWindowViewModel.PartNameGroup = elementAttributes.PartName;
                _mainWindowViewModel.ProfileGroup = elementAttributes.Profile;
                _mainWindowViewModel.MaterialGroup = elementAttributes.Material;
                _mainWindowViewModel.ClassGroup = elementAttributes.Class;
                _mainWindowViewModel.OnViewUpdate?.Invoke(this, new EventArgs());
            }
            catch (Exception)
            {
                //TODO: Logging
            }
        }
    }
}
