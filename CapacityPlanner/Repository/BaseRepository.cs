namespace CapacityPlanner.Repository
{
    public class BaseRepository
    {
        protected CapacityPlannerDbContext _dataContext;

        public BaseRepository(CapacityPlannerDbContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
