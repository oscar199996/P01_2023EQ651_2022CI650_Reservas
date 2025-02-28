using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01_2023EQ651_2022CI650.Models;
using System.Security.Cryptography;
using System.Text;

namespace P01_2023EQ651_2022CI650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ParqueoDBContext _context;

        public UsuariosController(ParqueoDBContext context)
        {
            _context = context;
        }

        // Endpoint para registrar un usuario
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(Usuario usuario)
        {
            if (_context.Usuarios.Any(u => u.Correo == usuario.Correo))
            {
                return BadRequest("El correo ya está registrado.");
            }

            usuario.Clave = HashPassword(usuario.Clave); // Guardamos la clave en forma de hash
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok("Usuario registrado exitosamente.");
        }

        // Endpoint para validar credenciales (login)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == usuario.Correo);

            if (user == null || !VerifyPassword(usuario.Clave, user.Clave))
            {
                return Unauthorized("Credenciales inválidas.");
            }

            return Ok(new { mensaje = "Inicio de sesión exitoso", user.Rol });
        }

        // Método para hashear contraseñas
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        // Método para verificar contraseñas
        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            return HashPassword(inputPassword) == storedHash;
        }

        // CRUD básico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();
            return usuario;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id) return BadRequest();
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
