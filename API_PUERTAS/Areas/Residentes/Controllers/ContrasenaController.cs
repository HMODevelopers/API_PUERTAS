using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helpers;
using Models;

namespace API_PUERTAS.Areas.Residentes.Controllers
{
    public class ContrasenaController : Controller
    {
        // GET: Residentes/Contrasena
        ModelContent db = new ModelContent();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CambiarContrasena(PLU_Residentes plu_residentes)
        {
            var rm = new ResponseModel();
            var IdResidente = SessionHelper.GetResidente();
            ResidentesHelper residentes = new ResidentesHelper();

            // Obtener el residente actual
            var data = db.PLU_Residentes.FirstOrDefault(x => x.IdResidentes == IdResidente);

            if (data != null)
            {
                // Copiar propiedades del residente actual al modelo a modificar
                var resi = new PLU_Residentes
                {
                    IdResidentes = data.IdResidentes,
                    IdSeccion = data.IdSeccion,
                    NombreCompleto = data.NombreCompleto,
                    Celular = data.Celular?.ToString(),
                    Pass = HashHelper.SHA1(plu_residentes.Pass),
                    NoCasa = data.NoCasa,
                    Domicilio = data.Domicilio,
                    Auth = data.Auth,
                    Activo = true,
                    FechaCreacion = data.FechaCreacion,
                };

                ModelState.Remove("Pass");
                ModelState.Remove("NoCasa");
                ModelState.Remove("Celular");
                ModelState.Remove("NombreCompleto");

                // Validar el modelo
                if (ModelState.IsValid)
                {
                    // Realizar la operación de cambio de contraseña
                    rm = residentes.CambiarPass(resi);

                    // Configurar la respuesta JSON
                    rm.message = rm.response ? "Contraseña actualizada con exito." : "Error al cambiar Contraseña.";
                    rm.function = rm.response ? "logout();" : "";
                    rm.error = !rm.response;
                }
            }
            else
            {
                // Manejar el caso donde el residente no fue encontrado
                rm.message = "No se encontró el residente actual.";
                rm.function = "";
                rm.error = true;
            }

            return Json(rm);
        }
    }



}