using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IClientLogic
    {
        Customers CSearch(int id);
        void CAdd(string name, string lastName, string celphone, Zones zone, string adress);
        void CModify(string celphone, Zones zone, string adress);
        void CDelete(int id);
        List<Customers> CList();
    }
}
