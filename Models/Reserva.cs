using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_2023EQ651_2022CI650.Models
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int EspacioParqueoId { get; set; }

        [Required]
        public DateTime FechaReserva { get; set; } 

        [Required]
        public int CantidadHoras { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [ForeignKey("EspacioParqueoId")]
        public EspacioParqueo EspacioParqueo { get; set; }
    }
}
