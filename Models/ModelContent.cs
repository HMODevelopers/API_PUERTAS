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

        public virtual DbSet<PLU_BitacoraAccesos> PLU_BitacoraAccesos { get; set; }
        public virtual DbSet<PLU_BitacoraCodigos> PLU_BitacoraCodigos { get; set; }
        public virtual DbSet<PLU_BitacoraControles> PLU_BitacoraControles { get; set; }
        public virtual DbSet<PLU_BitacoraTarjetas> PLU_BitacoraTarjetas { get; set; }
        public virtual DbSet<PLU_Codigos> PLU_Codigos { get; set; }
        public virtual DbSet<PLU_Controles> PLU_Controles { get; set; }
        public virtual DbSet<PLU_Menu> PLU_Menu { get; set; }
        public virtual DbSet<PLU_MenuRoles> PLU_MenuRoles { get; set; }
        public virtual DbSet<PLU_Puertas> PLU_Puertas { get; set; }
        public virtual DbSet<PLU_Residentes> PLU_Residentes { get; set; }
        public virtual DbSet<PLU_Rol> PLU_Rol { get; set; }
        public virtual DbSet<PLU_Seccion> PLU_Seccion { get; set; }
        public virtual DbSet<PLU_SubMenu> PLU_SubMenu { get; set; }
        public virtual DbSet<PLU_Tarjetas> PLU_Tarjetas { get; set; }
        public virtual DbSet<PLU_TipoCodigo> PLU_TipoCodigo { get; set; }
        public virtual DbSet<PLU_Usuario> PLU_Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PLU_Codigos>()
                .HasMany(e => e.PLU_BitacoraCodigos)
                .WithRequired(e => e.PLU_Codigos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PLU_Controles>()
                .HasMany(e => e.PLU_BitacoraControles)
                .WithRequired(e => e.PLU_Controles)
                .WillCascadeOnDelete(false);

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

            modelBuilder.Entity<PLU_Puertas>()
                .Property(e => e.NombrePuerta)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Puertas>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Residentes>()
                .Property(e => e.NombreCompleto)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Residentes>()
                .Property(e => e.Celular)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Residentes>()
                .Property(e => e.Pass)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Residentes>()
                .Property(e => e.NoCasa)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Residentes>()
                .Property(e => e.Domicilio)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Residentes>()
                .HasMany(e => e.PLU_BitacoraAccesos)
                .WithRequired(e => e.PLU_Residentes)
                .HasForeignKey(e => e.IdResidente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PLU_Residentes>()
                .HasMany(e => e.PLU_Codigos)
                .WithRequired(e => e.PLU_Residentes)
                .HasForeignKey(e => e.IdResidente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PLU_Residentes>()
                .HasMany(e => e.PLU_Controles)
                .WithRequired(e => e.PLU_Residentes)
                .HasForeignKey(e => e.IdResidente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PLU_Residentes>()
                .HasMany(e => e.PLU_Tarjetas)
                .WithRequired(e => e.PLU_Residentes)
                .HasForeignKey(e => e.IdResidente)
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

            modelBuilder.Entity<PLU_Seccion>()
                .Property(e => e.NombreSeccion)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Seccion>()
                .Property(e => e.CodeBase)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Seccion>()
                .HasMany(e => e.PLU_Codigos)
                .WithRequired(e => e.PLU_Seccion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PLU_Seccion>()
                .HasMany(e => e.PLU_Puertas)
                .WithRequired(e => e.PLU_Seccion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PLU_Seccion>()
                .HasMany(e => e.PLU_Residentes)
                .WithRequired(e => e.PLU_Seccion)
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

            modelBuilder.Entity<PLU_Tarjetas>()
                .HasMany(e => e.PLU_BitacoraTarjetas)
                .WithRequired(e => e.PLU_Tarjetas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PLU_TipoCodigo>()
                .Property(e => e.NombreTipoCodigo)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_TipoCodigo>()
                .HasMany(e => e.PLU_Codigos)
                .WithRequired(e => e.PLU_TipoCodigo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PLU_Usuario>()
                .Property(e => e.Usuario)
                .IsUnicode(false);

            modelBuilder.Entity<PLU_Usuario>()
                .Property(e => e.Pass)
                .IsUnicode(false);
        }
    }
}
