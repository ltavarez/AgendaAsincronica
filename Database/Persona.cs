namespace Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Personas")]
    public partial class Persona
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string Apellido { get; set; }

        [StringLength(16)]
        public string Telefono { get; set; }

        public int? IdTipoContacto { get; set; }

        public string FotoPerfil { get; set; }

        public virtual TipoContacto TipoContacto { get; set; }
    }
}
