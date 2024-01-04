using System;
using System.Collections.Generic;
using System.Linq;

using DataLayer;

namespace LogicLayer
{
    internal class BranchOfficeLogic : Interfaces.IOfficeBranchLogic
    {
        #region BranchOfficeLogicSingleton
        private static BranchOfficeLogic _instance = null;
        private BranchOfficeLogic() { }
        public static BranchOfficeLogic GetInstance()
        {
            if (_instance == null)
            {
                _instance = new BranchOfficeLogic();
            }
            return _instance;
        }
        #endregion

        public BranchOffices OfficeSearch(int ID)
        {
            return DbContextSingleton.TransporteContext.BranchOffices.Where(b => b.IdOffice == ID && b.Active == true).FirstOrDefault();
        }
        public void OAdd(BranchOffices br)
        {
            try
            {
                Validations.BranchOValidation(br);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                DbContextSingleton.TransporteContext.BranchOffices.Add(br);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(br).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                    throw new Exception("Ocurrio un error en BD al intentar agregar la nueva oficina, por favor intente nuevamente.");
                else
                    throw ex;
            }
        }
        public void OModify(BranchOffices br)
        {
            BranchOffices modBr = null;
            try
            {
                modBr = DbContextSingleton.TransporteContext.BranchOffices.Where(b => b.IdOffice == br.IdOffice && b.Active == true).FirstOrDefault(); ;
                if (modBr != null)
                {
                    /*modBr.Latitude = br.Latitude;
                    modBr.Longitude = br.Longitude;*/
                    modBr.BranchZone = br.BranchZone;
                    modBr.Phone = br.Phone;
                    modBr.OpTime = br.OpTime;
                    modBr.CloseTime = br.CloseTime;
                    modBr.Active = true;
                    Validations.BranchOValidation(modBr);
                    DbContextSingleton.TransporteContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ODelete(int br)
        {
            try
            {
                System.Data.SqlClient.SqlParameter _id = new System.Data.SqlClient.SqlParameter("@ID", br);
                System.Data.SqlClient.SqlParameter _ret = new System.Data.SqlClient.SqlParameter("@ret", System.Data.SqlDbType.Int);
                _ret.Direction = System.Data.ParameterDirection.Output;
                DbContextSingleton.TransporteContext.Database.ExecuteSqlCommand("exec DeleteOffice @ID, @ret output", _id, _ret);

                if ((int)_ret.Value == -1)
                    throw new Exception("No se encontro la oficina, por favor intente nuevamente.");
                else if ((int)_ret.Value == -2)
                    throw new Exception("Ocurrio un error interno al realizar la baja, por favor intente nuevamente mas tarde.");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BranchOffices> OfficeList()
        {
            List<BranchOffices> Offices = (from B in DbContextSingleton.TransporteContext.BranchOffices
                                           where B.Active == true
                                           select B).ToList();

            return Offices;
        }
    }
}
