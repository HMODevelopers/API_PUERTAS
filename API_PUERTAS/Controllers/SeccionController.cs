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
    public class SeccionController : Controller
    {
        ModelContent db = new ModelContent();
        // GET: Proyecto
        public ActionResult Index()
        {
            

            return View();
        }

        [HttpGet]
        public JsonResult GetSecciones()
        {

            var data = db.PLU_Seccion.Select(x => new { x.IdSeccion, x.NombreSeccion,x.Activo, x.FechaCreacion }).ToList();

            var jsonResult = Json(new { data }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        [HttpPost]
        public JsonResult Guardar(PLU_Seccion plu_seccion)
        {

            var rm = new ResponseModel();
            SeccionHelper seccion = new SeccionHelper();

            plu_seccion.FechaCreacion = DateTime.Now;

            if (ModelState.IsValid)
            {

                rm = seccion.Agregar(plu_seccion);
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
            SeccionHelper seccion = new SeccionHelper();
            PLU_Seccion sec = new PLU_Seccion();

            var data = db.PLU_Seccion.Where(x => x.IdSeccion == id).FirstOrDefault();

            sec.IdSeccion = data.IdSeccion;
            sec.NombreSeccion = data.NombreSeccion;
            sec.FechaCreacion = data.FechaCreacion;

            if (activo)
            {
                sec.Activo = false;
            }
            else
            {
                sec.Activo = true;
            }

            rm = seccion.CambiarStatus(sec);

            if (rm.response)
            {
                rm.message = "Se a desactivad la seccion con exito.";
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