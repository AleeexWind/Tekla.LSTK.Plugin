using FrameCreator.BusinessRules.Models;

namespace FrameCreator.BusinessRules.DataBoundaries
{
    public interface IBuildSchemaResponse
    {
        void DrawSchema(BuiltSchemaData builtSchemaData);
    }
}
