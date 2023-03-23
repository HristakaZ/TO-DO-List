using DataAccess.Contracts;

namespace DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public IQueryable<T> Get()
        {
            throw new NotImplementedException();
        }

        public void Create(T model)
        {
            throw new NotImplementedException();
        }

        public void Update(T model)
        {
            throw new NotImplementedException();
        }

        public void Delete(T model)
        {
            throw new NotImplementedException();
        }
    }
}