using LSTK.Frame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.Models
{
    public class ElementDataPrototype
    {
        public ElementGroupType ElementGroupType { get; set; }
        public ElementSideType ElementSideType { get; set; }

        public int AttributeGroupId { get; set; }
        public AttributeGroup AttributeGroup { get; set; }

        public Point StartPoint { get; set; } = new Point();
        public Point EndPoint { get; set; } = new Point();
    }
}
