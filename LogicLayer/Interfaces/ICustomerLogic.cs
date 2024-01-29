using System.Collections.Generic;
using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface ICustomerLogic
    {
        Customers CSearch(string ID);
        void CAdd(Customers c);
        void CModify(Customers c);
        List<Customers> CustomerList();
    }
}
