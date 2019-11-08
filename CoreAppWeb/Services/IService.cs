using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAppWeb.Services
{
    /// <summary>
    /// The Repositiry interface for performing 
    /// CRUD Operations using EF Core
    /// This will be independent to the Entity Classes
    /// TENtity is Always a class (entity class)
    /// TPk as 'in' means input parameter 
    /// </summary>
    public interface IService<TEntity, in TPk> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(TPk id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TPk id, TEntity entity);
        Task<bool> DeleteAsync(TPk id);
    }
}
