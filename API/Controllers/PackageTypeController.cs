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
    [RoutePrefix("api/PackageType")]
    public class PackageTypeController : ApiController
    {
        [HttpGet]
        [Route("GetPackageType")]
        public IHttpActionResult GetPackageType(int id)
        {
            try
            {
                PackageType pt = LogicFactory.GetPackageTypeLogic().PtSearch(id);
                if (pt != null)
                    return Ok(pt);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrio un error al buscar el tipo de paquete.");

            }
        }

        [HttpPost]
        [Route("PTAdd")]
        public IHttpActionResult PTAdd(PackageType PT)
        {
            try
            {
                LogicFactory.GetPackageTypeLogic().PtAdd(PT);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("PTModify")]
        public IHttpActionResult PTModify(PackageType PT)
        {
            try
            {
                LogicFactory.GetPackageTypeLogic().PtModify(PT);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("PTDelete")]
        public IHttpActionResult PTDelete(int id)
        {
            try
            {
                LogicFactory.GetPackageTypeLogic().PtDelete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("PTList")]
        public IHttpActionResult PTList()
        {
            try
            {
                List<PackageType> pt = LogicFactory.GetPackageTypeLogic().PtList();
                if (pt != null)
                    return Ok(pt);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al listar los tipos de paquete.");
            }
        }
    }
}