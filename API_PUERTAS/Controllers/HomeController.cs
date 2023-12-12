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

            return View();
        }

        public ActionResult BitacoraCode()
        {

            return View();
        }

        public ActionResult BitacoraControles()
        {

            return View();
        }

        public ActionResult BitacoraTarjetas()
        {

            return View();
        }



        public JsonResult GetBitacoraApp(int IdSeccion)
        {
            var data = db.PLU_BitacoraAccesos.Where(x => x.PLU_Residentes.IdSeccion == IdSeccion).Select(x => new { x.IdBitacoraAccesos, x.PLU_Residentes.NombreCompleto, x.PLU_Residentes.Domicilio, x.PLU_Residentes.NoCasa, x.PLU_Residentes.Celular, x.FechaUso }).ToList();
            var jsonResult = Json(new { data }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetBitacoraCode(int IdSeccion)
        {
            var data = db.PLU_BitacoraCodigos.Where(x => x.PLU_Codigos.PLU_Residentes.IdSeccion == IdSeccion).Select(x => new { x.IdBitacoraCodigo, x.PLU_Codigos.PLU_Residentes.NombreCompleto, x.PLU_Codigos.PLU_Residentes.Domicilio, x.PLU_Codigos.PLU_Residentes.NoCasa, x.PLU_Codigos.PLU_Residentes.Celular, x.FechaUso }).ToList();
            var jsonResult = Json(new { data }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        [HttpGet]
        public JsonResult GetSecciones()
        {
            var data = db.PLU_Seccion.Select(x => new { id = x.IdSeccion, text = x.NombreSeccion }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}
