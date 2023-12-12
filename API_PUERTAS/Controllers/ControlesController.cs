﻿using Helpers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API_PUERTAS.Controllers
{
    public class ControlesController : Controller
    {
        ModelContent db = new ModelContent();
        // GET: Controles
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetControles(int IdSeccion)
        {
            try
            {
                using (ModelContent db = new ModelContent())
                {
                    
                 var data = db.PLU_Controles.Where(x => x.PLU_Residentes.IdSeccion == IdSeccion).Select(x => new { x.IdControl, x.PLU_Residentes.NombreCompleto, x.PLU_Residentes.Celular, x.PLU_Residentes.PLU_Seccion.NombreSeccion, x.NumerControl,x.Activo, x.FechaCreacion }).ToList();
                 var jsonResult = Json(new { data }, JsonRequestBehavior.AllowGet);
                 jsonResult.MaxJsonLength = int.MaxValue;
                 return jsonResult;
                    
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        [HttpGet]
        public JsonResult GetResidentes(int IdSeccion)
        {
            try
            {
                using (ModelContent db = new ModelContent())
                {
                    var data = db.PLU_Residentes.Where(x => x.IdSeccion == IdSeccion).Select(x => new { id = x.IdResidentes, text = x.NombreCompleto +" - "+x.Celular }).ToList();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex )
            {

                throw ex;
            }
            
        }


        [HttpPost]
        public JsonResult Guardar(PLU_Controles plu_controles)
        {

            var rm = new ResponseModel();
            ControlesHelper controles = new ControlesHelper();
            plu_controles.Activo = true;
            plu_controles.FechaCreacion = DateTime.Now;

            if (ModelState.IsValid)
            {

                rm = controles.Agregar(plu_controles);
                if (rm.response)
                {
                    rm.message = "Control agregado con exito.";
                    rm.function = "CargarData(1);$('#close').trigger('click');";
                    rm.error = false;
                }
                else
                {
                    rm.message = "Error al agregar control.";
                    rm.function = "CargarData(1);$('#close').trigger('click');";
                    rm.error = true;
                }
            }


            return Json(rm, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CambiarStatus(int id, bool activo)
        {

            var rm = new ResponseModel();
            ControlesHelper controles = new ControlesHelper();
          
            var data = db.PLU_Controles.Where(x => x.IdControl == id).FirstOrDefault();
            var con = new PLU_Controles();
            

            if (activo)
            {
                 con = new PLU_Controles
                {
                    IdControl = data.IdControl,
                    IdResidente = data.IdResidente,
                    NumerControl = data.NumerControl,
                    Activo = false,
                    FechaCreacion = data.FechaCreacion
                };
            }
            else
            {
                 con = new PLU_Controles
                {
                    IdControl = data.IdControl,
                    IdResidente = data.IdResidente,
                    NumerControl = data.NumerControl,
                    Activo = true,
                    FechaCreacion = data.FechaCreacion
                };
            }

            rm = controles.CambiarStatus(con);

            if (rm.response)
            {
                rm.message = "Se a desactivado el control con exito.";
                rm.function = "CargarData(1);$('#close').trigger('click');";
                rm.error = false;
            }
            else
            {
                rm.message = "Error al cambiar estatus";
                rm.function = "CargarData(1);$('#close').trigger('click');";
                rm.error = true;
            }

            return Json(rm);
        }



    }


    


}