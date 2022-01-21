using CapacityPlanner.Interfaces;
using CapacityPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CapacityPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AffectationsController : ControllerBase
    {
        private IAffectation<Affectation> _affectationRepository;

        public AffectationsController(IAffectation<Affectation> affectationRepository)
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
        public IActionResult SearchByDateIntervalAction(int id, DateTime StartDate, DateTime EndDate)
        {
            var affectationsByInterval = _affectationRepository.SearchByDateInterval(id, StartDate, EndDate);
            if (affectationsByInterval != null)
            {
                return Ok(affectationsByInterval);
            }

            return NotFound(new { Message = "Aucune affectation trouvée dans cet intervalle de dates" });
        }

    }
}
