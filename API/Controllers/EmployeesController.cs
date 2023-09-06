using System.Collections.Generic;
using System.Web.Http;
using DataLayer;
using LogicLayer;
using System;
using API.Models;
using API.Validations;

namespace API.Controllers
{
    [RoutePrefix("api/employees")]
    public class EmployeesController : ApiController
    {
        #region Login

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IHttpActionResult login(LoginModel us)
        {
            string token;
            try
            {
                Employees log = LogicFactory.GetEmployeeLogic().EmpLogin(us.user, us.password);
                if (log != null)
                {
                    token = TokenValidations.JWTGenerator(log);
                    LoginModel ret = new LoginModel();
                    ret.name = log.EmpName + ' ' + log.EmpLastName;
                    ret.role = log.EmpRole;
                    ret.token = token;
                    return Ok(ret);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrió un error al realizar el logueo: " + ex.Message);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult verAuth(string token)
        {
            try
            {
                Employees temp = new Employees();
                if (token != "" && token != null)
                {
                    temp = TokenValidations.getClaimsFromToken(token);
                    if (temp != null)
                    {
                        Employees verify = LogicFactory.GetEmployeeLogic().ESearch(temp.ID);

                        if (verify != null && verify.EmpRole == temp.EmpRole)
                        {
                            LoginModel ret = new LoginModel();
                            ret.name = verify.EmpName + ' ' + verify.EmpLastName;
                            ret.role = verify.EmpRole;
                            return Ok(ret);
                        }
                        else
                            return NotFound();
                    }
                    else
                        return NotFound();
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region CRUD
        [HttpPost]
        public IHttpActionResult add(Employees Emp)
        {

            try
            {
                Employees AddedEmp = LogicFactory.GetEmployeeLogic().ESearch(Emp.ID);
                if (AddedEmp != null)
                    return BadRequest("El empleado que esta intentando agregar ya existe en el sistema.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetEmployeeLogic().EAdd(Emp);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult modify(Employees Emp)
        {
            try
            {
                Employees AddedEmp = LogicFactory.GetEmployeeLogic().ESearch(Emp.ID);
                if (AddedEmp == null)
                    return BadRequest("El empleado que esta intentando modificar no existe en el sistema.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetEmployeeLogic().EModify(Emp);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult delete(int emp)
        {
            try
            {
                LogicFactory.GetEmployeeLogic().EDelete(emp);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}