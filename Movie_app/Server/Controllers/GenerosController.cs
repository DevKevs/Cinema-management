using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_app.Shared.Models;

namespace Movie_app.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly MyDbContext _context;

        public GenerosController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Generos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
        {
            return await _context.Generos.ToListAsync();
        }

        // GET: api/Generos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> GetGenero(int id)
        {
            var genero = await _context.Generos.FindAsync(id);

            if (genero == null)
            {
                return NotFound();
            }

            return genero;
        }

        // PUT: api/Generos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseGender>> PutGenero(int id, Genero genero)
        {
            if (id != genero.Id)
            {
                return BadRequest();
            }

            _context.Entry(genero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new ResponseGender() { Message = "Editado corectamente", ok = true };

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneroExists(id))
                {
                    return new ResponseGender() { Message = "No se pudo editar", ok = false };
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Generos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResponseGender>> PostGenero(Genero genero)
        {
            try
            {
                _context.Generos.Add(genero);
                await _context.SaveChangesAsync();
                return new ResponseGender() { Message="Agregado corectamente", ok=true};
            }
            catch (Exception)
            {

                return new ResponseGender() { Message = "No se pudo agregar", ok = false };
            }
            
        }

        // DELETE: api/Generos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseGender>> DeleteGenero(int id)
        {
            var genero = await _context.Generos.FindAsync(id);
            if (genero == null)
            {
                return NotFound();
            }
            try
            {
                _context.Generos.Remove(genero);
                await _context.SaveChangesAsync();
                return new ResponseGender() { Message = "Eliminado corectamente", ok = true };
            }
            catch (Exception)
            {

                return new ResponseGender() { Message = "No se pudo eliminar...", ok = false };
            }
              
        }

        private bool GeneroExists(int id)
        {
            return _context.Generos.Any(e => e.Id == id);
        }
    }
    public class ResponseGender
    {
        public string Message { get; set; }
        public bool ok { get; set; }
    }

}
