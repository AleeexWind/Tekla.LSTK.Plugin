using FrameCreator.Utils;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using PointPlugin = FrameCreator.Entities;

namespace FrameCreator.Frameworks.TeklaAPI
{
    public class LocalPlaneManager
    {
        private readonly Model _model;
        private TransformationPlane _currentPlane;
        public LocalPlaneManager(Model model)
        {
            _model = model;
        }
        public void RecieveCurrentWorkPlane()
        {
            WorkPlaneHandler workPlaneHandler = _model.GetWorkPlaneHandler();
            _currentPlane = workPlaneHandler.GetCurrentTransformationPlane();
        }

        public bool SetTemporaryLocalPlane(PointPlugin.Point startPoint, PointPlugin.Point directionPoint)
        {
            (Vector, Vector) vectors = SetVectors(TeklaPointConverter.ConvertPoint(startPoint), TeklaPointConverter.ConvertPoint(directionPoint));
            CoordinateSystem localCoordinateSystem = SetLocalCoordinateSystem(TeklaPointConverter.ConvertPoint(startPoint), vectors);
            return SetLocalWorkPlane(localCoordinateSystem);
        }
        public bool SetCurrentWorkPlane()
        {
            return _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(_currentPlane);
        }

        private (Vector X, Vector Y) SetVectors(Point startPoint, Point endPoint)
        {
            Vector X = new Vector(-startPoint.X + endPoint.X, -startPoint.Y + endPoint.Y, -startPoint.Z + endPoint.Z);
            Vector Y = new Vector(0, 0, 1);

            return (X, Y);
        }
        private CoordinateSystem SetLocalCoordinateSystem(Point origin, (Vector X, Vector Y) vectors)
        {
            return new CoordinateSystem(origin, vectors.X, vectors.Y);
        }
        private bool SetLocalWorkPlane(CoordinateSystem localCoordinateSystem)
        {
            TransformationPlane localWorkPlane = new TransformationPlane(localCoordinateSystem);

            return _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(localWorkPlane);
        }
    }
}
