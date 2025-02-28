using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace P01_2023EQ651_2022CI650.Models
{
    public class Sucursal
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [Required, MaxLength(200)]
        public string Direccion { get; set; }

        [Required, MaxLength(15)]
        public string Telefono { get; set; }

        [Required]
        public string Administrador { get; set; } // Nombre del encargado

        public int NumeroEspacios { get; set; }

        // Relación con los espacios de parqueo
        public List<EspacioParqueo> EspaciosParqueo { get; set; } = new List<EspacioParqueo>();
    }
}
