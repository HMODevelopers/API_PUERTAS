namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PLU_Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public int IdRol { get; set; }

        public int? IdSeccion { get; set; }

        [Required]
        [StringLength(100)]
        public string Usuario { get; set; }

        [Required]
        [StringLength(255)]
        public string Pass { get; set; }

        public bool Activo { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public virtual PLU_Rol PLU_Rol { get; set; }

        public virtual PLU_Seccion PLU_Seccion { get; set; }
    }
}
