namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class PLU_Puertas
    {
        [Key]
        public int IdPuerta { get; set; }

        public int IdSeccion { get; set; }

        [Required]
        [StringLength(255)]
        public string NombrePuerta { get; set; }

        [Required]
        [StringLength(255)]
        public string Code { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public virtual PLU_Seccion PLU_Seccion { get; set; }

       


    }
}
