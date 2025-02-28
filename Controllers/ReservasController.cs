using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01_2023EQ651_2022CI650.Models;

namespace P01_2023EQ651_2022CI650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly ParqueoDBContext _context;

        public ReservasController(ParqueoDBContext context)
        {
            _context = context;
        }

        // Obtener todas las reservas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            return await _context.Reservas.ToListAsync();
        }

        // Obtener reserva por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return NotFound();
            return reserva;
        }

        // Crear una nueva reserva
        [HttpPost]
        public async Task<ActionResult<Reserva>> CreateReserva(Reserva reserva)
        {
            var espacio = await _context.EspaciosParqueo.FindAsync(reserva.EspacioParqueoId);
            if (espacio == null || !espacio.Disponible)
                return BadRequest("El espacio seleccionado no está disponible.");

            espacio.Disponible = false; // Marcar como ocupado
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReserva), new { id = reserva.Id }, reserva);
        }

        // Cancelar una reserva (antes de su uso)
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return NotFound();

            var espacio = await _context.EspaciosParqueo.FindAsync(reserva.EspacioParqueoId);
            if (espacio != null)
                espacio.Disponible = true; // Liberar espacio de parqueo

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Mostrar lista de reservas activas por usuario
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservasPorUsuario(int usuarioId)
        {
            var reservas = await _context.Reservas
                .Where(r => r.UsuarioId == usuarioId)
                .ToListAsync();

            return Ok(reservas);
        }
    }
}
