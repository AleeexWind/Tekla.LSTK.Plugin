using FrameCreator.BusinessRules.DataBoundaries;
using FrameCreator.BusinessRules.Gateways;
using FrameCreator.BusinessRules.Models;
using FrameCreator.BusinessRules.UseCases.Calculators;
using FrameCreator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameCreator.BusinessRules.UseCases
{
    public class SchemaCreateManager : ISchemaBuilder
    {
        private readonly IBuildSchemaResponse _schemaResponse;
        private readonly List<IDataCalculator> _calculators;
        private readonly IDataAccess _dataAccess;

        private List<ElementData> _elementsDatas;

        public SchemaCreateManager(IDataAccess dataAccess, List<IDataCalculator> calculators, IBuildSchemaResponse firstSchemaOutputBoundary)
        {
            _dataAccess = dataAccess;
            _calculators = calculators;
            _schemaResponse = firstSchemaOutputBoundary;
        }
        public void BuildSchema(SchemaInputData schemaInputData)
        {
            _elementsDatas = new List<ElementData>();
            foreach (IDataCalculator calc in _calculators)
            {
                calc.Calculate(_elementsDatas, schemaInputData);
            }

            if (!AddElementsToDB(_elementsDatas))
            {
                //TODO: Logging
            }
            double Ymax = GetMaxCoord(_elementsDatas, MaxYcoord);
            double Xmax = GetMaxCoord(_elementsDatas, MaxXcoord);

            BuiltSchemaData builtSchemaData = new BuiltSchemaData()
            {
                ElementDatas = _elementsDatas,
                CoordXmax = Xmax,
                Yoffset = 0,
                CoordYmax = Ymax,
            };

            _schemaResponse.DrawSchema(builtSchemaData);
        }

        public void RebuildSchema(List<ElementData> elementsDatas)
        {

            _elementsDatas = elementsDatas;

            double Ymax = GetMaxCoord(_elementsDatas, MaxYcoord);
            double Xmax = GetMaxCoord(_elementsDatas, MaxXcoord);

            if (!AddElementsToDB(_elementsDatas))
            {
                //TODO: Logging
            }
            BuiltSchemaData builtSchemaData = new BuiltSchemaData()
            {
                ElementDatas = _elementsDatas,
                CoordXmax = Xmax,
                Yoffset = 0,
                CoordYmax = Ymax,
            };
            _schemaResponse.DrawSchema(builtSchemaData);
        }

        private double GetSchemaYoffset(SchemaInputData schemaInputData)
        {
            double columnHeight = schemaInputData.HeightColumns;
            double result = columnHeight - schemaInputData.HeightRoofBottom;
            result = 0;
            return result;
        }
        private bool AddElementsToDB(List<ElementData> elements)
        {
            bool result = _dataAccess.AddElementDataCollection(elements);
            return result;
        }
        private double GetMaxCoord(List<ElementData> collection, Func<List<Point>,double> func)
        {
            List<Point> allPoints = collection.Select(x => x.StartPoint).ToList();
            allPoints.AddRange(collection.Select(x => x.EndPoint).ToList());

            return func(allPoints);
        }

        private double MaxXcoord(List<Point> points)
        {
            return points.Max(x => x.X);
        }
        private double MaxYcoord(List<Point> points)
        {
            return points.Max(x => x.Y);
        }
    }
}
