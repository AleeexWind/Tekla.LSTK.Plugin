using LSTK.Frame.Interactors;
using LSTK.Frame.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Tekla.Structures.Dialog;
using TD = Tekla.Structures.Datatype;

namespace LSTK.Frame
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string partName = string.Empty;
        private string partNameColumns = string.Empty;
        private string profileColumns = string.Empty;
        private string heightColumns = string.Empty;
        private string profileTopChord = string.Empty;

        private string bay = string.Empty;
        private string roofRidgeHeight = string.Empty;
        private string roofBottomHeight = string.Empty;
        private string frameOption;

        [StructuresDialog("name", typeof(TD.String))]
        public string PartName
        {
            get { return partName; }
            set { partName = value; OnPropertyChanged("PartName"); }
        }
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
        [StructuresDialog("heightColumns", typeof(TD.String))]
        public string HeightColumns
        {
            get { return heightColumns; }
            set { heightColumns = value; OnPropertyChanged("HeightColumns"); }
        }


        [StructuresDialog("bay", typeof(TD.String))]
        public string Bay
        {
            get { return bay; }
            set { bay = value; OnPropertyChanged("Bay"); }
        }

        [StructuresDialog("profileTopChord", typeof(TD.String))]
        public string ProfileTopChord
        {
            get { return profileTopChord; }
            set { profileTopChord = value; OnPropertyChanged("ProfileTopChord"); }
        }

        [StructuresDialog("roofRidgeHeight", typeof(TD.String))]
        public string RoofRidgeHeight
        {
            get { return roofRidgeHeight; }
            set { roofRidgeHeight = value; OnPropertyChanged("RoofRidgeHeight"); }
        }

        [StructuresDialog("roofBottomHeight", typeof(TD.String))]
        public string RoofBottomHeight
        {
            get { return roofBottomHeight; }
            set { roofBottomHeight = value; OnPropertyChanged("RoofBottomHeight"); }
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        public void Run()
        {
            //FrameData frameData = new FrameData();
            //TeklaPointSelector teklaPointSelector = new TeklaPointSelector();
            //LocalPlaneManager localPlaneManager = new LocalPlaneManager();
            //TeklaPartAttributeSetter teklaPartAttributeSetter = new TeklaPartAttributeSetter();
            //TeklaPartCreator teklaPartCreator = new TeklaPartCreator(frameData, teklaPartAttributeSetter);
            //FrameCreatorManager frameCreatorManager = new FrameCreatorManager(frameData, this, teklaPointSelector, localPlaneManager);

            //frameCreatorManager.SetLocalWorkingPlane();
            //frameCreatorManager.BuildFrameData();
            //frameCreatorManager.CreateColumns(teklaPartCreator);
            //frameCreatorManager.SetCurrentPlane();
            //frameCreatorManager.Commit();

            //MessageBox.Show($"Start.\nPart: {PartNameColumns}\nProfileColumns: {ProfileColumns}\nProfileTopChord: {ProfileTopChord}\nHeightColumns: {HeightColumns}\nBayOverall: {BayOverall}");
        }
    }
}
