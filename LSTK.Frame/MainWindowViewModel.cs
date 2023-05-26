using System.ComponentModel;
using System.Windows;
using Tekla.Structures.Dialog;
using TD = Tekla.Structures.Datatype;

namespace LSTK.Frame
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string partName = string.Empty;
        private string profileColumns = string.Empty;
        private string profileTopChord = string.Empty;

        [StructuresDialog("name", typeof(TD.String))]
        public string PartName
        {
            get { return partName; }
            set { partName = value; OnPropertyChanged("PartName"); }
        }
        [StructuresDialog("profileColumns", typeof(TD.String))]
        public string ProfileColumns
        {
            get { return profileColumns; }
            set { profileColumns = value; OnPropertyChanged("ProfileColumns"); }
        }
        [StructuresDialog("profileTopChord", typeof(TD.String))]
        public string ProfileTopChord
        {
            get { return profileTopChord; }
            set { profileTopChord = value; OnPropertyChanged("ProfileTopChord"); }
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
        public void Run()
        {
            MessageBox.Show($"Start. Part: {PartName}  ProfileColumns: {ProfileColumns} ProfileTopChord: {ProfileTopChord}");
        }
    }
}
