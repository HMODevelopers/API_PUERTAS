namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PLU_MenuRoles
    {
        [Key]
        public int IdMenuRol { get; set; }

        public int IdMenu { get; set; }

        public int IdRol { get; set; }

        public bool Activo { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaCreacion { get; set; }

        public virtual PLU_Menu PLU_Menu { get; set; }

        public virtual PLU_Rol PLU_Rol { get; set; }
    }
}
