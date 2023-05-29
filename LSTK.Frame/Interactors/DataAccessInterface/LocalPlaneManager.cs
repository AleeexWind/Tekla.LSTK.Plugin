using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace LSTK.Frame.Interactors
{
    public class LocalPlaneManager
    {
        private Model _model;
        public LocalPlaneManager()
        {
            _model = new Model();
        }
        public TransformationPlane RecieveCurrentWorkPlane()
        {
            WorkPlaneHandler workPlaneHandler = _model.GetWorkPlaneHandler();
            return workPlaneHandler.GetCurrentTransformationPlane();
        }

        public (Vector X, Vector Y) SetVectors(Point startPoint, Point endPoint)
        {
            Vector X = new Vector(startPoint.X - endPoint.X, startPoint.Y - endPoint.Y, startPoint.Z - endPoint.Z);
            Vector Y = new Vector(0, 0, 1);

            return (X , Y);
        }
        public CoordinateSystem SetLocalCoordinateSystem(Point origin, (Vector X, Vector Y) vectors)
        {
            return new CoordinateSystem(origin, vectors.X, vectors.Y);
        }
        public bool SetLocalWorkPlane(CoordinateSystem localCoordinateSystem)
        {
            TransformationPlane localWorkPlane = new TransformationPlane(localCoordinateSystem);

            return _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(localWorkPlane);
        }
        public bool SetCurrentWorkPlane(TransformationPlane currentPlane)
        {
            return _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);
        }
    }
}
