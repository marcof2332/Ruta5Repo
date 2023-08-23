using System.Collections.Generic;

using System.Web.Http;

using DataLayer;
using LogicLayer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;

namespace API.Controllers
{
    [RoutePrefix("/api/employees")]
    public class EmployeesController : ApiController
    {
        #region Login
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult login(string user, string password)
        {
            string token;
            try
            {
                Employees log = LogicFactory.GetEmployeeLogic().EmpLogin(user, password);
                if (log != null)
                {
                    token = JWTGenerator(log);
                    return Ok(token);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrió un error al realizar el logueo: " + ex.Message);
            }
        }

        // GENERAMOS EL TOKEN CON LA INFORMACIÓN DEL USUARIO
        private string JWTGenerator(Employees userInfo)
        {
            // CREAMOS EL HEADER //
            var key = Convert.ToBase64String(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JWT:ClaveSecreta"]));
            var _symmetricSecurityKey = new SymmetricSecurityKey(Convert.FromBase64String(key));

            //var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JWT:ClaveSecreta"]));
            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var _Header = new JwtHeader(_signingCredentials);

            // CREAMOS LOS CLAIMS //
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, userInfo.ID.ToString()),
                new Claim("role", userInfo.EmpRole),
                new Claim("user", userInfo.EmpUser),
            };

            // CREAMOS EL PAYLOAD //
            var _Payload = new JwtPayload(
                    issuer: ConfigurationManager.AppSettings["JWT:Issuer"],
                    audience: ConfigurationManager.AppSettings["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    // Exipra a la 8 horas.
                    expires: DateTime.UtcNow.AddHours(8)
                );

            // GENERAMOS EL TOKEN //
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );
            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }

        private Employees getClaimsFromToken(string token)
        {
            Employees e = new Employees();
            try
            {
                var key = Convert.ToBase64String(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JWT:ClaveSecreta"]));
                var _symmetricSecurityKey = new SymmetricSecurityKey(Convert.FromBase64String(key));

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _symmetricSecurityKey,
                    ValidateIssuer = true,
                    ValidIssuer = ConfigurationManager.AppSettings["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = ConfigurationManager.AppSettings["JWT:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromHours(8)
                };
            }
            catch (SecurityTokenException ex)
            {
                throw new Exception(ex.Message);
            }
            try 
            { 
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                var claimID = securityToken.Claims.FirstOrDefault(c => c.Type == "NameId")?.Value;
                var claimUs = securityToken.Claims.FirstOrDefault(c => c.Type == "EmpUser")?.Value;
                var claimRole = securityToken.Claims.FirstOrDefault(c => c.Type == "EmpRole")?.Value;
                e.ID = Convert.ToInt32(claimID);
                e.EmpUser = claimUs;
                e.EmpRole = claimRole;
                return e;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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