using LSTK.Frame.Entities;

namespace LSTK.Frame.BusinessRules.Gateways
{
    public interface ITeklaAccess
    {
        bool CreateLeftColumn(FrameData frameData);
        bool CreateRightColumn(FrameData frameData);
        bool CreateLeftTopChord(FrameData frameData);
        bool CreateRightTopChord(FrameData frameData);
        bool CreateLeftBottomChord(FrameData frameData);
        bool CreateRightBottomChord(FrameData frameData);
        bool CreateTrussPosts(FrameData frameData);
        bool CommitChanges();
    }
}
