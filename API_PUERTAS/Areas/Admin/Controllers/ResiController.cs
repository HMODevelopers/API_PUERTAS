using Helpers;
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
    public class ResiController : Controller
    {
        // GET: Residentes
        ModelContent db = new ModelContent();

        public ActionResult Index()
        {
            
            return View();
        }


        //Leer
        [HttpGet]
        public JsonResult GetResidentes()
        {
            var IdUsuario = Helpers.SessionHelper.GetUser();
            var usuario = db.PLU_Usuario.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
            var data = db.PLU_Residentes.Where(x => x.IdSeccion == usuario.IdSeccion).Select(x => new { x.IdResidentes, x.PLU_Seccion.NombreSeccion, x.NombreCompleto, x.Celular, x.Domicilio, x.NoCasa, x.Activo, x.FechaCreacion }).ToList();

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
            plu_residentes.Pass = HashHelper.SHA1(plu_residentes.Pass);
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

        public JsonResult GetSecciones()
        {
            var IdUsuario = Helpers.SessionHelper.GetUser();
            var usuario = db.PLU_Usuario.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
            var data = db.PLU_Seccion.Where(x => x.IdSeccion == usuario.IdSeccion).Select(x => new { Text = x.NombreSeccion, Value = x.IdSeccion }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteResidentes(int IdResidente)
        {

            var rm = new ResponseModel();
            ResidentesHelper Residentes = new ResidentesHelper();

            var data = db.PLU_Residentes.Where(x => x.IdResidentes == IdResidente).FirstOrDefault();

            var residente = new PLU_Residentes
            {
                IdResidentes = data.IdResidentes,
                IdSeccion = data.IdSeccion,
                NombreCompleto = data.NombreCompleto,
                Celular = data.Celular,
                Pass = data.Pass,
                NoCasa = data.NoCasa,
                Domicilio = data.Domicilio,
                Auth = data.Auth,
                Activo = data.Activo,
                FechaCreacion = data.FechaCreacion

            };

            if (ModelState.IsValid)
            {

                rm = Residentes.Delete(residente);
                if (rm.response)
                {
                    rm.message = "Residente Eliminado con exito.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.error = false;
                }
                else
                {
                    rm.message = "Error al Eliminar Residente.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.error = true;
                }
            }
            return Json(rm);
        }

        [HttpPost]
        public JsonResult ResetPassword(int id)
        {
            var rm = new ResponseModel();
            ResidentesHelper Residentes = new ResidentesHelper();
            PLU_Residentes residentes = new PLU_Residentes();

            var data = db.PLU_Residentes.Where(x => x.IdResidentes == id).FirstOrDefault();

            residentes.IdResidentes = data.IdResidentes;
            residentes.IdSeccion = data.IdSeccion;
            residentes.NombreCompleto = data.NombreCompleto;
            residentes.Celular = data.Celular;
            residentes.Pass = HashHelper.SHA1("123456789$");
            residentes.NoCasa = data.NoCasa;
            residentes.Domicilio = data.Domicilio;
            residentes.Auth = data.Auth;
            residentes.Activo = data.Activo;
            residentes.FechaCreacion = data.FechaCreacion;


            rm = Residentes.CambiarStatus(residentes);

            if (rm.response)
            {
                rm.message = "La contraseña del residente ha sido cambiado.";
                rm.error = false;
            }
            else
            {
                rm.message = "Error al restaurar contraseña";
                rm.error = true;
            }

            return Json(rm);
        }
    }
}