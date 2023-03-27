namespace DataAccess.Contracts
{
    public interface IBaseRepository
    {
        IQueryable<T> GetAll<T>() where T : class;

        void Create<T>(T model) where T : class;

        void Update<T>(T model) where T : class;

        void Delete<T>(T model) where T : class;
    }
}
