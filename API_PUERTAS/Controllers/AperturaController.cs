using Helpers;
using Models;
using System;
using System.Linq;
using System.Web.Http;

namespace API_PUERTAS.Controllers
{
    [RoutePrefix("api/Apertura")]
    public class AperturaController : ApiController
    {
        ModelContent ctx = new ModelContent();


        [HttpGet]
        [Route("Seccion/{IdSeccion}")]
        public IHttpActionResult GetSeccion([FromUri] int IdSeccion)
        {
            var data = ctx.PLU_Seccion.Where(x => x.IdSeccion == IdSeccion).Select(x => new { x.CodeBase }).FirstOrDefault();

            var respuesta = new
            {
                respuesta = data.CodeBase
            };


            if (data == null)
            {
                return NotFound();
            }

            return Ok(respuesta);
        }

        [HttpGet]
        [Route("Codigos/{Codigo}")]
        public IHttpActionResult GetCodigo([FromUri] int Codigo)
        {

            var data = ctx.PLU_Codigos.Where(x => x.Codigo == Codigo).Select(x => new { x.IdTipoCodigo, x.IdCodigo, x.IdResidente , x.IdSeccion, x.Codigo, x.FechaAlta, x.FechaBaja , x.FechaCreacion}).FirstOrDefault();
            var bitacoraaccesoHelper = new BitacoraAccessoHelper();
            var codigoshelper = new CodigosHelper();


            if (data == null)
            {
                var respuesta = new
                {
                    respuesta = "CODE0000000000000"
                };

                return Ok(respuesta);
            }
            else
            {
                var accescodigo1 = new PLU_BitacoraCodigos
                {
                    IdCodigo = data.IdCodigo,
                    IdResidente = data.IdResidente,
                    FechaUso = DateTime.Now,
                    Activo = true,
                    FechaCreacion = DateTime.Now
                };

                var Codigo1 = new PLU_Codigos
                {
                    IdCodigo = data.IdCodigo,
                    IdResidente = data.IdResidente,
                    IdSeccion = data.IdSeccion,
                    IdTipoCodigo = data.IdTipoCodigo,
                    Codigo = data.Codigo,
                    FechaAlta = data.FechaAlta,
                    FechaBaja = data.FechaBaja,
                    Activo = false,
                    FechaCreacion = data.FechaCreacion
                };

                if (data.IdTipoCodigo == 2)
                {
                    // Verificar si está activo
                    bool estaActivo = ObtenerEstadoActivo(Codigo);

                    if (estaActivo)
                    {

                        codigoshelper.CambiarStatus(Codigo1);
                        bitacoraaccesoHelper.AgregarAccesoCodigo(accescodigo1);

                        var respuesta = new
                        {
                            respuesta = "CODE1000000000000"
                        };

                        return Ok(respuesta);
                    }
                    else
                    {
                        var respuesta = new
                        {
                            respuesta = "CODE0000000000000"
                        };

                        return Ok(respuesta);
                    }
                }
                else if (data.IdTipoCodigo == 3)
                {
                    // Verificar la fecha de baja
                    bool fechaBajaValida = VerificarFechaBaja(Codigo);

                    if (fechaBajaValida)
                    {
                        var respuesta = new
                        {
                            respuesta = "CODE1000000000000"
                        };

                        bitacoraaccesoHelper.AgregarAccesoCodigo(accescodigo1);
                        return Ok(respuesta);
                    }
                    else
                    {
                        var respuesta = new
                        {
                            respuesta = "CODE0000000000000"
                        };

                        return Ok(respuesta);
                    }
                }
                else
                {
                    var respuesta = new
                    {
                        respuesta = "CODE0000000000000"
                    };

                    return Ok(respuesta);
                }
            }

          
        }


