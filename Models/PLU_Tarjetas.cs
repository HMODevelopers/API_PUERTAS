namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PLU_Tarjetas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PLU_Tarjetas()
        {
            PLU_BitacoraTarjetas = new HashSet<PLU_BitacoraTarjetas>();
        }

        [Key]
        public int IdTarjeta { get; set; }

        public int IdResidente { get; set; }

        public int NumeroTarjeta { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLU_BitacoraTarjetas> PLU_BitacoraTarjetas { get; set; }

        public virtual PLU_Residentes PLU_Residentes { get; set; }
    }
}
