using System.Collections.Generic;
using System.Threading.Tasks;

namespace Furion.Extras.Admin.NET.Service
{
    public interface ISysEmpPosService
    {
        Task AddOrUpdate(long empId, List<long> posIdList);

        Task DeleteEmpPosInfoByUserId(long empId);

        Task<List<EmpPosOutput>> GetEmpPosList(long empId);

        Task<bool> HasPosEmp(long posId);
    }
}