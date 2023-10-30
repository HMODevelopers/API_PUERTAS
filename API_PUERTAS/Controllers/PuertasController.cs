using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EDUES_ADMIN.Filters.AdminFilters;

namespace API_PUERTAS.Controllers
{
    [Autenticado]
    public class PuertasController : Controller
    {
        // GET: Puertas
        public ActionResult Index()
        {
            ViewBag.Title = "Puertas";
            return View();
        }
    }
}