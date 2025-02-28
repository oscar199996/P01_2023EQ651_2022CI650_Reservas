using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01_2023EQ651_2022CI650.Models;

namespace P01_2023EQ651_2022CI650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspaciosParqueoController : ControllerBase
    {
        private readonly ParqueoDBContext _context;

        public EspaciosParqueoController(ParqueoDBContext context)
        {
            _context = context;
        }

        // Obtener todos los espacios de parqueo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspacioParqueo>>> GetEspaciosParqueo()
        {
            return await _context.EspaciosParqueo.ToListAsync();
        }

        // Obtener un espacio de parqueo por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<EspacioParqueo>> GetEspacioParqueo(int id)
        {
            var espacio = await _context.EspaciosParqueo.FindAsync(id);
            if (espacio == null) return NotFound();
            return espacio;
        }

        // Registrar un nuevo espacio de parqueo
        [HttpPost]
        public async Task<ActionResult<EspacioParqueo>> CreateEspacioParqueo(EspacioParqueo espacio)
        {
            _context.EspaciosParqueo.Add(espacio);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEspacioParqueo), new { id = espacio.Id }, espacio);
        }

        // Editar un espacio de parqueo
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEspacioParqueo(int id, EspacioParqueo espacio)
        {
            if (id != espacio.Id) return BadRequest();
            _context.Entry(espacio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Eliminar un espacio de parqueo
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspacioParqueo(int id)
        {
            var espacio = await _context.EspaciosParqueo.FindAsync(id);
            if (espacio == null) return NotFound();
            _context.EspaciosParqueo.Remove(espacio);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Mostrar lista de espacios disponibles por día
        [HttpGet("disponibles/{fecha}")]
        public async Task<ActionResult<IEnumerable<EspacioParqueo>>> GetEspaciosDisponibles(DateTime fecha)
        {
            var espaciosDisponibles = await _context.EspaciosParqueo
                .Where(e => e.Disponible)
                .ToListAsync();

            return Ok(espaciosDisponibles);
        }

        // Mostrar lista de espacios reservados por día
        [HttpGet("reservados/{fecha}")]
        public async Task<ActionResult<IEnumerable<EspacioParqueo>>> GetEspaciosReservados(DateTime fecha)
        {
            var reservas = await _context.Reservas
                .Where(r => r.FechaReserva.Date == fecha.Date)
                .Include(r => r.EspacioParqueo)
                .Select(r => r.EspacioParqueo)
                .ToListAsync();

            return Ok(reservas);
        }

        // Mostrar lista de espacios reservados en un rango de fechas para una sucursal específica
        [HttpGet("reservados/{sucursalId}/{fechaInicio}/{fechaFin}")]
        public async Task<ActionResult<IEnumerable<EspacioParqueo>>> GetEspaciosReservadosPorRango(int sucursalId, DateTime fechaInicio, DateTime fechaFin)
        {
            var reservas = await _context.Reservas
                .Where(r => r.FechaReserva >= fechaInicio && r.FechaReserva <= fechaFin && r.EspacioParqueo.SucursalId == sucursalId)
                .Include(r => r.EspacioParqueo)
                .Select(r => r.EspacioParqueo)
                .ToListAsync();

            return Ok(reservas);
        }
    }
}
