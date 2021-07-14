using Admin.NET.EntityFramework.Core;
using Furion.Extras.Admin.NET;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Admin.NET.Application
{
    public interface IvideochannelsService
    {
        Task Add(AddvideochannelsInput input);
        Task Delete(DeletevideochannelsInput input);
        Task<videochannels> Get([FromQuery] QueryevideochannelsInput input);
        Task<dynamic> List([FromQuery] videochannelsInput input);
        Task<dynamic> Page([FromQuery] videochannelsInput input);
        Task Update(UpdatevideochannelsInput input);
    }
}