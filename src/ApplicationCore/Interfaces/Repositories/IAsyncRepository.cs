using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Repositories
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<IQueryable<T>> QueryableAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> range);
        Task<T> GetSingleBySpec(ISpecification<T> spec);
        Task<T> UpdateAsync(T entity);
        Task DeleteRange(IEnumerable<T> collection);
        Task DeleteAsync(T entity);
    }
}
