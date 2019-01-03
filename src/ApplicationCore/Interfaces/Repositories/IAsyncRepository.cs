using Inkett.ApplicationCore.Entitites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Repositories
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task<T> GetSingleBySpec(ISpecification<T> spec);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
