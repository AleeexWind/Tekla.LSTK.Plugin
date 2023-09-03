using LSTK.Frame.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using Tekla.Structures.Dialog;
using TD = Tekla.Structures.Datatype;

namespace LSTK.Frame
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
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

        public List<(Point, Point)> SchemaPoints = new List<(Point, Point)>()
        {
            (new Point(){ X = 0, Y = 0, Z = 0}, new Point(){ X = 100, Y = 100, Z = 0}),
            (new Point(){ X = 100, Y = 100, Z = 0}, new Point(){ X = 200, Y = 0, Z = 0})
        };


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
