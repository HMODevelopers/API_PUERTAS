﻿using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API_PUERTAS.Areas.Admin.Controllers
{
    public class AccesarController : Controller
    {
        // GET: Admin/Auth
        AdminHelper Admin = new AdminHelper();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Acceder(string username, string password_text)
        {
            var rm = Admin.Acceder(username, password_text);

            if (rm.response)
            {
                rm.href = "/Admin/Bitacoras/Index";
            }

            return Json(rm);
        }

        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSession();
            return Redirect("/Admin/Accesar/Index");
        }
    }
}