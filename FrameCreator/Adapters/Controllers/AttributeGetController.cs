using FrameCreator.Adapters.Controllers.Models;
using FrameCreator.BusinessRules.DataBoundaries;
using FrameCreator.Entities;
using System;

namespace FrameCreator.Adapters.Controllers
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
