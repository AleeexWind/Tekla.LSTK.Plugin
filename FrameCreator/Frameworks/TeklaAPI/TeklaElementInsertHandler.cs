using Tekla.Structures.Model;

namespace FrameCreator.Frameworks.TeklaAPI
{
    public static class TeklaElementInsertHandler
    {
        public static bool InsertElement(Part part)
        {
            try
            {
                return part.Insert();
            }
            catch
            {
                return false;
            }

        }
    }
}
