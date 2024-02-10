using FrameCreator.Adapters.Controllers.Models;
using FrameCreator.BusinessRules.DataBoundaries;
using System;

namespace FrameCreator.Adapters.Controllers
{
    public class DeleteElementsController
    {
        private readonly IDeleteElements _deleteElements;
        private readonly DeleteRequestModel _deleteRequestModel;
        public DeleteElementsController(IDeleteElements deleteElements, DeleteRequestModel deleteRequestModel)
        {
            _deleteElements = deleteElements;
            _deleteRequestModel = deleteRequestModel;
            _deleteRequestModel.OnSendingRequest += DeleteElements;
        }

        public void DeleteElements(object sender, EventArgs eventArgs)
        {
            try
            {
                _deleteElements.DeleteElements(_deleteRequestModel.ElementIds);
            }
            catch (Exception)
            {
                //TODO: Logging
            }
        }
    }
}
