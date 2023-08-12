using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Geometry3d;
using System.Collections;

namespace LSTK.Frame.Frameworks.TeklaAPI
{
    public class TeklaPointSelector
    {
        public (Point StartPoint, Point DirectionPoint) SelectPoints()
        {
            Picker picker = new Picker();
            ArrayList startPointArray = picker.PickPoints(Picker.PickPointEnum.PICK_ONE_POINT, "Please pick start point");
            ArrayList endPointArray = picker.PickPoints(Picker.PickPointEnum.PICK_ONE_POINT, "Please pick direction point");
            Point startPoint = startPointArray[0] as Point;
            Point endPoint = endPointArray[0] as Point;

            return (startPoint, endPoint);
        }
    }
}
