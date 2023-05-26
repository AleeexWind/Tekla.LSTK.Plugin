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
        private FrameData _frameData;
        public TeklaPartCreator(FrameData frameData)
        {
            _teklaPartAttributeSetter = new TeklaPartAttributeSetter();
            _frameData = frameData;
        }
        public Beam CreateStartColumn()
        {
            Beam beam = new Beam();
            _teklaPartAttributeSetter.SetCoordinatesToStartColumn(beam, _frameData.StartPoint, _frameData.ColumnHeight);
            _teklaPartAttributeSetter.SetProfileToStartColumn(beam, _frameData.ColumnProfile);
            _teklaPartAttributeSetter.SetMaterialToStartColumn(beam, _frameData.ColumnMaterial);
            _teklaPartAttributeSetter.SetPositionRotationToStartColumn(beam, _frameData.RotationEnum);
            _teklaPartAttributeSetter.SetPositionPlaneToStartColumn(beam, _frameData.PlaneEnum);
            _teklaPartAttributeSetter.SetPositionDepthToStartColumn(beam, _frameData.DepthEnum);
            return beam;
        }
    }
}
