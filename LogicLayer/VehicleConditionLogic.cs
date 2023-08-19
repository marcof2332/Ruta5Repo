using System;
using System.Collections.Generic;
using System.Linq;

using DataLayer;

namespace LogicLayer
{
    internal class VehicleConditionLogic : Interfaces.IVehicleConditionLogic
    {
        #region VehicleConditionLogicSingleton
        private static VehicleConditionLogic _instance = null;
        private VehicleConditionLogic() { }
        public static VehicleConditionLogic GetInstance()
        {
            if (_instance == null)
            {
                _instance = new VehicleConditionLogic();
            }
            return _instance;
        }
        #endregion

        #region Methods
        public VehiclesCondition VCSearch(int id)
        {
            return DbContextSingleton.TransporteContext.VehiclesCondition.Where(vc => vc.IdVC == id).FirstOrDefault();
        }
        public void VCAdd(VehiclesCondition VC)
        {
            try
            {
                Validations.VConditionValidation(VC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                DbContextSingleton.TransporteContext.VehiclesCondition.Add(VC);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(VC).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrio un error en BD al intentar agregar la informacion, por favor intente nuevamente.");
                }
                else
                    throw ex;
            }
        }
        public void VCModify(VehiclesCondition VC)
        {
            VehiclesCondition modVC = null;
            try
            {
                modVC = VCSearch(VC.IdVC);
                modVC.CondName = VC.CondName;
                Validations.VConditionValidation(modVC);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void VCDelete(int ID)
        {
            try
            {
                System.Data.SqlClient.SqlParameter _usu = new System.Data.SqlClient.SqlParameter("@ID", ID);
                System.Data.SqlClient.SqlParameter _return = new System.Data.SqlClient.SqlParameter("@ret", System.Data.SqlDbType.Int);
                _return.Direction = System.Data.ParameterDirection.Output;
                DbContextSingleton.TransporteContext.Database.ExecuteSqlCommand("exec DeleteVCondition @ID, @ret output", _usu, _return);

                if ((int)_return.Value == -1)
                    throw new Exception("No se encontro el estado de vehiculo en el sistema, por favor intente nuevamente.");
                else if ((int)_return.Value == -2)
                    throw new Exception("El estado tiene Vehiculos asignados en el sistema, no se puede realizar la baja.");
                else if ((int)_return.Value == -3)
                    throw new Exception("Ocurrio un error interno al realizar la baja, por favor intente nuevamente mas tarde.");
                else
                    DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<VehiclesCondition> VCList()
        {
            return (DbContextSingleton.TransporteContext.VehiclesCondition.ToList());
        }
        #endregion
    }
}
