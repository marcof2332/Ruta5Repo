using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IPackageType
    {
        PackageType PTSearch(int id);
        void PTAdd(string desc, double minWeight, double maxWeight, decimal amount);
        void PTDelete(string desc, double minWeight, double maxWeight, decimal amount);
        List<PackageType> PTList();
    }
}
