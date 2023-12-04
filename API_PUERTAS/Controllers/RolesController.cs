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
    public class RolesController : Controller
    {
        // GET: Roles
        ModelContent db = new ModelContent();
        public ActionResult Index()
        {
            return View();
        }

        //Leer
        [HttpGet]
        public JsonResult GetRoles()
        {

            var data = db.PLU_Rol.Select(x => new { x.IdRol, x.NombreRol, x.Descripcion, x.Activo, x.FechaCreacion }).ToList();

            var jsonResult = Json(new { data }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //Guardar
        [HttpPost]
        public JsonResult Guardar(PLU_Rol plu_rol)
        {

            var rm = new ResponseModel();
            RolesHelper Roles = new RolesHelper();
            plu_rol.FechaCreacion = DateTime.Now;

            if (ModelState.IsValid)
            {

                rm = Roles.Agregar(plu_rol);
                if (rm.response)
                {
                    rm.message = "Rol agregado con exito.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.error = false;
                }
                else
                {
                    rm.message = "Error al agregar Rol.";
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
            RolesHelper Roles = new RolesHelper();
            PLU_Rol rol = new PLU_Rol();

            var data = db.PLU_Rol.Where(x => x.IdRol == id).FirstOrDefault();

            rol.IdRol = data.IdRol;
            rol.NombreRol = data.NombreRol;
            rol.Descripcion = data.Descripcion;
            rol.FechaCreacion = data.FechaCreacion;

            if (activo)
            {
                rol.Activo = false;
            }
            else
            {
                rol.Activo = true;
            }

            rm = Roles.CambiarStatus(rol);

            if (rm.response)
            {
                rm.message = "Su estatus del Rol ha sido cambiado.";
                rm.error = false;
            }
            else
            {
                rm.message = "Error al cambiar estatus";
                rm.error = true;
            }

            return Json(rm);
        }



        [HttpPost]
        public JsonResult DeleteRol(int IdRol)
        {

            var rm = new ResponseModel();
            RolesHelper Rol = new RolesHelper();

            var data = db.PLU_Rol.Where(x => x.IdRol == IdRol).FirstOrDefault();

            var rol = new PLU_Rol
            {
                IdRol = data.IdRol,
                NombreRol = data.NombreRol,
                Descripcion = data.Descripcion,
                Activo = data.Activo,
                FechaCreacion = data.FechaCreacion

            };

            if (ModelState.IsValid)
            {

                rm = Rol.Delete(rol);
                if (rm.response)
                {
                    rm.message = "Rol Eliminado con exito.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.error = false;
                }
                else
                {
                    rm.message = "Error al Eliminar Rol.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.error = true;
                }
            }
            return Json(rm);
        }

    }
}