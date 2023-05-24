using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using Tekla.Structures.Plugins;

namespace LSTK.Frame
{
    [Plugin("LSTK_Frame")]
    [PluginUserInterface("LSTK.Frame.MainWindow")]
    public class MainPlugin : PluginBase
    {
        private Model _Model;
        private PluginData _Data;

        private string _profileColumns = string.Empty;
        private string _profileTopChord = string.Empty;
        private Model Model
        {
            get { return this._Model; }
            set { this._Model = value; }
        }

        private PluginData Data
        {
            get { return this._Data; }
            set { this._Data = value; }
        }

        public MainPlugin(PluginData data)
        {
            Model = new Model();
            Data = data;
        }


        public override List<InputDefinition> DefineInput()
        {
            return new List<InputDefinition>();
        }

        public override bool Run(List<InputDefinition> Input)
        {
            return true;
        }
    }
    public class PluginData
    {
        #region Fields
        //
        // Define the fields specified on the Form.
        //
        [StructuresField("name")]
        public string partName;
         
        [StructuresField("profileColumns")]
        public string profileColumns;
        [StructuresField("profileTopChord")]
        public string profileTopChord;

        #endregion
    }
}
