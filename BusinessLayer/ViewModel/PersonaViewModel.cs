using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ViewModel
{
    public class PersonaViewModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string Apellido { get; set; }

        [StringLength(16)]
        public string Telefono { get; set; }

        public string TipoDeContacto { get; set; }
    }
}
