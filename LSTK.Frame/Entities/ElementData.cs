using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.Entities
{
    public class ElementData
    {
        public Point StartPointLeft { get; set; }
        public Point EndPointLeft { get; set; }
        public Point StartPointRight { get; set; }
        public Point EndPointRight { get; set; }

        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public string Profile { get; set; }
        public string PartName { get; set; }
        public string Material { get; set; }
        public string Class { get; set; }
        public string Position { get; set; }
    }
}
