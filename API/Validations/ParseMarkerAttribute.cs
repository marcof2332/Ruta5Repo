using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using API.Models;

namespace API.Validations
{
    public class ParseMarkerAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public void OnActionExecuted(HttpActionExecutedContext context)//ActionExecutedContext context)
        {
            // No es necesario implementar este método si no se requiere lógica posterior a la ejecución de la acción.
        }
        public override void OnActionExecuting(HttpActionContext context)
        {
            if (context.ActionArguments.ContainsKey("B"))
            {
                var B = context.ActionArguments["B"] as addOffice;
                if (B != null)
                {
                    // Realiza el análisis y conversión aquí
                    B.marker = ParseZoneShape(B.WellKnownValue);
                }
            }

            base.OnActionExecuting(context);
        }

        private System.Data.Entity.Spatial.DbGeography ParseZoneShape(string zoneShape)
        {
            // Realiza la conversión o análisis necesario aquí
            // Por ejemplo, puedes analizar una cadena WKT y crear una instancia de DbGeography
            // o realizar cualquier otro procesamiento requerido.
            // Establecer el sistema de coordenadas (SRID) correcto.

            var wkt = zoneShape;
            var srid = 4326;
            return System.Data.Entity.Spatial.DbGeography.FromText(wkt, srid);
        }
    }
}