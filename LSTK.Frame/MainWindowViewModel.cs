using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tekla.Structures.Dialog;
using TD = Tekla.Structures.Datatype;

namespace LSTK.Frame
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string partName = string.Empty;
        private string profile = string.Empty;

        [StructuresDialog("name", typeof(TD.String))]
        public string PartName
        {
            get { return partName; }
            set { partName = value; OnPropertyChanged("PartName"); }
        }
        [StructuresDialog("profile", typeof(TD.String))]
        public string ProfileName
        {
            get { return profile; }
            set { profile = value; OnPropertyChanged("ProfileName"); }
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
            MessageBox.Show($"Start. Part: {partName}  Profile: {profile}");
        }
    }
}
