using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Models
{
    public partial class ModelContent : DbContext
    {
        public ModelContent()
            : base("name=ModelContent")
        {
        }

        public virtual DbSet<PLU_Menu> PLU_Menu { get; set; }
        public virtual DbSet<PLU_MenuRoles> PLU_MenuRoles { get; set; }
        public virtual DbSet<PLU_Rol> PLU_Rol { get; set; }
        public virtual DbSet<PLU_SubMenu> PLU_SubMenu { get; set; }
        public virtual DbSet<PLU_Usuario> PLU_Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PLU_Menu>()
                .Property(e => e.TituloMenu)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Menu>()
                .Property(e => e.Icono)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Menu>()
                .HasMany(e => e.PLU_MenuRoles)
                .WithRequired(e => e.PLU_Menu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PLU_Menu>()
                .HasMany(e => e.PLU_SubMenu)
                .WithRequired(e => e.PLU_Menu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PLU_Rol>()
                .Property(e => e.NombreRol)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Rol>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Rol>()
                .HasMany(e => e.PLU_MenuRoles)
                .WithRequired(e => e.PLU_Rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PLU_Rol>()
                .HasMany(e => e.PLU_Usuario)
                .WithRequired(e => e.PLU_Rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PLU_SubMenu>()
                .Property(e => e.TituloSubMenu)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_SubMenu>()
                .Property(e => e.Controlador)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_SubMenu>()
                .Property(e => e.Accion)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Usuario>()
                .Property(e => e.Usuario)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Usuario>()
                .Property(e => e.Pass)
                .IsUnicode(false);
        }
    }
}
