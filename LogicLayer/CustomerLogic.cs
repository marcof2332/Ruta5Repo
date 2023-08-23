using System;
using System.Collections.Generic;
using System.Linq;

using DataLayer;

namespace LogicLayer
{
    internal class CustomerLogic: Interfaces.ICustomerLogic
    {
        #region CustomerLogicSingleton
        private static CustomerLogic _instance = null;
        private CustomerLogic() { }
        public static CustomerLogic GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CustomerLogic();
            }
            return _instance;
        }
        #endregion

        public Customers CSearch(long ID)
        {
            return (DbContextSingleton.TransporteContext.Customers.Where(c => c.DocRut == ID).FirstOrDefault());
        }
        public void CAdd(Customers c)
        {
            try
            {
                Validations.CustomersValidation(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                DbContextSingleton.TransporteContext.Customers.Add(c);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(c).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrio un error en BD al intentar agregar el cliente, por favor intente nuevamente.");
                }
                else
                    throw ex;
            }
        }
        public void CModify(Customers c)
        {
            Customers modC = null;
            try
            {
                modC = DbContextSingleton.TransporteContext.Customers.Where(cu => cu.DocRut == c.DocRut).FirstOrDefault();
                modC.CliAddress = c.CliAddress;
                modC.Celphone = c.Celphone;
                modC.CustomerName = c.CustomerName;
                modC.CLastName = c.CLastName;
                modC.ClientZone = c.ClientZone;
                Validations.CustomersValidation(modC);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Customers> CustomerList()
        {
            return (DbContextSingleton.TransporteContext.Customers.ToList());
        }
    }
}
