using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CapacityPlanner.Models;
using CapacityPlanner.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CapacityPlanner.Repository
{
    public class CollaborateurRepository : BaseRepository, IRepository<Collaborateur>
    {
        public CollaborateurRepository(CapacityPlannerDbContext dataContext) : base(dataContext)
        {
        }

        public bool Add(Collaborateur entity)
        {
            _dataContext.Collaborateurs.Add(entity);
            return _dataContext.SaveChanges() > 0;
        }

        public List<Collaborateur> GetAll()
        {
            return _dataContext.Collaborateurs
                .Include(c => c.Affectations)
                .ToList();
        }

        public Collaborateur Get(int id)
        {
            return _dataContext.Collaborateurs
                .Include(c => c.Affectations)
                .FirstOrDefault(c => c.Id == id);
        }

        public bool Update(int id, Collaborateur entity)
        {
            Collaborateur c = Get(id);

            if (c != null)
            {
                c.Nom = entity.Nom;
                c.Prenom = entity.Prenom;
            }

            return _dataContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            _dataContext.Collaborateurs.Remove(Get(id));
            return _dataContext.SaveChanges() > 0;
        }

        public List<Collaborateur> SearchAll(Expression<Func<Collaborateur, bool>> searchMethod)
        {
            return _dataContext.Collaborateurs
                .Include(c => c.Affectations)
                .Where(searchMethod).ToList();
        }

        public Collaborateur Search(Expression<Func<Collaborateur, bool>> searchMethod)
        {
            return _dataContext.Collaborateurs
                .Include(c => c.Affectations)
                .FirstOrDefault(searchMethod);
        }


    }
}
