
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ModelosCitas.Models;
using ModelosCitas.Transaccions;

namespace CITA_MEDICAsBaack.Controllers
{
    public class citaController : ApiController
    {
        public IHttpActionResult Get()
        {

            try
            {
                List<CITA_MEDICA> todos = citaBLL.List();
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
                return Ok(citaBLL.Get(id));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }



        public IHttpActionResult Post(CITA_MEDICA a)
        {
            try
            {
                using (CitasMedicasEntities1 db = new CitasMedicasEntities1())
                {
                    //var userDetail = db.CITA_MEDICA.Where(x => x.usuario == a.usuario);
                    int userDetail = db.CITA_MEDICA.Count(x => x.id_medico == a.id_medico && x.id_paciente == a.id_paciente && x.fecha == a.fecha );
                    if (userDetail == 0)
                    {
                        citaBLL.Create(a);
                        return Content(HttpStatusCode.Created, "Cita agregado correctamente!");
                    }
                    else
                    {
                        return Content(HttpStatusCode.Conflict, "Por favor ingrese otro horario");
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
                citaBLL.Delete(id);
                return Content(HttpStatusCode.OK, "CITA_MEDICA borrado con éxito");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Solicitud no procesada , No se pudo eliminar");
            }
        }



        public IHttpActionResult Put(CITA_MEDICA CITA_MEDICA)
        {
            if (citaBLL.Update(CITA_MEDICA))
            {
                return Content(HttpStatusCode.OK, "CITA_MEDICA actualizado");
            }
            return Content(HttpStatusCode.InternalServerError, "Error interno del servidor");
        }
    }
}
