using System.Threading.Tasks;

namespace Furion.Extras.Admin.NET.Service
{
    public interface ISysOnlineUserService
    {
        Task<dynamic> List();
    }
}