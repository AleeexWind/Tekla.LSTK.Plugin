using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.DataBoundaries
{
    public interface ISchemaBuilder
    {
        void BuildSchema(SchemaInputData schemaInputData);

        void RebuildSchema(List<ElementData> elementsDatas);
    }
}
