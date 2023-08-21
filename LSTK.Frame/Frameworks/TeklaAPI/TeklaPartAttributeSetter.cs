using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Catalogs;

namespace LSTK.Frame.Frameworks.TeklaAPI
{
    public class TeklaPartAttributeSetter
    {
        public bool SetPartName(Beam beam, string partName)
        {
            try
            {
                beam.Name = partName;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetProfile(Beam beam, string profile)
        {
            try
            {
                LibraryProfileItem libraryProfileItem = new LibraryProfileItem();
                libraryProfileItem.Select(profile);

                foreach(var t in libraryProfileItem.aProfileItemParameters)
                {
                    ProfileItemParameter profileItemParameter = t as ProfileItemParameter;
                }
                

                beam.Profile.ProfileString = profile;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetMaterial(Beam beam, string material)
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
        public bool SetClass(Beam beam, string classNumber)
        {
            try
            {
                beam.Class = classNumber;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetRotationPosition(Beam beam, Position.RotationEnum rotationEnum)
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
        public bool SetPlanePosition(Beam beam, Position.PlaneEnum planeEnum)
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
        public bool SetDepthPosition(Beam beam, Position.DepthEnum depthEnum)
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

        public bool SetPoints(Beam beam, Point startPoint, Point endPoint)
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
    }
}
