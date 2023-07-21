using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IShippingStatusLogic
    {
        Stages SSSearch(int id);
        void SSAdd(string desc);
        void SSModify(string desc);
        void SSDelete(int id);
        List<Stages> SSList();
    }
}
