using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IStateLogic
    {
        States StateSearch(int id);
        void SAdd(States st);
        void SModify(States st);
        void SDelete(int st);
        List<States> StateList();
    }
}
