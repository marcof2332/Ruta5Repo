using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace LogicLayer
{
    internal class ShippmentLogic : Interfaces.IShippmentLogic
    {
        #region ShippmentLogicSingleton
        private static ShippmentLogic _instance = null;
        private ShippmentLogic() { }
        public static ShippmentLogic GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ShippmentLogic();
            }
            return _instance;
        }
        #endregion

        #region DropOffPackageMethods
        public DropOffPackages DoPSearch(int code)
        {
            return (DbContextSingleton.TransporteContext.DropOffPackages.Where(dr => dr.IdDropOff == code).FirstOrDefault());
        }
        public void DoPAdd(DropOffPackages DoP)
        {
            try
            {
                Validations.DropOffValidation(DoP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                DoP.Shippments = DbContextSingleton.TransporteContext.Shippments.Add(DoP.Shippments);
                DbContextSingleton.TransporteContext.DropOffPackages.Add(DoP); //Posible error
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(DoP).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrio un error en BD al intentar agregar el envio, por favor intente nuevamente.");
                }
                else
                    throw ex;
            }
        }
        public void DoPDelete(int ID)
        {
            try
            {
                System.Data.SqlClient.SqlParameter _ID = new System.Data.SqlClient.SqlParameter("@ID", ID);
                System.Data.SqlClient.SqlParameter _ret = new System.Data.SqlClient.SqlParameter("@ret", System.Data.SqlDbType.Int);
                _ret.Direction = System.Data.ParameterDirection.Output;
                DbContextSingleton.TransporteContext.Database.ExecuteSqlCommand("exec DeletePoD @Code, @ret output", _ID, _ret);

                if ((int)_ret.Value == -1)
                    throw new Exception("No se encontro el envio, por favor verifique e intente nuevamente.");
                else if ((int)_ret.Value == -2)
                    throw new Exception("El envio no puede eliminarse por que ya que fue asignado a distribución.");
                else if ((int)_ret.Value == -3)
                    throw new Exception("Ocurrio un error interno al intentar eliminar, por favor intente nuevamente mas tarde.");
                else
                {
                    DbContextSingleton.TransporteContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DropOffPackages> DoPList()
        {
            return (DbContextSingleton.TransporteContext.DropOffPackages.ToList());
        }
        #endregion

        #region HomePickupMethods
        public HomePickups HpSearch(int code)
        {
            return (DbContextSingleton.TransporteContext.HomePickups.Where(hp => hp.IdHomePickup == code).FirstOrDefault());
        }
        public int HpAdd(HomePickups Hp)
        {
            try
            {
                Validations.HomePickupValidation(Hp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                Hp.PickUpLocation = DbGeography.FromText($"POINT({Hp.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)} {Hp.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)})", 4326);
                Hp.Shippments.TargetLocation = DbGeography.FromText($"POINT({Hp.Shippments.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)} {Hp.Shippments.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)})", 4326);

                DbContextSingleton.TransporteContext.Shippments.Add(Hp.Shippments);
                DbContextSingleton.TransporteContext.SaveChanges();

                Hp.ShippmentId = Hp.Shippments.IdShippment;
                
                DbContextSingleton.TransporteContext.HomePickups.Add(Hp);
                DbContextSingleton.TransporteContext.SaveChanges();

                return Hp.Shippments.IdShippment;
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(Hp).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrio un error en BD al intentar agregar el envio, por favor intente nuevamente.");
                }
                else
                    throw ex;
            }
        }
        public void HpDelete(int ID)
        {
            try
            {
                System.Data.SqlClient.SqlParameter _ID = new System.Data.SqlClient.SqlParameter("@ID", ID);
                System.Data.SqlClient.SqlParameter _ret = new System.Data.SqlClient.SqlParameter("@ret", System.Data.SqlDbType.Int);
                _ret.Direction = System.Data.ParameterDirection.Output;
                DbContextSingleton.TransporteContext.Database.ExecuteSqlCommand("exec DeleteHp @ID, @ret output", _ID, _ret);

                if ((int)_ret.Value == -1)
                    throw new Exception("No se encontro el envio, por favor verifique e intente nuevamente.");
                else if ((int)_ret.Value == -2)
                    throw new Exception("El envio no puede eliminarse por que ya que fue asignado a distribución.");
                else if ((int)_ret.Value == -3)
                    throw new Exception("Ocurrio un error interno al intentar eliminar, por favor intente nuevamente mas tarde.");
                else
                {
                    DbContextSingleton.TransporteContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<HomePickups> HpList()
        {
            return (DbContextSingleton.TransporteContext.HomePickups.ToList());
        }
        #endregion

        #region SharedMethods
        public void PackageRemoval(int PackageId)
        {
            DropOffPackages modDoP = null;
            try
            {
                Shippments shippment = DbContextSingleton.TransporteContext.Shippments.SingleOrDefault(s => s.IdShippment == modDoP.Shippments.IdShippment);

                if (shippment != null)
                {
                    DbContextSingleton.TransporteContext.Entry(shippment).Collection(s => s.Packages).Load();
                    Packages packageToRemove = shippment.Packages.SingleOrDefault(p => p.IdPackage == PackageId);

                    if (packageToRemove != null)
                    {
                        bool ShippmentAssigned = DbContextSingleton.TransporteContext.ShippmentStages.Any(s => s.IdShSt == shippment.IdShippment && s.ShippmentID == 3);
                        if (ShippmentAssigned == false)
                        {
                            shippment.Packages.Remove(packageToRemove); // Remueve el paquete de la lista
                            DbContextSingleton.TransporteContext.Packages.Remove(packageToRemove); // Remueve el paquete del contexto
                            DbContextSingleton.TransporteContext.SaveChanges(); // Guarda los cambios en la base de datos
                        }
                        else
                            throw new Exception("El paquete no puede eliminarse ya que fue asignado a distribucion.");
                    }
                    else
                        throw new Exception("El paquete que buscas eliminar no existe.");
                }
                else
                    throw new Exception("El paquete que buscas eliminar no se encuentra en ningun envio.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ShippmentStageAdd(ShippmentStages ShS)
        {
            try
            {
                Validations.ShStageValidation(ShS);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                bool ShippmentExists = DbContextSingleton.TransporteContext.Shippments.Any(s => s.IdShippment == ShS.ShippmentID);
                bool EmployeeExists = DbContextSingleton.TransporteContext.Employees.Any(e => e.ID == ShS.EmployeeID && e.Active);

                if (ShippmentExists == true && EmployeeExists == true)
                {
                    DbContextSingleton.TransporteContext.ShippmentStages.Add(ShS);
                    DbContextSingleton.TransporteContext.SaveChanges();
                }
                else
                    throw new Exception("Ocurrio un error al agregar las etapas de envio, por favor verifique y vuelva a intentarlo.");
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(ShS).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrio un error en BD al intentar agregar el envio, por favor intente nuevamente.");
                }
                else
                    throw ex;
            }
        }
        #endregion
    }
}
