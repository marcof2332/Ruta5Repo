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
    [RoutePrefix("api/Licenses")]
    public class LicenceController : ApiController
    {
        [HttpGet]
        [Route("GetLicence")]
        public IHttpActionResult GetLicence(string cat)
        {
            try
            {
                Licenses l = LogicFactory.GetLicenceLogic().LSearch(cat);
                if (l != null)
                    return Ok(l);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrió un error al buscar la licencia: " + ex.Message);
            }
        }
        [HttpPost]
        [Route("LAdd")]
        public IHttpActionResult LAdd(Licenses li)
        {

            try
            {
                Licenses Lic = LogicFactory.GetLicenceLogic().LSearch(li.Category);
                if (Lic != null)
                    return BadRequest("La licencia que esta intentando agregar ya existe en el sistema.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetLicenceLogic().LAdd(li);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("LModify")]
        public IHttpActionResult LModify(Licenses li)
        {
            try
            {
                Licenses lic = LogicFactory.GetLicenceLogic().LSearch(li.Category);
                if (lic == null)
                    return BadRequest("La licencia que esta intentando modificar no existe en el sistema.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetLicenceLogic().LModify(li);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("LDelete")]
        public IHttpActionResult LDelete(string cat)
        {
            try
            {
                LogicFactory.GetLicenceLogic().LDelete(cat);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("LList")]
        public IHttpActionResult LList()
        {
            try
            {
                List<Licenses> l = LogicFactory.GetLicenceLogic().LicenceList();
                if (l != null)
                    return Ok(l);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrió un error al listar las licencias: " + ex.Message);
            }
        }
    }
}