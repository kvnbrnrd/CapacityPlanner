using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CapacityPlanner.Interfaces
{
    public interface IProjet<Projet>
    {
        public bool Add(Projet entity);
        public List<Projet> GetAll();
        public Projet Get(int id);
        public bool Update(int id, Projet entity);
        public bool Delete(int id);
    }
}
