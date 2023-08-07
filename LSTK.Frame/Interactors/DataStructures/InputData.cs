using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.Interactors.DataStructures
{
    public class InputData
    {
        //Columns section
        public string ProfileColumns { get; set; }
        public string PartNameColumns { get; set; }
        public string MaterialColumns { get; set; }
        public string ClassColumns { get; set; }
        public double HeightColumns { get; set; }
        

        //TopChord section
        public string ProfileTopChord { get; set; }
        public string PartNameTopChord { get; set; }
        public string MaterialTopChord { get; set; }
        public string ClassTopChord { get; set; }
        public double RoofRidgeHeight { get; set; }

        //BottomChord section
        public string ProfileBottomChord { get; set; }
        public string PartNameBottomChord { get; set; }
        public string MaterialBottomChord { get; set; }
        public string ClassBottomChord { get; set; }
        public double RoofBottomHeight { get; set; }


        //Common
        public double Bay { get; set; }
        public string FrameOption { get; set; }

        public Point StartPoint { get; set; }
        public Point DirectionPoint { get; set; }
    }
}
