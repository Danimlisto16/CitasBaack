using Microsoft.Ajax.Utilities;
using ModelosCitas.Models;
using ModelosCitas.Transaccions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CitasBaack.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class medicoController : ApiController
    {


        //CORS
        public IHttpActionResult Get()
        {

            try
            {
                List<MEDICO> todos = medicoBLL.List();
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
                return Ok(medicoBLL.Get(id));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }



        public IHttpActionResult Post(MEDICO a)
        {
            try
            {
                using (CitasMedicasEntities1 db = new CitasMedicasEntities1())
                {
                    //var userDetail = db.MEDICO.Where(x => x.usuario == a.usuario);
                    int userDetail = db.MEDICO.Count(x => x.PERSONA.usuario == a.PERSONA.usuario);
                    if (userDetail == 0)
                    {
                        medicoBLL.Create(a);
                        return Content(HttpStatusCode.Created, "MEDICO agregado correctamente!");
                    }
                    else
                    {
                        return Content(HttpStatusCode.Conflict, "Por favor ingrese otro nombre de usuario");
                    }    
                }
                
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
                medicoBLL.Delete(id);
                return Content(HttpStatusCode.OK, "MEDICO borrado con éxito");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Solicitud no procesada , No se pudo eliminar");
            }
        }



        public IHttpActionResult Put(MEDICO MEDICO)
        {
            if (medicoBLL.Update(MEDICO))
            {
                return Content(HttpStatusCode.OK, "MEDICO actualizado");
            }
            return Content(HttpStatusCode.InternalServerError, "Error interno del servidor");
        }
    }
        
}
