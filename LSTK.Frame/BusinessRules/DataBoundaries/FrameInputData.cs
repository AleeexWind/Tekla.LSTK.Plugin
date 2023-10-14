using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.Entities;
using System.Collections.Generic;

namespace LSTK.Frame.BusinessRules.DataBoundaries
{
    public class FrameInputData : InputData
    {
        //Columns section
        public string PartNameColumns { get; set; } = string.Empty;
        public string ProfileColumns { get; set; } = string.Empty;
        public string MaterialColumns { get; set; } = string.Empty;
        public string ClassColumns { get; set; } = string.Empty;
        public double HeightColumns { get; set; }
        public string RotationPositionColumns { get; set; } = "FRONT";
        public string PlanePositionColumns { get; set; } = "MIDDLE";
        public string DepthPositionColumns { get; set; } = "MIDDLE";


        //TopChord section
        public string PartNameTopChord { get; set; } = string.Empty;
        public string ProfileTopChord { get; set; } = string.Empty;
        public string MaterialTopChord { get; set; } = string.Empty;
        public string ClassTopChord { get; set; } = string.Empty;
        public double HeightRoofRidge { get; set; }
        public string RotationPositionTopChord { get; set; } = "FRONT";
        public string PlanePositionTopChord { get; set; } = "MIDDLE";
        public string DepthPositionTopChord { get; set; } = "MIDDLE";


        //BottomChord section
        public string PartNameBottomChord { get; set; } = string.Empty;
        public string ProfileBottomChord { get; set; } = string.Empty;
        public string MaterialBottomChord { get; set; } = string.Empty;
        public string ClassBottomChord { get; set; } = string.Empty;
        public double HeightRoofBottom { get; set; }
        public string RotationPositionBottomChord { get; set; } = "FRONT";
        public string PlanePositionBottomChord { get; set; } = "MIDDLE";
        public string DepthPositionBottomChord { get; set; } = "MIDDLE";

        //Group elements section
        public string PartNameGroup { get; set; } = string.Empty;
        public string ProfileGroup { get; set; } = string.Empty;
        public string MaterialGroup { get; set; } = string.Empty;
        public string ClassGroup { get; set; } = string.Empty;
        public string RotationPositionGroup { get; set; } = "FRONT";
        public string PlanePositionGroup { get; set; } = "MIDDLE";
        public string DepthPositionGroup { get; set; } = "MIDDLE";


        //Common
        public double Bay { get; set; }
        public string FrameOption { get; set; } = string.Empty;
        public string TopChordLineOption { get; set; } = string.Empty;
        public string ColumnLineOption { get; set; } = string.Empty;
        public string Panels { get; set; }

        public Point StartPoint { get; set; } = new Point();
        public Point DirectionPoint { get; set; } = new Point();

        public List<ElementData> ElementPrototypes { get; set; } = new List<ElementData>();
        public List<ElementDataPrototype> ElementDataPrototypes { get; set; } = new List<ElementDataPrototype>();
        public List<AttributeGroup> AttributeGroups { get; set; } = new List<AttributeGroup>();
    }
}
