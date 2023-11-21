namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PLU_Codigos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PLU_Codigos()
        {
            PLU_BitacoraCodigos = new HashSet<PLU_BitacoraCodigos>();
        }

        [Key]
        public int IdCodigo { get; set; }

        public int IdResidente { get; set; }

        public int IdSeccion { get; set; }

        public int IdTipoCodigo { get; set; }

        [Required]
        public int Codigo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaAlta { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaBaja { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_BitacoraCodigos> PLU_BitacoraCodigos { get; set; }

        public virtual PLU_Seccion PLU_Seccion { get; set; }

        public virtual PLU_TipoCodigo PLU_TipoCodigo { get; set; }

        public virtual PLU_Residentes PLU_Residentes { get; set; }
    }
}
