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
    [RoutePrefix("api/States")]
    public class StatesController : ApiController
    {
        [HttpGet]
        [Route("GetState")]
        public IHttpActionResult GetState(int st)
        {
            try
            {
                States s = LogicFactory.GetStatesLogic().StateSearch(st);
                if (s != null)
                    return Ok(s);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrió un error al buscar el departamento: " + ex.Message);
            }
        }
        [HttpPost]
        [Route("SAdd")]
        public IHttpActionResult SAdd(States st)
        {

            try
            {
                States s = LogicFactory.GetStatesLogic().StateSearch(st.IdState);
                if (s != null)
                    return BadRequest("El departamento que esta intentando agregar ya existe en el sistema.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetStatesLogic().SAdd(st);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("SModify")]
        public IHttpActionResult SModify(States st)
        {
            try
            {
                States s = LogicFactory.GetStatesLogic().StateSearch(st.IdState);
                if (s == null)
                    return BadRequest("El departamento que esta intentando modificar no existe en el sistema.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetStatesLogic().SModify(st);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("SDelete")]
        public IHttpActionResult SDelete(int st)
        {
            try
            {
                LogicFactory.GetStatesLogic().SDelete(st);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("SList")]
        public IHttpActionResult SList()
        {
            try
            {
                List<States> s = LogicFactory.GetStatesLogic().StateList();
                if (s != null)
                    return Ok(s);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrió un error al listar los departamentos: " + ex.Message);
            }
        }
    }
}