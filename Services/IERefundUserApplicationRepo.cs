using EMedicalClaimRefundSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMedicalClaimRefundSystem.Services
{
    public interface IERefundUserApplicationRepo : IGenericRepository<ERefundUserApplication>
    {
        IEnumerable<ERefundUserApplication> GetByUser(string UserId);
        void UpdateStatusAsync(int Id, int Status);

        IEnumerable<ERefundUserApplication> GetByAppStatus(int AppStatus);
    }
}
