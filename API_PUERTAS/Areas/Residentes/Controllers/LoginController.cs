using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EDUES_ADMIN.Filters.AdminFilters;

namespace API_PUERTAS.Areas.Residentes.Controllers
{

    public class LoginController : Controller
    {
        ResidentesHelper Residente = new ResidentesHelper();

        // GET: Residentes/Login
        [NoAuth]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Acceder(string username, string password_text)
        {
            var rm = Residente.Acceder(username, password_text);

            if (rm.response)
            {
                rm.href = "/Residentes/Puerta/Index";
            }

            return Json(rm);
        }

        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSession();
            return Redirect("/Residentes/Login/Index");
        }

    }
}