using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.BusinessRules.UseCases
{
    public class ElementsRotator : IRotateElements
    {
        private List<ElementData> _elementsDatas;
        private readonly IDataAccess _dataAccess;
        private readonly ISchemaBuilder _schemaBuilder;

        public ElementsRotator(IDataAccess dataAccess, ISchemaBuilder schemaBuilder)
        {
            _dataAccess = dataAccess;
            _schemaBuilder = schemaBuilder;
        }
        public bool RotateElements(List<int> elementIds)
        {
            _elementsDatas = _dataAccess.GetElementDatas();

            List<ElementData> selectedElements = _elementsDatas.Where(x => elementIds.Contains(x.Id)).ToList();

            List<ElementData> diaRods = selectedElements.Where(x => x.ElementGroupType.Equals(ElementGroupType.DiagonalRod)).ToList();

            foreach (var el in _elementsDatas)
            {
                if (elementIds.Contains(el.Id) && el.ElementGroupType.Equals(ElementGroupType.DiagonalRod))
                {
                    Point sPoint = el.StartPoint;
                    Point ePoint = el.EndPoint;

                    el.StartPoint = el.AlternativeStartPoint;
                    el.EndPoint = el.AlternativeEndPoint;

                    el.AlternativeStartPoint = sPoint;
                    el.AlternativeEndPoint = ePoint;
                }
            }

            _schemaBuilder.RebuildSchema(_elementsDatas);

            return true;
        }
    }
}
