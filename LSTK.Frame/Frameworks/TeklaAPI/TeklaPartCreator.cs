using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.Entities;
using LSTK.Frame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace LSTK.Frame.Frameworks.TeklaAPI
{
    public class TeklaPartCreator : ITeklaAccess
    {
        private readonly FrameData _frameData;
        private readonly TeklaPartAttributeSetter _teklaPartAttributeSetter;
        public TeklaPartCreator(FrameData frameData, TeklaPartAttributeSetter teklaPartAttributeSetter)
        {
            _frameData = frameData;
            _teklaPartAttributeSetter = teklaPartAttributeSetter;
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

            return true;
        }
        private Position.RotationEnum SetRotationPosition(ElementData element)
        {
            Position.RotationEnum rotationEnum;
            if (element.RotationPosition.Equals(Position.RotationEnum.BACK))
            {
                rotationEnum = Position.RotationEnum.BACK;
            }
            else if(element.RotationPosition.Equals(Position.RotationEnum.BELOW))
            {
                rotationEnum = Position.RotationEnum.BELOW;
            }
            else if(element.RotationPosition.Equals(Position.RotationEnum.FRONT))
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
            if (element.PlanePosition.Equals(Position.PlaneEnum.LEFT))
            {
                planeEnum = Position.PlaneEnum.LEFT;
            }
            else if (element.RotationPosition.Equals(Position.PlaneEnum.MIDDLE))
            {
                planeEnum = Position.PlaneEnum.MIDDLE;
            }
            else
            {
                planeEnum = Position.PlaneEnum.RIGHT;
            }

            return planeEnum;
        }
        private Position.DepthEnum SetDepthPosition(ElementData element)
        {
            Position.DepthEnum depthEnum;
            if (element.DepthPosition.Equals(Position.DepthEnum.BEHIND))
            {
                depthEnum = Position.DepthEnum.BEHIND;
            }
            else if (element.DepthPosition.Equals(Position.DepthEnum.FRONT))
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
