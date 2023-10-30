using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Helpers
{
    public class MenuHelper
    {
        ModelContent db = new ModelContent();


        public List<PLU_Menu> GetMenu(int IdUsuario)
        {
            var rol = db.PLU_Usuario.Where(x => x.IdUsuario == IdUsuario).Select(x => new { x.IdRol }).FirstOrDefault();
            var datos = db.PLU_MenuRoles.Where(x => x.IdRol == rol.IdRol && x.Activo == true).Select(x => new { x.IdMenu, x.PLU_Menu.TituloMenu, x.PLU_Menu.Icono, x.PLU_Menu.Orden }).OrderBy(x => x.Orden).ToList();
            var menuItems = datos.Select(x => new PLU_Menu { IdMenu = x.IdMenu, TituloMenu = x.TituloMenu, Icono = x.Icono, Orden = x.Orden }).ToList();
            return menuItems;
        }


        public List<PLU_SubMenu> GetSubMenu(int IdMenu)
        {
            var datos = db.PLU_SubMenu.Where(x => x.IdMenu == IdMenu && x.Activo == true).Select(x => new { x.IdSubMenu, x.TituloSubMenu, x.Controlador, x.Accion, x.Orden }).OrderBy(x => x.Orden).ToList();
            var menuItems = datos.Select(x => new PLU_SubMenu { TituloSubMenu = x.TituloSubMenu, Controlador = x.Controlador, Accion = x.Accion }).ToList();
            return menuItems;
        }


        public ResponseModel CambiarStatus(PLU_Menu plu_menu)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ModelContent())
                {

                    ctx.Entry(plu_menu).State = EntityState.Modified;
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

        public ResponseModel Agregar(PLU_Menu plu_menu)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ModelContent())
                {

                    ctx.Entry(plu_menu).State = EntityState.Added;
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
