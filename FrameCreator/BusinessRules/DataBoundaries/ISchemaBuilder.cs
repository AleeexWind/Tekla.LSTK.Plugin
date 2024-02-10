using FrameCreator.Entities;
using System.Collections.Generic;

namespace FrameCreator.BusinessRules.DataBoundaries
{
    public interface ISchemaBuilder
    {
        void BuildSchema(SchemaInputData schemaInputData);

        void RebuildSchema(List<ElementData> elementsDatas);
    }
}
