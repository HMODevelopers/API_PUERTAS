using Helpers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace API_PUERTAS.Areas.Residentes.Controllers
{

    public class PuertaController : Controller
    {
        

        public ActionResult Index()
        {
            using (ModelContent db = new ModelContent())
            {
                var IdResidente = SessionHelper.GetResidente();
                var seccion = db.PLU_Residentes.FirstOrDefault(x => x.IdResidentes == IdResidente);
                List<PLU_Puertas> puertas = null;

                if (seccion != null)
                {
                    puertas = db.PLU_Puertas.Where(x => x.IdSeccion == seccion.IdSeccion).ToList();
                }

                return View(puertas);
            }
        }

        public JsonResult AbrirPuerta(int IdPuerta, string Code)
        {
            var IdResidente = SessionHelper.GetResidente();
            var rm = new ResponseModel();

            try
            {
                using (ModelContent db = new ModelContent())
                {
                    var puerta = db.PLU_Puertas.FirstOrDefault(x => x.IdPuerta == IdPuerta);
                    var data = db.PLU_Seccion.FirstOrDefault(x => x.IdSeccion == puerta.IdSeccion);

                    if (data != null)
                    {
                        var sec = new PLU_Seccion
                        {
                            IdSeccion = data.IdSeccion,
                            NombreSeccion = data.NombreSeccion,
                            CodeBase = Code,
                            Activo = data.Activo,
                            FechaCreacion = data.FechaCreacion
                        };

                        var seccionHelper = new SeccionHelper();
                        rm = seccionHelper.SeccionApertura(sec);

                        if (rm.response)
                        {
                            rm.error = false;
                        }
                        else
                        {
                            rm.error = true;
                        }
                    }
                    else
                    {
                        rm.error = true;
                        rm.message = "No se encontró la sección asociada a la puerta.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción adecuadamente y registrarla si es necesario.
                rm.error = true;
                rm.message = "Error al abrir la puerta. Detalles: " + ex.Message;
            }

            return Json(rm, JsonRequestBehavior.AllowGet);
        }

       
    }
}