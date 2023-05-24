using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSTK.Frame.Models;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.UI;

namespace LSTK.Frame.Interactors
{
    public class TeklaPointSelector
    {
        public void SelectPoints(FrameData frameData)
        {
            Picker picker = new Picker();
            ArrayList startPointArray = picker.PickPoints(Picker.PickPointEnum.PICK_ONE_POINT, "Please pick start point");
            ArrayList endPointArray = picker.PickPoints(Picker.PickPointEnum.PICK_ONE_POINT, "Please pick direction point");
            Point startPoint = startPointArray[0] as Point;
            Point endPoint = endPointArray[0] as Point;
            frameData.StartPoint = startPoint;
            frameData.EndPoint = endPoint;
        }
    }
}
