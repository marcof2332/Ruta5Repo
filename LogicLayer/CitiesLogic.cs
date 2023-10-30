using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer
{
    internal class CitiesLogic: Interfaces.ICitiesLogic
    {
        #region CityLogicSingleton
        private static CitiesLogic _instance = null;
        private CitiesLogic() { }
        public static CitiesLogic GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CitiesLogic();
            }
            return _instance;
        }
        #endregion

        public Cities CitySearch(int id)
        {
            Cities ct = DbContextSingleton.TransporteContext.Cities.Where(s => s.IdCity == id).FirstOrDefault();
            return ct;
        }
        public void CAdd(Cities ct)
        {
            try
            {
                Validations.CityValidation(ct);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                DbContextSingleton.TransporteContext.Cities.Add(ct);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(ct).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrio un error en BD al intentar agregar la nueva ciudad, por favor intente nuevamente.");
                }
                else
                    throw ex;
            }
        }
        public void CModify(Cities ct)
        {
            Cities modCt = null;
            try
            {
                modCt = CitySearch(ct.IdCity);
                modCt.CityName = ct.CityName;
                Validations.CityValidation(modCt);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CDelete(int ct)
        {
            try
            {
                Cities var = DbContextSingleton.TransporteContext.Cities.Where(s => s.IdCity == ct).FirstOrDefault();

                System.Data.SqlClient.SqlParameter _id = new System.Data.SqlClient.SqlParameter("@ID", ct);
                System.Data.SqlClient.SqlParameter _ret = new System.Data.SqlClient.SqlParameter("@ret", System.Data.SqlDbType.Int);
                _ret.Direction = System.Data.ParameterDirection.Output;
                DbContextSingleton.TransporteContext.Database.ExecuteSqlCommand("exec DeleteCity @ID, @ret output", _id, _ret);

                if ((int)_ret.Value == -1)
                    throw new Exception("No se encontro la ciudad, por favor intente nuevamente.");
                else if ((int)_ret.Value == -2)
                    throw new Exception("La ciudad tiene zonas asignadas en el sistema, no se puede realizar la baja.");
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
        public List<Cities> CitesList()
        {
            List<Cities> cities = (from C in DbContextSingleton.TransporteContext.Cities
                                   orderby C.CityState descending
                                   select C).ToList();

            return cities;
        }
    }
}
