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
            List<ElementData> resultElementDatas = new List<ElementData>();

            if (_isValid)
            {
                resultElementDatas = _elementDatas;
            }
            _frameReceiverResponse.ShowResult(resultElementDatas);
        }
        private void GetElementDatas()
        {
            _elementDatas = _dataAccess.GetElementDatas();
        }
        //private void GetAttributes()
        //{
        //    _attributeGroups = _dataAccess.GetAttributeGroups();
        //}
    }
}
