using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace P01_2023EQ651_2022CI650.Models
{
    public class EspacioParqueo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required, MaxLength(50)]
        public string Ubicacion { get; set; }

        [Required]
        [Precision(10, 2)]
        public decimal CostoPorHora { get; set; }

        [Required]
        public bool Disponible { get; set; } = true;

        [ForeignKey("Sucursal")]
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
    }
}
