using FrameCreator.BusinessRules.DataBoundaries;
using FrameCreator.BusinessRules.Gateways;
using FrameCreator.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FrameCreator.BusinessRules.UseCases
{
    public class AttributeSetManager : IAttributeSetter
    {
        private readonly IDataAccess _dataAccess;
        private readonly ISchemaBuilder _schemaBuilder;
        public AttributeSetManager(IDataAccess dataAccess, ISchemaBuilder schemaBuilder)
        {
            _dataAccess = dataAccess;
            _schemaBuilder = schemaBuilder;
        }
        public bool SetAttributesToElements(List<int> elementIds, AttributeGroup attributeGroup)
        {
            try
            {
                List<ElementData> elementDatas = new List<ElementData>();

                List<ElementData> currentElementDatas = _dataAccess.GetElementDatas();

                List<ElementData> selectedElemDatas = currentElementDatas.Where(x => elementIds.Contains(x.Id)).ToList();

                foreach (var el in selectedElemDatas)
                {
                    SetAttributes(el, attributeGroup);
                }
                _dataAccess.AddElementDataCollection(currentElementDatas);
                elementDatas = _dataAccess.GetElementDatas();

                _schemaBuilder.RebuildSchema(elementDatas);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool SetAttributes(ElementData elementData, AttributeGroup attributeGroup)
        {
            try
            {
                elementData.PartName = attributeGroup.PartName;
                elementData.Profile = attributeGroup.Profile;
                elementData.Material = attributeGroup.Material;
                elementData.Class = attributeGroup.Class;
                elementData.RotationPosition = attributeGroup.RotationPosition;
                elementData.PlanePosition = attributeGroup.PlanePosition;
                elementData.DepthPosition = attributeGroup.DepthPosition;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
