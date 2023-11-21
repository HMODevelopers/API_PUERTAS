using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_PUERTAS.Controllers
{
    [RoutePrefix("api/Apertura")]
    public class AperturaController : ApiController
    {
        ModelContent ctx = new ModelContent();


        [HttpGet]
        [Route("Abrir")]
        [ActionName("Abrir")]
        public IHttpActionResult Get([FromUri] int IdSeccion)
        {
            var data = ctx.PLU_Seccion.Where(x => x.IdSeccion == IdSeccion).Select(x => new { x.CodeBase }).FirstOrDefault();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data.CodeBase);
        }
    }
}
