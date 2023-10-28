using LSTK.Frame.Adapters.Controllers.Models;
using LSTK.Frame.BusinessRules.DataBoundaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.Adapters.Controllers
{
    public class FrameReceiverController
    {
        private readonly IFrameReceiver _frameReceiver;
        private readonly FrameReceiverRequestModel _frameReceiverRequestModel;

        public FrameReceiverController(IFrameReceiver frameReceiver, FrameReceiverRequestModel frameReceiverRequestModel)
        {
            _frameReceiver = frameReceiver;
            _frameReceiverRequestModel = frameReceiverRequestModel;
            _frameReceiverRequestModel.OnSendingRequest += ReceiveFrame;
        }
        private void ReceiveFrame(object sender, EventArgs e)
        {
            _frameReceiver.ValidateData();
            _frameReceiver.ProvideData();
        }
    }
}
