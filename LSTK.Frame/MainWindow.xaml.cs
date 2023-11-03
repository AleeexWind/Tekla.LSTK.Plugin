using LSTK.Frame.Adapters.Controllers;
using LSTK.Frame.Adapters.Controllers.Models;
using LSTK.Frame.Adapters.Gateways;
using LSTK.Frame.Adapters.Presenters;
using LSTK.Frame.BusinessRules.DataBoundaries;
using LSTK.Frame.BusinessRules.Gateways;
using LSTK.Frame.BusinessRules.UseCases;
using LSTK.Frame.BusinessRules.UseCases.Calculators;
using LSTK.Frame.BusinessRules.UseCases.Calculators.SchemaCalculators;
using LSTK.Frame.Entities;
using LSTK.Frame.Frameworks.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private AttributeSetRequestModel _attributeSetRequestModel;
        private AttributeGetRequestModel _attributeGetRequestModel;
        private BuildSchemaRequestModel _buildSchemaRequestModel;
        private FrameReceiverRequestModel _frameReceiverRequestModel;


        private List<(int, Path)> _schemaElements = new List<(int, Path)>();
        private List<int> _selectedElements = new List<int>();


        public MainWindow(MainWindowViewModel DataModel)
        {
            InitializeComponent();
            dataModel = DataModel;
            OnInitialization();
            dataModel.OnDrawSchema += DrawSchema;
            dataModel.OnBuildSchema += TryToBuildFrame;
        }
        private void OnInitialization()
        {
            //DataBase dataBase = new DataBase();
            IDataAccess dataAccess = new DataHandler();

            //Schema build Use Case
            _buildSchemaRequestModel = new BuildSchemaRequestModel();
            IBuildSchemaResponse buildSchemaResponse = new BuildSchemaPresenter(dataModel);

            List<IDataCalculator> calculators = new List<IDataCalculator>()
                {
                    new TopChordSchemaCalculator(),
                    new BottomChordSchemaCalculator(),
                    new TrussPostsSchemaCalculator(),
                    new ColumnsScemaCalculator()
                };
            ISchemaBuilder schemaBuilder = new SchemaCreateManager(dataAccess, calculators, buildSchemaResponse);
            BuildSchemaController buildSchemaController = new BuildSchemaController(schemaBuilder, _buildSchemaRequestModel);


            //Attribute set Use Case
            _attributeSetRequestModel = new AttributeSetRequestModel();
           
            IAttributeSetter attributeSetter = new AttributeSetManager(dataAccess);

            AttributeSetController attributeSetController = new AttributeSetController(attributeSetter, _attributeSetRequestModel);


            //Attribute get Use Case
            _attributeGetRequestModel = new AttributeGetRequestModel();

            IAttributeGetter attributeGetter = new AttributeGetManager(dataAccess);
            IAttributeGetResponse attributeGetResponse = new AttributePresenter(dataModel);

            AttributeGetController attributeGetController = new AttributeGetController(attributeGetter, attributeGetResponse, _attributeGetRequestModel);

            _frameReceiverRequestModel = new FrameReceiverRequestModel();
            IFrameReceiverResponse frameReceiverResponse = new FrameReceiverPresenter(dataModel);
            IFrameReceiver frameReceiver = new FrameReceiver(frameReceiverResponse, dataAccess);
            FrameReceiverController frameReceiverController = new FrameReceiverController(frameReceiver, _frameReceiverRequestModel);
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
            _frameReceiverRequestModel.OnSendingRequest?.Invoke(this, new EventArgs());
        }

        private void WPFOkApplyModifyGetOnOffCancel_OnOffClicked(object sender, EventArgs e)
        {
            this.ToggleSelection();
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
            Path pgObject = sender as Path;
            pgObject.Stroke = new SolidColorBrush(Colors.Red);

            int selElemId = _schemaElements.FirstOrDefault(x => x.Item2.Equals(pgObject)).Item1;

            if(_selectedElements.Contains(selElemId))
            {
                _selectedElements.Remove(selElemId);
                pgObject.Stroke = new SolidColorBrush(Colors.Blue);
            }
            else
            {
                _selectedElements.Add(selElemId);
            }
        }

        private void b_schema_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in g_schema.Children)
            {
                Path pgObject = item as Path;
                pgObject.MouseDown -= Path_MouseDown;

            }
            g_schema.Children.Clear();

            _buildSchemaRequestModel.Bay = tb_Bay.Text;
            _buildSchemaRequestModel.HeightRoofRidge = tb_Height_RoofRidge.Text;
            _buildSchemaRequestModel.HeightRoofBottom = tb_Height_RoofBottom.Text;
            _buildSchemaRequestModel.Panels = tb_Panels.Text;
            _buildSchemaRequestModel.HeightColumns = tb_Height_Columns.Text;

            _buildSchemaRequestModel.OnSendingRequest?.Invoke(this, new EventArgs());
        }
        private void b_acceptAttribute_Click(object sender, RoutedEventArgs e)
        {
            _attributeSetRequestModel.ElementIds = _selectedElements;
            _attributeSetRequestModel.PartName = tb_PartName_Group.Text;
            _attributeSetRequestModel.Profile = tb_Profile_Group.Text;
            _attributeSetRequestModel.Material = tb_Material_Group.Text;
            _attributeSetRequestModel.Class = tb_Class_Group.Text;

            _attributeSetRequestModel.OnSendingRequest?.Invoke(this, new EventArgs());
            _selectedElements.Clear();
        }

        private void DrawSchema(object sender, EventArgs e)
        {
            foreach (var element in dataModel.SchemaElements)
            {
                string _brushColor = "Blue";
                SolidColorBrush brushColor = (SolidColorBrush)new BrushConverter().ConvertFromString(_brushColor);

                (LSTK.Frame.Entities.Point, LSTK.Frame.Entities.Point) points = (element.StartPoint, element.EndPoint);

                TransformCoordinatesForGrid(points, g_schema.ActualHeight);

                System.Windows.Point startPoint = new System.Windows.Point() { X = points.Item1.X, Y = points.Item1.Y };
                System.Windows.Point endPoint = new System.Windows.Point() { X = points.Item2.X, Y = points.Item2.Y };

                LineGeometry pg = new LineGeometry(startPoint, endPoint);
                Path pgObject = new Path
                {
                    Stroke = brushColor,
                    StrokeThickness = 5,
                    Data = pg
                };
                _schemaElements.Add((element.Id, pgObject));
                pgObject.MouseDown += Path_MouseDown;
                g_schema.Children.Add(pgObject);
            }
            tb_ElementPrototypes.Text = dataModel.ElementPrototypes;
            List<ElementData> el = DataBase.SchemaElements;
        }
        private void TryToBuildFrame(object sender, EventArgs e)
        {
            if(dataModel.ToBeBuilt)
            {
                this.Apply();
                this.Close();
            }
            else
            {
                // To display the message about not valid schema or attributes
            }
        }
        private double GetSchemaScaleX()
        {
            return g_schema.ActualWidth  * 0.98 / dataModel.FrameWidthForSchema;
        }
        private double GetSchemaScaleY()
        {
            return g_schema.ActualHeight * 0.98 / dataModel.FrameHeightForSchema;
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

            //if (element.Item1.Y == element.Item2.Y)
            //{
            //    double Y = maxY * 0.98 - element.Item1.Y;
            //    element.Item1.Y = Y;
            //    element.Item2.Y = Y;
            //}
            //else if(element.Item1.X == element.Item2.X && element.Item1.Y == 0.0)
            //{
            //    double Ystart = maxY * 0.98;
            //    double Yend;
            //    if (element.Item1.Y > element.Item2.Y)
            //    {
            //        Yend = Ystart - element.Item1.Y - element.Item2.Y;
            //    }
            //    else
            //    {
            //        Yend = Ystart - element.Item2.Y - element.Item1.Y;
            //    }
            //    element.Item1.Y = Ystart;
            //    element.Item2.Y = Yend;
            //}
            //else if (element.Item1.X == element.Item2.X)
            //{
            //    double Ystart = maxY * 0.98;
            //    double Yend;
            //    if (element.Item1.Y > element.Item2.Y)
            //    {
            //        Yend = Ystart - element.Item1.Y - element.Item2.Y;
            //    }
            //    else
            //    {
            //        Yend = Ystart - element.Item2.Y - element.Item1.Y;
            //    }
            //    element.Item1.Y = Ystart;
            //    element.Item2.Y = Yend;
            //}
            //else
            //{
            //    double Y1 = element.Item1.Y;
            //    double Y2 = element.Item2.Y;

            //    element.Item1.Y = Y2;
            //    element.Item2.Y = Y1;

            //    double minY = element.Item1.Y;
            //    if(element.Item1.Y > element.Item2.Y)
            //    {
            //        minY = element.Item2.Y;
            //    }

            //    element.Item1.Y = Y2 - minY;
            //    element.Item2.Y = Y1 - minY;
            //}
        }
    }
}
