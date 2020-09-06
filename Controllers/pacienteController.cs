using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ModelosCitas.Transaccions;
using ModelosCitas.Models;
using System.Web.Http.Cors;

namespace CitasBaack.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PERSONAController : ApiController
    {
        //CORS
        public IHttpActionResult Get()
        {
            
            try
            {
                List<PERSONA> todos = personaBLL.List();
                return Ok(todos);
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(personaBLL.Get(id));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
       


        public IHttpActionResult Post(PERSONA a)
        {
            try
            {
                using (CitasMedicasEntities1 db = new CitasMedicasEntities1())
                {
                    int userDetail = db.PERSONA.Count(x => x.usuario == a.usuario);
                    if (userDetail == 0)
                    {
                        personaBLL.Create(a);
                        return Content(HttpStatusCode.Created, "PERSONA agregado correctamente!");
                    }
                    else
                    {
                        return Content(HttpStatusCode.Conflict, "Por favor ingrese otro nombre de usuario");
                    }
                }
                return Content(HttpStatusCode.Created, "PERSONA agregado correctamente!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Solicitud no procesada");
            }
        }


        public IHttpActionResult Delete(int id)
        {
            try
            {
                personaBLL.Delete(id);
                return Content(HttpStatusCode.OK, "PERSONA borrado con éxito");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Solicitud no procesada , No se pudo eliminar");
            }
        }



        public IHttpActionResult Put(PERSONA PERSONA)
        {
            if (personaBLL.Update(PERSONA))
            {
                return Content(HttpStatusCode.OK, "PERSONA actualizado");
            }
            return Content(HttpStatusCode.InternalServerError, "Error interno del servidor");
        }
    }
}
