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
    [RoutePrefix("/api/licences")]
    public class LicencesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult find(string cat)
        {
            try
            {
                Licences l = LogicFactory.GetLicenceLogic().LSearch(cat);
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
        public IHttpActionResult add(Licences li)
        {

            try
            {
                Licences Lic = LogicFactory.GetLicenceLogic().LSearch(li.Category);
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
        public IHttpActionResult modify(Licences li)
        {
            try
            {
                Licences lic = LogicFactory.GetLicenceLogic().LSearch(li.Category);
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
        public IHttpActionResult delete(string cat)
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
        public IHttpActionResult list()
        {
            try
            {
                List<Licences> l = LogicFactory.GetLicenceLogic().LicenceList();
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