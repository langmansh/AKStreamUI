using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Furion.Extras.Admin.NET.Service
{
    public interface ISysExLogService
    {
        Task ClearExLog();

        Task<dynamic> QueryExLogPageList([FromQuery] ExLogPageInput input);
    }
}