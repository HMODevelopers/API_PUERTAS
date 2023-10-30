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
    public class SubMenuController : Controller
    {
        // GET: SubMenu
        ModelContent db = new ModelContent();
        public ActionResult Index()
        {
            var menu = db.PLU_Menu.ToList();
            ViewBag.menu = new SelectList(menu, "IdMenu", "TituloMenu");
            return View();
        }


        [HttpGet]
        public JsonResult GetSubMenuTable()
        {
            var data = db.PLU_SubMenu.Select(x => new { x.IdSubMenu, x.PLU_Menu.TituloMenu, x.TituloSubMenu, x.Controlador, x.Accion, x.Orden, x.Activo, x.FechaCreacion }).ToList();
            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Guardar(PLU_SubMenu plu_submenu)
        {

            var rm = new ResponseModel();
            SubMenuHelper submenu = new SubMenuHelper();
            plu_submenu.FechaCreacion = DateTime.Now;

            if (ModelState.IsValid)
            {

                rm = submenu.Agregar(plu_submenu);

                if (rm.response)
                {

                    rm.message = "SubMenú agregado con exito.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.error = false;
                }
                else
                {
                    rm.message = "Error al agregar SubMenú.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.error = true;
                }

            }

            return Json(rm);
        }
    }
}