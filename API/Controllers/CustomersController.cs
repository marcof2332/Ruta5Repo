using System;
using System.Collections.Generic;
using System.Web.Http;

using DataLayer;
using LogicLayer;

namespace API.Controllers
{
    [RoutePrefix("/api/customers")]
    public class CustomersController : ApiController
    {
        [HttpGet]
        public IHttpActionResult find (int id)
        {
            try
            {
                Customers c = LogicFactory.GetCustomerLogic().CSearch(id);
                if (c != null)
                    return Ok(c);
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al buscar el usuario.");
            }
        }
        [HttpPost]
        public IHttpActionResult add(Customers c)
        {
            try
            {
                LogicFactory.GetCustomerLogic().CAdd(c);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IHttpActionResult modify(Customers c)
        {
            try
            {
                Customers checkC = LogicFactory.GetCustomerLogic().CSearch(c.DocRut);
                if (checkC == null)
                    return BadRequest("El cliente que esta intentando modificar no existe en el sistema.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetLicenceLogic().LModify(li);
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
                List<Customers> c = LogicFactory.GetCustomerLogic().CustomerList();
                if (c != null)
                    return Ok(c);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrió un error al listar los clientes.");
            }
        }
    }
}