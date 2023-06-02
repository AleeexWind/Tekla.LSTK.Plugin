using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using LSTK.Frame.Models;

namespace LSTK.Frame.Interactors
{
    public class TeklaPartCreator
    {
        private readonly TeklaPartAttributeSetter _teklaPartAttributeSetter;
        private readonly FrameData _frameData;
        public TeklaPartCreator(FrameData frameData, TeklaPartAttributeSetter teklaPartAttributeSetter)
        {
            _teklaPartAttributeSetter = teklaPartAttributeSetter;
            _frameData = frameData;
        }
        public Beam CreateLeftColumn()
        {
            Beam beam = new Beam();
            _teklaPartAttributeSetter.SetCoordinatesToStartColumn(beam, _frameData.StartPointLeftColumn, _frameData.EndPointLeftColumn);
            _teklaPartAttributeSetter.SetProfileToStartColumn(beam, _frameData.ProfileColumns);
            _teklaPartAttributeSetter.SetMaterialToStartColumn(beam, _frameData.MaterialColumns);
            _teklaPartAttributeSetter.SetPositionRotationToStartColumn(beam, _frameData.RotationEnum);
            _teklaPartAttributeSetter.SetPositionPlaneToStartColumn(beam, _frameData.PlaneEnum);
            _teklaPartAttributeSetter.SetPositionDepthToStartColumn(beam, _frameData.DepthEnum);
            return beam;
        }
        public Beam CreateRightColumn()
        {
            Beam beam = new Beam();
            _teklaPartAttributeSetter.SetCoordinatesToStartColumn(beam, _frameData.StartPointRightColumn, _frameData.EndPointRightColumn);
            _teklaPartAttributeSetter.SetProfileToStartColumn(beam, _frameData.ProfileColumns);
            _teklaPartAttributeSetter.SetMaterialToStartColumn(beam, _frameData.MaterialColumns);
            _teklaPartAttributeSetter.SetPositionRotationToStartColumn(beam, _frameData.RotationEnum);
            _teklaPartAttributeSetter.SetPositionPlaneToStartColumn(beam, _frameData.PlaneEnum);
            _teklaPartAttributeSetter.SetPositionDepthToStartColumn(beam, _frameData.DepthEnum);
            return beam;
        }
    }
}
