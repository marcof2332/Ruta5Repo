using System;
using System.Collections.Generic;
using System.Web.Http;

using DataLayer;
using LogicLayer;

namespace API.Controllers
{
    [RoutePrefix("api/BranchOffice")]
    public class BranchOfficeController : ApiController
    {
        [HttpGet]
        [Route("GetBranchOffice")]
        public IHttpActionResult GetBranchOffice(int id)
        {
            try
            {
                BranchOffices zo = LogicFactory.GetBranchLogic().OfficeSearch(id);
                if (zo != null)
                    return Ok(zo);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al buscar la sucursal");
            }
        }

        [HttpPost]
        [Route("BAdd")]
        public IHttpActionResult BAdd(BranchOffices B)
        {
            try
            {
                LogicFactory.GetBranchLogic().OAdd(B);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("BModify")]
        public IHttpActionResult BModify(BranchOffices B)
        {
            try
            {
                LogicFactory.GetBranchLogic().OModify(B);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("ODelete")]
        public IHttpActionResult ODelete(int id)
        {
            try
            {
                LogicFactory.GetBranchLogic().ODelete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("OListByZone")]
        public IHttpActionResult OListByZone(int id)
        {
            try
            {
                List<BranchOffices> b = LogicFactory.GetBranchLogic().OfficeListByZone(id);
                if (b != null)
                    return Ok(b);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al listar las sucursales.");
            }
        }
    }
}