using LSTK.Frame.Entities;

namespace LSTK.Frame.BusinessRules.Gateways
{
    public interface ITargetAppAccess
    {
        bool CreatePart(ElementData elementData);
        bool RecieveCurrentWorkPlane();
        bool SetTemporaryLocalPlane(Point startPoint, Point directionPoint);
        bool SetCurrentWorkPlane();
        bool CommitChanges();
    }
}
