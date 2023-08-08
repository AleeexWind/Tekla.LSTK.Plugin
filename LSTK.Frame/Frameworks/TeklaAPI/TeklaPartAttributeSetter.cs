using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;

namespace LSTK.Frame.Frameworks.TeklaAPI
{
    public class TeklaPartAttributeSetter
    {
        public bool SetCoordinatesToStartColumn(Beam beam, Point startPoint, Point endPoint)
        {
            try
            {
                beam.StartPoint.X = startPoint.X;
                beam.StartPoint.Y = startPoint.Y;
                beam.StartPoint.Z = startPoint.Z;

                beam.EndPoint.X = endPoint.X;
                beam.EndPoint.Y = endPoint.Y;
                beam.EndPoint.Z = endPoint.Z;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetProfileToStartColumn(Beam beam, string profile)
        {
            try
            {
                beam.Profile.ProfileString = profile;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetMaterialToStartColumn(Beam beam, string material)
        {
            try
            {
                beam.Material.MaterialString = material;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetPositionDepthToStartColumn(Beam beam, Position.DepthEnum depthEnum)
        {
            try
            {
                beam.Position.Depth = depthEnum;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetPositionPlaneToStartColumn(Beam beam, Position.PlaneEnum planeEnum)
        {
            try
            {
                beam.Position.Plane = planeEnum;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetPositionRotationToStartColumn(Beam beam, Position.RotationEnum rotationEnum)
        {
            try
            {
                beam.Position.Rotation = rotationEnum;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
