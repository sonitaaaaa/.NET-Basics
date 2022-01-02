using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogsDEMO.Application.Interfaces.IRepositories
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(string id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetByCondition(Expression<Func<T, bool>> includeProperties);

        Task<long> CountByCondition(Expression<Func<T, bool>> includeProperties);

        Task<IReadOnlyList<T>> GetPagedResponseAsync(int pageNumber, int pageSize);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task DeleteRange(List<T> entity);
    }
}