using FrameCreator.Entities;

namespace FrameCreator.BusinessRules.Gateways
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
