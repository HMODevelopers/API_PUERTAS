namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PLU_BitacoraTarjetas
    {
        [Key]
        public int IdBitacoraTarjeta { get; set; }

        public int IdTarjeta { get; set; }

        public int IdResidente { get; set; }

        public DateTime FechaUso { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public virtual PLU_Tarjetas PLU_Tarjetas { get; set; }
    }
}
