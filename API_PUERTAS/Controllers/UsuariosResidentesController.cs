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
    public class UsuariosResidentesController : Controller
    {
        // GET: Residentes
        ModelContent db = new ModelContent();
        public ActionResult Index()
        {
            PLU_Residentes plu_residentes = new PLU_Residentes();
            plu_residentes.Pass = "96ef9fbd2bc8bedff9185ec427854ca67bfbec29";

            return View(plu_residentes);
        }


        //Leer
        [HttpGet]
        public JsonResult GetResidentes()
        {

            var data = db.PLU_Residentes.Select(x => new { x.IdResidentes, x.PLU_Seccion.NombreSeccion, x.NombreCompleto,x.Celular,x.Domicilio,x.NoCasa ,x.Activo, x.FechaCreacion }).ToList();

            var jsonResult = Json(new { data }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Guardar
        [HttpPost]
        public JsonResult Guardar(PLU_Residentes plu_residentes)
        {

            var rm = new ResponseModel();
            ResidentesHelper Residentes = new ResidentesHelper();
            plu_residentes.Pass = HashHelper.SHA1("123456789$");
            plu_residentes.FechaCreacion = DateTime.Now;

            if (ModelState.IsValid)
            {

                rm = Residentes.Agregar(plu_residentes);
                if (rm.response)
                {
                    rm.message = "Residente agregado con exito.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.error = false;
                }
                else
                {
                    rm.message = "Error al agregar Residente.";
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
            ResidentesHelper Residentes = new ResidentesHelper();
            PLU_Residentes residentes = new PLU_Residentes();

            var data = db.PLU_Residentes.Where(x => x.IdResidentes == id).FirstOrDefault();

            residentes.IdResidentes = data.IdResidentes;
            residentes.IdSeccion = data.IdSeccion;
            residentes.NombreCompleto = data.NombreCompleto;
            residentes.Celular = data.Celular;
            residentes.Pass = data.Pass;
            residentes.NoCasa = data.NoCasa;
            residentes.Domicilio = data.Domicilio;
            residentes.Auth = data.Auth;
            residentes.FechaCreacion = data.FechaCreacion;

            if (activo)
            {
                residentes.Activo = false;
            }
            else
            {
                residentes.Activo = true;
            }

            rm = Residentes.CambiarStatus(residentes);

            if (rm.response)
            {
                rm.message = "Su estatus del residente ha sido cambiado.";
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