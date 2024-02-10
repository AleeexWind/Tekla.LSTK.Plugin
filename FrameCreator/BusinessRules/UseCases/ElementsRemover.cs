using FrameCreator.BusinessRules.DataBoundaries;
using FrameCreator.BusinessRules.Gateways;
using FrameCreator.BusinessRules.Models;
using FrameCreator.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FrameCreator.BusinessRules.UseCases
{
    public class ElementsRemover : IDeleteElements
    {
        private List<ElementData> _elementsDatas;
        private readonly IDataAccess _dataAccess;
        private readonly ISchemaBuilder _schemaBuilder;

        public ElementsRemover(IDataAccess dataAccess, ISchemaBuilder schemaBuilder)
        {
            _dataAccess = dataAccess;
            _schemaBuilder = schemaBuilder;
        }

        public bool DeleteElements(List<int> elementsIds)
        {
            _elementsDatas = _dataAccess.GetElementDatas();

            List<ElementData> selectedElements = _elementsDatas.Where(x => elementsIds.Contains(x.Id)).ToList();

            List<ElementData> elementsToBeDeleted = selectedElements.Where(x => x.ElementGroupType.Equals(ElementGroupType.TrussPost)).ToList();

            foreach (var elem in elementsToBeDeleted)
            {
                elem.IsDeleted = true;
            }
            _schemaBuilder.RebuildSchema(_elementsDatas);
            return true;
        }
    }
}
