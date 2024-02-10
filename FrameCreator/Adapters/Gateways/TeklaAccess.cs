using FrameCreator.BusinessRules.Gateways;
using FrameCreator.Entities;
using FrameCreator.Frameworks.TeklaAPI;
using FrameCreator.Utils;
using Tekla.Structures.Model;

namespace FrameCreator.Adapters.Gateways
{
    public class TeklaAccess : ITargetAppAccess
    {
        private readonly Model _model;
        private readonly TeklaPartAttributeSetter _teklaPartAttributeSetter;
        private readonly LocalPlaneManager _localPlaneManager;
        private readonly bool _doubleProfile;

        public TeklaAccess(Model model, LocalPlaneManager localPlaneManager, TeklaPartAttributeSetter teklaPartAttributeSetter, bool doubleProfile)
        {
            _model = model;
            _localPlaneManager = localPlaneManager;
            _teklaPartAttributeSetter = teklaPartAttributeSetter;
            _doubleProfile = doubleProfile;
        }

        public bool CommitChanges()
        {
            return _model.CommitChanges();
        }

        public bool CreatePart(ElementData elementData)
        {
            Beam beam = new Beam();

            bool res = _teklaPartAttributeSetter.SetPartName(beam, elementData.PartName);
            if (!res) return res;
            res = _teklaPartAttributeSetter.SetProfile(beam, elementData.Profile);
            if (!res) return res;
            res = _teklaPartAttributeSetter.SetMaterial(beam, elementData.Material);
            if (!res) return res;
            res = _teklaPartAttributeSetter.SetClass(beam, elementData.Class);
            if (!res) return res;
            res = _teklaPartAttributeSetter.SetRotationPosition(beam, SetRotationPosition(elementData));
            if (!res) return res;
            res = _teklaPartAttributeSetter.SetPlanePosition(beam, SetPlanePosition(elementData));
            if (!res) return res;
            res = _teklaPartAttributeSetter.SetDepthPosition(beam, SetDepthPosition(elementData));
            if (!res) return res;

            res = _teklaPartAttributeSetter.SetPoints(beam, TeklaPointConverter.ConvertPoint(elementData.StartPoint), TeklaPointConverter.ConvertPoint(elementData.EndPoint));
            if (!res) return res;

            res = TeklaElementInsertHandler.InsertElement(beam);

            return res;
        }

        public bool RecieveCurrentWorkPlane()
        {
            try
            {
                _localPlaneManager.RecieveCurrentWorkPlane();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }           
        }

        public bool SetCurrentWorkPlane()
        {
            return _localPlaneManager.SetCurrentWorkPlane();
        }

        public bool SetTemporaryLocalPlane(Point startPoint, Point directionPoint)
        {
            return _localPlaneManager.SetTemporaryLocalPlane(startPoint, directionPoint);
        }

        private Position.RotationEnum SetRotationPosition(ElementData element)
        {
            Position.RotationEnum rotationEnum;

            if(_doubleProfile)
            {
                if (element.IsMirrored)
                {
                    rotationEnum = Position.RotationEnum.FRONT;

                }
                else
                {
                    rotationEnum = Position.RotationEnum.BACK;
                }
            }
            else
            {
                if (element.RotationPosition.Equals(Position.RotationEnum.BACK.ToString()))
                {
                    rotationEnum = Position.RotationEnum.BACK;
                }
                else if (element.RotationPosition.Equals(Position.RotationEnum.BELOW.ToString()))
                {
                    rotationEnum = Position.RotationEnum.BELOW;
                }
                else if (element.RotationPosition.Equals(Position.RotationEnum.FRONT.ToString()))
                {
                    rotationEnum = Position.RotationEnum.FRONT;
                }
                else
                {
                    rotationEnum = Position.RotationEnum.TOP;
                }
            }

            return rotationEnum;
        }
        private Position.PlaneEnum SetPlanePosition(ElementData element)
        {
            Position.PlaneEnum planeEnum;
            if (element.PlanePosition.Equals(Position.PlaneEnum.LEFT.ToString()))
            {
                planeEnum = Position.PlaneEnum.LEFT;
            }
            else if (element.RotationPosition.Equals(Position.PlaneEnum.RIGHT.ToString()))
            {
                planeEnum = Position.PlaneEnum.RIGHT;
            }
            else
            {
                planeEnum = Position.PlaneEnum.MIDDLE;
            }

            return planeEnum;
        }
        private Position.DepthEnum SetDepthPosition(ElementData element)
        {
            Position.DepthEnum depthEnum;

            if(_doubleProfile)
            {
                if(element.IsMirrored)
                {
                    depthEnum = Position.DepthEnum.FRONT;
                }
                else
                {
                    depthEnum = Position.DepthEnum.BEHIND;
                }
            }
            else
            {
                if (element.DepthPosition.Equals(Position.DepthEnum.BEHIND.ToString()))
                {
                    depthEnum = Position.DepthEnum.BEHIND;
                }
                else if (element.DepthPosition.Equals(Position.DepthEnum.FRONT.ToString()))
                {
                    depthEnum = Position.DepthEnum.FRONT;
                }
                else
                {
                    depthEnum = Position.DepthEnum.MIDDLE;
                }
            }

            return depthEnum;
        }
    }
}
