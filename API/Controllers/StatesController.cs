using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Validations;
using DataLayer;
using LogicLayer;

namespace API.Controllers
{
    [RoutePrefix("api/states")]
    [TokenAuthorizationFilter(new string[] { "GER", "ENC" })]
    public class StatesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult get (int st)
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
        public IHttpActionResult add (States st)
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
        public IHttpActionResult modify (States st)
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
        public IHttpActionResult delete (int st)
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
        public IHttpActionResult list()
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