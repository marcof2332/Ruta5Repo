using DataLayer;
using System.Collections.Generic;

namespace LogicLayer.Interfaces
{
    public interface IPackageLogic
    {
        Packages PSearch(int ID);
        Packages PAdd(Packages p);

        List<Packages> PAddMany(List<Packages> p);

        List<Packages> PList();
    }
}
