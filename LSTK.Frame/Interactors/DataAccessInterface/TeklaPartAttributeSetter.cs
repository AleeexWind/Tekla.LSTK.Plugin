using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;

namespace LSTK.Frame.Interactors
{
    public class TeklaPartAttributeSetter
    {
        public void SetCoordinatesToStartColumn(Beam beam, Point startPoint, Point endPoint)
        {
            beam.StartPoint.X = startPoint.X;
            beam.StartPoint.Y = startPoint.Y;
            beam.StartPoint.Z = startPoint.Z;

            beam.EndPoint.X = endPoint.X;
            beam.EndPoint.Y = endPoint.Y;
            beam.EndPoint.Z = endPoint.Z;
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
