using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface ILicenseLogic
    {
        Licenses LSearch(string cat);
        void LAdd(Licenses li); //(string cat, string desc, int weight);
        void SSModify(Licenses li);
        void LDelete(Licenses li);
        List<Licenses> LicenceList();
    }
}
