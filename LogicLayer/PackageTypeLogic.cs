using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace LogicLayer
{
    internal class PackageTypeLogic : Interfaces.IPackageType
    {
        #region PackageTypeLogicSingleton
        private static PackageTypeLogic _instance = null;
        private PackageTypeLogic() { }
        public static PackageTypeLogic GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PackageTypeLogic();
            }
            return _instance;
        }
        #endregion

        public PackageType PtSearch(int ID)
        {
            return (DbContextSingleton.TransporteContext.PackageType.Where(pt => pt.IdPackageType == ID).FirstOrDefault());
        }
        public void PtAdd(PackageType pt)
        {
            try
            {
                Validations.PackageTValidation(pt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                DbContextSingleton.TransporteContext.PackageType.Add(pt);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(pt).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrio un error en BD al intentar agregar el tipo de paquete, por favor intente nuevamente.");
                }
                else
                    throw ex;
            }
        }
        public void PtModify(PackageType li)
        {
            Licenses modLi = null;
            try
            {
                modLi = DbContextSingleton.TransporteContext.Licenses.Where(u => u.Category == li.Category).FirstOrDefault();
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
        public void PtDelete(int ID)
        {
            try
            {
                System.Data.SqlClient.SqlParameter _Id = new System.Data.SqlClient.SqlParameter("@ID", ID);
                System.Data.SqlClient.SqlParameter _ret = new System.Data.SqlClient.SqlParameter("@ret", System.Data.SqlDbType.Int);
                _ret.Direction = System.Data.ParameterDirection.Output;
                DbContextSingleton.TransporteContext.Database.ExecuteSqlCommand("exec DeletePt @ID, @ret output", _Id, _ret);

                if ((int)_ret.Value == -1)
                    throw new Exception("No se encontro el el tipo de paquete, por favor intente nuevamente.");
                else if ((int)_ret.Value == -2)
                    throw new Exception("El tipo de paquete tiene paquetes asociados en el sistema, no se puede realizar la baja.");
                else if ((int)_ret.Value == -3)
                    throw new Exception("Ocurrio un error interno al realizar la baja, por favor intente nuevamente mas tarde.");
                else
                    DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PackageType> PtList()
        {
            return (DbContextSingleton.TransporteContext.PackageType.ToList());
        }
    }
}
