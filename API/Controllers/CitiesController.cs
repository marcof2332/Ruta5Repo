using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DataLayer;
using LogicLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.Controllers
{
    [RoutePrefix("api/Cities")]
    public class CitiesController : ApiController
    {
        [HttpGet]
        [Route("GetCity")]
        public IHttpActionResult GetCity(int id)
        {
            try
            {
                Cities c = LogicFactory.GetCityLogic().CitySearch(id);
                if (c != null)
                {
                    // Configurar la configuración de serialización personalizada
                    //Configuramos las JsonSerializerSettings para ignorar los bucles de referencia (ReferenceLoopHandling.Ignore) 
                    //y utilizar CamelCasePropertyNamesContractResolver para cambiar el formato del nombre de las propiedades a minúsculas con notación camelCase.
                    JsonSerializerSettings settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                    // Convertir el objeto Cities a JSON con la configuración personalizada
                    //Al serializar el objeto Cities utilizando la función JsonConvert.SerializeObject, aplicaremos la configuración personalizada
                    //y obtendremos una respuesta JSON que incluye la propiedad States dentro del objeto Cities, pero sin causar el bucle de referencia.
                    string json = JsonConvert.SerializeObject(c, settings);

                    //Estamos creando un HttpResponseMessage con el contenido JSON
                    //y luego devolviendo la respuesta HTTP utilizando ResponseMessage(response).
                    //De esta manera, podrás devolver el objeto Cities con la información de States
                    //dentro de la respuesta JSON sin problemas de bucle de referencia y con el tipo de contenido adecuado.
                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    return ResponseMessage(response);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrió un error al buscar la ciudad: " + ex.Message);
            }
        }
        [HttpPost]
        [Route("CAdd")]
        public IHttpActionResult CAdd(Cities cy)
        {
            try
            {
                LogicFactory.GetCityLogic().CAdd(cy);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("CModify")]
        public IHttpActionResult CModify(Cities cy)
        {
            try
            {
                LogicFactory.GetCityLogic().CModify(cy);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("CDelete")]
        public IHttpActionResult CDelete(int cy)
        {
            try
            {
                LogicFactory.GetCityLogic().CDelete(cy);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("CListByState")]
        public IHttpActionResult CListByState(int State)
        {
            try
            {
                List<Cities> c = LogicFactory.GetCityLogic().CityListByState(State);
                if (c != null)
                {
                    // Configurar la configuración de serialización personalizada
                    //Configuramos las JsonSerializerSettings para ignorar los bucles de referencia (ReferenceLoopHandling.Ignore) 
                    //y utilizar CamelCasePropertyNamesContractResolver para cambiar el formato del nombre de las propiedades a minúsculas con notación camelCase.
                    JsonSerializerSettings settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                    // Convertir el objeto Cities a JSON con la configuración personalizada
                    //Al serializar el objeto Cities utilizando la función JsonConvert.SerializeObject, aplicaremos la configuración personalizada
                    //y obtendremos una respuesta JSON que incluye la propiedad States dentro del objeto Cities, pero sin causar el bucle de referencia.
                    string json = JsonConvert.SerializeObject(c, settings);

                    //Estamos creando un HttpResponseMessage con el contenido JSON
                    //y luego devolviendo la respuesta HTTP utilizando ResponseMessage(response).
                    //De esta manera, podrás devolver el objeto Cities con la información de States
                    //dentro de la respuesta JSON sin problemas de bucle de referencia y con el tipo de contenido adecuado.
                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    return ResponseMessage(response);

                    //return Ok(r);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrió un error al listar las ciudades: " + ex.Message);
            }
        }
    }
}