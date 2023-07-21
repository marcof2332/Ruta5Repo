using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IZonesLogic
    {
        Zones ZoneSearch(int id);
        void ZAdd(string name, Cities city);
        void ZModify(string name, Cities city);
        void ZDelete(int id);
        List<Zones> ZoneListbyCity(Cities city);
    }
}
