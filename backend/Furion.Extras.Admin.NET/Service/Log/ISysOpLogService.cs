using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Furion.Extras.Admin.NET.Service
{
    public interface ISysOpLogService
    {
        Task ClearOpLog();

        Task<dynamic> QueryOpLogPageList([FromQuery] OpLogPageInput input);
    }
}