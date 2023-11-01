using Helpers;
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

            var data = db.PLU_Puertas.Select(x => new { x.IdPuerta,x.IdSeccion,x.NombrePuerta ,x.PLU_Seccion.NombreSeccion,x.Code, x.Activo, x.FechaCreacion }).ToList();

            var jsonResult = Json(new { data }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetSecciones()
        {
            var data = db.PLU_Seccion.Select(x => new { Text = x.NombreSeccion , Value = x.IdSeccion}).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Guardar(PLU_Puertas plu_puertas)
        {

            var rm = new ResponseModel();
            PuertasHelper puerta = new PuertasHelper();

            plu_puertas.Activo = true;
            plu_puertas.FechaCreacion = DateTime.Now;

            if (ModelState.IsValid)
            {

                rm = puerta.Agregar(plu_puertas);
                if (rm.response)
                {
                    rm.message = "Seccion agregada con exito.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.error = false;
                }
                else
                {
                    rm.message = "Error al agregar Seccion.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.error = true;
                }
            }
            return Json(rm);
        }

        [HttpPost]
        public JsonResult CambiarStatus(int id, bool activo)
        {

            var rm = new ResponseModel();
            PuertasHelper puertas = new PuertasHelper();
            PLU_Puertas sec = new PLU_Puertas();

            var data = db.PLU_Puertas.Where(x => x.IdPuerta == id).FirstOrDefault();

            sec.IdPuerta = data.IdPuerta;
            sec.IdSeccion = data.IdSeccion;
            sec.NombrePuerta = data.NombrePuerta;
            sec.Code = data.Code;
            sec.FechaCreacion = data.FechaCreacion;

            if (activo)
            {
                sec.Activo = false;
            }
            else
            {
                sec.Activo = true;
            }

            rm = puertas.CambiarStatus(sec);

            if (rm.response)
            {
                rm.message = "Se a desactivad la puerta con exito.";
                rm.error = false;
            }
            else
            {
                rm.message = "Error al cambiar estatus";
                rm.error = true;
            }

            return Json(rm);
        }
    }
}