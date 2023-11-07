using System;
using System.Collections.Generic;
using System.Web.Http;
using DataLayer;
using LogicLayer;


namespace API.Controllers
{
    [RoutePrefix("api/homepickups")]
    public class HomePickupsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult find(int id)
        {
            try
            {
                 HomePickups hp = LogicFactory.GetShippmentLogic().HpSearch(id);
                if (hp != null)
                    return Ok(hp);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al buscar el pedido.");
            }
        }
        [HttpPost]
        public IHttpActionResult add(HomePickups hp)
        {
            try
            {
                LogicFactory.GetShippmentLogic().HpAdd(hp);
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
                LogicFactory.GetShippmentLogic().HpDelete(id);
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
                List<HomePickups> hp = LogicFactory.GetShippmentLogic().HpList();
                if (hp != null)
                    return Ok(hp);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al listar los pedidos.");
            }
        }
    }
}