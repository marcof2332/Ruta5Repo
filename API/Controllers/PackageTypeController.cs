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
    [RoutePrefix("api/packagetype")]
    public class PackageTypeController : ApiController
    {
        [HttpGet]
        public IHttpActionResult get(int id)
        {
            try
            {
                PackageType pt = LogicFactory.GetPackageTypeLogic().PtSearch(id);
                if (pt != null)
                    return Ok(pt);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrio un error al buscar el tipo de paquete.");

            }
        }

        [HttpPost]
        public IHttpActionResult add(PackageType PT)
        {
            try
            {
                LogicFactory.GetPackageTypeLogic().PtAdd(PT);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult modify(PackageType PT)
        {
            try
            {
                LogicFactory.GetPackageTypeLogic().PtModify(PT);
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
                LogicFactory.GetPackageTypeLogic().PtDelete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult plist()
        {
            try
            {
                List<PackageType> pt = LogicFactory.GetPackageTypeLogic().PtList();
                if (pt != null)
                    return Ok(pt);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al listar los tipos de paquete.");
            }
        }
    }
}