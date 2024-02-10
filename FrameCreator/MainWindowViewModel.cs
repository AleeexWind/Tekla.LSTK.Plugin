using FrameCreator.BusinessRules.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Tekla.Structures.Dialog;
using TD = Tekla.Structures.Datatype;

namespace FrameCreator
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public EventHandler OnBuildSchema { get; set; }
        public EventHandler OnDrawSchema { get; set; }
        public EventHandler OnAttributeGet { get; set; }
        public EventHandler OnViewUpdate { get; set; }

        private string frameOption;
        private string bay = string.Empty;
        private string heightColumns = string.Empty;

        private string heightRoofRidge = string.Empty;
        private string heightRoofBottom = string.Empty;
        private string panels = string.Empty;

        private string partNameGroup = string.Empty;
        private string profileGroup = string.Empty;
        private string materialGroup = string.Empty;
        private string classGroup = string.Empty;

        private string topChordLineOption;
        private string bottomChordLineOption;
        private string columnLineOption;
        private string centralColumnLineOption;
        private string doubleProfileOption;
        private string profileGap = string.Empty;

        private string elementPrototypes = string.Empty;

        public bool ToBeBuilt { get; set; }

        public List<SchemaElement> SchemaElements { get; set; } = new List<SchemaElement>();
        public string TempElementPrototypes { get; set; }
        public double FrameWidthForSchema { get; set; }
        public double FrameHeightForSchema { get; set; }
        public double YoffsetSchema { get; set; }

        [StructuresDialog("heightColumns", typeof(TD.String))]
        public string HeightColumns
        {
            get { return heightColumns; }
            set { heightColumns = value; OnPropertyChanged("HeightColumns"); }
        }

        [StructuresDialog("heightRoofRidge", typeof(TD.String))]
        public string HeightRoofRidge
        {
            get { return heightRoofRidge; }
            set { heightRoofRidge = value; OnPropertyChanged("HeightRoofRidge"); }
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
        [StructuresDialog("bottomChordLineOpt", typeof(TD.String))]
        public string BottomChordLineOption
        {
            get { return bottomChordLineOption; }
            set { bottomChordLineOption = value; OnPropertyChanged("BottomChordLineOption"); }
        }
        public List<string> TopChordLineOptionList { get; set; } = new List<string>()
        {
            "Center",
            "Below"
        };
        public List<string> BottomChordLineOptionList { get; set; } = new List<string>()
        {
            "Center",
            "Below",
            "Above"
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
        [StructuresDialog("centerColumnLineOpt", typeof(TD.String))]
        public string CentralColumnLineOption
        {
            get { return centralColumnLineOption; }
            set { centralColumnLineOption = value; OnPropertyChanged("centralColumnLineOption"); }
        }
        public List<string> CentralColumnLineOptionList { get; set; } = new List<string>()
        {
            "Center",
            "Inside"
        };

        [StructuresDialog("doubleProfileOption", typeof(TD.String))]
        public string DoubleProfileOption
        {
            get { return doubleProfileOption; }
            set { doubleProfileOption = value; OnPropertyChanged("doubleProfileOption"); }
        }
        public List<string> DoubleProfileOptionList { get; set; } = new List<string>()
        {
            "Yes",
            "No"
        };

        [StructuresDialog("profileGap", typeof(TD.String))]
        public string ProfileGap
        {
            get { return profileGap; }
            set { profileGap = value; OnPropertyChanged("ProfileGap"); }
        }

        [StructuresDialog("panels", typeof(TD.String))]
        public string Panels
        {
            get { return panels; }
            set { panels = value; OnPropertyChanged("Panels"); }
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
    }
}
