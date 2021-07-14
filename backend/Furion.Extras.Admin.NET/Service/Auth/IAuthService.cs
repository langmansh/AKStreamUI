using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Furion.Extras.Admin.NET.Service
{
    public interface IAuthService
    {
        Task<dynamic> GetCaptcha();

        Task<bool> GetCaptchaOpen();

        Task<LoginOutput> GetLoginUserAsync();

        string LoginAsync([FromBody] LoginInput input);

        Task LogoutAsync();

        Task<dynamic> VerificationCode(ClickWordCaptchaInput input);
    }
}