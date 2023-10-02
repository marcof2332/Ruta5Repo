using System.Collections.Generic;
using System.Web.Http;
using DataLayer;
using LogicLayer;
using System;
using API.Models;
using API.Validations;

namespace API.Controllers
{
    [RoutePrefix("api/employees")]
    [TokenAuthorizationFilter(new string[] { "GER" })]
    public class EmployeesController : ApiController
    {
        #region CRUD
        [HttpPost]
        public IHttpActionResult add(Employees Emp)
        {

            try
            {
                Employees AddedEmp = LogicFactory.GetEmployeeLogic().ESearch(Emp.ID);
                if (AddedEmp != null)
                    return BadRequest("El empleado que esta intentando agregar ya existe en el sistema.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetEmployeeLogic().EAdd(Emp);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult modify(Employees Emp)
        {
            try
            {
                Employees AddedEmp = LogicFactory.GetEmployeeLogic().ESearch(Emp.ID);
                if (AddedEmp == null)
                    return BadRequest("El empleado que esta intentando modificar no existe en el sistema.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                LogicFactory.GetEmployeeLogic().EModify(Emp);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult delete(int emp)
        {
            try
            {
                LogicFactory.GetEmployeeLogic().EDelete(emp);
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
                List<Employees> e = LogicFactory.GetEmployeeLogic().EList();
                if (e != null)
                    return Ok(e);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrió un error al listar los empleados: " + ex.Message);
            }
        }
        #endregion
    }
}