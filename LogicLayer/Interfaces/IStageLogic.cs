using System.Collections.Generic;
using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IStageLogic
    {
        Stages StSearch(int ID);
        void StAdd(Stages St);
        void StModify(Stages St);
        void StDelete(int ID);
        List<Stages> StList();
    }
}
