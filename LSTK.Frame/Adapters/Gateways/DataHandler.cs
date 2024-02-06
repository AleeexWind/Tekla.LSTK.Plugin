using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.DataBase;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.Adapters.Gateways
{
    public class DataHandler : IDataAccess
    {
        private readonly DataBase _dataBase;
        public DataHandler(DataBase dataBase)
        {
            _dataBase = dataBase;
        }

        private int AddElementData(ElementData elementData)
        {
            int addedItem = -1;
            try
            {
                elementData.Id = _dataBase.CurrentElementDataId;
                _dataBase.CurrentElementDataId++;
                _dataBase.SchemaElements.Add(elementData);
                addedItem = elementData.Id;
            }
            catch (System.Exception)
            {
                //TODO: Logging
            }
            return addedItem;
        }

        public ElementData GetElementData(int elementDataId)
        {
            ElementData result = null;
            ElementData foundElement = _dataBase.SchemaElements.FirstOrDefault(x => x.Id.Equals(elementDataId));
            if (foundElement != null)
            {
                result = CloneElementData(foundElement);
            }
            return result;
        }

        public List<ElementData> GetElementDatas()
        {
            List<ElementData> result = new List<ElementData>();
            var maxStateId = _dataBase.States.Max(state => state.Id);
            var currentElementIds = _dataBase.States.FirstOrDefault(state => state.Id == maxStateId)?.ElementIds;

            if (currentElementIds != null && currentElementIds.Any())
            {
                currentElementIds
                    .ForEach(elId => result.Add(GetElementData(elId)));
            }

            return result;
        }

        private int AddState(List<int> elementDataIds)
        {
            State state = new State
            {
                Id = _dataBase.CurrentStateId,
                ElementIds = elementDataIds
            };
            _dataBase.CurrentStateId++;
            _dataBase.States.Add(state);

            return state.Id;
        }

        public bool AddElementDataCollection(List<ElementData> elements)
        {
            bool result = false;
            List<int> addedItems = new List<int>();
            foreach (ElementData elementData in elements)
            {
                int addedItem = AddElementData(elementData);
                addedItems.Add(addedItem);
            }

            if (!addedItems.Contains(-1))
            {
                int addedState = AddState(addedItems);
                if (addedState != -1)
                {
                    result = true;
                }
            }

            return result;
        }
        private ElementData CloneElementData(ElementData elementData)
        {
            return new ElementData
            {
                Id = elementData.Id,
                ElementGroupType = elementData.ElementGroupType,
                ElementSideType = elementData.ElementSideType,
                StartPoint = elementData.StartPoint,
                EndPoint = elementData.EndPoint,
                Profile = elementData.Profile,
                ProfileHeight = elementData.ProfileHeight,
                PartName = elementData.PartName,
                Material = elementData.Material,
                Class = elementData.Class,
                RotationPosition = elementData.RotationPosition,
                PlanePosition = elementData.PlanePosition,
                DepthPosition = elementData.DepthPosition,
                IsMirrored = elementData.IsMirrored,
                IsDeleted = elementData.IsDeleted,
                AlternativeStartPoint = elementData.AlternativeStartPoint,
                AlternativeEndPoint = elementData.AlternativeEndPoint
            };
        }
    }
}
