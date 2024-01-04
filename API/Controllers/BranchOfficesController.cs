using System;
using System.Collections.Generic;
using System.Web.Http;

using API.Models;
using API.Validations;
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
        [ParseMarkerAttribute]
        public IHttpActionResult add (addOffice B)
        {
            try
            {
                BranchOffices newOffice = new BranchOffices();
                newOffice.BranchZone = B.BranchZone;
                newOffice.BranchAddress = B.BranchAddress;
                newOffice.OpTime = B.OpTime;
                newOffice.CloseTime = B.CloseTime;
                newOffice.Phone = B.Phone;
                newOffice.MarkerLocation = B.marker;
                newOffice.Active = true;
                LogicFactory.GetBranchLogic().OAdd(newOffice);
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
        public IHttpActionResult olist ()
        {
            try
            {
                List<BranchOffices> b = LogicFactory.GetBranchLogic().OfficeList();
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