using System;
using System.Collections.Generic;
using System.Web.Http;
using DataLayer;
using LogicLayer;

namespace API.Controllers
{
    [RoutePrefix("api/vehicles")]
    public class VehiclesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult get(string vreg)
        {
            try
            {
                 Vehicles v = LogicFactory.GetVehicleLogic().VSearch(vreg);
                if (v != null)
                    return Ok(v);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al buscar el vehiculo.");
            }
        }
        [HttpPost]
        public IHttpActionResult add(Vehicles v)
        {

            try
            {
                Vehicles vCheck = LogicFactory.GetVehicleLogic().VSearch(v.vRegistration);
                if (vCheck != null)
                    return BadRequest("El vehiculo que esta intentando agregar ya existe en el sistema.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetVehicleLogic().VAdd(v);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IHttpActionResult modify(Vehicles v)
        {
            try
            {
                Vehicles vCheck = LogicFactory.GetVehicleLogic().VSearch(v.vRegistration);
                if (vCheck != null)
                    return BadRequest("El vehiculo que esta intentando modificar no existe en el sistema.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetVehicleLogic().VModify(v);
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
                LogicFactory.GetVehicleLogic().VDelete(id);
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
                List<Vehicles> v = LogicFactory.GetVehicleLogic().VList();
                if (v != null)
                    return Ok(v);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al listar los vehiculos.");
            }
        }
    }
}