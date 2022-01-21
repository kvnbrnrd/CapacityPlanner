using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CapacityPlanner.Interfaces
{
    public interface IAffectation<Affectation>
    {
        public bool Add(Affectation entity);
        public List<Affectation> GetAll();
        public Affectation Get(int id);
        public bool Update(int id, Affectation entity);
        public bool Delete(int id);
        public List<Affectation> SearchAll(Expression<Func<Affectation, bool>> searchMethod);
        public Affectation Search(Expression<Func<Affectation, bool>> searchMethod);
    }
}
