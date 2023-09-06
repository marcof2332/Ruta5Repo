using System;
using System.Collections.Generic;
using System.Web.Http;
using DataLayer;
using LogicLayer;

namespace API.Controllers
{
    [RoutePrefix("api/stages")]
    public class StagesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult get (int id)
        {
            try
            {
                Stages st = LogicFactory.GetStageLogic().StSearch(id);
                if (st != null)
                    return Ok(st);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al buscar la etapa.");
            }
        }
        [HttpPost]
        public IHttpActionResult add (Stages st)
        {
            try
            {
                Stages checkst = LogicFactory.GetStageLogic().StSearch(st.IdSStage);
                if (checkst != null)
                    return BadRequest("La etapa que esta intentando agregar ya existe en el sistema.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetStageLogic().StAdd(st);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IHttpActionResult modify (Stages st)
        {
            try
            {
                Stages checkSt = LogicFactory.GetStageLogic().StSearch(st.IdSStage);
                if (checkSt == null)
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetStageLogic().StModify(st);
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
                LogicFactory.GetStageLogic().StDelete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IHttpActionResult list ()
        {
            try
            {
                List<Stages> st = LogicFactory.GetStageLogic().StList();
                if (st != null)
                    return Ok(st);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al listar las etapas.");
            }
        }
    }
}