//using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using DataLayer;
using LogicLayer;
using System;
using System.Linq;

using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace API.Validations
{
    public class TokenAuthorizationFilter : System.Web.Http.Filters.ActionFilterAttribute
    //Attribute, IActionFilter
    {
        private readonly string[] role;

        public TokenAuthorizationFilter(string[] roles)
        {
            this.role = roles;
        }

        public void OnActionExecuted(HttpActionExecutedContext context)//ActionExecutedContext context)
        {
            // No es necesario implementar este método si no se requiere lógica posterior a la ejecución de la acción.
        }

        public override void OnActionExecuting(HttpActionContext context)
        {
            try
            {
                string tokenId = context.Request.Headers.GetValues("Authorization").FirstOrDefault();
                if (string.IsNullOrEmpty(tokenId))
                {
                    context.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                    {
                        Content = new System.Net.Http.StringContent("El token es requerido.")
                    };
                    return;
                }
                else
                {
                    Employees TokenUser = TokenValidations.getClaimsFromToken(tokenId);
                    if (TokenUser == null)
                    {
                        context.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
                        {
                            Content = new System.Net.Http.StringContent("Token inválido.")
                        };
                        return;
                    }
                    else
                    {
                        Employees validated = new Employees();
                        validated = LogicFactory.GetEmployeeLogic().ESearch(TokenUser.ID);

                        if (!role.Contains(validated.EmpRole))
                        {
                            context.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                            {
                                Content = new System.Net.Http.StringContent("El usuario no está autorizado a realizar esta acción.")
                            };
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); // Verificar este catch
            }
        }

    }
}