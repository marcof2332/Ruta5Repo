using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IRoleLogic
    {
        Roles RSearch(string code);
        void RAdd(Roles ro);
        void RDelete(Roles ro);
        void RModify(Roles ro);
        List<Roles> RList();
    }
}
