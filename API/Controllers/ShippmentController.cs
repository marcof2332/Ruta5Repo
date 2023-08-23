using System;
using System.Web.Http;
using DataLayer;
using LogicLayer;

namespace API.Controllers
{
    [RoutePrefix("/api/shippment")]
    public class ShippmentController : ApiController
    {
        [HttpPut]
        [Route("delpack")]
        public IHttpActionResult delpack(int PackageId)
        {
            try
            {
                Packages pCheck = LogicFactory.GetPackageLogic().PSearch(PackageId);
                if (pCheck != null)
                {
                    LogicFactory.GetShippmentLogic().PackageRemoval(PackageId);
                    return Ok();
                }
                else
                    return BadRequest("No se encuentra en el almacenamiento el paquete que esta intentando eliminar.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("addstage")]
        public IHttpActionResult addstage(ShippmentStage st)
        {
            try
            {
                LogicFactory.GetShippmentLogic().ShippmentStageAdd(st);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}