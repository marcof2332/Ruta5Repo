using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using API.Models;

namespace API.Validations
{
    public class ParseZoneShapeAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public void OnActionExecuted(HttpActionExecutedContext context)//ActionExecutedContext context)
        {
            // No es necesario implementar este método si no se requiere lógica posterior a la ejecución de la acción.
        }
        public override void OnActionExecuting(HttpActionContext context)
        {
            if (context.ActionArguments.ContainsKey("zo"))
            {
                var zo = context.ActionArguments["zo"] as AddZone;
                if (zo != null)
                {
                    // Realiza el análisis y conversión aquí
                    zo.polygon = ParseZoneShape(zo.WellKnownValue);
                }
            }

            base.OnActionExecuting(context);
        }

        private System.Data.Entity.Spatial.DbGeography ParseZoneShape(string zoneShape)
        {
            // Realiza la conversión o análisis necesario aquí
            // Por ejemplo, puedes analizar una cadena WKT y crear una instancia de DbGeography
            // o realizar cualquier otro procesamiento requerido.
            // Asegúrate de establecer el sistema de coordenadas (SRID) correcto.

            // Ejemplo de conversión desde WKT (Well-Known Text) si la zona llega como cadena
            var wkt = zoneShape;
            var srid = 4326; // El SRID adecuado para tus coordenadas
            return System.Data.Entity.Spatial.DbGeography.FromText(wkt, srid);
        }
    }
}