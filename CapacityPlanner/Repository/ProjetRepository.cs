using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CapacityPlanner.Models;
using CapacityPlanner.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CapacityPlanner.Repository
{
    public class ProjetRepository : BaseRepository, IRepository<Projet>
    {
        public ProjetRepository(CapacityPlannerDbContext dataContext) : base(dataContext)
        {
        }

        public bool Add(Projet entity)
        {
            _dataContext.Projets.Add(entity);
            return _dataContext.SaveChanges() > 0;
        }

        public List<Projet> GetAll()
        {
            return _dataContext.Projets
                .Include(p => p.Affectations)
                .ToList();
        }

        public Projet Get(int id)
        {
            return _dataContext.Projets
                .Include(p => p.Affectations)
                .FirstOrDefault(p => p.Id == id);
        }

        public bool Update(int id, Projet entity)
        {
            Projet p = Get(id);

            if (p != null)
            {
                p.Client = entity.Client;
                p.Nom = entity.Nom;
                p.Statut = entity.Statut;
                p.Type = entity.Type;
            }

            return _dataContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            _dataContext.Projets.Remove(Get(id));
            return _dataContext.SaveChanges() > 0;
        }

        public List<Projet> SearchAll(Expression<Func<Projet, bool>> searchMethod)
        {
            return _dataContext.Projets
                .Include(p => p.Affectations)
                .Where(searchMethod).ToList();
        }

        public Projet Search(Expression<Func<Projet, bool>> searchMethod)
        {
            return _dataContext.Projets
                .Include(p => p.Affectations)
                .FirstOrDefault(searchMethod);
        }


    }
}

