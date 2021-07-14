using System.Collections.Generic;
using System.Threading.Tasks;

namespace Furion.Extras.Admin.NET.Service
{
    public interface ISysEmpExtOrgPosService
    {
        Task AddOrUpdate(long empId, List<EmpExtOrgPosOutput> extIdList);

        Task DeleteEmpExtInfoByUserId(long empId);

        Task<List<EmpExtOrgPosOutput>> GetEmpExtOrgPosList(long empId);

        Task<bool> HasExtOrgEmp(long orgId);

        Task<bool> HasExtPosEmp(long posId);
    }
}