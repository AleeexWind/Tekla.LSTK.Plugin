using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;

namespace LSTK.Frame.Interactors
{
    public class TeklaPartAttributeSetter
    {
        public void SetCoordinatesToStartColumn(Beam beam, Point startPoint, double height)
        {
            beam.StartPoint.X = startPoint.X;
            beam.StartPoint.Y = startPoint.Y;
            beam.StartPoint.Z = startPoint.Z;

            beam.EndPoint.X = startPoint.X;
            beam.EndPoint.Y = startPoint.Y;
            beam.EndPoint.Z = height;
        }
        public void SetProfileToStartColumn(Beam beam, string profile)
        {
            beam.Profile.ProfileString = profile;
        }
        public void SetMaterialToStartColumn(Beam beam, string material)
        {
            beam.Material.MaterialString = material;
        }
        public void SetPositionDepthToStartColumn(Beam beam, Position.DepthEnum depthEnum)
        {
            beam.Position.Depth = depthEnum;
        }
        public void SetPositionPlaneToStartColumn(Beam beam, Position.PlaneEnum planeEnum)
        {
            beam.Position.Plane = planeEnum;
        }
        public void SetPositionRotationToStartColumn(Beam beam, Position.RotationEnum rotationEnum)
        {
            beam.Position.Rotation = rotationEnum;
        }
    }
}
