using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace LSTK.Frame.Frameworks.TeklaAPI
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
