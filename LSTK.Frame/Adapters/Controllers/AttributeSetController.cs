using LSTK.Frame.Adapters.Controllers.Models;
using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using System;

namespace LSTK.Frame.Adapters.Controllers
{
    public class AttributeSetController
    {
        private readonly IAttributeSetter _attributeSetter;
        private readonly AttributeSetRequestModel _attributeSetRequestModel;
        public AttributeSetController(IAttributeSetter attributeSetter, AttributeSetRequestModel attributeSetRequestModel)
        {
            _attributeSetter = attributeSetter;
            _attributeSetRequestModel = attributeSetRequestModel;
            _attributeSetRequestModel.OnSendingRequest += SetAttributes;
        }
        public void SetAttributes(object sender, EventArgs eventArgs)
        {
            AttributeGroup attributeGroup = null;
            try
            {
                if (_attributeSetRequestModel != null)
                {
                    attributeGroup = new AttributeGroup()
                    {
                        PartName = _attributeSetRequestModel.PartName,
                        Profile = _attributeSetRequestModel.Profile,
                        Material = _attributeSetRequestModel.Material,
                        Class = _attributeSetRequestModel.Class
                    };
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

            _attributeSetter.SetAttributesToElements(_attributeSetRequestModel.ElementIds, attributeGroup);
        }
    }
}