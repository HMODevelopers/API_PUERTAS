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
    public class HomeController : Controller
    {
        ModelContent db = new ModelContent();

        public ActionResult Index()
        {
            ViewBag.Title = "BITACORA";

            return View();
        }

        public JsonResult GetBitacoraCode(int IdSeccion)
        {
            try
            {
                using (ModelContent db = new ModelContent())
                {

                    var data = db.PLU_BitacoraAccesos.Where(x => x.PLU_Residentes.IdSeccion == IdSeccion).Select(x => new { x.IdBitacoraAccesos,x.PLU_Residentes.NombreCompleto, x.PLU_Residentes.Domicilio, x.PLU_Residentes.NoCasa, x.PLU_Residentes.Celular, x.FechaUso }).ToList();
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet]
        public JsonResult GetSecciones()
        {
            var data = db.PLU_Seccion.Select(x => new { id = x.IdSeccion, text = x.NombreSeccion }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}
