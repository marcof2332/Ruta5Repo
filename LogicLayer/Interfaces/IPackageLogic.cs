using DataLayer;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface IPackageLogic
    {
        Packages PSearch(int ID);
        void PAdd(Packages p);
        List<Packages> PList();
    }
}
