using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class UsuarioHelper
    {

        public ResponseModel Acceder(string user, string pass)
        {

            var rm = new ResponseModel();

            try
            {
                using (var ctx = new ModelContent())
                {
                    var pass_hash = HashHelper.SHA1(pass);
                    var usuario = ctx.PLU_Usuario.Where(x => x.Usuario == user)
                                                 .Where(x => x.Pass == pass_hash)
                                                 .Where(x => x.Activo == true).FirstOrDefault();

                    if (usuario != null)
                    {
                        SessionHelper.AddUserToSession(usuario.IdUsuario.ToString());
                        rm.SetResponse(true);
                    }
                    else
                    {
                        rm.SetResponse(false, "Correo o Contraseña Incorrecta");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return rm;
        }

    

        public PLU_Usuario Obtener(int id)
        {

            var usuario = new PLU_Usuario();
            try
            {
                using (var ctx = new ModelContent())
                {
                    usuario = ctx.PLU_Usuario.Where(x => x.IdUsuario == id).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return usuario;
        }

        public ResponseModel Agregar(PLU_Usuario plu_usuario)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ModelContent())
                {

                    ctx.Entry(plu_usuario).State = EntityState.Added;
                    ctx.SaveChanges();
                    rm.SetResponse(true);

                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception)
            {

                throw;
            }

            return rm;
        }

        public ResponseModel CambiarStatus(PLU_Usuario plu_usuario)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ModelContent())
                {

                    ctx.Entry(plu_usuario).State = EntityState.Modified;
                    ctx.SaveChanges();
                    rm.SetResponse(true);

                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception)
            {

                throw;
            }

            return rm;
        }



    }
}
