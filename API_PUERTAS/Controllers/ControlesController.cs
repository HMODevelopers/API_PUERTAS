using Helpers;
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
            if (ExisteNumeroControl(plu_controles.NumerControl))
            {
                

                var rm = new ResponseModel();
                rm.message = "Numero de control ya existe.";
                rm.function = "CargarData(1);$('#close').trigger('click');";
                rm.error = false;

                return Json(rm, JsonRequestBehavior.AllowGet);

            }
            else
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
            var IdSeccion = data.PLU_Residentes.IdSeccion;
            rm = controles.CambiarStatus(con);

            if (rm.response)
            {
                rm.message = "Se desactivado el control con exito.";
                rm.function = "CargarData("+IdSeccion+");$('#close').trigger('click');";
                rm.result = IdSeccion;
                rm.error = false;
            }
            else
            {
                rm.message = "Error al cambiar estatus";
                rm.function = "CargarData("+IdSeccion+");$('#close').trigger('click');";
                rm.result = IdSeccion;
                rm.error = true;
            }

            return Json(rm);
        }


        [HttpPost]
        public JsonResult DeleteControl(int IdControl)
        {

            var rm = new ResponseModel();
            ControlesHelper Control = new ControlesHelper();

            var data = db.PLU_Controles.Where(x => x.IdControl == IdControl).FirstOrDefault();

            var control = new PLU_Controles
            {
                IdControl = data.IdControl,
                IdResidente = data.IdResidente,
                NumerControl = data.NumerControl,
                Activo = false,
                FechaCreacion = data.FechaCreacion

            };
            var IdSeccion = data.PLU_Residentes.IdSeccion;

            if (ModelState.IsValid)
            {

                rm = Control.Delete(control);
                if (rm.response)
                {
                    rm.message = "Control Eliminado con exito.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.result = IdSeccion;
                    rm.error = false;
                }
                else
                {
                    rm.message = "Error al Eliminar Control.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.result = IdSeccion;
                    rm.error = true;
                }
            }
            return Json(rm);
        }


        public bool ExisteNumeroControl(int numero)
        {
            using (ModelContent db = new ModelContent())
            {
                
                var existe = db.PLU_Controles.Any(c => c.NumerControl == numero);

                
                return existe;
            }
        }

    }


    


}