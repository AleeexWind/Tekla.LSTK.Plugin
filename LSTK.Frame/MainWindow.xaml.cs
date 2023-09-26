using LSTK.Frame.Utils;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Tekla.Structures.Dialog;

namespace LSTK.Frame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : PluginWindowBase
    {
        public MainWindowViewModel dataModel;
        int i = 2;
        public MainWindow(MainWindowViewModel DataModel)
        {
            InitializeComponent();
            //try
            //{
            //    ServiceProvider.AddService<MainWindowViewModel>();
            //}
            //catch (Exception ex)
            //{

            //}
            dataModel = DataModel;
            dataModel.OnViewUpdate += BuildSchema;

        }
        //public MainWindow(MainWindowViewModel DataModel)
        //{
        //    InitializeComponent();
        //    dataModel = DataModel;
        //    ServiceProvider.AddService<MainWindowViewModel>();
        //}
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

        private void profileCatalogGroup_SelectClicked(object sender, EventArgs e)
        {
            this.profileCatalogGroup.SelectedProfile = this.dataModel.ProfileGroup;
        }
        private void profileCatalogGroup_SelectionDone(object sender, EventArgs e)
        {
            this.dataModel.ProfileGroup = this.profileCatalogGroup.SelectedProfile;
        }
        private void WpfMaterialCatalogGroup_SelectClicked(object sender, EventArgs e)
        {
            this.materialCatalogGroup.SelectedMaterial = this.dataModel.MaterialGroup;
        }
        private void WpfMaterialCatalogGroup_SelectionDone(object sender, EventArgs e)
        {
            this.dataModel.MaterialGroup = this.materialCatalogGroup.SelectedMaterial;
        }

        private void Path_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("of");
        }

        private void b_schema_Click(object sender, RoutedEventArgs e)
        {
            this.g_schema.Children.Clear();
            dataModel.OnBuildSchema?.Invoke(this, new EventArgs());



            //double scaleX = GetSchemaScaleX();
            //double scaleY = GetSchemaScaleY();

            //foreach (var points in dataModel.SchemaPoints)
            //{
            //    string _brushColor = "Blue";
            //    SolidColorBrush brushColor = (SolidColorBrush)new BrushConverter().ConvertFromString(_brushColor);

            //    Point startPoint = new Point() { X = points.Item1.X * scaleX, Y = points.Item1.Y * scaleY };
            //    Point endPoint = new Point() { X = points.Item2.X * scaleX, Y = points.Item2.Y * scaleY };
            //    LineGeometry pg = new LineGeometry(startPoint, endPoint);
            //    Path pgObject = new Path
            //    {
            //        Stroke = brushColor,
            //        StrokeThickness = 5,
            //        Data = pg
            //    };
            //    this.g_schema.Children.Add(pgObject);
            //} 
        }

        void BuildSchema(object sender, EventArgs e)
        {
            foreach (var points in dataModel.SchemaPoints)
            {
                string _brushColor = "Blue";
                SolidColorBrush brushColor = (SolidColorBrush)new BrushConverter().ConvertFromString(_brushColor);

                TransformCoordinatesForGrid(points, g_schema.ActualHeight);

                //Point startPoint = new Point() { X = points.Item1.X * scaleX, Y = (points.Item1.Y - yOffset)* scaleY };
                //Point endPoint = new Point() { X = points.Item2.X * scaleX, Y = (points.Item2.Y - yOffset) * scaleY };

                Point startPoint = new Point() { X = points.Item1.X, Y = points.Item1.Y};
                Point endPoint = new Point() { X = points.Item2.X, Y = points.Item2.Y };

                LineGeometry pg = new LineGeometry(startPoint, endPoint);
                Path pgObject = new Path
                {
                    Stroke = brushColor,
                    StrokeThickness = 5,
                    Data = pg
                };
                this.g_schema.Children.Add(pgObject);
            }        
        }

        private double GetSchemaScaleX()
        {
            return (g_schema.ActualWidth  * 0.98) / (dataModel.FrameWidthForSchema);
        }
        private double GetSchemaScaleY()
        {
            return (g_schema.ActualHeight * 0.98) / (dataModel.FrameHeightForSchema);
        }
        private double GetSchemaYoffset()
        {
            return dataModel.YoffsetSchema;
        }
        private void TransformCoordinatesForGrid((Entities.Point, Entities.Point) element, double maxY)
        {
            element.Item1.X = element.Item1.X * GetSchemaScaleX();
            element.Item1.Y = (element.Item1.Y - GetSchemaYoffset()) * GetSchemaScaleY();

            element.Item2.X = element.Item2.X * GetSchemaScaleX();
            element.Item2.Y = (element.Item2.Y - GetSchemaYoffset()) * GetSchemaScaleY();

            if (element.Item1.Y == element.Item2.Y)
            {
                double Y = maxY * 0.98 - element.Item1.Y;
                element.Item1.Y = Y;
                element.Item2.Y = Y;
            }
            else
            {
                double Y1 = element.Item1.Y;
                double Y2 = element.Item2.Y;

                element.Item1.Y = Y2;
                element.Item2.Y = Y1;

                double minY = element.Item1.Y;
                if(element.Item1.Y > element.Item2.Y)
                {
                    minY = element.Item2.Y;
                }

                element.Item1.Y = Y2 - minY;
                element.Item2.Y = Y1 - minY;
            }
        }
    }
}
