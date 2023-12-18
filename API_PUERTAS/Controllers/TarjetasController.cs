using Helpers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API_PUERTAS.Controllers
{

    public class TarjetasController : Controller
    {
        ModelContent db = new ModelContent();
        // GET: Tarjetas
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetTarjetas(int IdSeccion)
        {
            try
            {
                using (ModelContent db = new ModelContent())
                {

                    var data = db.PLU_Tarjetas.Where(x => x.PLU_Residentes.IdSeccion == IdSeccion).Select(x => new { x.IdTarjeta, x.PLU_Residentes.NombreCompleto, x.PLU_Residentes.Celular, x.PLU_Residentes.PLU_Seccion.NombreSeccion, x.NumeroTarjeta, x.Activo, x.FechaCreacion }).ToList();
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
                    var data = db.PLU_Residentes.Where(x => x.IdSeccion == IdSeccion).Select(x => new { id = x.IdResidentes, text = x.NombreCompleto + " - " + x.Celular }).ToList();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        [HttpPost]
        public JsonResult Guardar(PLU_Tarjetas plu_tarjetas)
        {
            if (ExisteTarjeta(plu_tarjetas.NumeroTarjeta))
            {


                var rm = new ResponseModel();
                rm.message = "Numero de tarjeta ya existe.";
                rm.function = "CargarData(1);$('#close').trigger('click');";
                rm.error = false;

                return Json(rm, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var rm = new ResponseModel();
                TarjetasHelper Tarjeta = new TarjetasHelper();
                plu_tarjetas.Activo = true;
                plu_tarjetas.FechaCreacion = DateTime.Now;

                if (ModelState.IsValid)
                {

                    rm = Tarjeta.Agregar(plu_tarjetas);
                    if (rm.response)
                    {
                        rm.message = "Tarjeta agregada con exito.";
                        rm.function = "CargarData(1);$('#close').trigger('click');";
                        rm.error = false;
                    }
                    else
                    {
                        rm.message = "Error al agregar tarjeta.";
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
            TarjetasHelper Tarjetas = new TarjetasHelper();

            var data = db.PLU_Tarjetas.Where(x => x.IdTarjeta == id).FirstOrDefault();
            var tar = new PLU_Tarjetas();


            if (activo)
            {
                tar = new PLU_Tarjetas
                {
                    IdTarjeta = data.IdTarjeta,
                    IdResidente = data.IdResidente,
                    NumeroTarjeta = data.NumeroTarjeta,
                    Activo = false,
                    FechaCreacion = data.FechaCreacion
                };
            }
            else
            {
                tar = new PLU_Tarjetas
                {
                    IdTarjeta = data.IdTarjeta,
                    IdResidente = data.IdResidente,
                    NumeroTarjeta = data.NumeroTarjeta,
                    Activo = true,
                    FechaCreacion = data.FechaCreacion
                };
            }
            var IdSeccion = data.PLU_Residentes.IdSeccion;
            rm = Tarjetas.CambiarStatus(tar);

            if (rm.response)
            {
                rm.message = "Se desactivado la tarjeta con exito.";
                rm.function = "CargarData(" + IdSeccion + ");$('#close').trigger('click');";
                rm.result = IdSeccion;
                rm.error = false;
            }
            else
            {
                rm.message = "Error al cambiar estatus";
                rm.function = "CargarData(" + IdSeccion + ");$('#close').trigger('click');";
                rm.result = IdSeccion;
                rm.error = true;
            }

            return Json(rm);
        }


        [HttpPost]
        public JsonResult DeleteTarjetas(int IdTarjeta)
        {

            var rm = new ResponseModel();
            TarjetasHelper Tarjeta = new TarjetasHelper();

            var data = db.PLU_Tarjetas.Where(x => x.IdTarjeta == IdTarjeta).FirstOrDefault();

            var tarjeta = new PLU_Tarjetas
            {
                IdTarjeta = data.IdTarjeta,
                IdResidente = data.IdResidente,
                NumeroTarjeta = data.NumeroTarjeta,
                Activo = false,
                FechaCreacion = data.FechaCreacion

            };
            var IdSeccion = data.PLU_Residentes.IdSeccion;

            if (ModelState.IsValid)
            {

                rm = Tarjeta.Delete(tarjeta);
                if (rm.response)
                {
                    rm.message = "Tarjeta Eliminada con exito.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.result = IdSeccion;
                    rm.error = false;
                }
                else
                {
                    rm.message = "Error al Eliminar Tarjeta.";
                    rm.function = "CargarData();$('#close').trigger('click');";
                    rm.result = IdSeccion;
                    rm.error = true;
                }
            }
            return Json(rm);
        }


        public bool ExisteTarjeta(int numero)
        {
            using (ModelContent db = new ModelContent())
            {

                var existe = db.PLU_Tarjetas.Any(c => c.NumeroTarjeta == numero);


                return existe;
            }
        }
    }
}