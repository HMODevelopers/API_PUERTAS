namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PLU_TipoCodigo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PLU_TipoCodigo()
        {
            PLU_Codigos = new HashSet<PLU_Codigos>();
        }

        [Key]
        public int IdTipoCodigo { get; set; }

        [Required]
        [StringLength(255)]
        public string NombreTipoCodigo { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_Codigos> PLU_Codigos { get; set; }
    }
}
