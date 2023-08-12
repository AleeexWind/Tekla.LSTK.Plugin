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
            this.Close();
            //this.Apply();
            //new Task(delegate
            //{
            //    dataModel.Run();
            //}).Start();
            //this.Close();

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
        private void WpfMaterialCatalogColumns_SelectClicked(object sender, EventArgs e)
        {
            this.materialCatalogColumns.SelectedMaterial = this.dataModel.MaterialColumns;
        }
        private void WpfMaterialCatalogColumns_SelectionDone(object sender, EventArgs e)
        {
            this.dataModel.MaterialColumns = this.materialCatalogColumns.SelectedMaterial;
        }




        private void profileCatalogTopChord_SelectClicked(object sender, EventArgs e)
        {
            this.profileCatalogTopChord.SelectedProfile = this.dataModel.ProfileTopChord;
        }
        private void profileCatalogTopChord_SelectionDone(object sender, EventArgs e)
        {
            this.dataModel.ProfileTopChord = this.profileCatalogTopChord.SelectedProfile;
        }
        private void WpfMaterialCatalogTopChord_SelectClicked(object sender, EventArgs e)
        {
            this.materialCatalogTopChord.SelectedMaterial = this.dataModel.MaterialTopChord;
        }
        private void WpfMaterialCatalogTopChord_SelectionDone(object sender, EventArgs e)
        {
            this.dataModel.MaterialTopChord = this.materialCatalogTopChord.SelectedMaterial;
        }


        private void profileCatalogBottomChord_SelectClicked(object sender, EventArgs e)
        {
            this.profileCatalogBottomChord.SelectedProfile = this.dataModel.ProfileBottomChord;
        }
        private void profileCatalogBottomChord_SelectionDone(object sender, EventArgs e)
        {
            this.dataModel.ProfileBottomChord = this.profileCatalogBottomChord.SelectedProfile;
        }
        private void WpfMaterialCatalogBottomChord_SelectClicked(object sender, EventArgs e)
        {
            this.materialCatalogBottomChord.SelectedMaterial = this.dataModel.MaterialBottomChord;
        }
        private void WpfMaterialCatalogBottomChord_SelectionDone(object sender, EventArgs e)
        {
            this.dataModel.MaterialBottomChord = this.materialCatalogBottomChord.SelectedMaterial;
        }
    }
}
