namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PLU_Residentes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PLU_Residentes()
        {
            PLU_BitacoraAccesos = new HashSet<PLU_BitacoraAccesos>();
            PLU_Codigos = new HashSet<PLU_Codigos>();
            PLU_Controles = new HashSet<PLU_Controles>();
            PLU_Tarjetas = new HashSet<PLU_Tarjetas>();
        }

        [Key]
        public int IdResidentes { get; set; }

        public int IdSeccion { get; set; }

        [Required]
        [StringLength(255)]
        public string NombreCompleto { get; set; }

        [Required]
        [StringLength(50)]
        public string Celular { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        [Required]
        [StringLength(10)]
        public string NoCasa { get; set; }

        public string Domicilio { get; set; }

        public bool Auth { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_BitacoraAccesos> PLU_BitacoraAccesos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_Codigos> PLU_Codigos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_Controles> PLU_Controles { get; set; }

        public virtual PLU_Seccion PLU_Seccion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_Tarjetas> PLU_Tarjetas { get; set; }
    }
}
