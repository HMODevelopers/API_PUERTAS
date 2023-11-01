using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EDUES_ADMIN.Filters.AdminFilters;

namespace API_PUERTAS.Controllers
{
    [Autenticado]
    public class PuertasController : Controller
    {
        ModelContent db = new ModelContent();
        // GET: Puertas
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public JsonResult GetPuertas()
        {

            var data = db.PLU_Puertas.Select(x => new { x.IdSeccion,x.NombrePuerta ,x.PLU_Seccion.NombreSeccion, x.Activo, x.FechaCreacion }).ToList();

            var jsonResult = Json(new { data }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}