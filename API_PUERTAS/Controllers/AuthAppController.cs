using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Helpers;
using API_PUERTAS.Security;

namespace API_PUERTAS.Controllers
{
    public class AuthAppController : ApiController
    {
        private ModelContent db = new ModelContent();
        private PLU_Residentes r;

        [HttpPost]
        public IHttpActionResult Login(PLU_Residentes residentes)
        {
            PLU_Residentes tblNewUser = ResidentesHelper.GetByCelular(residentes.Celular);
            bool bLoggedCorrectly = false;


            if (tblNewUser == null)
            {

                return BadRequest("Residente no existe");
            }
            else if (tblNewUser.Pass != HashHelper.SHA1(residentes.Pass))
            {

                return BadRequest("Contraseña incorrectos.");
            }
            else if (!tblNewUser.Activo)
            {

                return BadRequest("Usuario inactivo");
            }
            else if (tblNewUser.Pass == HashHelper.SHA1(residentes.Pass))
            {
                bLoggedCorrectly = true;
                residentes = tblNewUser;
            }
            if (bLoggedCorrectly)
            {
                r = db.PLU_Residentes.Where(x => x.Celular == residentes.Celular).SingleOrDefault();
                var token = TokenGenerator.GenerateTokenJwt(r);

                var us = new
                {
                    id = r.IdResidentes,
                    celular = r.IdResidentes,
                    nombreCompleto = r.NombreCompleto,
                    token = token
                };

                return Ok(us);
            }
            else
            {
                return BadRequest("Usuario y/o contraseña incorrectos.");
            }
        }
    }
}
