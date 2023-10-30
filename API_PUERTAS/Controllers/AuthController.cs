using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EDUES_ADMIN.Filters.AdminFilters;

namespace API_PUERTAS.Controllers
{
    public class AuthController : Controller
    {
        SeccionHelpers Usuario = new SeccionHelpers();

        // GET: Auth
        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult Acceder(string username, string password_text)
        {
            var rm = Usuario.Acceder(username, password_text);

            if (rm.response)
            {
                rm.href = "/Home/Index";
            }

            return Json(rm);
        }

        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSession();
            return Redirect("/Auth/Index");
        }
    }
}