        [HttpGet]
        [Route("Controles/{NumeroControl}")]
        public IHttpActionResult GetControles([FromUri] int NumeroControl)
        {

            var data = ctx.PLU_Controles.Where(x => x.NumerControl == NumeroControl).Select(x => new { x.IdControl,x.IdResidente, x.NumerControl, x.Activo, x.FechaCreacion }).FirstOrDefault();
            var bitacoraaccesoHelper = new BitacoraAccessoHelper();


            if (data == null)
            {
                var respuesta = new
                {
                    respuesta = "CODE0000000000000"
                };

                return Ok(respuesta);
            }
            else
            {
                var accescodigo1 = new PLU_BitacoraControles
                {
                    IdControl = data.IdControl,
                    IdResidente = data.IdResidente,
                    FechaUso = DateTime.Now,
                    Activo = true,
                    FechaCreacion = DateTime.Now
                };

                    // Verificar si está activo
                bool estaActivo = ObtenerEstadoActivoControl(NumeroControl);

                if (estaActivo)
                {
    
                        bitacoraaccesoHelper.AgregarAccesoControl(accescodigo1);

                        var respuesta = new
                        {
                            respuesta = "CODE1000000000000"
                        };

                        return Ok(respuesta);
                 }
                 else
                 {
                        var respuesta = new
                        {
                            respuesta = "CODE0000000000000"
                        };

                        return Ok(respuesta);
                 }
            }
        }

        [HttpGet]
        [Route("Tarjetas/{NumeroTarjeta}")]
        public IHttpActionResult GetTarjetas([FromUri] int NumeroTarjeta)
        {

            var data = ctx.PLU_Tarjetas.Where(x => x.NumeroTarjeta == NumeroTarjeta).Select(x => new { x.IdTarjeta, x.IdResidente, x.NumeroTarjeta, x.Activo, x.FechaCreacion }).FirstOrDefault();
            var bitacoraaccesoHelper = new BitacoraAccessoHelper();


            if (data == null)
            {
                var respuesta = new
                {
                    respuesta = "CODE0000000000000"
                };

                return Ok(respuesta);
            }
            else
            {
                var accescodigo1 = new PLU_BitacoraTarjetas
                {
                    IdTarjeta = data.IdTarjeta,
                    IdResidente = data.IdResidente,
                    FechaUso = DateTime.Now,
                    Activo = true,
                    FechaCreacion = DateTime.Now
                };

                // Verificar si está activo
                bool estaActivo = ObtenerEstadoActivoTarjeta(NumeroTarjeta);

                if (estaActivo)
                {

                    bitacoraaccesoHelper.AgregarAccesoTarjeta(accescodigo1);

                    var respuesta = new
                    {
                        respuesta = "CODE1000000000000"
                    };

                    return Ok(respuesta);
                }
                else
                {
                    var respuesta = new
                    {
                        respuesta = "CODE0000000000000"
                    };

                    return Ok(respuesta);
                }
            }
        }


        private bool ObtenerEstadoActivo(int codigo)
        {
            var data = ctx.PLU_Codigos.Where(x => x.Codigo == codigo).Select(x => new { x.Activo }).FirstOrDefault();

            if (data.Activo)
            {
                return true;
            }
            else
            {
                return false;
            }


        }



        private bool ObtenerEstadoActivoControl(int NumeroControl)
        {
            var data = ctx.PLU_Controles.Where(x => x.NumerControl == NumeroControl).Select(x => new { x.Activo }).FirstOrDefault();

            if (data.Activo)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        private bool ObtenerEstadoActivoTarjeta(int NumeroTarjeta)
        {
            var data = ctx.PLU_Tarjetas.Where(x => x.NumeroTarjeta == NumeroTarjeta).Select(x => new { x.Activo }).FirstOrDefault();

            if (data.Activo)
            {
                return true;
            }
            else
            {
                return false;
            }


        }


        private bool VerificarFechaBaja(int codigo)
        {
            var data = ctx.PLU_Codigos.Where(x => x.Codigo == codigo && x.Activo == true).Select(x => new { x.FechaBaja }).FirstOrDefault();


            DateTime fechaBaja = (DateTime)data.FechaBaja;
            DateTime fechaActual = DateTime.Now.Date;

            if (fechaBaja.Date >= fechaActual)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

       

    }
}
