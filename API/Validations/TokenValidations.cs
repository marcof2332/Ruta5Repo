using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Validations
{
    internal class TokenValidations
    {
        // GENERAMOS EL TOKEN CON LA INFORMACIÓN DEL USUARIO
            internal static string JWTGenerator(Employees userInfo)
            {
                // CREAMOS EL HEADER //
                var key = Convert.ToBase64String(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JWT:ClaveSecreta"]));
                var _symmetricSecurityKey = new SymmetricSecurityKey(Convert.FromBase64String(key));

                //var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JWT:ClaveSecreta"]));
                var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
                var _Header = new JwtHeader(_signingCredentials);

                // CREAMOS LOS CLAIMS //
                var _Claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("nameid", userInfo.ID.ToString()),
                    new Claim("role", userInfo.EmpRole),
                    new Claim("user", userInfo.EmpUser),
                };

                // CREAMOS EL PAYLOAD //
                var _Payload = new JwtPayload(
                        issuer: ConfigurationManager.AppSettings["JWT:Issuer"],
                        audience: ConfigurationManager.AppSettings["JWT:Audience"],
                        claims: _Claims,
                        notBefore: DateTime.UtcNow,
                        // Exipra a la 8 horas.
                        expires: DateTime.UtcNow.AddHours(8)
                    );

                // GENERAMOS EL TOKEN //
                var _Token = new JwtSecurityToken(
                        _Header,
                        _Payload
                    );
                return new JwtSecurityTokenHandler().WriteToken(_Token);
            }

        internal static Employees getClaimsFromToken(string token)
        {
            Employees e = new Employees();
            try
            {
                var key = Convert.ToBase64String(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JWT:ClaveSecreta"]));
                var _symmetricSecurityKey = new SymmetricSecurityKey(Convert.FromBase64String(key));

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _symmetricSecurityKey,
                    ValidateIssuer = true,
                    ValidIssuer = ConfigurationManager.AppSettings["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = ConfigurationManager.AppSettings["JWT:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromHours(8)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                var claimID = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                var claimUs = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "user")?.Value;
                var claimRole = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

                e.ID = Convert.ToInt32(claimID);
                e.EmpUser = claimUs;
                e.EmpRole = claimRole;

                return e;
            }
            catch (SecurityTokenException ex)
            {
                throw new Exception("Error al validar el token: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error general: " + ex.Message);
            }
        }
    }
}