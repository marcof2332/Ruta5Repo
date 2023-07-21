using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer
{
    internal class StatesLogic: Interfaces.IStateLogic
    {
        #region StatesLogicSingleton
        private static StatesLogic _instance = null;
        private StatesLogic() { }
        public static StatesLogic GetInstance()
        {
            if (_instance == null)
            {
                _instance = new StatesLogic();
            }
            return _instance;
        }
        #endregion

        #region Methods
        public States StateSearch(int id)
        {
            return (DbContextSingleton.TransporteContext.States.Where(s => s.IdState == id).FirstOrDefault());

        }
        public void SAdd(States st) 
        {
            try
            {
                Validations.StateValidation(st);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                DbContextSingleton.TransporteContext.States.Add(st);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(st).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrio un error en BD al intentar agregar el nuevo Departamento, por favor intente nuevamente.");
                }
                else
                    throw ex;
            }
        }
        public void SModify(States st) 
        {
            States modSt = null;
            try
            {
                modSt = DbContextSingleton.TransporteContext.States.Where(s => s.IdState == st.IdState).FirstOrDefault();
                modSt.StateName = st.StateName;
                Validations.StateValidation(modSt);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SDelete(States st)
        {
            try
            {
                System.Data.SqlClient.SqlParameter _id = new System.Data.SqlClient.SqlParameter("@ID", st.IdState);
                System.Data.SqlClient.SqlParameter _ret = new System.Data.SqlClient.SqlParameter("@ret", System.Data.SqlDbType.Int);
                _ret.Direction = System.Data.ParameterDirection.Output;
                DbContextSingleton.TransporteContext.Database.ExecuteSqlCommand("exec DeleteState @ID, @ret output", _id, _ret);

                if ((int)_ret.Value == -1)
                    throw new Exception("No se encontro el departamento, por favor intente nuevamente.");
                else if ((int)_ret.Value == -2)
                    throw new Exception("El departamento tiene ciudades asignadas en el sistema, no se puede realizar la baja.");
                else if ((int)_ret.Value == -3)
                    throw new Exception("Ocurrio un error interno al realizar la baja, por favor intente nuevamente mas tarde.");
                else
                {
                    DbContextSingleton.TransporteContext.Entry(st).State = System.Data.Entity.EntityState.Detached;
                    DbContextSingleton.TransporteContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<States> StateList()
        {
            return (DbContextSingleton.TransporteContext.States.ToList());
        }
        #endregion
    }
}
