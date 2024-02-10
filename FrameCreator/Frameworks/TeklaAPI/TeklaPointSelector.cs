using System.Collections;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.UI;

namespace FrameCreator.Frameworks.TeklaAPI
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
