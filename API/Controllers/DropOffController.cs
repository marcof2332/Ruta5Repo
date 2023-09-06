using System;
using System.Collections.Generic;
using System.Web.Http;
using DataLayer;
using LogicLayer;


namespace API.Controllers
{
    [RoutePrefix("api/dropoff")]
    public class DropOffController : ApiController
    {
        [HttpGet]
        public IHttpActionResult find(int id)
        {
            try
            {
                DropOffPackage dpCheck = LogicFactory.GetShippmentLogic().DoPSearch(id);
                if (dpCheck != null)
                    return Ok(dpCheck);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al buscar el paquete.");
            }
        }
        [HttpPost]
        public IHttpActionResult add(DropOffPackage dp)
        {
            try
            {
                LogicFactory.GetShippmentLogic().DoPAdd(dp);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public IHttpActionResult delete(int ID)
        {
            try
            {
                LogicFactory.GetShippmentLogic().DoPDelete(ID);
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
                List<DropOffPackage> dp = LogicFactory.GetShippmentLogic().DoPList();
                if (dp != null)
                    return Ok(dp);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrió un error al listar los pedidos.");
            }
        }
    }
}