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
    public  class PuertasHelper 
    {

        public ResponseModel Agregar(PLU_Puertas plu_puertas)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ModelContent())
                {

                    ctx.Entry(plu_puertas).State = EntityState.Added;
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



        public ResponseModel CambiarStatus(PLU_Puertas plu_puertas)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ModelContent())
                {

                    ctx.Entry(plu_puertas).State = EntityState.Modified;
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
