using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.DataBoundaries
{
    public interface IBuildSchemaResponse
    {
        void TransferSchema(BuiltSchemaData builtSchemaData);
    }
}
