using ModelosCitas.Models;
using CitasBaack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiMiVeci.Models;
using ModelosCitas.Transaccions;

namespace CitasBaack.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Login")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class loginController : ApiController
    {

        [HttpPost]
        [Route("Authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            PERSONA usuario = new PERSONA();
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);    
            usuario.usuario = login.usuario;
            usuario.contrasena = login.contrasena;
                usuario = personaBLL.validate(usuario);
            
            
            if (usuario != null)
            {
                return Ok(new { token = TokenGenerator.GenerateTokenJwt(usuario) });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
