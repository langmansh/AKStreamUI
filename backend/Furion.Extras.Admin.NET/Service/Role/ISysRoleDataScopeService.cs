using System.Collections.Generic;
using System.Threading.Tasks;

namespace Furion.Extras.Admin.NET.Service
{
    public interface ISysRoleDataScopeService
    {
        Task DeleteRoleDataScopeListByOrgIdList(List<long> orgIdList);

        Task DeleteRoleDataScopeListByRoleId(long roleId);

        Task<List<long>> GetRoleDataScopeIdList(List<long> roleIdList);

        Task GrantDataScope(GrantRoleDataInput input);
    }
}