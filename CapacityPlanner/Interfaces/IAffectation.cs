using System;
using System.Collections;
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
        public List<Affectation> SearchAllAffectations(Expression<Func<Affectation, bool>> searchMethod);
        public Affectation SearchAffectation(Expression<Func<Affectation, bool>> searchMethod);
        public IEnumerable SearchByDate(int id, DateTime searchDate);
        public IEnumerable SearchByDateInterval(int id, DateTime StartDate, DateTime EndDate);
        public IEnumerable SearchAllByDateInterval(DateTime StartDate, DateTime EndDate);
    }
}
