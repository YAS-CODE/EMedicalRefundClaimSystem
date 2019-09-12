using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMedicalClaimRefundSystem.Services
{
    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(int id);
        Task<int> InsertAsync(TEntity entity);
        void DeleteAsync(int id);
        void UpdateAsync(TEntity entityToUpdate);
    }
}
