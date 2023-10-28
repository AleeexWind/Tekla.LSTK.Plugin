using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.UseCases
{
    public class FrameReceiver : IFrameReceiver
    {
        private readonly IFrameReceiverResponse _frameReceiverResponse;
        private readonly IDataAccess _dataAccess;
        private bool _isValid;
        private List<ElementData> _elementDatas;

        public FrameReceiver(IFrameReceiverResponse frameReceiverResponse, IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _frameReceiverResponse = frameReceiverResponse;
        }
        public void ValidateData()
        {
            GetElementDatas();

            //TODO: implement validation
            _isValid = true;
        }
        public void ProvideData()
        {
            List<ElementData> result = new List<ElementData>();
            if (_isValid)
            {
                result = _elementDatas;           
            }
            _frameReceiverResponse.ShowResult(result);
        }
        private void GetElementDatas()
        {
            _elementDatas = _dataAccess.GetElementDatas();
        }
    }
}
