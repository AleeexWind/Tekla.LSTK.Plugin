using Tekla.Structures.Model;

namespace LSTK.Frame.Interactors
{
    public class TeklaElementInsertHandler
    {
        public bool InsertElement(Part part)
        {
            return part.Insert();
        }
    }
}
