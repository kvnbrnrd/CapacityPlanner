using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CapacityPlanner.Models;
using CapacityPlanner.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CapacityPlanner.Repository
{
    public class AffectationRepository : BaseRepository, IRepository<Affectation>
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

        public List<Affectation> SearchAll(Expression<Func<Affectation, bool>> searchMethod)
        {
            throw new NotImplementedException();
        }

        public Affectation Search(Expression<Func<Affectation, bool>> searchMethod)
        {
            throw new NotImplementedException();
        }


    }
}

