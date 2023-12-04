using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web;

namespace Helpers
{
    public class ResidentesHelper
    {

        public ResponseModel Acceder(string user, string pass)
        {

            var rm = new ResponseModel();

            try
            {
                using (var ctx = new ModelContent())
                {
                    var pass_hash = HashHelper.SHA1(pass);
                    var usuario = ctx.PLU_Residentes.Where(x => x.Celular == user)
                                                 .Where(x => x.Pass == pass_hash)
                                                 .Where(x => x.Activo == true).FirstOrDefault();

                    if (usuario != null)
                    {
                        SessionHelper.AddUserToSession(usuario.IdResidentes.ToString());
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


        public PLU_Residentes Obtener(int id)
        {

            var residente = new PLU_Residentes();
            try
            {
                using (var ctx = new ModelContent())
                {
                    residente = ctx.PLU_Residentes.Where(x => x.IdResidentes == id).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return residente;
        }


        public ResponseModel Agregar(PLU_Residentes plu_residentes)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ModelContent())
                {

                    ctx.Entry(plu_residentes).State = EntityState.Added;
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

        public ResponseModel CambiarStatus(PLU_Residentes plu_residentes)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ModelContent())
                {

                    ctx.Entry(plu_residentes).State = EntityState.Modified;
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

        public ResponseModel Delete(PLU_Residentes plu_residentes)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ModelContent())
                {

                    ctx.Entry(plu_residentes).State = EntityState.Deleted;
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

        public static int GetSeccion(int IdResidente)
        {
            using (var ctx = new ModelContent())
            {

                var data = ctx.PLU_Residentes.Where(x => x.IdResidentes == IdResidente).Select(x => new {x.IdSeccion}).FirstOrDefault();
                return data.IdSeccion;
            }
           
        }


    }
}
