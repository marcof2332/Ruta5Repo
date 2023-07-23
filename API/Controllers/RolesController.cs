using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DataLayer;
using LogicLayer;

namespace API.Controllers
{
    //[Route("api/[controller]")]
    [RoutePrefix("api/Roles")]
    public class RolesController : ApiController
    {
        [HttpGet]
        [Route("GetRol")]
        public IHttpActionResult GetRol(string code)
        {
            try
            {
                Roles r = LogicFactory.GetRolesLogic().RSearch(code);
                if (r != null)
                    return Ok(r);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrió un error al buscar el rol: " + ex.Message);
            }
        }
        [HttpPost]
        [Route("RAdd")]
        public IHttpActionResult RAdd([FromBody] Roles ro)
        {

            try
            {
                Roles r = LogicFactory.GetRolesLogic().RSearch(ro.Code);
                if (r != null)
                    return BadRequest("El rol que esta intentando agregar ya existe en el sistema.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetRolesLogic().RAdd(ro);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("RModify")]
        public IHttpActionResult RModify(Roles ro)
        {
            try
            {
                Roles r = LogicFactory.GetRolesLogic().RSearch(ro.Code);
                if (r == null)
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetRolesLogic().RModify(ro);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("RDelete")]
        public IHttpActionResult RDelete(string ro)
        {
            try
            {
                LogicFactory.GetRolesLogic().RDelete(ro);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}