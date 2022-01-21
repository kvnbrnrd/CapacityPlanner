using CapacityPlanner.Interfaces;
using CapacityPlanner.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CapacityPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaborateursController : ControllerBase
    {
        private ICollaborateur<Collaborateur> _collaborateurRepository;

        public CollaborateursController(ICollaborateur<Collaborateur> collaborateurRepository)
        {
            _collaborateurRepository = collaborateurRepository;
        }

        // POST: api/Collaborateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostCollaborateur(Collaborateur collaborateur)
        {
            if (_collaborateurRepository.Add(collaborateur))
            {
                return Ok(new { Message = $"{collaborateur.Prenom} {collaborateur.Nom} ajouté avec succès", Collaborateur = collaborateur });
            }

            return NotFound(new { Message = $"Le collaborateur {collaborateur.Prenom} {collaborateur.Nom} n'a pas pu être ajouté" });
        }

        // GET: api/Collaborateurs
        [HttpGet]
        public IActionResult GetCollaborateurs()
        {
            return Ok(_collaborateurRepository.GetAll());
        }

        // GET: api/Collaborateurs/5
        [HttpGet("{id}")]
        public IActionResult GetCollaborateur(int id)
        {
            Collaborateur c = _collaborateurRepository.Get(id);

            if (c != null)
            {
                return Ok(c);
            }

            return NotFound(new { Message = "Aucun collaborateur avec cet id" });
        }

        // PUT: api/Collaborateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutCollaborateur(int id, Collaborateur collaborateur)
        {
            Collaborateur c = _collaborateurRepository.Get(id);

            if (c != null)
            {
                c.Nom = collaborateur.Nom;
                c.Prenom = collaborateur.Prenom;
            }

            if (_collaborateurRepository.Update(id, c))
            {
                var newCollaborateur = _collaborateurRepository.Get(id);
                return Ok(new { Message = $"Collaborateur édité avec succès", newCollaborateur = newCollaborateur });
            }

            return NotFound(new { Message = $"Erreur lors de l'édition du collaborateur" });
        }

        // DELETE: api/Collaborateurs/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_collaborateurRepository.Delete(id))
            {
                return Ok(new { Message = "Collaborateur " + id + " supprimé avec succès" });
            }

            return NotFound(new { Message = "Le collaborateur " + id + " n'a pas pu être supprimé" });
        }

    }
}
