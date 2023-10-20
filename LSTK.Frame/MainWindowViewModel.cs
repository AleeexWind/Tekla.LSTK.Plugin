using LSTK.Frame.Adapters.Controllers;
using LSTK.Frame.Adapters.Presenters;
using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Models;
using LSTK.Frame.BusinessRules.UseCases;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using LSTK.Frame.BusinessRules.UseCases.Calculators.SchemaCalculators;
using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Tekla.Structures.Dialog;
using TD = Tekla.Structures.Datatype;

namespace LSTK.Frame
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public EventHandler OnBuildSchema;
        public EventHandler OnDrawSchema;
        public EventHandler<List<int>> OnSchemaAttributeSet;
        public EventHandler OnViewUpdate;
        private string partNameColumns = string.Empty;
        private string profileColumns = string.Empty;
        private string materialColumns = string.Empty;
        private string classColumns = string.Empty;
        private string heightColumns = string.Empty;

        private string partNameTopChord = string.Empty;
        private string profileTopChord = string.Empty;
        private string materialTopChord = string.Empty;
        private string classTopChord = string.Empty;
        private string heightRoofRidge = string.Empty;

        private string partNameBottomChord = string.Empty;
        private string profileBottomChord = string.Empty;
        private string materialBottomChord = string.Empty;
        private string classBottomChord = string.Empty;
        private string heightRoofBottom = string.Empty;

        private string partNameGroup = string.Empty;
        private string profileGroup = string.Empty;
        private string materialGroup = string.Empty;
        private string classGroup = string.Empty;

        private string bay = string.Empty;     
        private string frameOption;
        private string topChordLineOption;
        private string columnLineOption;
        private string panels = string.Empty;

        private string attributeGroups = string.Empty;
        private string elementPrototypes = string.Empty;

        public List<int> SelectedElements = new List<int>();

        //public List<(Point, Point)> SchemaPoints { get; set; } = new List<(Point, Point)>();

        public List<SchemaElement> SchemaElements { get; set; } = new List<SchemaElement>();

        public double FrameWidthForSchema { get; set; }
        public double FrameHeightForSchema { get; set; }
        public double YoffsetSchema { get; set; }

        [StructuresDialog("partNameColumns", typeof(TD.String))]
        public string PartNameColumns
        {
            get { return partNameColumns; }
            set { partNameColumns = value; OnPropertyChanged("PartNameColumns"); }
        }
        [StructuresDialog("profileColumns", typeof(TD.String))]
        public string ProfileColumns
        {
            get { return profileColumns; }
            set { profileColumns = value; OnPropertyChanged("ProfileColumns"); }
        }
        [StructuresDialog("materialColumns", typeof(TD.String))]
        public string MaterialColumns
        {
            get { return materialColumns; }
            set { materialColumns = value; OnPropertyChanged("MaterialColumns"); }
        }
        [StructuresDialog("classColumns", typeof(TD.String))]
        public string ClassColumns
        {
            get { return classColumns; }
            set { classColumns = value; OnPropertyChanged("ClassColumns"); }
        }
        [StructuresDialog("heightColumns", typeof(TD.String))]
        public string HeightColumns
        {
            get { return heightColumns; }
            set { heightColumns = value; OnPropertyChanged("HeightColumns"); }
        }



        [StructuresDialog("partNameTopChord", typeof(TD.String))]
        public string PartNameTopChord
        {
            get { return partNameTopChord; }
            set { partNameTopChord = value; OnPropertyChanged("PartNameTopChord"); }
        }

        [StructuresDialog("profileTopChord", typeof(TD.String))]
        public string ProfileTopChord
        {
            get { return profileTopChord; }
            set { profileTopChord = value; OnPropertyChanged("ProfileTopChord"); }
        }
        [StructuresDialog("materialTopChord", typeof(TD.String))]
        public string MaterialTopChord
        {
            get { return materialTopChord; }
            set { materialTopChord = value; OnPropertyChanged("MaterialTopChord"); }
        }
        [StructuresDialog("classTopChord", typeof(TD.String))]
        public string ClassTopChord
        {
            get { return classTopChord; }
            set { classTopChord = value; OnPropertyChanged("ClassTopChord"); }
        }
        [StructuresDialog("heightRoofRidge", typeof(TD.String))]
        public string HeightRoofRidge
        {
            get { return heightRoofRidge; }
            set { heightRoofRidge = value; OnPropertyChanged("HeightRoofRidge"); }
        }




        [StructuresDialog("partNameBottomChord", typeof(TD.String))]
        public string PartNameBottomChord
        {
            get { return partNameBottomChord; }
            set { partNameBottomChord = value; OnPropertyChanged("PartNameBottomChord"); }
        }

        [StructuresDialog("profileBottomChord", typeof(TD.String))]
        public string ProfileBottomChord
        {
            get { return profileBottomChord; }
            set { profileBottomChord = value; OnPropertyChanged("ProfileBottomChord"); }
        }
        [StructuresDialog("materialBottomChord", typeof(TD.String))]
        public string MaterialBottomChord
        {
            get { return materialBottomChord; }
            set { materialBottomChord = value; OnPropertyChanged("MaterialBottomChord"); }
        }
        [StructuresDialog("classBottomChord", typeof(TD.String))]
        public string ClassBottomChord
        {
            get { return classBottomChord; }
            set { classBottomChord = value; OnPropertyChanged("ClassBottomChord"); }
        }
        [StructuresDialog("heightRoofBottom", typeof(TD.String))]
        public string HeightRoofBottom
        {
            get { return heightRoofBottom; }
            set { heightRoofBottom = value; OnPropertyChanged("HeightRoofBottom"); }
        }


        [StructuresDialog("partNameGroup", typeof(TD.String))]
        public string PartNameGroup
        {
            get { return partNameGroup; }
            set { partNameGroup = value; OnPropertyChanged("PartNameGroup"); }
        }

        [StructuresDialog("profileGroup", typeof(TD.String))]
        public string ProfileGroup
        {
            get { return profileGroup; }
            set { profileGroup = value; OnPropertyChanged("ProfileGroup"); }
        }
        [StructuresDialog("materialGroup", typeof(TD.String))]
        public string MaterialGroup
        {
            get { return materialGroup; }
            set { materialGroup = value; OnPropertyChanged("MaterialGroup"); }
        }
        [StructuresDialog("classGroup", typeof(TD.String))]
        public string ClassGroup
        {
            get { return classGroup; }
            set { classGroup = value; OnPropertyChanged("ClassGroup"); }
        }


        [StructuresDialog("bay", typeof(TD.String))]
        public string Bay
        {
            get { return bay; }
            set { bay = value; OnPropertyChanged("Bay"); }
        }
        [StructuresDialog("frameOption", typeof(TD.String))]
        public string FrameOption
        {
            get { return frameOption; }
            set { frameOption = value; OnPropertyChanged("FrameOption"); }
        }
        public List<string> FrameOptionList { get; set; } = new List<string>()
        {
            "Whole",
            "Half"
        };
        [StructuresDialog("topChordLineOption", typeof(TD.String))]
        public string TopChordLineOption
        {
            get { return topChordLineOption; }
            set { topChordLineOption = value; OnPropertyChanged("TopChordLineOption"); }
        }
        public List<string> TopChordLineOptionList { get; set; } = new List<string>()
        {
            "Center",
            "Below"
        };
        [StructuresDialog("columnLineOption", typeof(TD.String))]
        public string ColumnLineOption
        {
            get { return columnLineOption; }
            set { columnLineOption = value; OnPropertyChanged("ColumnLineOption"); }
        }
        public List<string> ColumnLineOptionList { get; set; } = new List<string>()
        {
            "Center",
            "Inside"
        };
        [StructuresDialog("panels", typeof(TD.String))]
        public string Panels
        {
            get { return panels; }
            set { panels = value; OnPropertyChanged("Panels"); }
        }

        [StructuresDialog("attributeGroups", typeof(TD.String))]
        public string AttributeGroups
        {
            get { return attributeGroups; }
            set { attributeGroups = value; OnPropertyChanged("AttributeGroups"); }
        }
        [StructuresDialog("elementPrototypes", typeof(TD.String))]
        public string ElementPrototypes
        {
            get { return elementPrototypes; }
            set { elementPrototypes = value; OnPropertyChanged("ElementPrototypes"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        private ISchemaBuilder _firstSchemaInputBoundary;
        //public MainWindowViewModel()
        //{
        //    IBuildSchemaResponse firstSchemaOutputBoundary = new BuildSchemaPresenter(this);
        //    List<IDataCalculator> calculators = new List<IDataCalculator>()
        //        {
        //            new TopChordSchemaCalculator(),
        //            new BottomChordSchemaCalculator(),
        //            new TrussPostsSchemaCalculator(),
        //        };
        //    _firstSchemaInputBoundary = new SchemaCreateManager(calculators, firstSchemaOutputBoundary);
        //    BuildSchemaController buildSchemaController = new BuildSchemaController(_firstSchemaInputBoundary, this);
        //}
    }
}
