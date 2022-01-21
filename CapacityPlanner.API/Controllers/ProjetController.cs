using CapacityPlanner.Interfaces;
using CapacityPlanner.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CapacityPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetsController : ControllerBase
    {
        private IProjet<Projet> _projetRepository;

        public ProjetsController(IProjet<Projet> projetRepository)
        {
            _projetRepository = projetRepository;
        }

        // POST: api/Projets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostProjet(Projet Projet)
        {
            if (_projetRepository.Add(Projet))
            {
                return Ok(new { Message = $"Le projet {Projet.Nom} a bien été ajouté", Projet = Projet });
            }

            return NotFound(new { Message = $"Le projet {Projet.Nom} n'a pas pu être ajouté" });
        }

        // GET: api/Projets
        [HttpGet]
        public IActionResult GetProjets()
        {
            return Ok(_projetRepository.GetAll());
        }

        // GET: api/Projets/5
        [HttpGet("{id}")]
        public IActionResult GetProjet(int id)
        {
            Projet p = _projetRepository.Get(id);

            if (p != null)
            {
                return Ok(p);
            }

            return NotFound(new { Message = "Aucun projet avec cet id" });
        }

        // PUT: api/Projets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutProjet(int id, Projet projet)
        {
            Projet p = _projetRepository.Get(id);

            if (p != null)
            {
                p.Client = projet.Client;
                p.Nom = projet.Nom;
                p.Statut = projet.Statut;
                p.Type = projet.Type;
            }

            if (_projetRepository.Update(id, p))
            {
                var newProjet = _projetRepository.Get(id);
                return Ok(new { Message = $"Projet édité avec succès", newProjet = newProjet });
            }

            return NotFound(new { Message = $"Erreur lors de l'édition du projet" });
        }

        // DELETE: api/Projets/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_projetRepository.Delete(id))
            {
                return Ok(new { Message = "Projet " + id + " supprimé avec succès" });
            }

            return NotFound(new { Message = "Le projet " + id + " n'a pas pu être supprimé" });
        }

    }
}
