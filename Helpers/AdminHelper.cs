using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class AdminHelper
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
                        rm.SetResponse(false, "Celular o Contraseña Incorrecta");
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
    }
}
