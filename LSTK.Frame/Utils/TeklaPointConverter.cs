using PointPlugin = LSTK.Frame.Entities;
using PointTekla = Tekla.Structures.Geometry3d;

namespace LSTK.Frame.Utils
{
    public static class TeklaPointConverter
    {
        public static PointTekla.Point ConvertPoint(PointPlugin.Point point)
        {
            PointTekla.Point targetPoint = new PointTekla.Point()
            {
                X = point.X,
                Y = point.Y,
                Z = point.Z
            };
            return targetPoint;
        }
        public static PointPlugin.Point ConvertPoint(PointTekla.Point point)
        {
            PointPlugin.Point targetPoint = new PointPlugin.Point()
            {
                X = point.X,
                Y = point.Y,
                Z = point.Z
            };
            return targetPoint;
        }
    }
}
