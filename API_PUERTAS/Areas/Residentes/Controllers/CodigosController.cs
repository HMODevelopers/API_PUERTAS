using Helpers;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EDUES_ADMIN.Filters.AdminFilters;

namespace API_PUERTAS.Areas.Residentes.Controllers
{
    [Auth]
    public class CodigosController : Controller
    {
        ModelContent db = new ModelContent();   
        // GET: Residentes/Codigos
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCodigos(int IdResidente)
        {

            if (CambiarEstadoCodigosVencidos(IdResidente))
            {
             
                // La función se ejecutó correctamente, puedes realizar más acciones si es necesario
                var data = db.PLU_Codigos
               .Where(x => x.IdResidente == IdResidente && x.Activo == true)
               .Select(x => new
               {
                   x.IdCodigo,
                   x.Codigo,
                   TipoCodigo = x.PLU_TipoCodigo.NombreTipoCodigo,
                   x.FechaAlta,
                   x.FechaBaja,
                   x.Activo,
                   x.FechaCreacion
               })
               .ToList();

                var jsonResult = Json(new { data }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {
                // La función falló, maneja la situación según tus necesidades
                return null;
            }



        }

        public JsonResult GetTipoCodigo()
        {
            var data = db.PLU_TipoCodigo.Select(x => new { Text = x.NombreTipoCodigo, Value = x.IdTipoCodigo }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCodigoUnico()
        {
            int nuevoCodigo;
            var random = new Random();
            using (var db = new ModelContent())  
            {
                do
                {
                    nuevoCodigo = random.Next(1000, 10000);
                }
                while (db.PLU_Codigos.Any(x => x.Codigo == nuevoCodigo));
            }

            return Json(nuevoCodigo ,JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Guardar(PLU_Codigos plu_codigos)
        {

            var rm = new ResponseModel();
            CodigosHelper codigos = new CodigosHelper();

            
            plu_codigos.Activo = true;
            plu_codigos.FechaCreacion = DateTime.Now;

            if (ModelState.IsValid)
            {

                rm = codigos.Agregar(plu_codigos);
                if (rm.response)
                {
                    rm.message =  "Codigo agregado con exito.";
                    rm.function = "CargarData();getSecciones();generarCodigo();$('#close').trigger('click');";
                    rm.error = false;
                }
                else
                {
                    rm.message =  "Error al agregar Codigo.";
                    rm.function = "CargarData();getSecciones();generarCodigo();$('#close').trigger('click');";
                    rm.error = true;
                }
            }
            return Json(rm);
        }


        [HttpPost]
        public JsonResult DeleteCodigo(int IdCodigo)
        {

            var rm = new ResponseModel();
            CodigosHelper Codigos = new CodigosHelper();

            var data = db.PLU_Codigos.Where(x => x.IdCodigo == IdCodigo).FirstOrDefault();

            var codigo = new PLU_Codigos
            {
                IdCodigo = data.IdCodigo,
                IdResidente = data.IdResidente,
                IdSeccion = data.IdSeccion,
                IdTipoCodigo = data.IdTipoCodigo,
                Codigo = data.Codigo,
                FechaAlta = data.FechaAlta,
                FechaBaja = data.FechaBaja,
                Activo = data.Activo,
                FechaCreacion = data.FechaCreacion

            };

            if (ModelState.IsValid)
            {

                rm = Codigos.Delete(codigo);
                if (rm.response)
                {
                    rm.message = "Codigo eliminado con exito.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.error = false;
                }
                else
                {
                    rm.message = "Error al Eliminar Codigo.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.error = true;
                }
            }
            return Json(rm);
        }


        public bool CambiarEstadoCodigosVencidos(int idResidente)
        {
            try
            {
                DateTime fechaActual = DateTime.Now;

                // Obtener los registros que cumplen con la condición
                var codigosAEliminar = db.PLU_Codigos
                    .Where(x => x.IdResidente == idResidente && x.Activo == true && x.FechaBaja != null && x.FechaBaja <= fechaActual && x.IdTipoCodigo == 3)
                    .ToList();

                // Cambiar el estado de los registros
                foreach (var codigo in codigosAEliminar)
                {
                    codigo.Activo = false;
                }

                // Guardar los cambios en la base de datos
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción aquí, puedes registrarla, lanzarla de nuevo, etc.
                return false;
            }
        }

    }
}