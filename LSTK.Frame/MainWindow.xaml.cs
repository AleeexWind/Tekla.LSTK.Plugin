using System;
using Tekla.Structures.Dialog;

namespace LSTK.Frame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : PluginWindowBase
    {
        public MainWindowViewModel dataModel;

        public MainWindow(MainWindowViewModel DataModel)
        {
            InitializeComponent();
            dataModel = DataModel;
        }
        private void WPFOkApplyModifyGetOnOffCancel_ApplyClicked(object sender, EventArgs e)
        {
            this.Apply();
        }
        private void WPFOkApplyModifyGetOnOffCancel_CancelClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WPFOkApplyModifyGetOnOffCancel_GetClicked(object sender, EventArgs e)
        {
            this.Get();
        }

        private void WPFOkApplyModifyGetOnOffCancel_ModifyClicked(object sender, EventArgs e)
        {
            this.Modify();
        }
        private void WPFOkApplyModifyGetOnOffCancel_OkClicked(object sender, EventArgs e)
        {
            this.Apply();
            dataModel.Run();
            this.Close();
        }

        private void WPFOkApplyModifyGetOnOffCancel_OnOffClicked(object sender, EventArgs e)
        {
            this.ToggleSelection();
        }

        private void WpfProfileCatalogColumns_SelectClicked(object sender, EventArgs e)
        {
            this.profileCatalogColumns.SelectedProfile = this.dataModel.ProfileColumns;
        }

        private void WpfProfileCatalogColumns_SelectionDone(object sender, EventArgs e)
        {
            this.dataModel.ProfileColumns = this.profileCatalogColumns.SelectedProfile;
        }

        private void profileCatalogTopChord_SelectClicked(object sender, EventArgs e)
        {
            this.profileCatalogTopChord.SelectedProfile = this.dataModel.ProfileTopChord;
        }

        private void profileCatalogTopChord_SelectionDone(object sender, EventArgs e)
        {
            this.dataModel.ProfileTopChord = this.profileCatalogTopChord.SelectedProfile;
        }
    }
}
