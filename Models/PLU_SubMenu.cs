namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PLU_SubMenu
    {
        [Key]
        public int IdSubMenu { get; set; }

        public int IdMenu { get; set; }

        [Required]
        [StringLength(100)]
        public string TituloSubMenu { get; set; }

        [StringLength(50)]
        public string Controlador { get; set; }

        [StringLength(50)]
        public string Accion { get; set; }

        public int Orden { get; set; }

        public bool? Activo { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public virtual PLU_Menu PLU_Menu { get; set; }
    }
}
