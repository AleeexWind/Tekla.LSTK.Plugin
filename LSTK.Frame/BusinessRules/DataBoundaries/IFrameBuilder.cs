using LSTK.Frame.BusinessRules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.DataBoundaries
{
    public interface IFrameBuilder
    {
        bool BuildFrame(FrameBuildInputData frameBuildInputData);
    }
}
