namespace Furion.Extras.Admin.NET
{
    public interface IGeneralCaptcha
    {
        dynamic CheckCode(GeneralCaptchaInput input);

        dynamic CreateCaptchaImage(int length = 4);
    }
}