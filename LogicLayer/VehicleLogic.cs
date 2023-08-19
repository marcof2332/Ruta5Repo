using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;

namespace LogicLayer
{
    internal class VehicleLogic : Interfaces.IVehicleLogic
    {
        #region VehicleLogicSingleton
        private static VehicleLogic _instance = null;
        private VehicleLogic() { }
        public static VehicleLogic GetInstance()
        {
            if (_instance == null)
            {
                _instance = new VehicleLogic();
            }
            return _instance;
        }
        #endregion

        public Vehicles VSearch(int ID)
        {
            return (DbContextSingleton.TransporteContext.Vehicles.Where(v => v.IdVehicle == ID && v.Active == true).FirstOrDefault());
        }
        public void VAdd(Vehicles V)
        {
            try
            {
                Validations.VehicleValidation(V);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                Vehicles VDeleted = DbContextSingleton.TransporteContext.Vehicles.Where(v => v.Plate == V.Plate && v.Active == false).FirstOrDefault();
                if (VDeleted != null)
                {
                    VDeleted.BrandModel = V.BrandModel;
                    VDeleted.Condition = V.Condition;
                    VDeleted.VehicleWeight = V.VehicleWeight;
                    VDeleted.vRegistration = V.vRegistration;                    
                    VDeleted.Active = true;
                    DbContextSingleton.TransporteContext.SaveChanges();
                }
                else
                {
                    DbContextSingleton.TransporteContext.Vehicles.Add(V);
                    DbContextSingleton.TransporteContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(V).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrio un error en BD al intentar agregar el vehiculo, por favor intente nuevamente.");
                }
                else
                    throw ex;
            }
        }
        public void VModify(Vehicles V)
        {
            Vehicles modV = null;
            try
            {
                modV = DbContextSingleton.TransporteContext.Vehicles.Where(v => v.IdVehicle == V.IdVehicle && v.Active == true).FirstOrDefault();
                modV.Condition = V.Condition;
                modV.Plate = V.Plate;
                Validations.VehicleValidation(modV);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void VDelete(int ID)
        {
            try
            {
                System.Data.SqlClient.SqlParameter _cat = new System.Data.SqlClient.SqlParameter("@ID", ID);
                System.Data.SqlClient.SqlParameter _ret = new System.Data.SqlClient.SqlParameter("@ret", System.Data.SqlDbType.Int);
                _ret.Direction = System.Data.ParameterDirection.Output;
                DbContextSingleton.TransporteContext.Database.ExecuteSqlCommand("exec DeleteVehicle @Cat, @ret output", _cat, _ret);

                if ((int)_ret.Value == -1)
                    throw new Exception("No se encontro el vehiculo, por favor intente nuevamente.");
                else if ((int)_ret.Value == -2)
                    throw new Exception("Ocurrio un error interno al realizar la baja, por favor intente nuevamente mas tarde.");
                else
                    DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Vehicles> VicenceList()
        {
            return (DbContextSingleton.TransporteContext.Vehicles.Where(v => v.Active == true).ToList());
        }
    }
}
