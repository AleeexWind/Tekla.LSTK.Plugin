using Tekla.Structures.Model;

namespace UnitTests.Models
{
    public class StartColumnDataTest
    {
        public Beam Beam { get; set; }
        public StartColumnDataTest()
        {
            Beam = new Beam();
            Beam.Name = "COLUMN";
            Beam.Profile.ProfileString = "ПСУ400х100х20х3,0";
            Beam.Material.MaterialString = "350";
            Beam.Class = "2";
            Beam.StartPoint.X = 0;
            Beam.StartPoint.Y = 0;
            Beam.StartPoint.Z = 0;
            Beam.EndPoint.X = 0;
            Beam.EndPoint.Y = 0;
            Beam.EndPoint.Z = 2000;
        }
    }
}
