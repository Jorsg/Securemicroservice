using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.API.Data;
using Movies.API.Model;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("ClientIdPolicy")]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesContext _context;

        public MoviesController(MoviesContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model.Movies>>> GetMovies()
        {
          if (_context.Movies == null)
          {
              return NotFound();
          }
            return await _context.Movies.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Model.Movies>> GetMovies(int id)
        {
          if (_context.Movies == null)
          {
              return NotFound();
          }
            var movies = await _context.Movies.FindAsync(id);

            if (movies == null)
            {
                return NotFound();
            }

            return movies;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovies(int id, Model.Movies movies)
        {
            if (id != movies.Id)
            {
                return BadRequest();
            }

            _context.Entry(movies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Model.Movies>> PostMovies(Model.Movies movies)
        {
          if (_context.Movies == null)
          {
              return Problem("Entity set 'MoviesAPIContext.Movies'  is null.");
          }
            _context.Movies.Add(movies);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovies", new { id = movies.Id }, movies);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovies(int id)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movies);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoviesExists(int id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
