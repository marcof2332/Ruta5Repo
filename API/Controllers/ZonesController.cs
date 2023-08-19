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
    [RoutePrefix("api/Zones")]
    public class ZonesController : ApiController
    {
        [HttpGet]
        [Route("GetZone")]
        public IHttpActionResult GetZone(int id)
        {
            try
            {
                Zones zo = LogicFactory.GetZonesLogic().ZoneSearch(id);
                if (zo != null)
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
                    string json = JsonConvert.SerializeObject(zo, settings);

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
                return BadRequest("Ocurrió un error al buscar la zona: " + ex.Message);
            }
        }
        [HttpPost]
        [Route("ZAdd")]
        public IHttpActionResult ZAdd(Zones zo)
        {
            try
            {
                LogicFactory.GetZonesLogic().ZAdd(zo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("ZModify")]
        public IHttpActionResult ZModify(Zones zo)
        {
            try
            {
                LogicFactory.GetZonesLogic().ZModify(zo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("ZDelete")]
        public IHttpActionResult ZDelete(int zo)
        {
            try
            {
                LogicFactory.GetZonesLogic().ZDelete(zo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("ZListByCity")]
        public IHttpActionResult ZListByCity(int city)
        {
            try
            {
                List<Zones> z = LogicFactory.GetZonesLogic().ZoneListbyCity(city);
                if (z != null)
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
                    string json = JsonConvert.SerializeObject(z, settings);

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
                return BadRequest("Ocurrió un error al listar las zonas: " + ex.Message);
            }
        }
    }
}