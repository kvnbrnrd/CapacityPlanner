using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapacityPlanner.Models;
using CapacityPlanner.Repository;

namespace CapacityPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestGeneratedController : ControllerBase
    {
        private readonly CapacityPlannerDbContext _context;

        public TestGeneratedController(CapacityPlannerDbContext context)
        {
            _context = context;
        }

        // GET: api/Collaborateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collaborateur>>> GetCollaborateurs()
        {
            return await _context.Collaborateurs.ToListAsync();
        }

        // GET: api/Collaborateurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Collaborateur>> GetCollaborateur(int id)
        {
            var collaborateur = await _context.Collaborateurs.FindAsync(id);

            if (collaborateur == null)
            {
                return NotFound();
            }

            return collaborateur;
        }

        // PUT: api/Collaborateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollaborateur(int id, Collaborateur collaborateur)
        {
            if (id != collaborateur.Id)
            {
                return BadRequest();
            }

            _context.Entry(collaborateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollaborateurExists(id))
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

        // POST: api/Collaborateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Collaborateur>> PostCollaborateur(Collaborateur collaborateur)
        {
            _context.Collaborateurs.Add(collaborateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollaborateur", new { id = collaborateur.Id }, collaborateur);
        }

        // DELETE: api/Collaborateurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollaborateur(int id)
        {
            var collaborateur = await _context.Collaborateurs.FindAsync(id);
            if (collaborateur == null)
            {
                return NotFound();
            }

            _context.Collaborateurs.Remove(collaborateur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CollaborateurExists(int id)
        {
            return _context.Collaborateurs.Any(e => e.Id == id);
        }
    }
}
