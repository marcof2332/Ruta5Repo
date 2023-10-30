using System;
using System.Collections.Generic;
using System.Web.Http;

using DataLayer;
using LogicLayer;

namespace API.Controllers
{
    [RoutePrefix("api/cities")]
    public class CitiesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult get (int id)
        {
            try
            {
                Cities c = LogicFactory.GetCityLogic().CitySearch(id);
                if (c != null)
                {
                    /*// Configurar la configuración de serialización personalizada
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
                    return ResponseMessage(response);*/

                    return Ok(c);
                }
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al buscar la ciudad.");
            }
        }
        [HttpPost]
        public IHttpActionResult add (Cities cy)
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
        public IHttpActionResult modify (Cities cy)
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
        public IHttpActionResult delete (int cy)
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
        public IHttpActionResult clist ()
        {
            try
            {
                List<Cities> c = LogicFactory.GetCityLogic().CitesList();
                if (c != null)
                {
                    return Ok(c);
                }
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Ocurrió un error al listar las ciudades.");
            }
        }
    }
}