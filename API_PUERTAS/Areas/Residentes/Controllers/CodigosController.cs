using Helpers;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API_PUERTAS.Areas.Residentes.Controllers
{
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

            var data = db.PLU_Codigos.Where(x => x.IdResidente == IdResidente).Select(x => new { x.IdCodigo,x.Codigo,x.PLU_TipoCodigo.NombreTipoCodigo,x.FechaAlta,x.FechaBaja,x.Activo,x.FechaCreacion }).ToList();

            var jsonResult = Json(new { data }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
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
    }
}