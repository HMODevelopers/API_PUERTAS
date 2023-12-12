namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PLU_Seccion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PLU_Seccion()
        {
            PLU_Codigos = new HashSet<PLU_Codigos>();
            PLU_Puertas = new HashSet<PLU_Puertas>();
            PLU_Residentes = new HashSet<PLU_Residentes>();
            PLU_Usuario = new HashSet<PLU_Usuario>();
        }

        [Key]
        public int IdSeccion { get; set; }

        [Required]
        [StringLength(255)]
        public string NombreSeccion { get; set; }

        [Required]
        [StringLength(50)]
        public string CodeBase { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_Codigos> PLU_Codigos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_Puertas> PLU_Puertas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_Residentes> PLU_Residentes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_Usuario> PLU_Usuario { get; set; }
    }
}
