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
    [RoutePrefix("api/roles")]
    public class RolesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult get(string code)
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
        public IHttpActionResult add(Roles ro)
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
        public IHttpActionResult modify(Roles ro)
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
        public IHttpActionResult delete(string ro)
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
        [HttpGet]
        public IHttpActionResult list()
        {
            try
            {
                List<Roles> r = LogicFactory.GetRolesLogic().RList();
                if (r != null)
                    return Ok(r);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrió un error al listar los roles: " + ex.Message);
            }
        }
    }
}