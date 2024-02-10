using FrameCreator.Adapters.Controllers;
using FrameCreator.Adapters.Controllers.Models;
using FrameCreator.Adapters.Gateways;
using FrameCreator.Adapters.Presenters;
using FrameCreator.BusinessRules.DataBoundaries;
using FrameCreator.BusinessRules.Gateways;
using FrameCreator.BusinessRules.UseCases;
using FrameCreator.BusinessRules.UseCases.Calculators;
using FrameCreator.BusinessRules.UseCases.Calculators.SchemaCalculators;
using FrameCreator.Frameworks.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Tekla.Structures.Dialog;
using Localization = Tekla.Structures.Dialog.Localization;
using SWSH = System.Windows.Shapes;

namespace FrameCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : PluginWindowBase
    {
        public MainWindowViewModel dataModel;

        private Localization _loc;
        private AttributeSetRequestModel _attributeSetRequestModel;
        private AttributeGetRequestModel _attributeGetRequestModel;
        private BuildSchemaRequestModel _buildSchemaRequestModel;
        private RotateRequestModel _rotateRequestModel;
        private DeleteRequestModel _deleteRequestModel;


        private List<(int, SWSH.Path)> _schemaElements = new List<(int, SWSH.Path)>();
        private List<int> _selectedElements = new List<int>();
        private int _i = 2;

        public MainWindow(MainWindowViewModel DataModel)
        {
            InitializeComponent();

            string resourceName = "FrameCreator.FrameCreator.ail";

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new FileNotFoundException($"Could not find embedded resource '{resourceName}'.");
            }

            // Read the .ail file from the resource stream
            byte[] ailData = new BinaryReader(stream).ReadBytes((int)stream.Length);

            // Convert the bytes to a string
            string ailString = Encoding.UTF8.GetString(ailData);
            string tempFilePath = Path.Combine(Path.GetTempPath(), "tempFrameCreator.ail");
            File.WriteAllText(tempFilePath, ailString);

            Dialogs.SetSettings(string.Empty);
            Localization.Language = (string)Tekla.Structures.Datatype.Settings.GetValue("language");
            Localization.LoadAilFile(tempFilePath);

            dataModel = DataModel;
            this.Closed += WindowClosing;
            dataModel.OnDrawSchema += DrawSchema;
            dataModel.OnBuildSchema += TryToBuildFrame;
            dataModel.OnAttributeGet += ShowAttributes;
            OnInitialization();
        }

        private void WindowClosing(object sender, EventArgs e)
        {
            dataModel.OnDrawSchema -= DrawSchema;
            dataModel.OnBuildSchema -= TryToBuildFrame;
            dataModel.OnAttributeGet -= ShowAttributes;
            this.Closed -= WindowClosing;
        }
        private void Localize()
        {
            List<Label> labels = new List<Label>()
            {
                label_FrameOption,
                label_Bay,
                label_Height_Columns,
                label_Height_RoofRidge,
                label_Height_RoofBottom,
                label_Panels,
                label_TopChordLineOption,
                label_BottomChordLineOption,
                label_ColumnLineOption,
                label_CentralColumnLineOption,
                label_DoubleProfileOption,
                label_ProfileGap,
                label_PartName_Group,
                label_Profile_Group,
                label_Material_Group,
                label_Class_Group
            };

            foreach (var label in labels)
            {
                label.Content = Localization.GetText(label.Content.ToString());
            }

            List<Button> buttons = new List<Button>()
            {
                b_schemaAsNew,
                b_rotate,
                b_remove,
                b_acceptAttribute,
                b_getAttribute
            };

            foreach (var button in buttons)
            {
                button.Content = Localization.GetText(button.Content.ToString());
            }

        }
        private void OnInitialization()
        {
            Tekla.Structures.ModelInternal.Operation.dotStartAction("dotdiaLoadDialogs", "");
            Tekla.Structures.ModelInternal.Operation.dotStartAction("dotdiaReloadDialogs", "");
            Tekla.Structures.Model.Operations.Operation.DisplayPrompt("Dialogs reloaded..");

            Localize();

            DataBase dataBase = new DataBase();
            IDataAccess dataAccess = new DataHandler(dataBase);

            //Schema build Use Case
            _buildSchemaRequestModel = new BuildSchemaRequestModel();
            IBuildSchemaResponse buildSchemaResponse = new BuildSchemaPresenter(dataModel);
            List<IDataCalculator> calculators = new List<IDataCalculator>()
                {
                    new TopChordSchemaCalculator(),
                    new BottomChordSchemaCalculator(),
                    new TrussPostsSchemaCalculator(),
                    new ColumnsScemaCalculator(),
                    new DiagonalRodsSchemaCalculator()
                };
            ISchemaBuilder schemaBuilder = new SchemaCreateManager(dataAccess, calculators, buildSchemaResponse);
            _ = new BuildSchemaController(schemaBuilder, _buildSchemaRequestModel);


            //Elements rotation Use Case
            _rotateRequestModel = new RotateRequestModel();
            IRotateElements rotateElements = new ElementsRotator(dataAccess, schemaBuilder);
            _ = new RotateElementsController(rotateElements, _rotateRequestModel);

            //Elements deletion Use Case
            _deleteRequestModel = new DeleteRequestModel();
            IDeleteElements deleteElements = new ElementsRemover(dataAccess, schemaBuilder);
            DeleteElementsController deleteElementsController = new DeleteElementsController(deleteElements, _deleteRequestModel);

            //Attribute set Use Case
            _attributeSetRequestModel = new AttributeSetRequestModel();
            IAttributeSetter attributeSetter = new AttributeSetManager(dataAccess, schemaBuilder);
            _ = new AttributeSetController(attributeSetter, _attributeSetRequestModel);


            //Attribute get Use Case
            _attributeGetRequestModel = new AttributeGetRequestModel();
            IAttributeGetter attributeGetter = new AttributeGetManager(dataAccess);
            IAttributeGetResponse attributeGetResponse = new AttributePresenter(dataModel);
            _ = new AttributeGetController(attributeGetter, attributeGetResponse, _attributeGetRequestModel);

            dataModel.PropertyChanged += HandlePropertyChanged;
        }
        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ElementPrototypes") && _i != 0)
            {
                _i--;
                InvokeOnSendingRequest(true);
            }
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
            dataModel.ElementPrototypes = dataModel.TempElementPrototypes;
            this.Modify();
        }
        private void WPFOkApplyModifyGetOnOffCancel_OkClicked(object sender, EventArgs e)
        {
            dataModel.PropertyChanged -= HandlePropertyChanged;
            dataModel.ElementPrototypes = dataModel.TempElementPrototypes;
            TryToBuildFrame(this, new EventArgs());
            this.Close();
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
            SWSH.Path pgObject = sender as SWSH.Path;
            pgObject.Stroke = new SolidColorBrush(Colors.Red);

            int selElemId = _schemaElements.FirstOrDefault(x => x.Item2.Equals(pgObject)).Item1;

            if (_selectedElements.Contains(selElemId))
            {
                _selectedElements.Remove(selElemId);
                pgObject.Stroke = new SolidColorBrush(Colors.Blue);
            }
            else
            {
                _selectedElements.Add(selElemId);
            }
        }

        private void b_schema_as_new_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in g_schema.Children)
            {
                SWSH.Path pgObject = item as SWSH.Path;
                pgObject.MouseDown -= Path_MouseDown;

            }
            g_schema.Children.Clear();
            dataModel.ElementPrototypes = string.Empty;
            InvokeOnSendingRequest(true);
        }
        private void InvokeOnSendingRequest(bool isFirstBuild)
        {
            _buildSchemaRequestModel.FirstBuild = isFirstBuild;
            if (string.IsNullOrEmpty(dataModel.ElementPrototypes))
            {
                _buildSchemaRequestModel.Bay = tb_Bay.Text;
                _buildSchemaRequestModel.HeightRoofRidge = tb_Height_RoofRidge.Text;
                _buildSchemaRequestModel.HeightRoofBottom = tb_Height_RoofBottom.Text;
                _buildSchemaRequestModel.Panels = tb_Panels.Text;
                _buildSchemaRequestModel.HeightColumns = tb_Height_Columns.Text;
                _buildSchemaRequestModel.ExistedSchema = string.Empty;
            }
            else
            {
                _buildSchemaRequestModel.Bay = dataModel.Bay;
                _buildSchemaRequestModel.HeightRoofRidge = dataModel.HeightRoofRidge;
                _buildSchemaRequestModel.HeightRoofBottom = dataModel.HeightRoofBottom;
                _buildSchemaRequestModel.Panels = dataModel.Panels;
                _buildSchemaRequestModel.HeightColumns = dataModel.HeightColumns;
                _buildSchemaRequestModel.ExistedSchema = dataModel.ElementPrototypes;
            }

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

            foreach (var elemId in _selectedElements)
            {
                var selPath = _schemaElements.FirstOrDefault(f => f.Item1.Equals(elemId));
                if (selPath.Item2 != null)
                {
                    selPath.Item2.Stroke = new SolidColorBrush(Colors.Blue);
                }
            }

            _selectedElements.Clear();
        }
        private void b_rotate_Click(object sender, RoutedEventArgs e)
        {
            _rotateRequestModel.ElementIds = _selectedElements;

            _rotateRequestModel.OnSendingRequest?.Invoke(this, new EventArgs());

            foreach (var elemId in _selectedElements)
            {
                var selPath = _schemaElements.FirstOrDefault(f => f.Item1.Equals(elemId));
                if (selPath.Item2 != null)
                {
                    selPath.Item2.Stroke = new SolidColorBrush(Colors.Blue);
                }
            }

            _selectedElements.Clear();
        }
        private void b_remove_Click(object sender, RoutedEventArgs e)
        {
            _deleteRequestModel.ElementIds = _selectedElements;

            _deleteRequestModel.OnSendingRequest?.Invoke(this, new EventArgs());

            foreach (var elemId in _selectedElements)
            {
                var selPath = _schemaElements.FirstOrDefault(f => f.Item1.Equals(elemId));
                if (selPath.Item2 != null)
                {
                    selPath.Item2.Stroke = new SolidColorBrush(Colors.Blue);
                }
            }

            _selectedElements.Clear();
        }
        private void b_getAttribute_Click(object sender, RoutedEventArgs e)
        {
            _attributeGetRequestModel.ElementIds = _selectedElements;
            _attributeGetRequestModel.OnSendingRequest?.Invoke(this, new EventArgs());
        }

        private void DrawSchema(object sender, EventArgs e)
        {
            _schemaElements.Clear();
            g_schema.Children.Clear();
            foreach (var element in dataModel.SchemaElements)
            {
                if (element.ToBeDrawn == false)
                    continue;
                string _brushColor = "Blue";
                SolidColorBrush brushColor = (SolidColorBrush)new BrushConverter().ConvertFromString(_brushColor);

                (FrameCreator.Entities.Point, FrameCreator.Entities.Point) points = (element.StartPoint, element.EndPoint);

                TransformCoordinatesForGrid(points, 230, element.Id);

                System.Windows.Point startPoint = new System.Windows.Point() { X = points.Item1.X, Y = points.Item1.Y };
                System.Windows.Point endPoint = new System.Windows.Point() { X = points.Item2.X, Y = points.Item2.Y };

                LineGeometry pg = new LineGeometry(startPoint, endPoint);
                SWSH.Path pgObject = new SWSH.Path
                {
                    Stroke = brushColor,
                    StrokeThickness = 5,
                    Data = pg
                };
                _schemaElements.Add((element.Id, pgObject));
                pgObject.MouseDown += Path_MouseDown;
                g_schema.Children.Add(pgObject);
            }
        }
        private void TryToBuildFrame(object sender, EventArgs e)
        {
            this.Apply();
            this.Close();
        }
        private void ShowAttributes(object sender, EventArgs e)
        {
            if (dataModel.ToBeBuilt)
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
            return g_schema.Width * 0.98 / dataModel.FrameWidthForSchema;
        }
        private double GetSchemaScaleY()
        {
            return g_schema.Height * 0.98 / dataModel.FrameHeightForSchema;
        }
        private double GetSchemaYoffset()
        {
            return dataModel.YoffsetSchema;
        }
        private void TransformCoordinatesForGrid((Entities.Point, Entities.Point) element, double maxY, int id)
        {
            element.Item1.X = element.Item1.X * GetSchemaScaleX();
            element.Item1.Y = (element.Item1.Y - GetSchemaYoffset()) * GetSchemaScaleY();

            element.Item2.X = element.Item2.X * GetSchemaScaleX();
            element.Item2.Y = (element.Item2.Y - GetSchemaYoffset()) * GetSchemaScaleY();

            //Horizontal
            if (element.Item1.Y == element.Item2.Y)
            {
                double Y = maxY * 0.98 - element.Item1.Y;
                element.Item1.Y = Y;
                element.Item2.Y = Y;
            }
            //Column
            else if (element.Item1.X == element.Item2.X && element.Item1.Y == 0.0)
            {
                double Ystart = maxY * 0.98;
                double Yend;
                if (element.Item1.Y > element.Item2.Y)
                {
                    Yend = Ystart - element.Item1.Y - element.Item2.Y;
                }
                else
                {
                    Yend = Ystart - element.Item2.Y - element.Item1.Y;
                }
                element.Item1.Y = Ystart;
                element.Item2.Y = Yend;
            }
            else
            {
                double Ystart = maxY * 0.98 - element.Item1.Y;
                double Yend = maxY * 0.98 - element.Item2.Y;

                element.Item1.Y = Ystart;
                element.Item2.Y = Yend;
            }
        }
    }
}
