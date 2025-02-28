using System.ComponentModel.DataAnnotations;

namespace P01_2023EQ651_2022CI650.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [Required, MaxLength(100)]
        public string Correo { get; set; }

        [Required, MaxLength(15)]
        public string Telefono { get; set; }

        [Required]
        public string Clave { get; set; } // Será almacenada en forma de hash

        [Required]
        public string Rol { get; set; } // Cliente o Empleado
    }
}
