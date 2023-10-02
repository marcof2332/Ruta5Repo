using API.Models;
using DataLayer;
using LogicLayer;
using System;
using System.Collections.Generic;
using API.Models;
using API.Validations;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/session")]
    public class SessionController : ApiController
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

        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult verAuth(LoginModel us)
        {
            try
            {
                Employees temp = new Employees();
                if (us.token != "" && us != null)
                {
                    temp = TokenValidations.getClaimsFromToken(us.token);
                    if (temp != null)
                    {
                        Employees verify = LogicFactory.GetEmployeeLogic().ESearch(temp.ID);

                        if (verify != null)
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

    }
}