using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CapacityPlanner.Interfaces
{
    public interface ICollaborateur<Collaborateur> 
    {
        public bool Add(Collaborateur entity);
        public List<Collaborateur> GetAll();
        public Collaborateur Get(int id);
        public bool Update(int id, Collaborateur entity);
        public bool Delete(int id);
    }
}
