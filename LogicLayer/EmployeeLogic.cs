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
            Employees emp = DbContextSingleton.TransporteContext.Employees.Where(u => u.EmpUser == user.Trim() && u.EmpPassword == pass.Trim()).FirstOrDefault();
            emp.Licenses = LogicFactory.GetLicenceLogic().LSearch(emp.Licence);
            return emp;
        }
        public Employees ESearch(int id)
        {
            Employees emp = DbContextSingleton.TransporteContext.Employees.Where(u => u.ID == id).FirstOrDefault();
            emp.Licenses = LogicFactory.GetLicenceLogic().LSearch(emp.Licence);
            return emp;
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
                DbContextSingleton.TransporteContext.Employees.Add(emp);
                DbContextSingleton.TransporteContext.SaveChanges();
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
                modEmp = DbContextSingleton.TransporteContext.Employees.Where(u => u.ID == emp.ID).FirstOrDefault();
                modEmp.EmpPassword = emp.EmpPassword;
                modEmp.EmpName = emp.EmpName;
                modEmp.EmpLastName = emp.EmpLastName;
                modEmp.DateOfBirth = emp.DateOfBirth;
                modEmp.Celphone = emp.Celphone;
                modEmp.EmpAddress = emp.EmpAddress;
                modEmp.Roles = emp.Roles;
                modEmp.Licence = emp.Licence;
                Validations.EmpValidation(modEmp);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EDelete(Employees emp)
        {
            try
            {
                System.Data.SqlClient.SqlParameter _usu = new System.Data.SqlClient.SqlParameter("@empID", emp.ID);
                System.Data.SqlClient.SqlParameter _return = new System.Data.SqlClient.SqlParameter("@ret", System.Data.SqlDbType.Int);
                _return.Direction = System.Data.ParameterDirection.Output;
                DbContextSingleton.TransporteContext.Database.ExecuteSqlCommand("exec DeleteEmployee @empID, @ret output", _usu, _return);

                if ((int)_return.Value == -1)
                    throw new Exception("No se encontro el usuario, por favor intente nuevamente.");
                else if ((int)_return.Value == -2)
                    throw new Exception("El usuario tiene envíos asociados en el sistema, no se puede realizar la baja.");
                else if ((int)_return.Value == -3)
                    throw new Exception("Ocurrio un error interno al realizar la baja, por favor intente nuevamente mas tarde.");
                else
                {
                    DbContextSingleton.TransporteContext.Entry(emp).State = System.Data.Entity.EntityState.Detached;
                    DbContextSingleton.TransporteContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}