using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //List<ElementData> result = new List<ElementData>();

            List<ElementData> selectedElements = _elementsDatas.Where(x => elementIds.Contains(x.Id)).ToList();

            List<ElementData> diaRods = selectedElements.Where(x => x.ElementGroupType.Equals(ElementGroupType.DiagonalRod)).ToList();

            foreach (ElementData diaRod in diaRods)
            {
                DiagonalRodElement diagonalRodElement = diaRod as DiagonalRodElement;

                diaRod.StartPoint = diagonalRodElement.StartPoint;
                diaRod.EndPoint = diagonalRodElement.EndPoint;
            }
            foreach (var el in _elementsDatas)
            {
                if(elementIds.Contains(el.Id) && el.ElementGroupType.Equals(ElementGroupType.DiagonalRod))
                {
                    Point sPoint = el.StartPoint;
                    Point ePoint = el.EndPoint;


                    DiagonalRodElement diagonalRodElement = el as DiagonalRodElement;
                    el.StartPoint = diagonalRodElement.AlternativeStartPoint;
                    el.EndPoint = diagonalRodElement.AlternativeEndPoint;

                    diagonalRodElement.AlternativeStartPoint = el.StartPoint;
                    diagonalRodElement.AlternativeEndPoint = el.EndPoint;


                }
            }

            _schemaBuilder.RebuildSchema(_elementsDatas);

            return true;
        }
    }
}
