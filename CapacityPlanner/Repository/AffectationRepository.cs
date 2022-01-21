using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CapacityPlanner.Models;
using CapacityPlanner.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CapacityPlanner.Repository
{
    public class AffectationRepository : BaseRepository, IAffectation<Affectation>
    {
        public AffectationRepository(CapacityPlannerDbContext dataContext) : base(dataContext)
        {
        }

        public bool Add(Affectation entity)
        {
            _dataContext.Affectations.Add(entity);
            return _dataContext.SaveChanges() > 0;
        }

        public List<Affectation> GetAll()
        {
            return _dataContext.Affectations
                .Include(a => a.Collaborateur)
                .Include(a => a.Projet)
                .ToList();
        }

        public Affectation Get(int id)
        {
            return _dataContext.Affectations
                .Include(a => a.Collaborateur)
                .Include(a => a.Projet)
                .FirstOrDefault(a => a.Id == id);
        }

        public bool Update(int id, Affectation entity)
        {
            Affectation a = Get(id);

            if (a != null)
            {
                a.CollaborateurId = entity.CollaborateurId;
                a.ProjetId = entity.ProjetId;
                a.Charge = entity.Charge;
            }

            return _dataContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            _dataContext.Affectations.Remove(Get(id));
            return _dataContext.SaveChanges() > 0;
        }

        public List<Affectation> SearchAllAffectations(Expression<Func<Affectation, bool>> searchMethod)
        {
            return _dataContext.Affectations
                .Include(a => a.Collaborateur)
                .Include(a => a.Projet)
                .Where(searchMethod).ToList();
        }

        public Affectation SearchAffectation(Expression<Func<Affectation, bool>> searchMethod)
        {
            return _dataContext.Affectations
                .Include(a => a.Collaborateur)
                .Include(a => a.Projet)
                .FirstOrDefault(searchMethod);
        }

        public IEnumerable SearchByDate(int id, DateTime searchDate)
        {
            var aToGet = SearchAllAffectations(a => a.Collaborateur.Id == id && a.DateDebut <= searchDate && a.DateFin >= searchDate);
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
                        message = $"Le collaborateur {aToGet[0].Collaborateur.Prenom} {aToGet[0].Collaborateur.Nom} est surchargé à la date du {searchDate}";

                    }

                    else
                    {
                        message = $"Le collaborateur {aToGet[0].Collaborateur.Prenom} {aToGet[0].Collaborateur.Nom} a une charge de travail adéquate à la date du {searchDate}";
                    }

                    yield return new { Message = $"{message}", Charge = chargeTotale, Affectations = aToGet };
                }

            }

            yield return new { Message = $"Aucune affectation trouvée à cette date" } ;
        }

        public IEnumerable SearchByDateInterval(int id, DateTime StartDate, DateTime EndDate)
        {
            List<DateTime> searchDate = new List<DateTime>();

            for (DateTime date = StartDate; date.Date <= EndDate.Date; date = date.AddDays(1))
            {
                searchDate.Add(date);
            }

            foreach (DateTime date in searchDate)
            {
                yield return (SearchByDate(id, date));
            }
        }

        public IEnumerable SearchAllByDateInterval(DateTime StartDate, DateTime EndDate)
        {
            List<Affectation> affectations = GetAll();

            if (affectations != null)
            {
                List<Collaborateur> collaborateurs = new List<Collaborateur>();
                int id = 0;
                int prevId = 0;

                foreach (Affectation affectation in affectations)
                {
                    prevId = affectation.CollaborateurId;
                    if (prevId != 0 && prevId != id)
                    {
                        id = prevId;
                        collaborateurs.Add(affectation.Collaborateur);
                    }
                }

                foreach (Collaborateur collaborateur in collaborateurs)
                {
                    yield return SearchByDateInterval(collaborateur.Id, StartDate, EndDate);
                }
            }

            yield break;
        }

    }
}

