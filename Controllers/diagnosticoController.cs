using ModelosCitas.Models;
using ModelosCitas.Transaccions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CitasBaack.Controllers
{
    public class diagnosticoController : ApiController
    {
        public IHttpActionResult Get()
        {

            try
            {
                List<DIAGNOSTICO> todos = diagnosticoBLL.List();
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
                return Ok(diagnosticoBLL.Get(id));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }



        public IHttpActionResult Post(DIAGNOSTICO a)
        {
            try
            {
                diagnosticoBLL.Create(a);
                return Content(HttpStatusCode.Created, "DIAGNOSTICO agregado correctamente!");
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
                diagnosticoBLL.Delete(id);
                return Content(HttpStatusCode.OK, "DIAGNOSTICO borrado con éxito");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Solicitud no procesada , No se pudo eliminar");
            }
        }


        public IHttpActionResult Put(DIAGNOSTICO DIAGNOSTICO)
        {
            if (diagnosticoBLL.Update(DIAGNOSTICO))
            {
                return Content(HttpStatusCode.OK, "DIAGNOSTICO actualizado");
            }
            return Content(HttpStatusCode.InternalServerError, "Error interno del servidor");
        }

    }
}
