using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;

namespace LogicLayer
{
    internal class StageLogic : Interfaces.IStageLogic
    {
        #region StageLogicSingleton
        private static StageLogic _instance = null;
        private StageLogic() { }
        public static StageLogic GetInstance()
        {
            if (_instance == null)
            {
                _instance = new StageLogic();
            }
            return _instance;
        }
        #endregion

        public Stages StSearch(int ID)
        {
            return (DbContextSingleton.TransporteContext.Stages.Where(s => s.IdSStage == ID).FirstOrDefault());
        }
        public void StAdd(Stages St)
        {
            try
            {
                Validations.StageValidation(St);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                DbContextSingleton.TransporteContext.Stages.Add(St);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(St).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrio un error en BD al intentar agregar la Etapa, por favor intente nuevamente.");
                }
                else
                    throw ex;
            }
        }
        public void StModify(Stages St)
        {
            Stages modSt = null;
            try
            {
                modSt = DbContextSingleton.TransporteContext.Stages.Where(s => s.IdSStage == St.IdSStage).FirstOrDefault();
                modSt.StageDescription = St.StageDescription;
                Validations.StageValidation(modSt);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void StDelete(int ID)
        {
            try
            {
                Stages var = DbContextSingleton.TransporteContext.Stages.Where(s => s.IdSStage == ID).FirstOrDefault();

                System.Data.SqlClient.SqlParameter _cat = new System.Data.SqlClient.SqlParameter("@ID", ID);
                System.Data.SqlClient.SqlParameter _ret = new System.Data.SqlClient.SqlParameter("@ret", System.Data.SqlDbType.Int);
                _ret.Direction = System.Data.ParameterDirection.Output;
                DbContextSingleton.TransporteContext.Database.ExecuteSqlCommand("exec DeleteSStage @ID, @ret output", _cat, _ret);

                if ((int)_ret.Value == -1)
                    throw new Exception("No se encontro la Etapa solicitada, por favor intente nuevamente.");
                else if ((int)_ret.Value == -2)
                    throw new Exception("La Etapa tiene pedidos asociados en el sistema, no se puede realizar la baja.");
                else if ((int)_ret.Value == -3)
                    throw new Exception("Ocurrio un error interno al realizar la baja, por favor intente nuevamente mas tarde.");
                else
                    DbContextSingleton.TransporteContext.Entry(var).State = System.Data.Entity.EntityState.Detached;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Stages> StList()
        {
            return (DbContextSingleton.TransporteContext.Stages.ToList());
        }
    }
}
