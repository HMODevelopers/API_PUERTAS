using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EDUES_ADMIN.Filters.AdminFilters;

namespace API_PUERTAS.Areas.Admin.Controllers
{

    [Admin]
    public class BitacorasController : Controller
    {
        ModelContent db = new ModelContent();
        // GET: Admin/Bitacoras
        public ActionResult Index()
        {
            var IdUsuario = Helpers.SessionHelper.GetUser();
            var data = db.PLU_Usuario.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
            return View(data);
        }

        public ActionResult BitacoraCode()
        {
            var IdUsuario = Helpers.SessionHelper.GetUser();
            var data = db.PLU_Usuario.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();

            return View(data);
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
    }
}