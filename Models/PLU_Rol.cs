namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PLU_Rol
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PLU_Rol()
        {
            PLU_MenuRoles = new HashSet<PLU_MenuRoles>();
            PLU_Usuario = new HashSet<PLU_Usuario>();
        }

        [Key]
        public int IdRol { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreRol { get; set; }

        [StringLength(255)]
        public string Descripcion { get; set; }

        public bool? Activo { get; set; }

        public DateTime? FechaCreacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_MenuRoles> PLU_MenuRoles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_Usuario> PLU_Usuario { get; set; }
    }
}
