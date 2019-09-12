using EMedicalClaimRefundSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMedicalClaimRefundSystem.Services
{
    public interface IApplicationHistoryRepo : IGenericRepository<ApplicationHistory> {
        Task<IEnumerable<ApplicationHistory>> GetByUserAsync();
    }
    
}
