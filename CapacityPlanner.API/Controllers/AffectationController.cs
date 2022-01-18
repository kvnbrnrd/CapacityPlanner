using CapacityPlanner.Interfaces;
using CapacityPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CapacityPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AffectationsController : ControllerBase
    {
        private IRepository<Affectation> _affectationRepository;

        public AffectationsController(IRepository<Affectation> affectationRepository)
        {
            _affectationRepository = affectationRepository;
        }

        // POST: api/Affectations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostAffectation(Affectation Affectation)
        {
            if (_affectationRepository.Add(Affectation))
            {
                return Ok(new { Message = $"L'affectation n°{Affectation.Id} a bien été ajoutée ", Affectation = Affectation });
            }

            return NotFound(new { Message = $"L'affectation n°{Affectation.Id} n'a pas pu être ajoutée" });
        }

        // GET: api/Affectations
        [HttpGet]
        public IActionResult GetAffectations()
        {
            return Ok(_affectationRepository.GetAll());
        }

        // GET: api/Affectations/5
        [HttpGet("{id}")]
        public IActionResult GetAffectation(int id)
        {
            Affectation a = _affectationRepository.Get(id);

            if (a != null)
            {
                return Ok(a);
            }

            return NotFound(new { Message = "Aucune affectation avec cet id" });
        }

        // PUT: api/Affectations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutAffectation(int id, Affectation affectation)
        {
            Affectation a = _affectationRepository.Get(id);

            if (a != null)
            {
                a.CollaborateurId = affectation.CollaborateurId;
                a.ProjetId = affectation.ProjetId;
                a.Charge = affectation.Charge;
            }

            if (_affectationRepository.Update(id, a))
            {
                var newAffectation = _affectationRepository.Get(id);
                return Ok(new { Message = $"Affectation éditée avec succès", newAffectation = newAffectation });
            }

            return NotFound(new { Message = $"Erreur lors de l'édition de l'affectation" });
        }

        // DELETE: api/Affectations/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_affectationRepository.Delete(id))
            {
                return Ok(new { Message = "Affectation n° " + id + " supprimée avec succès" });
            }

            return NotFound(new { Message = "L'affectation n° " + id + " n'a pas pu être supprimée" });
        }

        // Get: api/Affectations/search/5
        [HttpGet("search/{id}")]
        public IActionResult SearchByDate(int id, DateTime searchDate)
        {
            var aToGet = _affectationRepository.SearchAll(a => a.Collaborateur.Id == id && a.DateDebut <= searchDate && a.DateFin >= searchDate);
            if (aToGet != null)
            {
                int chargeTotale = 0;
                string message = "";

                if (aToGet.Count != 0)
                {

                    for (int i = 0; i < aToGet.Count; i++)
                    {
                        chargeTotale += aToGet[i].Charge;
                    }

                    if (chargeTotale <= 50)
                    {
                        message = $"Le collaborateur {aToGet[0].Collaborateur.Prenom} {aToGet[0].Collaborateur.Nom} est sous-chargé à la date du {searchDate}";

                    }

                    else if (chargeTotale > 100)
                    {
                        message = message = $"Le collaborateur {aToGet[0].Collaborateur.Prenom} {aToGet[0].Collaborateur.Nom} est surchargé à la date du {searchDate}";

                    }

                    else
                    {
                        message = message = $"Le collaborateur {aToGet[0].Collaborateur.Prenom} {aToGet[0].Collaborateur.Nom} a une charge de travail adéquate à la date du {searchDate}";
                    }

                    return Ok(new { Message = $"{message}", Affectations = aToGet, Charge = chargeTotale });
                }

            }

            return NotFound(new { Message = $"Aucune affectation ne correspond à la recherche" });
        }

    }
}
