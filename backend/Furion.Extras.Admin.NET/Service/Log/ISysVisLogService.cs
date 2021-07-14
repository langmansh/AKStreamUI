using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Furion.Extras.Admin.NET.Service
{
    public interface ISysVisLogService
    {
        Task ClearVisLog();

        Task<dynamic> QueryVisLogPageList([FromQuery] VisLogPageInput input);
    }
}