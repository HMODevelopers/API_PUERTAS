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
    public class BitacoraAccessoHelper
    {
        public ResponseModel AgregarAccesoApp(PLU_BitacoraAccesos plu_bitacoraaccesos)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ModelContent())
                {

                    ctx.Entry(plu_bitacoraaccesos).State = EntityState.Added;
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

        public ResponseModel AgregarAccesoCodigo(PLU_BitacoraCodigos plu_accesoscodigos)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ModelContent())
                {

                    ctx.Entry(plu_accesoscodigos).State = EntityState.Added;
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
