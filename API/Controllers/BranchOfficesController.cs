using System;
using System.Collections.Generic;
using System.Web.Http;

using DataLayer;
using LogicLayer;

namespace API.Controllers
{
    [RoutePrefix("api/branchoffices")]
    public class BranchOfficesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult get (int id)
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
        public IHttpActionResult add (BranchOffices B)
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
        public IHttpActionResult modify (BranchOffices B)
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
        public IHttpActionResult delete (int id)
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
        [Route("olist")]
        public IHttpActionResult olist (int id)
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