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
    public class CatalogosController : Controller
    {
        ModelContent db = new ModelContent();
        // GET: Catalogos
        public ActionResult Index()
        {
            
            return View();
        }


        public ActionResult Usuarios()
        {
            
            return View();
        }


        public ActionResult RolesUsuarios()
        {
            
            return View();
        }

        public ActionResult Roles()
        {
            return View();
        }

        public ActionResult Seccion()
        {
            return View();
        }

        public ActionResult Puertas()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetRoles()
        {
            var data = db.PLU_Rol.Select(x => new { id = x.IdRol, text = x.NombreRol }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        
        }

    }
}