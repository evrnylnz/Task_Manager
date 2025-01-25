using System.Linq.Expressions;

namespace Task_Manager.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Find(object id);

        Task<T> GetByFilter(Expression<Func<T, bool>> filter, bool asNoTracking = false);

        Task Create(T entity);

        Task Update(T entity);

        Task Delete(int id);

        IQueryable<T> GetQuery();
    }
}
