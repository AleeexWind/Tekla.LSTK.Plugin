using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.Entities;
using LSTK.Frame.Utils;
using Tekla.Structures.Model;

namespace LSTK.Frame.Frameworks.TeklaAPI
{
    public class TeklaPartCreator : ITeklaAccess
    {
        private readonly Model _model;
        private readonly FrameData _frameData;
        private readonly TeklaPartAttributeSetter _teklaPartAttributeSetter;
        public TeklaPartCreator(Model model, FrameData frameData, TeklaPartAttributeSetter teklaPartAttributeSetter)
        {
            _model = model;
            _frameData = frameData;
            _teklaPartAttributeSetter = teklaPartAttributeSetter;
        }
        public bool CommitChanges()
        {
            return _model.CommitChanges();
        }
        public bool CreateLeftColumn()
        {
            return CreatePart(_frameData.ColumnsData.LeftColumn);
        }

        public bool CreateRightColumn()
        {
            return CreatePart(_frameData.ColumnsData.RightColumn);
        }
        private bool CreatePart(ElementData element)
        {
            Beam beam = new Beam();

            bool res = _teklaPartAttributeSetter.SetCoordinatesToStartColumn(beam, TeklaPointConverter.ConvertPoint(element.StartPoint), TeklaPointConverter.ConvertPoint(element.EndPoint));
            if (!res) return res;
            res = _teklaPartAttributeSetter.SetProfileToStartColumn(beam, element.Profile);
            if (!res) return res;
            res = _teklaPartAttributeSetter.SetMaterialToStartColumn(beam, element.Material);
            if (!res) return res;
            res = _teklaPartAttributeSetter.SetPositionRotationToStartColumn(beam, SetRotationPosition(element));
            if (!res) return res;
            res = _teklaPartAttributeSetter.SetPositionPlaneToStartColumn(beam, SetPlanePosition(element));
            if (!res) return res;
            res = _teklaPartAttributeSetter.SetPositionDepthToStartColumn(beam, SetDepthPosition(element));
            if (!res) return res;

            res = TeklaElementInsertHandler.InsertElement(beam);

            return res;
        }
        private Position.RotationEnum SetRotationPosition(ElementData element)
        {
            Position.RotationEnum rotationEnum;
            if (element.RotationPosition.Equals(Position.RotationEnum.BACK.ToString()))
            {
                rotationEnum = Position.RotationEnum.BACK;
            }
            else if(element.RotationPosition.Equals(Position.RotationEnum.BELOW.ToString()))
            {
                rotationEnum = Position.RotationEnum.BELOW;
            }
            else if(element.RotationPosition.Equals(Position.RotationEnum.FRONT.ToString()))
            {
                rotationEnum = Position.RotationEnum.FRONT;
            }
            else
            {
                rotationEnum = Position.RotationEnum.TOP;
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

            return depthEnum;
        }
    }
}
