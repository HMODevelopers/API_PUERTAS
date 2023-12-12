namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PLU_BitacoraControles
    {
        [Key]
        public int IdBitacoraControl { get; set; }

        public int IdControl { get; set; }

        public int IdResidente { get; set; }

        public DateTime FechaUso { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public virtual PLU_Controles PLU_Controles { get; set; }
    }
}
