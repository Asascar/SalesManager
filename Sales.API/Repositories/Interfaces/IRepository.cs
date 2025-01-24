namespace Sales.API.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> Get(int skip = 0, int take = 0);

        Task<T> GetById(Guid id);

        Task<T> Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
