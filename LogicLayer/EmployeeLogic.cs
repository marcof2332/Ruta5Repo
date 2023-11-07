using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;
using System.Data.SqlClient;

namespace LogicLayer
{
    internal class EmployeeLogic : Interfaces.IEmployeeLogic
    {
        #region EmployeeLogicSingleton
        private static EmployeeLogic _instance = null;
        private EmployeeLogic() { }
        public static EmployeeLogic GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EmployeeLogic();
            }
            return _instance;
        }
        #endregion

        #region Methods
        public Employees EmpLogin(string user, string pass)
        {
            return DbContextSingleton.TransporteContext.Employees.Where(u => u.EmpUser == user.Trim() && u.EmpPassword == pass.Trim() && u.Active == true).FirstOrDefault();
        }
        public Employees ESearch(int id)
        {
            using (var dbContext = new TransportEntities())
            {
                return DbContextSingleton.TransporteContext.Employees.Where(u => u.ID == id && u.Active == true).FirstOrDefault();
            }
        }
        public void EAdd(Employees emp)
        {
            try
            {
                Validations.EmpValidation(emp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                Employees EDeleted = DbContextSingleton.TransporteContext.Employees.Where(e => e.ID == emp.ID && e.Active == false).FirstOrDefault();
                if (EDeleted != null)
                {
                    EDeleted.DateOfBirth = emp.DateOfBirth;
                    EDeleted.EmpName = emp.EmpName;
                    EDeleted.EmpLastName = emp.EmpLastName;
                    EDeleted.EmpAddress = emp.EmpAddress;
                    EDeleted.Celphone = emp.Celphone;
                    EDeleted.EmpUser = emp.EmpUser;
                    EDeleted.EmpPassword = emp.EmpPassword;
                    EDeleted.EmpRole = emp.EmpRole;
                    EDeleted.Licence = emp.Licence;
                    EDeleted.Active = true;
                    DbContextSingleton.TransporteContext.SaveChanges();
                }
                else
                {
                    emp.Active = true;
                    DbContextSingleton.TransporteContext.Employees.Add(emp);
                    DbContextSingleton.TransporteContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(emp).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrio un error en BD al intentar agregar el usuario, por favor intente nuevamente.");
                }
                else
                    throw ex;
            }
        }
        public void EModify(Employees emp)
        {
            Employees modEmp = null;
            try
            {
                modEmp = DbContextSingleton.TransporteContext.Employees.Where(u => u.ID == emp.ID && u.Active == true).FirstOrDefault();
                modEmp.EmpPassword = emp.EmpPassword;
                modEmp.EmpName = emp.EmpName;
                modEmp.EmpLastName = emp.EmpLastName;
                modEmp.DateOfBirth = emp.DateOfBirth;
                modEmp.Celphone = emp.Celphone;
                modEmp.EmpAddress = emp.EmpAddress;
                modEmp.EmpRole = emp.EmpRole;
                modEmp.Licence = emp.Licence;
                Validations.EmpValidation(modEmp);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EDelete(int Emp)
        {
            try
            {
                Employees var = DbContextSingleton.TransporteContext.Employees.Where(u => u.ID == Emp && u.Active == true).FirstOrDefault();

                System.Data.SqlClient.SqlParameter _usu = new System.Data.SqlClient.SqlParameter("@empID", Emp);
                System.Data.SqlClient.SqlParameter _return = new System.Data.SqlClient.SqlParameter("@ret", System.Data.SqlDbType.Int);
                _return.Direction = System.Data.ParameterDirection.Output;
                DbContextSingleton.TransporteContext.Database.ExecuteSqlCommand("exec DeleteEmployee @empID, @ret output", _usu, _return);

                if ((int)_return.Value == -1)
                    throw new Exception("No se encontro el usuario, por favor intente nuevamente.");
                else if ((int)_return.Value == -2)
                    throw new Exception("Ocurrio un error interno al realizar la baja, por favor intente nuevamente mas tarde.");
                else
                    DbContextSingleton.TransporteContext.Entry(var).State = System.Data.Entity.EntityState.Detached;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Employees> EList()
        {
            try
            {
                using (var dbContext = new TransportEntities())
                {
                    return DbContextSingleton.TransporteContext.Employees.Where(u => u.Active == true).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}