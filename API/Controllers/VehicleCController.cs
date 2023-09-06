using System;
using System.Collections.Generic;
using System.Web.Http;

using DataLayer;
using LogicLayer;

namespace API.Controllers
{
    [RoutePrefix("api/vehiclec")]
    public class VehicleCController : ApiController
    {
        [HttpGet]
        public IHttpActionResult get(int id)
        {
            try
            {
                VehiclesCondition vc = LogicFactory.GetVehicleConditionLogic().VCSearch(id);
                if (vc != null)
                    return Ok(vc);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al buscar el estado del vehiculo.");
            }
        }
        [HttpPost]
        public IHttpActionResult add(VehiclesCondition vc)
        {
            try
            {
                LogicFactory.GetVehicleConditionLogic().VCAdd(vc);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IHttpActionResult modify(VehiclesCondition vc)
        {
            try
            {
                VehiclesCondition vCheck = LogicFactory.GetVehicleConditionLogic().VCSearch(vc.IdVC);
                if (vCheck == null)
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetVehicleConditionLogic().VCModify(vc);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public IHttpActionResult delete(int id)
        {
            try
            {
                LogicFactory.GetVehicleConditionLogic().VCDelete(id);
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
                List<VehiclesCondition> vc = LogicFactory.GetVehicleConditionLogic().VCList();
                if (vc != null)
                    return Ok(vc);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al listar los estados de los vehiculos.");
            }
        }
    }
}