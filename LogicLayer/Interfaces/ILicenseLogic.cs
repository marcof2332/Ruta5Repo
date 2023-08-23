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
        Licences LSearch(string cat);
        void LAdd(Licences li); //(string cat, string desc, int weight);
        void LModify(Licences li);
        void LDelete(string li);
        List<Licences> LicenceList();
    }
}
