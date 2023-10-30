namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PLU_Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PLU_Menu()
        {
            PLU_MenuRoles = new HashSet<PLU_MenuRoles>();
            PLU_SubMenu = new HashSet<PLU_SubMenu>();
        }

        [Key]
        public int IdMenu { get; set; }

        [Required]
        [StringLength(100)]
        public string TituloMenu { get; set; }

        [StringLength(50)]
        public string Icono { get; set; }

        public int Orden { get; set; }

        public bool Activo { get; set; }

        public DateTime? FechaCreacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_MenuRoles> PLU_MenuRoles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_SubMenu> PLU_SubMenu { get; set; }
    }
}
