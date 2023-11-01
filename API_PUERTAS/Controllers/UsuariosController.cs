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
    public class UsuariosController : Controller
    {
        ModelContent db = new ModelContent();
        public ActionResult Index()
        {
            var roles = db.PLU_Rol.ToList();
            ViewBag.Roles = new SelectList(roles, "IdRol", "NombreRol");
            return View();
        }

        [HttpGet]
        public JsonResult GetUsuarios()
        {

            var data = db.PLU_Usuario.Select(x => new { x.IdUsuario, x.PLU_Rol.NombreRol, x.Usuario, x.Pass, x.Activo, x.FechaCreacion }).ToList();

            var jsonResult = Json(new { data }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult Guardar(PLU_Usuario plu_usuario)
        {

            var rm = new ResponseModel();
            UsuarioHelper user = new UsuarioHelper();
            plu_usuario.Pass = HashHelper.SHA1("123456789$");
            plu_usuario.FechaCreacion = DateTime.Now;

            if (ModelState.IsValid)
            {

                rm = user.Agregar(plu_usuario);
                if (rm.response)
                {
                    rm.message = "Usuario agregado con exito.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.error = false;
                }
                else
                {
                    rm.message = "Error al agregar usuario.";
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
            UsuarioHelper   user = new UsuarioHelper();
            PLU_Usuario usuario = new PLU_Usuario();
            var data = db.PLU_Usuario.Where(x => x.IdUsuario == id).FirstOrDefault();
            usuario.IdUsuario = data.IdUsuario;
            usuario.IdRol = data.IdRol;
            usuario.Usuario = data.Usuario;
            usuario.Pass = data.Pass;
            usuario.FechaCreacion = data.FechaCreacion;

            if (activo)
            {
                usuario.Activo = false;
            }
            else
            {
                usuario.Activo = true;
            }

            rm = user.CambiarStatus(usuario);

            if (rm.response)
            {
                rm.message = "Su estado de usuario ha sido cambiado.";
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