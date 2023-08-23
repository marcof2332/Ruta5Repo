using System;
using System.Collections.Generic;
using System.Web.Http;

using DataLayer;
using LogicLayer;

namespace API.Controllers
{
    [RoutePrefix("/api/package")]
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
                LogicFactory.GetPackageLogic().PAdd(PK);
                return Ok();
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