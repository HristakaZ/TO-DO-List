using DataAccess.Contracts;

namespace DataAccess.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return this.applicationDbContext.Set<T>();
        }

        public void Create<T>(T model) where T : class
        {
            this.applicationDbContext.Add<T>(model);
            this.applicationDbContext.SaveChanges();
        }

        public void Update<T>(T model) where T : class
        {
            this.applicationDbContext.Update<T>(model);
            this.applicationDbContext.SaveChanges();
        }

        public virtual void Delete<T>(T model) where T : class //might be used for soft delete(reason to set to virtual)
        {
            this.applicationDbContext.Remove<T>(model);
            this.applicationDbContext.SaveChanges();
        }
    }
}