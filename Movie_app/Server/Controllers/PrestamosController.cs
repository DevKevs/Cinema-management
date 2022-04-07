using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Movie_app.Shared.Models;

namespace Movie_app.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly string _connectionString;

        public PrestamosController(MyDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration["ConnectionStrings:MyConnection"];
        }

        // GET: api/Prestamos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos()
        {
            return await _context.Prestamos.ToListAsync();
        }

        // GET: api/Prestamos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prestamo>> GetPrestamo(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);

            if (prestamo == null)
            {
                return NotFound();
            }

            return prestamo;
        }

        // PUT: api/Prestamos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestamo(int id, Prestamo prestamo)
        {
            if (id != prestamo.Id)
            {
                return BadRequest();
            }

            _context.Entry(prestamo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrestamoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        

        // POST: api/Prestamos/Prestar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Prestar")]
        public async Task<ActionResult<ResponsePrestamo>> PostPrestarPelicula(Prestamo prestamo)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand($"exec Prestar_Pelicula {prestamo.IdPelicula}, '{prestamo.Prestatario}'", sql))
                    {
                        await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                
                            }
                        }
                    }

                }
                return new ResponsePrestamo() { Message = "Pelicula prestada", ok = true };

            }
            catch (Exception)
            {

                return new ResponsePrestamo() { Message = "No se pudo prestar", ok = false };
            }
        }

        // POST: api/Prestamos/Devolver
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Devolver")]
        public async Task<ActionResult<ResponsePrestamo>> PostDevolverPelicula(Prestamo prestamo)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand($"exec Devolver_Pelicula {prestamo.IdPelicula}", sql))
                    {
                        await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                            }
                        }
                    }

                }
                return new ResponsePrestamo() { Message = "Pelicula devuelta", ok = true };

            }
            catch (Exception)
            {

                return new ResponsePrestamo() { Message = "No se pudo devolver", ok = false };
            }
        }

        // DELETE: api/Prestamos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestamo(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }

            _context.Prestamos.Remove(prestamo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamos.Any(e => e.Id == id);
        }
    }
    public class ResponsePrestamo
    {
        public string Message { get; set; }
        public bool ok { get; set; }
    }
}
