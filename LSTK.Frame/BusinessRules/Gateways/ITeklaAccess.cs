using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.BusinessRules.Gateways
{
    public interface ITeklaAccess
    {
        bool CreateLeftColumn();
        bool CreateRightColumn();
    }
}
