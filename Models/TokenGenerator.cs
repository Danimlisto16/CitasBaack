
using Microsoft.IdentityModel.Tokens;
using ModelosCitas.Models;
using ModelosCitas.Transaccions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace WebApiMiVeci.Models
{
    internal static class TokenGenerator
    {
        public static string GenerateTokenJwt(PERSONA persona)
        {
            // RECUPERAMOS LAS VARIABLES DE CONFIGURACIÓN
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
            var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            // CREAMOS EL HEADER //
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var _Header = new JwtHeader(signingCredentials);
            // CREAMOS LOS CLAIMS //

            string especialidad = "";
            if (persona.rol == "m")
            {
                especialidad = medicoBLL.GetEspecialidad(persona.id).especialidad;
            }
            else
                especialidad = "no hay";

            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, persona.id.ToString()),
                new Claim("rol", persona.rol),
                new Claim("Nombres", persona.nombres),
                new Claim("Apellidos", persona.apellidos),
                new Claim("especialidad", especialidad),
                new Claim(JwtRegisteredClaimNames.Email,persona.correo),
                new Claim( ClaimTypes.Role, persona.rol)
                
                
            };
            // CREAMOS EL PAYLOAD //
            var _Payload = new JwtPayload(
                    issuer: issuerToken,
                    audience: audienceToken,
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    // Expira en 10 min.
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime))
                );

            // GENERAMOS EL TOKEN //
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}