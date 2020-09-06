using ModelosCitas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CitasBaack.Controllers
{
    public class reporteController : ApiController
    {
        public static List<getMedicos_Result> List()
        {
            using (CitasMedicasEntities1 db = new CitasMedicasEntities1())
            {
                try
                {
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al enviar la lista de MEDICOs " + ex.Message);
                    throw ex;
                }
            }
        }
    }
}
