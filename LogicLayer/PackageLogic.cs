using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;

namespace LogicLayer
{
    internal class PackageLogic : Interfaces.IPackageLogic
    {
        #region PackageLogicSingleton
        private static PackageLogic _instance = null;
        private PackageLogic() { }
        public static PackageLogic GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PackageLogic();
            }
            return _instance;
        }
        #endregion

        public Packages PSearch(int ID)
        {
            return (DbContextSingleton.TransporteContext.Packages.Where(p => p.IdPackage == ID).FirstOrDefault());
        }
        public Packages PAdd(Packages p)
        {
            try
            {
                Validations.PackageValidation(p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                Shippments shippment = DbContextSingleton.TransporteContext.Shippments.SingleOrDefault(s => s.IdShippment == p.Shippment);
                if (shippment != null)
                {
                    bool ShippmentAssigned = DbContextSingleton.TransporteContext.ShippmentStages.Any(s => s.ShippmentID == shippment.IdShippment && s.IdShSt == 3);
                    if (ShippmentAssigned == false)
                    {
                        DbContextSingleton.TransporteContext.Packages.Add(p);
                        DbContextSingleton.TransporteContext.SaveChanges();

                        return p;
                    }
                    else
                        throw new Exception("El paquete no puede agregarse por que el envio ya fue asignado a distribución.");
                }
                else
                    throw new Exception("No se encuentra el envio solicitado al cual se desea agregar el nuevo paquete.");
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(p).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrio un error en BD al intentar agregar el paquete, por favor intente nuevamente.");
                }
                else
                    throw ex;
            }
        }

        public List<Packages> PAddMany(List<Packages> packages)
        {
            try
            {
                foreach (var package in packages)
                {
                    Validations.PackageValidation(package);
                }

                int shippmentId = packages.FirstOrDefault()?.Shippment ?? 0;

                Shippments shippment = DbContextSingleton.TransporteContext.Shippments.SingleOrDefault(s => s.IdShippment == shippmentId);

                if (shippment != null)
                {
                    bool shippmentAssigned = DbContextSingleton.TransporteContext.ShippmentStages.Any(s => s.ShippmentID == shippment.IdShippment && s.IdShSt == 3);

                    if (!shippmentAssigned)
                    {
                        foreach (var package in packages)
                        {
                            DbContextSingleton.TransporteContext.Packages.Add(package);
                        }

                        DbContextSingleton.TransporteContext.SaveChanges();

                        return packages;
                    }
                    else
                    {
                        throw new Exception("El paquete no puede agregarse porque el envío ya fue asignado a distribución.");
                    }
                }
                else
                {
                    throw new Exception("No se encuentra el envío solicitado al cual se desea agregar el nuevo paquete.");
                }
            }
            catch (Exception ex)
            {
                // Manejar las excepciones y desvincular las entidades de Entity Framework
                foreach (var package in packages)
                {
                    DbContextSingleton.TransporteContext.Entry(package).State = System.Data.Entity.EntityState.Detached;
                }

                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrió un error en la base de datos al intentar agregar el paquete, por favor intente nuevamente.");
                }
                else
                {
                    throw ex;
                }
            }
        }

        public List<Packages> PList()
        {
            return (DbContextSingleton.TransporteContext.Packages.ToList());
        }
    }
}
