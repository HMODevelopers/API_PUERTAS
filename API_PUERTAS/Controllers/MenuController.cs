using Helpers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using static EDUES_ADMIN.Filters.AdminFilters;

namespace API_PUERTAS.Controllers
{
    [Autenticado]
    public class MenuController : Controller
    {
        ModelContent db = new ModelContent();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult MenuPerfil()
        {
            var roles = db.PLU_Rol.ToList();
            ViewBag.Roles = new SelectList(roles, "IdRol", "NombreRol");
            return View();
        }


        [HttpGet]
        public JsonResult GetMenuTable()
        {
            var data = db.PLU_Menu.Select(x => new { x.IdMenu, x.TituloMenu, x.Icono, x.Orden, x.Activo, x.FechaCreacion }).ToList();
            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMenuRolTable(int IdRol)
        {
            var data = db.PLU_MenuRoles.Where(x => x.IdRol == IdRol).Select(x => new { x.IdMenuRol, x.PLU_Menu.TituloMenu, x.Activo, x.FechaCreacion }).ToList();
            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CambiarStatus(int IdMenu, bool activo)
        {

            var rm = new ResponseModel();
            MenuHelper menu = new MenuHelper();
            PLU_Menu menus = new PLU_Menu();
            var data = db.PLU_Menu.Where(x => x.IdMenu == IdMenu).FirstOrDefault();
            menus.IdMenu = data.IdMenu;
            menus.TituloMenu = data.TituloMenu;
            menus.Icono = data.Icono;
            menus.Orden = data.Orden;
            menus.FechaCreacion = data.FechaCreacion;

            if (activo)
            {
                menus.Activo = false;
            }
            else
            {
                menus.Activo = true;
            }

            rm = menu.CambiarStatus(menus);

            if (rm.response)
            {
                rm.message = "Se cambio el estatus del menú con exito.";
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
        public JsonResult Guardar(PLU_Menu plu_menu)
        {

            var rm = new ResponseModel();
            MenuHelper menu = new MenuHelper();
            plu_menu.FechaCreacion = DateTime.Now;

            if (ModelState.IsValid)
            {

                rm = menu.Agregar(plu_menu);

                if (rm.response)
                {

                    rm.message = "Menu agregado con exito.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.error = false;
                }
                else
                {
                    rm.message = "Error al agregar Menu.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.error = true;
                }

            }

            return Json(rm);
        }
    }
}