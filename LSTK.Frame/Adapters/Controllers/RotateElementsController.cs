using LSTK.Frame.Adapters.Controllers.Models;
using LSTK.Frame.BusinessRules.DataBoundaries;
using System;
namespace LSTK.Frame.Adapters.Controllers
{
    public class RotateElementsController
    {
        private readonly IRotateElements _rotateElements;
        private readonly RotateRequestModel _rotateRequestModel;
        public RotateElementsController(IRotateElements rotateElements, RotateRequestModel rotateRequestModel)
        {
            _rotateElements = rotateElements;
            _rotateRequestModel = rotateRequestModel;
            _rotateRequestModel.OnSendingRequest += RotateElements;
        }
        public void RotateElements(object sender, EventArgs eventArgs)
        {
            try
            {
                _rotateElements.RotateElements(_rotateRequestModel.ElementIds);
            }
            catch (Exception)
            {
                //TODO: Logging
            }
        }
    }
}
