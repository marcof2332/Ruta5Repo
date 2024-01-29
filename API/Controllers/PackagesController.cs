using System;
using System.Collections.Generic;
using System.Web.Http;

using DataLayer;
using LogicLayer;

namespace API.Controllers
{
    [RoutePrefix("api/package")]
    public class PackagesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult get(int id)
        {
            try
            {
                Packages pk = LogicFactory.GetPackageLogic().PSearch(id);
                if (pk != null)
                    return Ok(pk);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrio un error al buscar el paquete.");

            }
        }

        [HttpPost]
        public IHttpActionResult add(Packages PK)
        {
            try
            {
                Packages pk = LogicFactory.GetPackageLogic().PAdd(PK);
                return Ok(pk);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("addMany")]
        public IHttpActionResult addMany(List<Packages> PKs)
        {
            try
            {
                List<Packages> result = LogicFactory.GetPackageLogic().PAddMany(PKs);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult plist()
        {
            try
            {
                List<Packages> pk = LogicFactory.GetPackageLogic().PList();
                if (pk != null)
                    return Ok(pk);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al listar los paquetes.");
            }
        }

    }
}