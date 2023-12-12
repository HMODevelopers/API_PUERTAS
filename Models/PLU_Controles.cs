namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PLU_Controles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PLU_Controles()
        {
            PLU_BitacoraControles = new HashSet<PLU_BitacoraControles>();
        }

        [Key]
        public int IdControl { get; set; }

        public int IdResidente { get; set; }

        [Display(Name = "Numero Control", Prompt = "", Description = "")]
        public int NumerControl { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_BitacoraControles> PLU_BitacoraControles { get; set; }

        public virtual PLU_Residentes PLU_Residentes { get; set; }
    }
}
