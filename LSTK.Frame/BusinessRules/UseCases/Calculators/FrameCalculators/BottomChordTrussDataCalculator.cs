using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LSTK.Frame.BusinessRules.UseCases.Calculators
{
    public class BottomChordTrussDataCalculator : IDataCalculator
    {
        private FrameInputData _frameInputData;
        private ElementData _leftBottomChord;
        private ElementData _rightBottomChord;
        public bool Calculate(List<ElementData> elementsDatas, InputData inputData)
        {
            _frameInputData = inputData as FrameInputData;
            FilterElements(elementsDatas);


            return CalcLeftBottomChord() && CalcRightBottomChord();
        }
        private void FilterElements(List<ElementData> elementsDatas)
        {
            _leftBottomChord = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(ElementGroupType.TopChord) && x.ElementSideType.Equals(ElementSideType.Left));
            _rightBottomChord = elementsDatas.FirstOrDefault(x => x.ElementGroupType.Equals(ElementGroupType.BottomChord) && x.ElementSideType.Equals(ElementSideType.Left));
        }

        private bool CalcLeftBottomChord()
        {
            try
            {              
                Point startPoint = new Point()
                {
                    X = 0.0,
                    Y = _frameInputData.HeightColumns - _frameInputData.HeightRoofBottom,
                    Z = 0.0
                };
                Point endPoint = new Point()
                {
                    X = _frameInputData.Bay/2,
                    Y = _frameInputData.HeightColumns - _frameInputData.HeightRoofBottom,
                    Z = 0.0
                };
                _leftBottomChord.StartPoint = startPoint;
                _leftBottomChord.EndPoint = endPoint;
                return true;
            }
            catch (Exception)
            {
                //TODO: Logging
                return false;
                throw;
            }
        }
        private bool CalcRightBottomChord()
        {
            try
            {
                Point startPoint = new Point()
                {
                    X = _frameInputData.Bay/2,
                    Y = _frameInputData.HeightColumns - _frameInputData.HeightRoofBottom,
                    Z = 0.0
                };
                Point endPoint = new Point()
                {
                    X = _frameInputData.Bay,
                    Y = _frameInputData.HeightColumns - _frameInputData.HeightRoofBottom,
                    Z = 0.0
                };

                _rightBottomChord.StartPoint = startPoint;
                _rightBottomChord.EndPoint = endPoint;
                return true;
            }
            catch (Exception)
            {
                //TODO: Logging
                return false;
                throw;
            }
        }
    }
}
