using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_PUERTAS.Controllers
{
    
    public class AperturaController : ApiController
    {
        ModelContent ctx = new ModelContent();


        [HttpGet]
        [ActionName("Apertura/{IdSeccion}")]
        public IHttpActionResult getApertura()
        {
            var data = ctx.PLU_Seccion.Select(x => new { x.CodeBase }).FirstOrDefault();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data.CodeBase);
        }
    }
}
