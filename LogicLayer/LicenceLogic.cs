using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer
{
    internal class LicenceLogic : Interfaces.ILicenseLogic
    {
        #region LicenceLogicSingleton
        private static LicenceLogic _instance = null;
        private LicenceLogic() { }
        public static LicenceLogic GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LicenceLogic();
            }
            return _instance;
        }
        #endregion

        public Licences LSearch(string cat)
        {
            return (DbContextSingleton.TransporteContext.Licences.Where(l => l.Category == cat).FirstOrDefault());
        }
        public void LAdd(Licences li)
        {
            try
            {
                Validations.LicenseValidation(li);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                DbContextSingleton.TransporteContext.Licences.Add(li);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(li).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrio un error en BD al intentar agregar la licencia, por favor intente nuevamente.");
                }
                else
                    throw ex;
            }
        }
        public void LModify(Licences li)
        {
            Licences modLi = null;
            try
            {
                modLi = DbContextSingleton.TransporteContext.Licences.Where(u => u.Category == li.Category).FirstOrDefault();
                modLi.LicenceDescription = li.LicenceDescription;
                modLi.Capacity = li.Capacity;
                Validations.LicenseValidation(modLi);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LDelete(string li)
        {
            try
            {
                Licences licence = DbContextSingleton.TransporteContext.Licences.Where(l => l.Category == li).FirstOrDefault();

                System.Data.SqlClient.SqlParameter _cat = new System.Data.SqlClient.SqlParameter("@Cat", li);
                System.Data.SqlClient.SqlParameter _ret = new System.Data.SqlClient.SqlParameter("@ret", System.Data.SqlDbType.Int);
                _ret.Direction = System.Data.ParameterDirection.Output;
                DbContextSingleton.TransporteContext.Database.ExecuteSqlCommand("exec DeleteLicence @Cat, @ret output", _cat, _ret);

                if ((int)_ret.Value == -1)
                    throw new Exception("No se encontro la licencia, por favor intente nuevamente.");
                else if ((int)_ret.Value == -2)
                    throw new Exception("La licencia tiene empleados asociados en el sistema, no se puede realizar la baja.");
                else if ((int)_ret.Value == -3)
                    throw new Exception("Ocurrio un error interno al realizar la baja, por favor intente nuevamente mas tarde.");
                else
                    DbContextSingleton.TransporteContext.Entry(licence).State = System.Data.Entity.EntityState.Detached;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Licences> LicenceList()
        {
            try
            {
                using (var dbContext = new TransportEntities()) 
                {
                    return (DbContextSingleton.TransporteContext.Licences.ToList());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
