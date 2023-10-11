using LSTK.Frame.Adapters.Controllers.Models;
using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.Entities;
using System;

namespace LSTK.Frame.Adapters.Controllers
{
    public class AttributeGetController
    {
        private readonly IAttributeGetter _attributeGetter;
        private readonly IAttributeGetResponse _attributePresenter;
        private readonly AttributeGetRequestModel _attributeGetRequestModel;
        public AttributeGetController(IAttributeGetter attributeGetter, IAttributeGetResponse attributePresenter, AttributeGetRequestModel attributeGetRequestModel)
        {
            _attributeGetter = attributeGetter;
            _attributePresenter = attributePresenter;
            _attributeGetRequestModel = attributeGetRequestModel;
            _attributeGetRequestModel.OnSendingRequest += ShowAttributes;
        }
        public void ShowAttributes(object sender, EventArgs eventArgs)
        {
            try
            {
                AttributeGroup attributeGroup = _attributeGetter.GetAttributes(_attributeGetRequestModel.ElementIds);
                if (attributeGroup != null)
                {
                    ElementAttributes elementAttributes = new ElementAttributes()
                    {
                        PartName = attributeGroup.PartName,
                        Profile = attributeGroup.Profile,
                        Material = attributeGroup.Material,
                        Class = attributeGroup.Class
                    };
                    _attributePresenter.ShowAttributes(elementAttributes);
                }
                else
                {
                    //TODO: Logging
                }
            }
            catch (System.Exception)
            {
                //TODO: Logging
            }
        }
    }
}
