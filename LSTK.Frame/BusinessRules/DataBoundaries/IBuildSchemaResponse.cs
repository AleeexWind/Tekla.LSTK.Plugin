using LSTK.Frame.BusinessRules.Models;

namespace LSTK.Frame.BusinessRules.DataBoundaries
{
    public interface IBuildSchemaResponse
    {
        void DrawSchema(BuiltSchemaData builtSchemaData);
    }
}
