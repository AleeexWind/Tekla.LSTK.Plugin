using Tekla.Structures.Model;

namespace UnitTests.Models
{
    public class StartColumnDataTest
    {
        public Beam Beam { get; set; }
        public StartColumnDataTest(FrameDataTest frameDataTest)
        {
            Beam = new Beam();
            Beam.Name = frameDataTest.PartNameColumns;
            Beam.Profile.ProfileString = frameDataTest.ProfileColumns;
            Beam.Material.MaterialString = frameDataTest.MaterilColumns;
            Beam.Class = frameDataTest.ClassColumns;
            Beam.StartPoint = frameDataTest.StartPointLeftColumn;
            Beam.EndPoint = frameDataTest.EndPointLeftColumn;
        }
    }
}
